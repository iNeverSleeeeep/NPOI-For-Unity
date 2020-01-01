using System.Text;

namespace NPOI.SS.Formula.Functions
{
	/// Enumeration to support <b>4</b> valued comparison results.<p />
	/// Excel lookup functions have complex behaviour in the case where the lookup array has mixed 
	/// types, and/or Is Unordered.  Contrary to suggestions in some Excel documentation, there
	/// does not appear to be a Universal ordering across types.  The binary search algorithm used
	/// Changes behaviour when the Evaluated 'mid' value has a different type to the lookup value.<p />
	///
	/// A simple int might have done the same job, but there Is risk in confusion with the well 
	/// known <c>Comparable.CompareTo()</c> and <c>Comparator.Compare()</c> which both use
	/// a ubiquitous 3 value result encoding.
	public class CompareResult
	{
		private bool _isTypeMismatch;

		private bool _isLessThan;

		private bool _isEqual;

		private bool _isGreaterThan;

		public static readonly CompareResult TypeMismatch = new CompareResult(IsTypeMismatch: true, 0);

		public static readonly CompareResult LessThan = new CompareResult(IsTypeMismatch: false, -1);

		public static readonly CompareResult Equal = new CompareResult(IsTypeMismatch: false, 0);

		public static readonly CompareResult GreaterThan = new CompareResult(IsTypeMismatch: false, 1);

		public bool IsTypeMismatch => _isTypeMismatch;

		public bool IsLessThan => _isLessThan;

		public bool IsEqual => _isEqual;

		public bool IsGreaterThan => _isGreaterThan;

		private string FormatAsString
		{
			get
			{
				if (_isTypeMismatch)
				{
					return "TYPE_MISMATCH";
				}
				if (_isLessThan)
				{
					return "LESS_THAN";
				}
				if (_isEqual)
				{
					return "EQUAL";
				}
				if (_isGreaterThan)
				{
					return "GREATER_THAN";
				}
				return "error";
			}
		}

		private CompareResult(bool IsTypeMismatch, int simpleCompareResult)
		{
			if (IsTypeMismatch)
			{
				_isTypeMismatch = true;
				_isLessThan = false;
				_isEqual = false;
				_isGreaterThan = false;
			}
			else
			{
				_isTypeMismatch = false;
				_isLessThan = (simpleCompareResult < 0);
				_isEqual = (simpleCompareResult == 0);
				_isGreaterThan = (simpleCompareResult > 0);
			}
		}

		public static CompareResult ValueOf(int simpleCompareResult)
		{
			if (simpleCompareResult < 0)
			{
				return LessThan;
			}
			if (simpleCompareResult > 0)
			{
				return GreaterThan;
			}
			return Equal;
		}

		public static CompareResult ValueOf(bool matches)
		{
			if (matches)
			{
				return Equal;
			}
			return LessThan;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(FormatAsString);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
