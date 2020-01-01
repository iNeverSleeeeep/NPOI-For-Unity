using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Record for the top margin.
	/// NOTE: This source was automatically generated.
	///
	/// @author Shawn Laubach (slaubach at apache dot org)
	public class TopMarginRecord : StandardRecord, IMargin
	{
		public const short sid = 40;

		private double field_1_margin;

		protected override int DataSize => 8;

		public override short Sid => 40;

		/// Get the margin field for the TopMargin record.
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

		public TopMarginRecord()
		{
		}

		/// Constructs a TopMargin record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public TopMarginRecord(RecordInputStream in1)
		{
			field_1_margin = in1.ReadDouble();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[TopMargin]\n");
			stringBuilder.Append("    .margin               = ").Append(" (").Append(Margin)
				.Append(" )\n");
			stringBuilder.Append("[/TopMargin]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteDouble(field_1_margin);
		}

		public override object Clone()
		{
			TopMarginRecord topMarginRecord = new TopMarginRecord();
			topMarginRecord.field_1_margin = field_1_margin;
			return topMarginRecord;
		}
	}
}
