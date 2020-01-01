using System.Collections.Generic;

namespace NPOI.HSSF.Record
{
	/// VerticalPageBreak record that stores page breaks at columns
	///
	/// This class Is just used so that SID Compares work properly in the RecordFactory
	/// @see PageBreakRecord
	/// @author Danny Mui (dmui at apache dot org) 
	public class VerticalPageBreakRecord : PageBreakRecord
	{
		public new const short sid = 26;

		public override short Sid => 26;

		public VerticalPageBreakRecord()
		{
		}

		/// @param in the RecordInputstream to Read the record from
		public VerticalPageBreakRecord(RecordInputStream in1)
			: base(in1)
		{
		}

		public override object Clone()
		{
			PageBreakRecord pageBreakRecord = new VerticalPageBreakRecord();
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
