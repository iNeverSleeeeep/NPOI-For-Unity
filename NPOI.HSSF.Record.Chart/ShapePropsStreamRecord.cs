using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The ShapePropsStream record specifies the shape formatting properties for chart elements.
	/// These shape formatting properties are a superset of the properties stored in the LineFormat,
	/// AreaFormat, MarkerFormat, and GelFrame records. They are stored in the rgb field, which is an 
	/// XML stream (section 2.1.7.22), as defined in [ECMA-376] Part 4, section 5.7.2.198.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class ShapePropsStreamRecord : RowDataRecord
	{
		public const short sid = 2212;

		protected override int DataSize => base.DataSize;

		public override short Sid => 2212;

		public ShapePropsStreamRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
