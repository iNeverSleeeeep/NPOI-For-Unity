using NPOI.HSSF.Model;
using System;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates
{
	/// Manages the all the records associated with a 'Custom View Settings' sub-stream.<br />
	/// Includes the Initial USERSVIEWBEGIN(0x01AA) and USERSVIEWEND(0x01AB).
	///
	/// @author Josh Micich
	public class CustomViewSettingsRecordAggregate : RecordAggregate
	{
		private Record _begin;

		private Record _end;

		/// All the records between BOF and EOF
		private List<RecordBase> _recs;

		private PageSettingsBlock _psBlock;

		public CustomViewSettingsRecordAggregate(RecordStream rs)
		{
			_begin = rs.GetNext();
			if (_begin.Sid != 426)
			{
				throw new InvalidOperationException("Bad begin record");
			}
			List<RecordBase> list = new List<RecordBase>();
			while (rs.PeekNextSid() != 427)
			{
				if (PageSettingsBlock.IsComponentRecord(rs.PeekNextSid()))
				{
					if (_psBlock != null)
					{
						throw new InvalidOperationException("Found more than one PageSettingsBlock in custom view Settings sub-stream");
					}
					_psBlock = new PageSettingsBlock(rs);
					list.Add(_psBlock);
				}
				else
				{
					list.Add(rs.GetNext());
				}
			}
			_recs = list;
			_end = rs.GetNext();
			if (_end.Sid != 427)
			{
				throw new InvalidOperationException("Bad custom view Settings end record");
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			if (_recs.Count != 0)
			{
				rv.VisitRecord(_begin);
				for (int i = 0; i < _recs.Count; i++)
				{
					RecordBase recordBase = _recs[i];
					if (recordBase is RecordAggregate)
					{
						((RecordAggregate)recordBase).VisitContainedRecords(rv);
					}
					else
					{
						rv.VisitRecord((Record)recordBase);
					}
				}
				rv.VisitRecord(_end);
			}
		}

		public static bool IsBeginRecord(int sid)
		{
			return sid == 426;
		}

		public void Append(RecordBase r)
		{
			_recs.Add(r);
		}
	}
}
