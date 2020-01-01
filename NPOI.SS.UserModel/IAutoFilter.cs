namespace NPOI.SS.UserModel
{
	/// Represents autofiltering for the specified worksheet.
	///
	/// <p>
	/// Filtering data is a quick and easy way to find and work with a subset of data in a range of cells or table.
	/// For example, you can filter to see only the values that you specify, filter to see the top or bottom values,
	/// or filter to quickly see duplicate values.
	/// </p>
	///
	/// TODO YK: For now (Aug 2010) POI only supports Setting a basic autofilter on a range of cells.
	/// In future, when we support more auto-filter functions like custom criteria, sort, etc. we will add
	/// corresponding methods to this interface.
	public interface IAutoFilter
	{
	}
}
