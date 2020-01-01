using System.Text;

namespace NPOI.SS.Formula.Eval
{
	/// @author Josh Micich
	public class NameEval : ValueEval
	{
		private string _functionName;

		public string FunctionName => _functionName;

		/// Creates a NameEval representing a function name
		public NameEval(string functionName)
		{
			_functionName = functionName;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(_functionName);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
