namespace NPOI.SS.Util
{
	public enum NameType
	{
		/// <summary>
		/// Allow accessing the Initial value.
		/// </summary>
		None = 0,
		Cell = 1,
		NamedRange = 2,
		Column = 3,
		Row = 4,
		BadCellOrNamedRange = -1
	}
}
