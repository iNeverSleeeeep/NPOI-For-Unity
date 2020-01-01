using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// specifies the text elements that are formatted using the information specified by
	/// the Text record immediately following this record.
	/// </summary>
	public class DefaultTextRecord : StandardRecord
	{
		public const short sid = 4132;

		private short field_1_categoryDataType;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4132;

		/// <summary>
		/// specifies the text elements that are formatted using the position and appearance 
		/// information specified by the Text record immediately following this record.
		/// </summary>
		public TextFormatInfo FormatType
		{
			get
			{
				return (TextFormatInfo)field_1_categoryDataType;
			}
			set
			{
				field_1_categoryDataType = (short)value;
			}
		}

		public DefaultTextRecord()
		{
		}

		/// Constructs a DefaultDataLabelTextProperties record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public DefaultTextRecord(RecordInputStream in1)
		{
			field_1_categoryDataType = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DEFAULTTEXT]\n");
			stringBuilder.Append("    .categoryDataType     = ").Append("0x").Append(HexDump.ToHex((short)FormatType))
				.Append(" (")
				.Append(FormatType)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/DEFAULTTEXT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_categoryDataType);
		}

		public override object Clone()
		{
			DefaultTextRecord defaultTextRecord = new DefaultTextRecord();
			defaultTextRecord.field_1_categoryDataType = field_1_categoryDataType;
			return defaultTextRecord;
		}
	}
}
