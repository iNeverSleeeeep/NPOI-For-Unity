using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using System;
using System.Text;

namespace NPOI.SS.Formula
{
	/// @author Josh Micich 
	public class LazyRefEval : RefEvalBase
	{
		private SheetRefEvaluator _evaluator;

		public override ValueEval InnerValueEval => _evaluator.GetEvalForCell(base.Row, base.Column);

		public LazyRefEval(int rowIndex, int columnIndex, SheetRefEvaluator sre)
			: base(rowIndex, columnIndex)
		{
			if (sre == null)
			{
				throw new ArgumentException("sre must not be null");
			}
			_evaluator = sre;
		}

		public LazyRefEval(Ref3DPtg ptg, SheetRefEvaluator sre)
			: base(ptg.Row, ptg.Column)
		{
			_evaluator = sre;
		}

		public override AreaEval Offset(int relFirstRowIx, int relLastRowIx, int relFirstColIx, int relLastColIx)
		{
			AreaI ptg = new OffsetArea(base.Row, base.Column, relFirstRowIx, relLastRowIx, relFirstColIx, relLastColIx);
			return new LazyAreaEval(ptg, _evaluator);
		}

		public override string ToString()
		{
			CellReference cellReference = new CellReference(base.Row, base.Column);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name).Append("[");
			stringBuilder.Append(_evaluator.SheetName);
			stringBuilder.Append('!');
			stringBuilder.Append(cellReference.FormatAsString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
