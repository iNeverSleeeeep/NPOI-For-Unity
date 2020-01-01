using NPOI.SS.Formula.Eval;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	public class Clean : SingleArgTextFunc
	{
		public override ValueEval Evaluate(string arg)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in arg)
			{
				if (TextFunction.IsPrintable(c))
				{
					stringBuilder.Append(c);
				}
			}
			return new StringEval(stringBuilder.ToString());
		}
	}
}
