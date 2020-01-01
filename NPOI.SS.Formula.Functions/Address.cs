using NPOI.SS.Formula.Eval;
using NPOI.SS.Util;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	/// Creates a text reference as text, given specified row and column numbers.
	///
	/// @author Aniket Banerjee (banerjee@google.com)
	public class Address : Function
	{
		public const int REF_ABSOLUTE = 1;

		public const int REF_ROW_ABSOLUTE_COLUMN_RELATIVE = 2;

		public const int REF_ROW_RELATIVE_RELATIVE_ABSOLUTE = 3;

		public const int REF_RELATIVE = 4;

		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			if (args.Length >= 2 && args.Length <= 5)
			{
				try
				{
					int num = (int)NumericFunction.SingleOperandEvaluate(args[0], srcRowIndex, srcColumnIndex);
					int num2 = (int)NumericFunction.SingleOperandEvaluate(args[1], srcRowIndex, srcColumnIndex);
					bool pAbsRow;
					bool pAbsCol;
					switch ((args.Length <= 2 || args[2] == MissingArgEval.instance) ? 1 : ((int)NumericFunction.SingleOperandEvaluate(args[2], srcRowIndex, srcColumnIndex)))
					{
					case 1:
						pAbsRow = true;
						pAbsCol = true;
						break;
					case 2:
						pAbsRow = true;
						pAbsCol = false;
						break;
					case 3:
						pAbsRow = false;
						pAbsCol = true;
						break;
					case 4:
						pAbsRow = false;
						pAbsCol = false;
						break;
					default:
						throw new EvaluationException(ErrorEval.VALUE_INVALID);
					}
					if (args.Length > 3)
					{
						ValueEval singleValue = OperandResolver.GetSingleValue(args[3], srcRowIndex, srcColumnIndex);
						if (singleValue != MissingArgEval.instance)
						{
							bool value = OperandResolver.CoerceValueToBoolean(singleValue, stringsAreBlanks: false).Value;
						}
					}
					string text;
					if (args.Length == 5)
					{
						ValueEval singleValue2 = OperandResolver.GetSingleValue(args[4], srcRowIndex, srcColumnIndex);
						text = ((singleValue2 == MissingArgEval.instance) ? null : OperandResolver.CoerceValueToString(singleValue2));
					}
					else
					{
						text = null;
					}
					CellReference cellReference = new CellReference(num - 1, num2 - 1, pAbsRow, pAbsCol);
					StringBuilder stringBuilder = new StringBuilder(32);
					if (text != null)
					{
						SheetNameFormatter.AppendFormat(stringBuilder, text);
						stringBuilder.Append('!');
					}
					stringBuilder.Append(cellReference.FormatAsString());
					return new StringEval(stringBuilder.ToString());
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
			return ErrorEval.VALUE_INVALID;
		}
	}
}
