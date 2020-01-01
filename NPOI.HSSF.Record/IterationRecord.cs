using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Iteration Record
	/// Description:  Tells whether to iterate over forumla calculations or not
	///               (if a formula Is dependant upon another formula's result)
	///               (odd feature for something that can only have 32 elements in
	///                a formula!)
	/// REFERENCE:  PG 325 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class IterationRecord : StandardRecord
	{
		public const short sid = 17;

		private static BitField iterationOn = BitFieldFactory.GetInstance(1);

		private int _flags;

		/// Get whether or not to iterate for calculations
		///
		/// @return whether iterative calculations are turned off or on
		public bool Iteration
		{
			get
			{
				return iterationOn.IsSet(_flags);
			}
			set
			{
				_flags = iterationOn.SetBoolean(_flags, value);
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 17;

		public IterationRecord(bool iterateOn)
		{
			_flags = iterationOn.SetBoolean(0, iterateOn);
		}

		/// Constructs an Iteration record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public IterationRecord(RecordInputStream in1)
		{
			_flags = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ITERATION]\n");
			stringBuilder.Append("    .flags      = ").Append(HexDump.ShortToHex(_flags)).Append("\n");
			stringBuilder.Append("[/ITERATION]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_flags);
		}

		public override object Clone()
		{
			return new IterationRecord(Iteration);
		}
	}
}
