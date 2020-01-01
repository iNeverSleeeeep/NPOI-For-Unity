using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// This record is used whenever a escher record is encountered that
	/// we do not explicitly support.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class UnknownEscherRecord : EscherRecord
	{
		private static byte[] NO_BYTES = new byte[0];

		/// The data for this record not including the the 8 byte header 
		private byte[] _thedata = NO_BYTES;

		private List<EscherRecord> _childRecords = new List<EscherRecord>();

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <value>The data.</value>
		public byte[] Data => _thedata;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => 8 + _thedata.Length;

		/// <summary>
		/// Returns the children of this record.  By default this will
		/// be an empty list.  EscherCotainerRecord is the only record
		/// that may contain children.
		/// </summary>
		/// <value></value>
		public override List<EscherRecord> ChildRecords
		{
			get
			{
				return _childRecords;
			}
			set
			{
				_childRecords = value;
			}
		}

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "Unknown 0x" + HexDump.ToHex(RecordId);

		/// <summary>
		/// This method deSerializes the record from a byte array.
		/// </summary>
		/// <param name="data"> The byte array containing the escher record information</param>
		/// <param name="offset">The starting offset into data </param>
		/// <param name="recordFactory">May be null since this is not a container record.</param>
		/// <returns>The number of bytes Read from the byte array.</returns>
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			int num2 = data.Length - (offset + 8);
			if (num > num2)
			{
				num = num2;
			}
			if (base.IsContainerRecord)
			{
				int num3 = 0;
				_thedata = new byte[0];
				offset += 8;
				num3 += 8;
				while (num > 0)
				{
					EscherRecord escherRecord = recordFactory.CreateRecord(data, offset);
					int num4 = escherRecord.FillFields(data, offset, recordFactory);
					num3 += num4;
					offset += num4;
					num -= num4;
					ChildRecords.Add(escherRecord);
				}
				return num3;
			}
			_thedata = new byte[num];
			Array.Copy(data, offset + 8, _thedata, 0, num);
			return num + 8;
		}

		/// <summary>
		/// Writes this record and any contained records to the supplied byte
		/// array.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="data"></param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>the number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			int num = _thedata.Length;
			IEnumerator enumerator = ChildRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				num += escherRecord.RecordSize;
			}
			LittleEndian.PutInt(data, offset + 4, num);
			Array.Copy(_thedata, 0, data, offset + 8, _thedata.Length);
			int num2 = offset + 8 + _thedata.Length;
			IEnumerator enumerator2 = ChildRecords.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				EscherRecord escherRecord2 = (EscherRecord)enumerator2.Current;
				num2 += escherRecord2.Serialize(num2, data);
			}
			listener.AfterRecordSerialize(num2, RecordId, num2 - offset, this);
			return num2 - offset;
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
				IEnumerator enumerator = ChildRecords.GetEnumerator();
				while (enumerator.MoveNext())
				{
					EscherRecord escherRecord = (EscherRecord)enumerator.Current;
					stringBuilder.Append(escherRecord.ToString());
					stringBuilder.Append(newLine);
				}
			}
			string text = "";
			try
			{
				if (_thedata.Length != 0)
				{
					text = "  Extra Data(" + _thedata.Length + "):" + newLine;
					text += HexDump.Dump(_thedata, 0L, 0);
				}
			}
			catch (Exception)
			{
				text = "Error!!";
			}
			return GetType().Name + ":" + newLine + "  isContainer: " + base.IsContainerRecord + newLine + "  version: 0x" + HexDump.ToHex(Version) + newLine + "  instance: 0x" + HexDump.ToHex(Instance) + newLine + "  recordId: 0x" + HexDump.ToHex(RecordId) + newLine + "  numchildren: " + ChildRecords.Count + newLine + text + stringBuilder.ToString();
		}

		public override string ToXml(string tab)
		{
			string value = HexDump.ToHex(_thedata, 32);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<IsContainer>")
				.Append(base.IsContainerRecord)
				.Append("</IsContainer>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Numchildren>")
				.Append(HexDump.ToHex(_childRecords.Count))
				.Append("</Numchildren>\n");
			IEnumerator<EscherRecord> enumerator = _childRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord current = enumerator.Current;
				stringBuilder.Append(current.ToXml(tab + "\t"));
			}
			stringBuilder.Append(value).Append("\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Adds the child record.
		/// </summary>
		/// <param name="childRecord">The child record.</param>
		public void AddChildRecord(EscherRecord childRecord)
		{
			ChildRecords.Add(childRecord);
		}
	}
}
