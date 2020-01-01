using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author Josh Micich
	[Serializable]
	public abstract class Ref2DPtgBase : RefPtgBase
	{
		private const int SIZE = 5;

		protected abstract byte Sid
		{
			get;
		}

		public override int Size => 5;

		/// Takes in a String representation of a cell reference and fills out the
		/// numeric fields.
		protected Ref2DPtgBase(string cellref)
			: base(cellref)
		{
		}

		protected Ref2DPtgBase(CellReference cr)
			: base(cr)
		{
		}

		protected Ref2DPtgBase(int row, int column, bool isRowRelative, bool isColumnRelative)
		{
			base.Row = row;
			base.Column = column;
			base.IsRowRelative = isRowRelative;
			base.IsColRelative = isColumnRelative;
		}

		protected Ref2DPtgBase(ILittleEndianInput in1)
		{
			ReadCoordinates(in1);
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(Sid + base.PtgClass);
			WriteCoordinates(out1);
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
