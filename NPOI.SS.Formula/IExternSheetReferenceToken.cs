namespace NPOI.SS.Formula
{
	/// Should be implemented by any {@link Ptg} subclass that needs Has an extern sheet index <br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public interface IExternSheetReferenceToken
	{
		int ExternSheetIndex
		{
			get;
		}
	}
}
