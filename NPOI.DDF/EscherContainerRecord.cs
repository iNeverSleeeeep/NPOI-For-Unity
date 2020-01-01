using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// Escher container records store other escher records as children.
	/// The container records themselves never store any information beyond
	/// the standard header used by all escher records.  This one record is
	/// used to represent many different types of records.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherContainerRecord : EscherRecord
	{
		public const short DGG_CONTAINER = -4096;

		public const short BSTORE_CONTAINER = -4095;

		public const short DG_CONTAINER = -4094;

		public const short SPGR_CONTAINER = -4093;

		public const short SP_CONTAINER = -4092;

		public const short SOLVER_CONTAINER = -4091;

		private static POILogger log = POILogFactory.GetLogger(typeof(EscherContainerRecord));

		/// in case if document contains any charts we have such document structure:
		/// BOF
		/// ...
		/// DrawingRecord
		/// ...
		/// ObjRecord|TxtObjRecord
		/// ...
		/// EOF
		/// ...
		/// BOF(Chart begin)
		/// ...
		/// DrawingRecord
		/// ...
		/// ObjRecord|TxtObjRecord
		/// ...
		/// EOF
		/// So, when we call EscherAggregate.createAggregate() we have not all needed data.
		/// When we got warning "WARNING: " + bytesRemaining + " bytes remaining but no space left"
		/// we should save value of bytesRemaining
		/// and add it to container size when we serialize it
		private int _remainingLength;

		private List<EscherRecord> _childRecords = new List<EscherRecord>();

		/// <summary>
		/// Subclasses should effeciently return the number of bytes required to
		/// Serialize the record.
		/// </summary>
		/// <value>number of bytes</value>
		public override int RecordSize
		{
			get
			{
				int num = 0;
				IEnumerator enumerator = ChildRecords.GetEnumerator();
				while (enumerator.MoveNext())
				{
					EscherRecord escherRecord = (EscherRecord)enumerator.Current;
					num += escherRecord.RecordSize;
				}
				return 8 + num;
			}
		}

		/// <summary>
		/// Returns a list of all the child (escher) records
		/// of the container.
		/// </summary>
		/// <value></value>
		public override List<EscherRecord> ChildRecords
		{
			get
			{
				return new List<EscherRecord>(_childRecords);
			}
			set
			{
				if (value == _childRecords)
				{
					throw new InvalidOperationException("Child records private data member has escaped");
				}
				_childRecords.Clear();
				_childRecords.AddRange(value);
			}
		}

		/// <summary>
		/// Returns all of our children which are also
		/// EscherContainers (may be 0, 1, or vary rarely
		/// 2 or 3)
		/// </summary>
		/// <value>The child containers.</value>
		public IList<EscherContainerRecord> ChildContainers
		{
			get
			{
				IList<EscherContainerRecord> list = new List<EscherContainerRecord>();
				IEnumerator enumerator = ChildRecords.GetEnumerator();
				while (enumerator.MoveNext())
				{
					EscherRecord escherRecord = (EscherRecord)enumerator.Current;
					if (escherRecord is EscherContainerRecord)
					{
						list.Add((EscherContainerRecord)escherRecord);
					}
				}
				return list;
			}
		}

		/// <summary>
		/// Subclasses should return the short name for this escher record.
		/// </summary>
		/// <value></value>
		public override string RecordName
		{
			get
			{
				switch (RecordId)
				{
				case -4096:
					return "DggContainer";
				case -4095:
					return "BStoreContainer";
				case -4094:
					return "DgContainer";
				case -4093:
					return "SpgrContainer";
				case -4092:
					return "SpContainer";
				case -4091:
					return "SolverContainer";
				default:
					return "Container 0x" + HexDump.ToHex(RecordId);
				}
			}
		}

		/// <summary>
		/// The contract of this method is to deSerialize an escher record including
		/// it's children.
		/// </summary>
		/// <param name="data">The byte array containing the Serialized escher
		/// records.</param>
		/// <param name="offset">The offset into the byte array.</param>
		/// <param name="recordFactory">A factory for creating new escher records</param>
		/// <returns>The number of bytes written.</returns>        
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			int num2 = 8;
			offset += 8;
			while (num > 0 && offset < data.Length)
			{
				EscherRecord escherRecord = recordFactory.CreateRecord(data, offset);
				int num3 = escherRecord.FillFields(data, offset, recordFactory);
				num2 += num3;
				offset += num3;
				num -= num3;
				AddChildRecord(escherRecord);
				if (offset >= data.Length && num > 0)
				{
					_remainingLength = num;
					log.Log(5, "Not enough Escher data: " + num + " bytes remaining but no space left");
				}
			}
			return num2;
		}

		/// <summary>
		/// Serializes to an existing byte array without serialization listener.
		/// This is done by delegating to Serialize(int, byte[], EscherSerializationListener).
		/// </summary>
		/// <param name="offset">the offset within the data byte array.</param>
		/// <param name="data"> the data array to Serialize to.</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>The number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			int num = 0;
			IEnumerator enumerator = ChildRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				num += escherRecord.RecordSize;
			}
			num += _remainingLength;
			LittleEndian.PutInt(data, offset + 4, num);
			int num2 = offset + 8;
			IEnumerator enumerator2 = ChildRecords.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				EscherRecord escherRecord2 = (EscherRecord)enumerator2.Current;
				num2 += escherRecord2.Serialize(num2, data, listener);
			}
			listener.AfterRecordSerialize(num2, RecordId, num2 - offset, this);
			return num2 - offset;
		}

		/// <summary>
		/// Do any of our (top level) children have the
		/// given recordId?
		/// </summary>
		/// <param name="recordId">The record id.</param>
		/// <returns>
		/// 	<c>true</c> if [has child of type] [the specified record id]; otherwise, <c>false</c>.
		/// </returns>
		public bool HasChildOfType(short recordId)
		{
			IEnumerator enumerator = ChildRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				if (escherRecord.RecordId == recordId)
				{
					return true;
				}
			}
			return false;
		}

		public bool RemoveChildRecord(EscherRecord toBeRemoved)
		{
			return _childRecords.Remove(toBeRemoved);
		}

		public List<EscherRecord>.Enumerator GetChildIterator()
		{
			return _childRecords.GetEnumerator();
		}

		/// <summary>
		/// The display methods allows escher variables to print the record names
		/// according to their hierarchy.
		/// </summary>
		/// <param name="indent">The current indent level.</param> 
		public override void Display(int indent)
		{
			base.Display(indent);
			IEnumerator enumerator = _childRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				escherRecord.Display(indent + 1);
			}
		}

		/// <summary>
		/// Adds the child record.
		/// </summary>
		/// <param name="record">The record.</param>
		public void AddChildRecord(EscherRecord record)
		{
			_childRecords.Add(record);
		}

		public void AddChildBefore(EscherRecord record, int insertBeforeRecordId)
		{
			for (int i = 0; i < _childRecords.Count; i++)
			{
				EscherRecord escherRecord = _childRecords[i];
				if (escherRecord.RecordId == insertBeforeRecordId)
				{
					_childRecords.Insert(i++, record);
				}
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			if (ChildRecords.Count > 0)
			{
				stringBuilder.Append("  children: " + newLine);
				int num = 0;
				IEnumerator enumerator = ChildRecords.GetEnumerator();
				while (enumerator.MoveNext())
				{
					EscherRecord escherRecord = (EscherRecord)enumerator.Current;
					stringBuilder.Append("    Child " + num + ":" + newLine);
					string text = escherRecord.ToString();
					text = text.Replace("\n", "\n    ");
					stringBuilder.Append("    ");
					stringBuilder.Append(text);
					stringBuilder.Append(newLine);
					num++;
				}
			}
			return GetType().Name + " (" + RecordName + "):" + newLine + "  isContainer: " + base.IsContainerRecord + newLine + "  version: 0x" + HexDump.ToHex(Version) + newLine + "  instance: 0x" + HexDump.ToHex(Instance) + newLine + "  recordId: 0x" + HexDump.ToHex(RecordId) + newLine + "  numchildren: " + ChildRecords.Count + newLine + stringBuilder.ToString();
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(RecordName, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance)));
			IEnumerator<EscherRecord> enumerator = _childRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord current = enumerator.Current;
				stringBuilder.Append(current.ToXml(tab + "\t"));
			}
			stringBuilder.Append(tab).Append("</").Append(RecordName)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Gets the child by id.
		/// </summary>
		/// <param name="recordId">The record id.</param>
		/// <returns></returns>
		public EscherRecord GetChildById(short recordId)
		{
			IEnumerator enumerator = _childRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				if (escherRecord.RecordId == recordId)
				{
					return escherRecord;
				}
			}
			return null;
		}

		/// <summary>
		/// Recursively find records with the specified record ID
		/// </summary>
		/// <param name="recordId"></param>
		/// <param name="out1">list to store found records</param>
		public void GetRecordsById(short recordId, ref ArrayList out1)
		{
			IEnumerator enumerator = ChildRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				EscherRecord escherRecord = (EscherRecord)current;
				if (escherRecord is EscherContainerRecord)
				{
					EscherContainerRecord escherContainerRecord = (EscherContainerRecord)escherRecord;
					escherContainerRecord.GetRecordsById(recordId, ref out1);
				}
				else if (escherRecord.RecordId == recordId)
				{
					out1.Add(current);
				}
			}
		}
	}
}
