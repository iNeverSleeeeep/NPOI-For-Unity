using NPOI.SS.Formula.PTG;
using System.Text;

namespace NPOI.SS.Formula.Eval
{
	/// @author Josh Micich
	public class NameXEval : ValueEval
	{
		private NameXPtg _ptg;

		public NameXPtg Ptg => _ptg;

		public NameXEval(NameXPtg ptg)
		{
			_ptg = ptg;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(_ptg.SheetRefIndex).Append(", ").Append(_ptg.NameIndex);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
