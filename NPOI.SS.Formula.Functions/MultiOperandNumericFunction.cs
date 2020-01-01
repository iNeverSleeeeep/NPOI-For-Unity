using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// This Is the base class for all excel function evaluator
	/// classes that take variable number of operands, and
	/// where the order of operands does not matter
	public abstract class MultiOperandNumericFunction : Function
	{
		private class DoubleList
		{
			private double[] _array;

			private int _Count;

			public DoubleList()
			{
				_array = new double[8];
				_Count = 0;
			}

			public double[] ToArray()
			{
				if (_Count < 1)
				{
					return EMPTY_DOUBLE_ARRAY;
				}
				double[] array = new double[_Count];
				Array.Copy(_array, 0, array, 0, _Count);
				return array;
			}

			public void Add(double[] values)
			{
				int num = values.Length;
				EnsureCapacity(_Count + num);
				Array.Copy(values, 0, _array, _Count, num);
				_Count += num;
			}

			private void EnsureCapacity(int reqSize)
			{
				if (reqSize > _array.Length)
				{
					int num = reqSize * 3 / 2;
					double[] array = new double[num];
					Array.Copy(_array, 0, array, 0, _Count);
					_array = array;
				}
			}

			public void Add(double value)
			{
				EnsureCapacity(_Count + 1);
				_array[_Count] = value;
				_Count++;
			}
		}

		private const int DEFAULT_MAX_NUM_OPERANDS = 30;

		private static double[] EMPTY_DOUBLE_ARRAY = new double[0];

		private bool _isReferenceBoolCounted;

		private bool _isBlankCounted;

		/// Maximum number of operands accepted by this function.
		/// Subclasses may override to Change default value.
		protected int MaxNumOperands => 30;

		/// Whether to count nested subtotals.
		public virtual bool IsSubtotalCounted => true;

		protected MultiOperandNumericFunction(bool isReferenceBoolCounted, bool isBlankCounted)
		{
			_isReferenceBoolCounted = isReferenceBoolCounted;
			_isBlankCounted = isBlankCounted;
		}

		protected internal abstract double Evaluate(double[] values);

		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			double num;
			try
			{
				double[] numberArray = GetNumberArray(args);
				num = Evaluate(numberArray);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			if (double.IsNaN(num) || double.IsInfinity(num))
			{
				return ErrorEval.NUM_ERROR;
			}
			return new NumberEval(num);
		}

		/// Collects values from a single argument
		private void CollectValues(ValueEval operand, DoubleList temp)
		{
			if (operand is TwoDEval)
			{
				TwoDEval twoDEval = (TwoDEval)operand;
				int width = twoDEval.Width;
				int height = twoDEval.Height;
				for (int i = 0; i < height; i++)
				{
					for (int j = 0; j < width; j++)
					{
						ValueEval value = twoDEval.GetValue(i, j);
						if (IsSubtotalCounted || !twoDEval.IsSubTotal(i, j))
						{
							CollectValue(value, isViaReference: true, temp);
						}
					}
				}
			}
			else if (operand is RefEval)
			{
				RefEval refEval = (RefEval)operand;
				CollectValue(refEval.InnerValueEval, isViaReference: true, temp);
			}
			else
			{
				CollectValue(operand, isViaReference: false, temp);
			}
		}

		private void CollectValue(ValueEval ve, bool isViaReference, DoubleList temp)
		{
			if (ve == null)
			{
				throw new ArgumentException("ve must not be null");
			}
			if (ve is NumberEval)
			{
				NumberEval numberEval = (NumberEval)ve;
				temp.Add(numberEval.NumberValue);
			}
			else
			{
				if (ve is ErrorEval)
				{
					throw new EvaluationException((ErrorEval)ve);
				}
				if (ve is StringEval)
				{
					if (!isViaReference)
					{
						string stringValue = ((StringEval)ve).StringValue;
						double num = OperandResolver.ParseDouble(stringValue);
						if (double.IsNaN(num))
						{
							throw new EvaluationException(ErrorEval.VALUE_INVALID);
						}
						temp.Add(num);
					}
				}
				else if (ve is BoolEval)
				{
					if (!isViaReference || _isReferenceBoolCounted)
					{
						BoolEval boolEval = (BoolEval)ve;
						temp.Add(boolEval.NumberValue);
					}
				}
				else
				{
					if (ve != BlankEval.instance)
					{
						throw new InvalidOperationException("Invalid ValueEval type passed for conversion: (" + ve.GetType() + ")");
					}
					if (_isBlankCounted)
					{
						temp.Add(0.0);
					}
				}
			}
		}

		/// Returns a double array that contains values for the numeric cells
		/// from among the list of operands. Blanks and Blank equivalent cells
		/// are ignored. Error operands or cells containing operands of type
		/// that are considered invalid and would result in #VALUE! error in
		/// excel cause this function to return <c>null</c>.
		///
		/// @return never <c>null</c>
		protected double[] GetNumberArray(ValueEval[] operands)
		{
			if (operands.Length > MaxNumOperands)
			{
				throw EvaluationException.InvalidValue();
			}
			DoubleList doubleList = new DoubleList();
			int i = 0;
			for (int num = operands.Length; i < num; i++)
			{
				CollectValues(operands[i], doubleList);
			}
			return doubleList.ToArray();
		}

		/// Ensures that a two dimensional array has all sub-arrays present and the same Length
		/// @return <c>false</c> if any sub-array Is missing, or Is of different Length
		protected static bool AreSubArraysConsistent(double[][] values)
		{
			if (values == null || values.Length < 1)
			{
				return true;
			}
			if (values[0] == null)
			{
				return false;
			}
			int num = values.Length;
			int num2 = values[0].Length;
			for (int i = 1; i < num; i++)
			{
				double[] array = values[i];
				if (array == null)
				{
					return false;
				}
				if (num2 != array.Length)
				{
					return false;
				}
			}
			return true;
		}
	}
}
