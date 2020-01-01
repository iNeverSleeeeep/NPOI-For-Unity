using System;
using System.Collections.Generic;
using System.Globalization;

namespace NPOI.SS.Format
{
	/// This object represents a condition in a cell format.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public abstract class CellFormatCondition
	{
		private class LT_CellFormatCondition : CellFormatCondition
		{
			private double _c;

			public LT_CellFormatCondition(double c)
			{
				_c = c;
			}

			public override bool Pass(double value)
			{
				return value < _c;
			}
		}

		private class LE_CellFormatCondition : CellFormatCondition
		{
			private double _c;

			public LE_CellFormatCondition(double c)
			{
				_c = c;
			}

			public override bool Pass(double value)
			{
				return value <= _c;
			}
		}

		private class GT_CellFormatCondition : CellFormatCondition
		{
			private double _c;

			public GT_CellFormatCondition(double c)
			{
				_c = c;
			}

			public override bool Pass(double value)
			{
				return value > _c;
			}
		}

		private class GE_CellFormatCondition : CellFormatCondition
		{
			private double _c;

			public GE_CellFormatCondition(double c)
			{
				_c = c;
			}

			public override bool Pass(double value)
			{
				return value >= _c;
			}
		}

		private class EQ_CellFormatCondition : CellFormatCondition
		{
			private double _c;

			public EQ_CellFormatCondition(double c)
			{
				_c = c;
			}

			public override bool Pass(double value)
			{
				return value == _c;
			}
		}

		private class NE_CellFormatCondition : CellFormatCondition
		{
			private double _c;

			public NE_CellFormatCondition(double c)
			{
				_c = c;
			}

			public override bool Pass(double value)
			{
				return value != _c;
			}
		}

		private const int LT = 0;

		private const int LE = 1;

		private const int GT = 2;

		private const int GE = 3;

		private const int EQ = 4;

		private const int NE = 5;

		private static Dictionary<string, int> TESTS;

		static CellFormatCondition()
		{
			TESTS = new Dictionary<string, int>();
			TESTS.Add("<", 0);
			TESTS.Add("<=", 1);
			TESTS.Add(">", 2);
			TESTS.Add(">=", 3);
			TESTS.Add("=", 4);
			TESTS.Add("==", 4);
			TESTS.Add("!=", 5);
			TESTS.Add("<>", 5);
		}

		/// Returns an instance of a condition object.
		///
		/// @param opString The operator as a string.  One of <tt>"&lt;"</tt>,
		///                 <tt>"&lt;="</tt>, <tt>"&gt;"</tt>, <tt>"&gt;="</tt>,
		///                 <tt>"="</tt>, <tt>"=="</tt>, <tt>"!="</tt>, or
		///                 <tt>"&lt;&gt;"</tt>.
		/// @param constStr The constant (such as <tt>"12"</tt>).
		///
		/// @return A condition object for the given condition.
		public static CellFormatCondition GetInstance(string opString, string constStr)
		{
			if (TESTS.ContainsKey(opString))
			{
				int num = TESTS[opString];
				double c = double.Parse(constStr, CultureInfo.InvariantCulture);
				switch (num)
				{
				case 0:
					return new LT_CellFormatCondition(c);
				case 1:
					return new LE_CellFormatCondition(c);
				case 2:
					return new GT_CellFormatCondition(c);
				case 3:
					return new GE_CellFormatCondition(c);
				case 4:
					return new EQ_CellFormatCondition(c);
				case 5:
					return new NE_CellFormatCondition(c);
				default:
					throw new ArgumentException("Cannot create for test number " + num + "(\"" + opString + "\")");
				}
			}
			throw new ArgumentException("Unknown test: " + opString);
		}

		/// Returns <tt>true</tt> if the given value passes the constraint's test.
		///
		/// @param value The value to compare against.
		///
		/// @return <tt>true</tt> if the given value passes the constraint's test.
		public abstract bool Pass(double value);
	}
}
