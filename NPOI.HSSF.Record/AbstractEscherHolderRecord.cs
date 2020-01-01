using NPOI.DDF;
using NPOI.HSSF.Util;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// The escher container record is used to hold escher records.  It is abstract and
	/// must be subclassed for maximum benefit.
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @author Michael Zalewski (zalewski at optonline.net)
	public abstract class AbstractEscherHolderRecord : Record
	{
		private static bool DESERIALISE;

		private List<EscherRecord> escherRecords;

		private LazilyConcatenatedByteArray rawDataContainer = new LazilyConcatenatedByteArray();

		protected abstract string RecordName
		{
			get;
		}

		/// Size of record (including 4 byte header)
		public override int RecordSize
		{
			get
			{
				byte[] rawData = RawData;
				if (escherRecords.Count == 0 && rawData != null)
				{
					return rawData.Length + 4;
				}
				int num = 4;
				foreach (EscherRecord escherRecord in escherRecords)
				{
					num += escherRecord.RecordSize;
				}
				return num;
			}
		}

		public List<EscherRecord> EscherRecords => escherRecords;

		public byte[] RawData
		{
			get
			{
				return rawDataContainer.ToArray();
			}
			set
			{
				rawDataContainer.Clear();
				rawDataContainer.Concatenate(value);
			}
		}

		static AbstractEscherHolderRecord()
		{
			DESERIALISE = false;
		}

		public AbstractEscherHolderRecord()
		{
			escherRecords = new List<EscherRecord>();
		}

		/// Constructs a Bar record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public AbstractEscherHolderRecord(RecordInputStream in1)
		{
			escherRecords = new List<EscherRecord>();
			if (!DESERIALISE)
			{
				rawDataContainer.Concatenate(in1.ReadRemainder());
			}
			else
			{
				byte[] array = in1.ReadAllContinuedRemainder();
				ConvertToEscherRecords(0, array.Length, array);
			}
		}

		protected void ConvertRawBytesToEscherRecords()
		{
			byte[] rawData = RawData;
			ConvertToEscherRecords(0, rawData.Length, rawData);
		}

		private void ConvertToEscherRecords(int offset, int size, byte[] data)
		{
			escherRecords.Clear();
			IEscherRecordFactory escherRecordFactory = new DefaultEscherRecordFactory();
			int num;
			for (int i = offset; i < offset + size; i += num)
			{
				EscherRecord escherRecord = escherRecordFactory.CreateRecord(data, i);
				num = escherRecord.FillFields(data, i, escherRecordFactory);
				escherRecords.Add(escherRecord);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Environment.NewLine;
			stringBuilder.Append('[' + RecordName + ']' + newLine);
			if (escherRecords.Count == 0)
			{
				stringBuilder.Append("No Escher Records Decoded" + newLine);
			}
			foreach (EscherRecord escherRecord in escherRecords)
			{
				stringBuilder.Append(escherRecord.ToString());
			}
			stringBuilder.Append("[/" + RecordName + ']' + newLine);
			return stringBuilder.ToString();
		}

		public override int Serialize(int offset, byte[] data)
		{
			LittleEndian.PutShort(data, offset, Sid);
			LittleEndian.PutShort(data, 2 + offset, (short)(RecordSize - 4));
			byte[] rawData = RawData;
			if (escherRecords.Count == 0 && rawData != null)
			{
				LittleEndian.PutShort(data, offset, Sid);
				LittleEndian.PutShort(data, 2 + offset, (short)(RecordSize - 4));
				Array.Copy(rawData, 0, data, 4 + offset, rawData.Length);
				return rawData.Length + 4;
			}
			LittleEndian.PutShort(data, offset, Sid);
			LittleEndian.PutShort(data, 2 + offset, (short)(RecordSize - 4));
			int num = offset + 4;
			foreach (EscherRecord escherRecord in escherRecords)
			{
				num += escherRecord.Serialize(num, data, new NullEscherSerializationListener());
			}
			return RecordSize;
		}

		public override object Clone()
		{
			return CloneViaReserialise();
		}

		/// Clone the current record, via a call to serialise
		///  it, and another to Create a new record from the
		///  bytes.
		/// May only be used for classes which don't have
		///  internal counts / ids in them. For those which
		///  do, a full record-aware serialise is needed, which
		///  allocates new ids / counts as needed.
		public void AddEscherRecord(int index, EscherRecord element)
		{
			escherRecords.Insert(index, element);
		}

		public bool AddEscherRecord(EscherRecord element)
		{
			escherRecords.Add(element);
			return true;
		}

		public void ClearEscherRecords()
		{
			escherRecords.Clear();
		}

		/// If we have a EscherContainerRecord as one of our
		///  children (and most top level escher holders do),
		///  then return that.
		public EscherContainerRecord GetEscherContainer()
		{
			IEnumerator enumerator = escherRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				if (current is EscherContainerRecord)
				{
					return (EscherContainerRecord)current;
				}
			}
			return null;
		}

		/// Descends into all our children, returning the
		///  first EscherRecord with the given id, or null
		///  if none found
		public EscherRecord FindFirstWithId(short id)
		{
			return FindFirstWithId(id, EscherRecords);
		}

		private EscherRecord FindFirstWithId(short id, List<EscherRecord> records)
		{
			IEnumerator enumerator = records.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				if (escherRecord.RecordId == id)
				{
					return escherRecord;
				}
			}
			IEnumerator enumerator2 = records.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				EscherRecord escherRecord2 = (EscherRecord)enumerator2.Current;
				if (escherRecord2.IsContainerRecord)
				{
					EscherRecord escherRecord3 = FindFirstWithId(id, escherRecord2.ChildRecords);
					if (escherRecord3 != null)
					{
						return escherRecord3;
					}
				}
			}
			return null;
		}

		public EscherRecord GetEscherRecord(int index)
		{
			return escherRecords[index];
		}

		/// Big drawing Group records are split but it's easier to deal with them
		/// as a whole Group so we need to join them toGether.
		public void Join(AbstractEscherHolderRecord record)
		{
			rawDataContainer.Concatenate(record.RawData);
		}

		public void ProcessContinueRecord(byte[] record)
		{
			rawDataContainer.Concatenate(record);
		}

		/// Convert raw data to escher records.
		public void Decode()
		{
			if (escherRecords == null || escherRecords.Count == 0)
			{
				byte[] rawData = RawData;
				ConvertToEscherRecords(0, rawData.Length, rawData);
			}
		}
	}
}
