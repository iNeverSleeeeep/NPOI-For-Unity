using System;

namespace NPOI.SS.UserModel
{
	/// Condition operator enum
	public static class OperatorType
	{
		public const int BETWEEN = 0;

		public const int NOT_BETWEEN = 1;

		public const int EQUAL = 2;

		public const int NOT_EQUAL = 3;

		public const int GREATER_THAN = 4;

		public const int LESS_THAN = 5;

		public const int GREATER_OR_EQUAL = 6;

		public const int LESS_OR_EQUAL = 7;

		/// default value to supply when the operator type is not used 
		public const int IGNORED = 0;

		public static void ValidateSecondArg(int comparisonOperator, string paramValue)
		{
			switch (comparisonOperator)
			{
			case 0:
				if (paramValue == null)
				{
					throw new ArgumentException("expr2 must be supplied for 'between' comparisons");
				}
				break;
			case 1:
				if (paramValue == null)
				{
					throw new ArgumentException("expr2 must be supplied for 'between' comparisons");
				}
				break;
			}
		}
	}
}
