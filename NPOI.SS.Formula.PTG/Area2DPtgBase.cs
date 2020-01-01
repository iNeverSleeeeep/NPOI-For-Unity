using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Common superclass of 2-D area refs 
	[Serializable]
	public abstract class Area2DPtgBase : AreaPtgBase
	{
		private const int SIZE = 9;

		protected abstract byte Sid
		{
			get;
		}

		public override int Size => 9;

		protected Area2DPtgBase(int firstRow, int lastRow, int firstColumn, int lastColumn, bool firstRowRelative, bool lastRowRelative, bool firstColRelative, bool lastColRelative)
			: base(firstRow, lastRow, firstColumn, lastColumn, firstRowRelative, lastRowRelative, firstColRelative, lastColRelative)
		{
		}

		protected Area2DPtgBase(AreaReference ar)
			: base(ar)
		{
		}

		protected Area2DPtgBase(ILittleEndianInput in1)
		{
			ReadCoordinates(in1);
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(Sid + base.PtgClass);
			WriteCoordinates(out1);
		}

		public Area2DPtgBase(string arearef)
			: base(arearef)
		{
		}

		public override string ToFormulaString()
		{
			return FormatReferenceAsString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append(" [");
			stringBuilder.Append(FormatReferenceAsString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
