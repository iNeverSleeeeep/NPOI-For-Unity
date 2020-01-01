using NPOI.SS.Formula.Eval;
using System;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	internal abstract class LookupValueComparerBase : LookupValueComparer
	{
		private Type _targetType;

		protected LookupValueComparerBase(ValueEval targetValue)
		{
			if (targetValue == null)
			{
				throw new Exception("targetValue cannot be null");
			}
			_targetType = targetValue.GetType();
		}

		public CompareResult CompareTo(ValueEval other)
		{
			if (other == null)
			{
				throw new Exception("Compare to value cannot be null");
			}
			if (_targetType != other.GetType())
			{
				return CompareResult.TypeMismatch;
			}
			return CompareSameType(other);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(GetValueAsString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		protected abstract CompareResult CompareSameType(ValueEval other);

		/// used only for debug purposes 
		protected abstract string GetValueAsString();
	}
}
