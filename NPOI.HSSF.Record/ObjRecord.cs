using NPOI.Util;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// The obj record is used to hold various graphic objects and controls.
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	public class ObjRecord : Record
	{
		private const int NORMAL_PAD_ALIGNMENT = 2;

		private const int MAX_PAD_ALIGNMENT = 4;

		public const short sid = 93;

		private List<SubRecord> subrecords;

		/// used when POI has no idea what is going on 
		private byte[] _uninterpretedData;

		/// Excel seems to tolerate padding to quad or double byte length
		private bool _isPaddedToQuadByteMultiple;

		/// Size of record (excluding 4 byte header)
		public override int RecordSize
		{
			get
			{
				if (_uninterpretedData != null)
				{
					return _uninterpretedData.Length + 4;
				}
				int i = 0;
				for (int num = subrecords.Count - 1; num >= 0; num--)
				{
					SubRecord subRecord = subrecords[num];
					i += subRecord.DataSize + 4;
				}
				if (!_isPaddedToQuadByteMultiple)
				{
					for (; i % 2 != 0; i++)
					{
					}
				}
				else
				{
					for (; i % 4 != 0; i++)
					{
					}
				}
				return i + 4;
			}
		}

		public override short Sid => 93;

		public List<SubRecord> SubRecords => subrecords;

		public ObjRecord()
		{
			subrecords = new List<SubRecord>(2);
			_uninterpretedData = null;
		}

		/// Constructs a OBJ record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public ObjRecord(RecordInputStream in1)
		{
			byte[] array = in1.ReadRemainder();
			if (LittleEndian.GetUShort(array, 0) != 21)
			{
				_uninterpretedData = array;
				subrecords = null;
			}
			else
			{
				subrecords = new List<SubRecord>();
				using (MemoryStream @is = new MemoryStream(array))
				{
					LittleEndianInputStream littleEndianInputStream = new LittleEndianInputStream(@is);
					CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)SubRecord.CreateSubRecord(littleEndianInputStream, CommonObjectType.Group);
					subrecords.Add(commonObjectDataSubRecord);
					SubRecord subRecord;
					do
					{
						subRecord = SubRecord.CreateSubRecord(littleEndianInputStream, commonObjectDataSubRecord.ObjectType);
						subrecords.Add(subRecord);
					}
					while (!subRecord.IsTerminating);
					int num = littleEndianInputStream.Available();
					if (num > 0)
					{
						_isPaddedToQuadByteMultiple = (array.Length % 4 == 0);
						if (num >= (_isPaddedToQuadByteMultiple ? 4 : 2))
						{
							if (!CanPaddingBeDiscarded(array, num))
							{
								string exception = "Leftover " + num + " bytes in subrecord data " + HexDump.ToHex(array);
								throw new RecordFormatException(exception);
							}
							_isPaddedToQuadByteMultiple = false;
						}
					}
					else
					{
						_isPaddedToQuadByteMultiple = false;
					}
					_uninterpretedData = null;
				}
			}
		}

		/// Some XLS files have ObjRecords with nearly 8Kb of excessive padding. These were probably
		/// written by a version of POI (around 3.1) which incorrectly interpreted the second short of
		/// the ftLbs subrecord (0x1FEE) as a length, and read that many bytes as padding (other bugs
		/// helped allow this to occur).
		///
		/// Excel reads files with this excessive padding OK, truncating the over-sized ObjRecord back
		/// to the its proper size.  POI does the same.
		private static bool CanPaddingBeDiscarded(byte[] data, int nRemainingBytes)
		{
			for (int i = data.Length - nRemainingBytes; i < data.Length; i++)
			{
				if (data[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[OBJ]\n");
			for (int i = 0; i < subrecords.Count; i++)
			{
				SubRecord subRecord = subrecords[i];
				stringBuilder.Append("SUBRECORD: ").Append(subRecord.ToString());
			}
			stringBuilder.Append("[/OBJ]\n");
			return stringBuilder.ToString();
		}

		public override int Serialize(int offset, byte[] data)
		{
			int recordSize = RecordSize;
			int num = recordSize - 4;
			LittleEndianByteArrayOutputStream littleEndianByteArrayOutputStream = new LittleEndianByteArrayOutputStream(data, offset, recordSize);
			littleEndianByteArrayOutputStream.WriteShort(93);
			littleEndianByteArrayOutputStream.WriteShort(num);
			if (_uninterpretedData == null)
			{
				for (int i = 0; i < subrecords.Count; i++)
				{
					SubRecord subRecord = subrecords[i];
					subRecord.Serialize(littleEndianByteArrayOutputStream);
				}
				int num2 = offset + num;
				while (littleEndianByteArrayOutputStream.WriteIndex < num2)
				{
					littleEndianByteArrayOutputStream.WriteByte(0);
				}
			}
			else
			{
				littleEndianByteArrayOutputStream.Write(_uninterpretedData);
			}
			return recordSize;
		}

		public void ClearSubRecords()
		{
			subrecords.Clear();
		}

		public void AddSubRecord(int index, SubRecord element)
		{
			subrecords.Insert(index, element);
		}

		public void AddSubRecord(SubRecord o)
		{
			subrecords.Add(o);
		}

		public override object Clone()
		{
			ObjRecord objRecord = new ObjRecord();
			for (int i = 0; i < subrecords.Count; i++)
			{
				SubRecord subRecord = subrecords[i];
				objRecord.AddSubRecord((SubRecord)subRecord.Clone());
			}
			return objRecord;
		}
	}
}
