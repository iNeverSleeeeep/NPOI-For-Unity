using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Record for the right margin. * NOTE: This source was automatically generated. * @author Shawn Laubach (slaubach at apache dot org)
	public class RightMarginRecord : StandardRecord, IMargin
	{
		public const short sid = 39;

		private double field_1_margin;

		public override short Sid => 39;

		protected override int DataSize => 8;

		/// Get the margin field for the RightMargin record.
		public double Margin
		{
			get
			{
				return field_1_margin;
			}
			set
			{
				field_1_margin = value;
			}
		}

		public RightMarginRecord()
		{
		}

		/// Constructs a RightMargin record and Sets its fields appropriately.     *     * @param id    id must be 0x27 or an exception     *              will be throw upon validation     * @param size  size the size of the data area of the record     * @param data  data of the record (should not contain sid/len)
		public RightMarginRecord(RecordInputStream in1)
		{
			field_1_margin = in1.ReadDouble();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[RightMargin]\n");
			stringBuilder.Append("    .margin               = ").Append(" (").Append(Margin)
				.Append(" )\n");
			stringBuilder.Append("[/RightMargin]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteDouble(field_1_margin);
		}

		public override object Clone()
		{
			RightMarginRecord rightMarginRecord = new RightMarginRecord();
			rightMarginRecord.field_1_margin = field_1_margin;
			return rightMarginRecord;
		}
	}
}
