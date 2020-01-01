using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The RichTextStream record specifies additional text properties for the text in 
	/// the entire chart, text in the current legend, text in the current legend entry, 
	/// or text in the attached label. These text properties are a superset of the 
	/// properties stored in the Text, Font, FontX, BRAI, and ObjectLink records based 
	/// on the following table, as specified by the Chart Sheet Substream ABNF. In each 
	/// case, the associated Font record is specified by the associated FontX record. 
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class RichTextStreamRecord : RowDataRecord
	{
		public const short sid = 2214;

		protected override int DataSize => base.DataSize;

		public override short Sid => 2214;

		public RichTextStreamRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
