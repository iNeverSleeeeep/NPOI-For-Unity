using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The CrtMlFrt record specifies additional properties for chart elements, as specified by 
	/// the Chart Sheet Substream ABNF. These properties complement the record to which they 
	/// correspond, and are stored as a structure chain defined in XmlTkChain. An application 
	/// can ignore this record without loss of functionality, except for the additional properties. 
	/// If this record is longer than 8224 bytes, it MUST be split into several records. The first
	/// section of the data appears in this record and subsequent sections appear in one or more 
	/// CrtMlFrtContinue records that follow this record.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class CrtMlFrtRecord : RowDataRecord
	{
		public const short sid = 2206;

		protected override int DataSize => base.DataSize;

		public override short Sid => 2206;

		public CrtMlFrtRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
