using NPOI.HSSF.Model;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates
{
	internal class PLSAggregate : RecordAggregate
	{
		private static ContinueRecord[] EMPTY_CONTINUE_RECORD_ARRAY = new ContinueRecord[0];

		private Record _pls;

		/// holds any continue records found after the PLS record.<br />
		/// This would not be required if PLS was properly interpreted.
		/// Currently, PLS is an {@link UnknownRecord} and does not automatically
		/// include any trailing {@link ContinueRecord}s.
		private ContinueRecord[] _plsContinues;

		public PLSAggregate(RecordStream rs)
		{
			_pls = rs.GetNext();
			if (rs.PeekNextSid() == 60)
			{
				List<ContinueRecord> list = new List<ContinueRecord>();
				while (rs.PeekNextSid() == 60)
				{
					list.Add((ContinueRecord)rs.GetNext());
				}
				_plsContinues = new ContinueRecord[list.Count];
				_plsContinues = list.ToArray();
			}
			else
			{
				_plsContinues = EMPTY_CONTINUE_RECORD_ARRAY;
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(_pls);
			for (int i = 0; i < _plsContinues.Length; i++)
			{
				rv.VisitRecord(_plsContinues[i]);
			}
		}
	}
}
