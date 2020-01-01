using NPOI.Util;

namespace NPOI.HSSF.Record
{
	/// <summary>
	/// The ContinueFrt12 record specifies a continuation of the data in a preceding Future Record 
	/// Type record that has data longer than 8,224 bytes. Such records are split into several records. 
	/// The first section of the data appears in the base record and subsequent sections appear in 
	/// one or more ContinueFrt12 records that appear after the base record. The preceding base record 
	/// MUST contain a FrtRefHeader or a FrtHeader field.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class ContinueFrt12Record : RowDataRecord
	{
		public const short sid = 2175;

		protected override int DataSize => base.DataSize;

		public override short Sid => 2175;

		public ContinueFrt12Record(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
