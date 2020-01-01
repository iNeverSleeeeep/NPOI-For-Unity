using NPOI.SS.UserModel;

namespace NPOI.SS.Formula
{
	/// Abstracts a cell for the purpose of formula evaluation.  This interface represents both formula
	/// and non-formula cells.<br />
	///
	/// Implementors of this class must implement {@link #HashCode()} and {@link #Equals(Object)}
	/// To provide an <em>identity</em> relationship based on the underlying HSSF or XSSF cell <p />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public interface IEvaluationCell
	{
		IEvaluationSheet Sheet
		{
			get;
		}

		int RowIndex
		{
			get;
		}

		int ColumnIndex
		{
			get;
		}

		CellType CellType
		{
			get;
		}

		double NumericCellValue
		{
			get;
		}

		string StringCellValue
		{
			get;
		}

		bool BooleanCellValue
		{
			get;
		}

		int ErrorCellValue
		{
			get;
		}

		object IdentityKey
		{
			get;
		}

		CellType CachedFormulaResultType
		{
			get;
		}
	}
}
