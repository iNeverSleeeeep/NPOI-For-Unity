using NPOI.SS.Util;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// ReferencePtg - handles references (such as A1, A2, IA4)
	/// @author  Andrew C. Oliver (acoliver@apache.org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	[Serializable]
	public class RefPtg : Ref2DPtgBase
	{
		public const byte sid = 36;

		protected override byte Sid => 36;

		/// Takes in a String representation of a cell reference and Fills out the
		/// numeric fields.
		public RefPtg(string cellref)
			: base(new CellReference(cellref))
		{
		}

		public RefPtg(int row, int column, bool isRowRelative, bool isColumnRelative)
			: base(row, column, isRowRelative, isColumnRelative)
		{
			base.Row = row;
			base.Column = column;
			base.IsRowRelative = isRowRelative;
			base.IsColRelative = isColumnRelative;
		}

		public RefPtg(ILittleEndianInput in1)
			: base(in1)
		{
		}

		public RefPtg(CellReference cr)
			: base(cr)
		{
		}
	}
}
