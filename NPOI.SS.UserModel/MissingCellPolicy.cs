namespace NPOI.SS.UserModel
{
	/// <summary>
	/// Used to specify the different possible policies
	/// if for the case of null and blank cells
	/// </summary>    
	public class MissingCellPolicy
	{
		private static int NEXT_ID = 1;

		public int id;

		/// <summary>Missing cells are returned as null, Blank cells are returned as normal</summary>
		public static readonly MissingCellPolicy RETURN_NULL_AND_BLANK = new MissingCellPolicy();

		/// <summary>Missing cells are returned as null, as are blank cells</summary>
		public static readonly MissingCellPolicy RETURN_BLANK_AS_NULL = new MissingCellPolicy();

		/// <summary>A new, blank cell is Created for missing cells. Blank cells are returned as normal</summary>
		public static readonly MissingCellPolicy CREATE_NULL_AS_BLANK = new MissingCellPolicy();

		public MissingCellPolicy()
		{
			id = NEXT_ID++;
		}
	}
}
