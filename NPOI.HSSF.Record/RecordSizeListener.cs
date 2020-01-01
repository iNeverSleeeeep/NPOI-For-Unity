using NPOI.DDF;
using System.Collections.Generic;

namespace NPOI.HSSF.Record
{
	internal class RecordSizeListener : EscherSerializationListener
	{
		private IList<int> spEndingOffsets;

		private EscherRecord record;

		public RecordSizeListener(IList<int> spEndingOffsets, EscherRecord e)
		{
			this.spEndingOffsets = spEndingOffsets;
			record = e;
		}

		void EscherSerializationListener.BeforeRecordSerialize(int offset, short recordId, EscherRecord record)
		{
		}

		void EscherSerializationListener.AfterRecordSerialize(int offset, short recordId, int size, EscherRecord record)
		{
			if (recordId == -4079 || recordId == -4083)
			{
				spEndingOffsets.Add(offset);
			}
		}
	}
}
