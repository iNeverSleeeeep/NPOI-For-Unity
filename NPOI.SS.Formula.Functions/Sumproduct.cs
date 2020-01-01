using NPOI.SS.Formula.Eval;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class Sumproduct : Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			int num = args.Length;
			if (num < 1)
			{
				return ErrorEval.VALUE_INVALID;
			}
			ValueEval valueEval = args[0];
			try
			{
				if (valueEval is NumericValueEval)
				{
					return EvaluateSingleProduct(args);
				}
				if (valueEval is RefEval)
				{
					return EvaluateSingleProduct(args);
				}
				if (valueEval is TwoDEval)
				{
					TwoDEval twoDEval = (TwoDEval)valueEval;
					if (twoDEval.IsRow && twoDEval.IsColumn)
					{
						return EvaluateSingleProduct(args);
					}
					return EvaluateAreaSumProduct(args);
				}
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			throw new RuntimeException("Invalid arg type for SUMPRODUCT: (" + valueEval.GetType().Name + ")");
		}

		private ValueEval EvaluateSingleProduct(ValueEval[] evalArgs)
		{
			int num = evalArgs.Length;
			double num2 = 1.0;
			for (int i = 0; i < num; i++)
			{
				double scalarValue = GetScalarValue(evalArgs[i]);
				num2 *= scalarValue;
			}
			return new NumberEval(num2);
		}

		private static double GetScalarValue(ValueEval arg)
		{
			ValueEval valueEval;
			if (arg is RefEval)
			{
				RefEval refEval = (RefEval)arg;
				valueEval = refEval.InnerValueEval;
			}
			else
			{
				valueEval = arg;
			}
			if (valueEval == null)
			{
				throw new ArgumentException("parameter may not be null");
			}
			if (valueEval is AreaEval)
			{
				AreaEval areaEval = (AreaEval)valueEval;
				if (!areaEval.IsColumn || !areaEval.IsRow)
				{
					throw new EvaluationException(ErrorEval.VALUE_INVALID);
				}
				valueEval = areaEval.GetRelativeValue(0, 0);
			}
			if (valueEval == null)
			{
				throw new ArgumentException("Unexpected value eval class (" + valueEval.GetType().Name + ")");
			}
			return GetProductTerm(valueEval, IsScalarProduct: true);
		}

		private ValueEval EvaluateAreaSumProduct(ValueEval[] evalArgs)
		{
			int num = evalArgs.Length;
			AreaEval[] array = new AreaEval[num];
			try
			{
				Array.Copy(evalArgs, 0, array, 0, num);
			}
			catch (Exception)
			{
				return ErrorEval.VALUE_INVALID;
			}
			AreaEval areaEval = array[0];
			int num2 = areaEval.LastRow - areaEval.FirstRow + 1;
			int num3 = areaEval.LastColumn - areaEval.FirstColumn + 1;
			if (!AreasAllSameSize(array, num2, num3))
			{
				for (int i = 1; i < array.Length; i++)
				{
					ThrowFirstError(array[i]);
				}
				return ErrorEval.VALUE_INVALID;
			}
			double num4 = 0.0;
			for (int j = 0; j < num2; j++)
			{
				for (int k = 0; k < num3; k++)
				{
					double num5 = 1.0;
					for (int l = 0; l < num; l++)
					{
						double productTerm = GetProductTerm(array[l].GetRelativeValue(j, k), IsScalarProduct: false);
						num5 *= productTerm;
					}
					num4 += num5;
				}
			}
			return new NumberEval(num4);
		}

		private static void ThrowFirstError(TwoDEval areaEval)
		{
			int height = areaEval.Height;
			int width = areaEval.Width;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					ValueEval value = areaEval.GetValue(i, j);
					if (value is ErrorEval)
					{
						throw new EvaluationException((ErrorEval)value);
					}
				}
			}
		}

		private static bool AreasAllSameSize(TwoDEval[] args, int height, int width)
		{
			foreach (TwoDEval twoDEval in args)
			{
				if (twoDEval.Height != height)
				{
					return false;
				}
				if (twoDEval.Width != width)
				{
					return false;
				}
			}
			return true;
		}

		/// Determines a <c>double</c> value for the specified <c>ValueEval</c>. 
		/// @param IsScalarProduct <c>false</c> for SUMPRODUCTs over area refs.
		/// @throws EvalEx if <c>ve</c> represents an error value.
		/// <p />
		/// Note - string values and empty cells are interpreted differently depending on 
		/// <c>isScalarProduct</c>.  For scalar products, if any term Is blank or a string, the
		/// error (#VALUE!) Is raised.  For area (sum)products, if any term Is blank or a string, the
		/// result Is zero.
		private static double GetProductTerm(ValueEval ve, bool IsScalarProduct)
		{
			if (ve is BlankEval || ve == null)
			{
				if (IsScalarProduct)
				{
					throw new EvaluationException(ErrorEval.VALUE_INVALID);
				}
				return 0.0;
			}
			if (ve is ErrorEval)
			{
				throw new EvaluationException((ErrorEval)ve);
			}
			if (ve is StringEval)
			{
				if (IsScalarProduct)
				{
					throw new EvaluationException(ErrorEval.VALUE_INVALID);
				}
				return 0.0;
			}
			if (ve is NumericValueEval)
			{
				NumericValueEval numericValueEval = (NumericValueEval)ve;
				return numericValueEval.NumberValue;
			}
			throw new RuntimeException("Unexpected value eval class (" + ve.GetType().Name + ")");
		}
	}
}
