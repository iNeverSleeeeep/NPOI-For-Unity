using NPOI.Util;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Continue Record - Helper class used primarily for SST Records 
	/// Description:  handles overflow for prior record in the input
	///               stream; content Is tailored to that prior record
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Csaba Nagy (ncsaba at yahoo dot com)
	/// @version 2.0-pre
	public class ContinueRecord : StandardRecord, ICloneable
	{
		public const short sid = 60;

		private byte[] field_1_data;

		protected override int DataSize => field_1_data.Length;

		/// Get the data for continuation
		/// @return byte array containing all of the continued data
		public byte[] Data
		{
			get
			{
				return field_1_data;
			}
			set
			{
				field_1_data = value;
			}
		}

		public override short Sid => 60;

		/// default constructor
		private ContinueRecord()
		{
		}

		public ContinueRecord(byte[] data)
		{
			field_1_data = data;
		}

		/// Main constructor -- kinda dummy because we don't validate or fill fields
		///
		/// @param in the RecordInputstream to Read the record from
		public ContinueRecord(RecordInputStream in1)
		{
			field_1_data = in1.ReadRemainder();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.Write(field_1_data);
		}

		/// Writes the full encoding of a Continue record without making an instance
		[Obsolete]
		public static int Write(byte[] destBuf, int destOffset, byte? initialDataByte, byte[] srcData)
		{
			return Write(destBuf, destOffset, initialDataByte, srcData, 0, srcData.Length);
		}

		/// @param initialDataByte (optional - often used for unicode flag). 
		/// If supplied, this will be written before srcData
		/// @return the total number of bytes written
		[Obsolete]
		public static int Write(byte[] destBuf, int destOffset, byte? initialDataByte, byte[] srcData, int srcOffset, int len)
		{
			int num = len + (((int?)initialDataByte).HasValue ? 1 : 0);
			LittleEndian.PutUShort(destBuf, destOffset, 60);
			LittleEndian.PutUShort(destBuf, destOffset + 2, num);
			int num2 = destOffset + 4;
			if (((int?)initialDataByte).HasValue)
			{
				LittleEndian.PutByte(destBuf, num2, Convert.ToByte(initialDataByte, CultureInfo.InvariantCulture));
				num2++;
			}
			Array.Copy(srcData, srcOffset, destBuf, num2, len);
			return 4 + num;
		}

		/// Debugging toString
		///
		/// @return string representation
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CONTINUE RECORD]\n");
			stringBuilder.Append("    .data        = ").Append(StringUtil.ToHexString((short)60)).Append("\n");
			stringBuilder.Append("[/CONTINUE RECORD]\n");
			return stringBuilder.ToString();
		}

		/// Clone this record.
		public override object Clone()
		{
			ContinueRecord continueRecord = new ContinueRecord();
			continueRecord.Data = field_1_data;
			return continueRecord;
		}
	}
}
