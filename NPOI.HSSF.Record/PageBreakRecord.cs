using NPOI.Util;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Record that Contains the functionality page _breaks (horizontal and vertical)
	///
	/// The other two classes just specifically Set the SIDS for record creation.
	///
	/// REFERENCE:  Microsoft Excel SDK page 322 and 420
	///
	/// @see HorizontalPageBreakRecord
	/// @see VerticalPageBreakRecord
	/// @author Danny Mui (dmui at apache dot org)
	public class PageBreakRecord : StandardRecord
	{
		/// Since both records store 2byte integers (short), no point in 
		/// differentiating it in the records.
		///
		/// The subs (rows or columns, don't seem to be able to Set but excel Sets
		/// them automatically)
		public class Break
		{
			public const int ENCODED_SIZE = 6;

			public int main;

			public int subFrom;

			public int subTo;

			public Break(RecordInputStream in1)
			{
				main = in1.ReadUShort() - 1;
				subFrom = in1.ReadUShort();
				subTo = in1.ReadUShort();
			}

			public Break(int main, int subFrom, int subTo)
			{
				this.main = main;
				this.subFrom = subFrom;
				this.subTo = subTo;
			}

			public void Serialize(ILittleEndianOutput out1)
			{
				out1.WriteShort(main + 1);
				out1.WriteShort(subFrom);
				out1.WriteShort(subTo);
			}
		}

		private const bool IS_EMPTY_RECORD_WRITTEN = false;

		private static readonly int[] EMPTY_INT_ARRAY = new int[0];

		public short sid;

		private IList<Break> _breaks;

		private Hashtable _breakMap;

		public override short Sid => sid;

		protected override int DataSize => 2 + _breaks.Count * 6;

		public override int RecordSize
		{
			get
			{
				int count = _breaks.Count;
				if (count < 1)
				{
					return 0;
				}
				return 4 + DataSize;
			}
		}

		public int NumBreaks => _breaks.Count;

		public bool IsEmpty => _breaks.Count == 0;

		public PageBreakRecord()
		{
			_breaks = new List<Break>();
			_breakMap = new Hashtable();
		}

		public PageBreakRecord(RecordInputStream in1)
		{
			int num = in1.ReadShort();
			_breaks = new List<Break>(num + 2);
			_breakMap = new Hashtable();
			for (int i = 0; i < num; i++)
			{
				Break @break = new Break(in1);
				_breaks.Add(@break);
				_breakMap[@break.main] = @break;
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			int count = _breaks.Count;
			out1.WriteShort(count);
			for (int i = 0; i < count; i++)
			{
				_breaks[i].Serialize(out1);
			}
		}

		public IEnumerator<Break> GetBreaksEnumerator()
		{
			return _breaks.GetEnumerator();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string str;
			string value;
			string value2;
			if (Sid == 27)
			{
				str = "HORIZONTALPAGEBREAK";
				value = "row";
				value2 = "col";
			}
			else
			{
				str = "VERTICALPAGEBREAK";
				value = "column";
				value2 = "row";
			}
			stringBuilder.Append("[" + str + "]").Append("\n");
			stringBuilder.Append("     .Sid        =").Append(Sid).Append("\n");
			stringBuilder.Append("     .num_breaks =").Append(NumBreaks).Append("\n");
			IEnumerator breaksEnumerator = GetBreaksEnumerator();
			for (int i = 0; i < NumBreaks; i++)
			{
				Break @break = (Break)breaksEnumerator.Current;
				stringBuilder.Append("     .").Append(value).Append(" (zero-based) =")
					.Append(@break.main)
					.Append("\n");
				stringBuilder.Append("     .").Append(value2).Append("From    =")
					.Append(@break.subFrom)
					.Append("\n");
				stringBuilder.Append("     .").Append(value2).Append("To      =")
					.Append(@break.subTo)
					.Append("\n");
			}
			stringBuilder.Append("[" + str + "]").Append("\n");
			return stringBuilder.ToString();
		}

		/// Adds the page break at the specified parameters
		/// @param main Depending on sid, will determine row or column to put page break (zero-based)
		/// @param subFrom No user-interface to Set (defaults to minumum, 0)
		/// @param subTo No user-interface to Set
		public void AddBreak(int main, int subFrom, int subTo)
		{
			Break @break = (Break)_breakMap[main];
			if (@break != null)
			{
				@break.main = main;
				@break.subFrom = subFrom;
				@break.subTo = subTo;
			}
			else
			{
				@break = new Break(main, subFrom, subTo);
				_breaks.Add(@break);
			}
			_breakMap[main] = @break;
		}

		/// Removes the break indicated by the parameter
		/// @param main (zero-based)
		public void RemoveBreak(int main)
		{
			Break item = (Break)_breakMap[main];
			_breaks.Remove(item);
			_breakMap.Remove(main);
		}

		/// Retrieves the region at the row/column indicated
		/// @param main FIXME: Document this!
		/// @return The Break or null if no break exists at the row/col specified.
		public Break GetBreak(int main)
		{
			return (Break)_breakMap[main];
		}

		public int[] GetBreaks()
		{
			int numBreaks = NumBreaks;
			if (numBreaks < 1)
			{
				return EMPTY_INT_ARRAY;
			}
			int[] array = new int[numBreaks];
			for (int i = 0; i < numBreaks; i++)
			{
				Break @break = _breaks[i];
				array[i] = @break.main;
			}
			return array;
		}
	}
}
