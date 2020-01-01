namespace NPOI.SS.UserModel
{
	/// ValidationType enum
	public static class ValidationType
	{
		/// 'Any value' type - value not restricted 
		public const int ANY = 0;

		/// int ('Whole number') type 
		public const int INTEGER = 1;

		/// Decimal type 
		public const int DECIMAL = 2;

		/// List type ( combo box type ) 
		public const int LIST = 3;

		/// Date type 
		public const int DATE = 4;

		/// Time type 
		public const int TIME = 5;

		/// String length type 
		public const int TEXT_LENGTH = 6;

		/// Formula ( 'Custom' ) type 
		public const int FORMULA = 7;
	}
}
