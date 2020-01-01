using NPOI.DDF;
using System.Collections.Generic;

namespace NPOI.HSSF.Record
{
	internal class SerializationListener : EscherSerializationListener
	{
		private IList<int> spEndingOffsets;

		private IList<EscherRecord> records;

		private EscherRecord record;

		public SerializationListener(IList<int> spEndingOffsets, IList<EscherRecord> records, EscherRecord e)
		{
			this.spEndingOffsets = spEndingOffsets;
			this.records = records;
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
				records.Add(record);
			}
		}
	}
}
