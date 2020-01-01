using NPOI.SS.Util;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// Specifies a rectangular area of cells A1:A4 for instance.
	/// @author Jason Height (jheight at chariot dot net dot au)
	[Serializable]
	public class AreaPtg : Area2DPtgBase
	{
		public const byte sid = 37;

		protected override byte Sid => 37;

		public AreaPtg(int firstRow, int lastRow, int firstColumn, int lastColumn, bool firstRowRelative, bool lastRowRelative, bool firstColRelative, bool lastColRelative)
			: base(firstRow, lastRow, firstColumn, lastColumn, firstRowRelative, lastRowRelative, firstColRelative, lastColRelative)
		{
		}

		public AreaPtg(ILittleEndianInput in1)
			: base(in1)
		{
		}

		public AreaPtg(string arearef)
			: base(arearef)
		{
		}

		public AreaPtg(AreaReference areaRef)
			: base(areaRef)
		{
		}
	}
}
