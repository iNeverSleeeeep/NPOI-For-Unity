using System.Collections.Generic;

namespace NPOI.HSSF.Record
{
	/// HorizontalPageBreak record that stores page breaks at rows
	///
	/// This class Is just used so that SID Compares work properly in the RecordFactory
	/// @see PageBreakRecord
	/// @author Danny Mui (dmui at apache dot org) 
	public class HorizontalPageBreakRecord : PageBreakRecord
	{
		public new const short sid = 27;

		public override short Sid => 27;

		public HorizontalPageBreakRecord()
		{
		}

		/// @param in the RecordInputstream to Read the record from
		public HorizontalPageBreakRecord(RecordInputStream in1)
			: base(in1)
		{
		}

		public override object Clone()
		{
			PageBreakRecord pageBreakRecord = new HorizontalPageBreakRecord();
			IEnumerator<Break> breaksEnumerator = GetBreaksEnumerator();
			while (breaksEnumerator.MoveNext())
			{
				Break current = breaksEnumerator.Current;
				pageBreakRecord.AddBreak(current.main, current.subFrom, current.subTo);
			}
			return pageBreakRecord;
		}
	}
}
