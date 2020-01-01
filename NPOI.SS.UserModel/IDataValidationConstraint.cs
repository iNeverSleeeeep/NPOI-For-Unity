namespace NPOI.SS.UserModel
{
	public interface IDataValidationConstraint
	{
		/// @return the operator used for this constraint
		/// @see OperatorType
		/// <summary>
		/// get or set then comparison operator for this constraint
		/// </summary>
		int Operator
		{
			get;
			set;
		}

		string[] ExplicitListValues
		{
			get;
			set;
		}

		/// <summary>
		/// get or set the formula for expression 1. May be <code>null</code>
		/// </summary>
		string Formula1
		{
			get;
			set;
		}

		/// <summary>
		/// get or set the formula for expression 2. May be <code>null</code>
		/// </summary>
		string Formula2
		{
			get;
			set;
		}

		/// @return data validation type of this constraint
		/// @see ValidationType
		int GetValidationType();
	}
}
