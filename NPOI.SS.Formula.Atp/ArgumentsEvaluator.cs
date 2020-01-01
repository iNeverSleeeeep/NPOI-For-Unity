using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace NPOI.SS.Formula.Atp
{
	/// Evaluator for formula arguments.
	///
	/// @author jfaenomoto@gmail.com
	internal class ArgumentsEvaluator
	{
		public static ArgumentsEvaluator instance = new ArgumentsEvaluator();

		private ArgumentsEvaluator()
		{
		}

		/// Evaluate a generic {@link ValueEval} argument to a double value that represents a date in POI.
		///
		/// @param arg {@link ValueEval} an argument.
		/// @param srcCellRow number cell row.
		/// @param srcCellCol number cell column.
		/// @return a double representing a date in POI.
		/// @throws EvaluationException exception upon argument evaluation.
		public double EvaluateDateArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, (short)srcCellCol);
			if (singleValue is StringEval)
			{
				string stringValue = ((StringEval)singleValue).StringValue;
				double num = OperandResolver.ParseDouble(stringValue);
				if (!double.IsNaN(num))
				{
					return num;
				}
				DateTime date = DateParser.ParseDate(stringValue);
				return DateUtil.GetExcelDate(date, use1904windowing: false);
			}
			return OperandResolver.CoerceValueToDouble(singleValue);
		}

		/// Evaluate a generic {@link ValueEval} argument to an array of double values that represents dates in POI.
		///
		/// @param arg {@link ValueEval} an argument.
		/// @param srcCellRow number cell row.
		/// @param srcCellCol number cell column.
		/// @return an array of doubles representing dates in POI.
		/// @throws EvaluationException exception upon argument evaluation.
		public double[] EvaluateDatesArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			if (arg == null)
			{
				return new double[0];
			}
			if (arg is StringEval)
			{
				return new double[1]
				{
					EvaluateDateArg(arg, srcCellRow, srcCellCol)
				};
			}
			if (arg is AreaEvalBase)
			{
				List<double> list = new List<double>();
				AreaEvalBase areaEvalBase = (AreaEvalBase)arg;
				for (int i = areaEvalBase.FirstRow; i <= areaEvalBase.LastRow; i++)
				{
					for (int j = areaEvalBase.FirstColumn; j <= areaEvalBase.LastColumn; j++)
					{
						list.Add(EvaluateDateArg(areaEvalBase.GetValue(i, j), i, j));
					}
				}
				double[] array = new double[list.Count];
				for (int k = 0; k < list.Count; k++)
				{
					array[k] = list[k];
				}
				return array;
			}
			return new double[1]
			{
				OperandResolver.CoerceValueToDouble(arg)
			};
		}

		/// Evaluate a generic {@link ValueEval} argument to a double value.
		///
		/// @param arg {@link ValueEval} an argument.
		/// @param srcCellRow number cell row.
		/// @param srcCellCol number cell column.
		/// @return a double value.
		/// @throws EvaluationException exception upon argument evaluation.
		public double EvaluateNumberArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			if (arg == null)
			{
				return 0.0;
			}
			return OperandResolver.CoerceValueToDouble(arg);
		}
	}
}
