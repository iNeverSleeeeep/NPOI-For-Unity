using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public abstract class XYNumericFunction : Fixed2ArgFunction
	{
		private abstract class ValueArray : ValueVector
		{
			private int _size;

			public int Size => _size;

			protected ValueArray(int size)
			{
				_size = size;
			}

			public ValueEval GetItem(int index)
			{
				if (index < 0 || index > _size)
				{
					throw new ArgumentException("Specified index " + index + " is outside range (0.." + (_size - 1) + ")");
				}
				return GetItemInternal(index);
			}

			protected abstract ValueEval GetItemInternal(int index);
		}

		private class SingleCellValueArray : ValueArray
		{
			private ValueEval _value;

			public SingleCellValueArray(ValueEval value)
				: base(1)
			{
				_value = value;
			}

			protected override ValueEval GetItemInternal(int index)
			{
				return _value;
			}
		}

		private class RefValueArray : ValueArray
		{
			private RefEval _ref;

			public RefValueArray(RefEval ref1)
				: base(1)
			{
				_ref = ref1;
			}

			protected override ValueEval GetItemInternal(int index)
			{
				return _ref.InnerValueEval;
			}
		}

		private class AreaValueArray : ValueArray
		{
			private TwoDEval _ae;

			private int _width;

			public AreaValueArray(TwoDEval ae)
				: base(ae.Width * ae.Height)
			{
				_ae = ae;
				_width = ae.Width;
			}

			protected override ValueEval GetItemInternal(int index)
			{
				int rowIndex = index / _width;
				int columnIndex = index % _width;
				return _ae.GetValue(rowIndex, columnIndex);
			}
		}

		protected class DoubleArrayPair
		{
			private double[] _xArray;

			private double[] _yArray;

			public DoubleArrayPair(double[] xArray, double[] yArray)
			{
				_xArray = xArray;
				_yArray = yArray;
			}

			public double[] GetXArray()
			{
				return _xArray;
			}

			public double[] GetYArray()
			{
				return _yArray;
			}
		}

		protected static int X = 0;

		protected static int Y = 1;

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double num;
			try
			{
				ValueVector valueVector = CreateValueVector(arg0);
				ValueVector valueVector2 = CreateValueVector(arg1);
				int size = valueVector.Size;
				if (size == 0 || valueVector2.Size != size)
				{
					return ErrorEval.NA;
				}
				num = EvaluateInternal(valueVector, valueVector2, size);
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

		/// Constructs a new instance of the Accumulator used to calculated this function
		public abstract Accumulator CreateAccumulator();

		private double EvaluateInternal(ValueVector x, ValueVector y, int size)
		{
			Accumulator accumulator = CreateAccumulator();
			ErrorEval errorEval = null;
			ErrorEval errorEval2 = null;
			bool flag = false;
			double num = 0.0;
			for (int i = 0; i < size; i++)
			{
				ValueEval item = x.GetItem(i);
				ValueEval item2 = y.GetItem(i);
				if (item is ErrorEval && errorEval == null)
				{
					errorEval = (ErrorEval)item;
				}
				else if (item2 is ErrorEval && errorEval2 == null)
				{
					errorEval2 = (ErrorEval)item2;
				}
				else if (item is NumberEval && item2 is NumberEval)
				{
					flag = true;
					NumberEval numberEval = (NumberEval)item;
					NumberEval numberEval2 = (NumberEval)item2;
					num += accumulator.Accumulate(numberEval.NumberValue, numberEval2.NumberValue);
				}
			}
			if (errorEval != null)
			{
				throw new EvaluationException(errorEval);
			}
			if (errorEval2 != null)
			{
				throw new EvaluationException(errorEval2);
			}
			if (!flag)
			{
				throw new EvaluationException(ErrorEval.DIV_ZERO);
			}
			return num;
		}

		private static double[] TrimToSize(double[] arr, int len)
		{
			double[] array = arr;
			if (arr.Length > len)
			{
				array = new double[len];
				Array.Copy(arr, 0, array, 0, len);
			}
			return array;
		}

		private static bool IsNumberEval(NPOI.SS.Formula.Eval.Eval eval)
		{
			bool result = false;
			if (eval is NumberEval)
			{
				result = true;
			}
			else if (eval is RefEval)
			{
				RefEval refEval = (RefEval)eval;
				ValueEval innerValueEval = refEval.InnerValueEval;
				result = (innerValueEval is NumberEval);
			}
			return result;
		}

		private static double GetDoubleValue(NPOI.SS.Formula.Eval.Eval eval)
		{
			double result = 0.0;
			if (eval is NumberEval)
			{
				NumberEval numberEval = (NumberEval)eval;
				result = numberEval.NumberValue;
			}
			else if (eval is RefEval)
			{
				RefEval refEval = (RefEval)eval;
				ValueEval innerValueEval = refEval.InnerValueEval;
				result = ((innerValueEval is NumberEval) ? ((NumberEval)innerValueEval).NumberValue : double.NaN);
			}
			else if (eval is ErrorEval)
			{
				result = double.NaN;
			}
			return result;
		}

		private static ValueVector CreateValueVector(ValueEval arg)
		{
			if (arg is ErrorEval)
			{
				throw new EvaluationException((ErrorEval)arg);
			}
			if (arg is TwoDEval)
			{
				return new AreaValueArray((TwoDEval)arg);
			}
			if (arg is RefEval)
			{
				return new RefValueArray((RefEval)arg);
			}
			return new SingleCellValueArray(arg);
		}
	}
}
