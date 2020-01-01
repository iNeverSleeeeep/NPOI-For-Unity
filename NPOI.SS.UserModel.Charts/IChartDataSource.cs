namespace NPOI.SS.UserModel.Charts
{
	public interface IChartDataSource<T>
	{
		/// Return number of points contained by data source.
		///
		/// @return number of points contained by data source
		int PointCount
		{
			get;
		}

		/// Returns {@code true} if charts data source is valid cell range.
		///
		/// @return {@code true} if charts data source is valid cell range
		bool IsReference
		{
			get;
		}

		/// Returns {@code true} if data source points should be treated as numbers.
		///
		/// @return {@code true} if data source points should be treated as numbers
		bool IsNumeric
		{
			get;
		}

		/// Returns formula representation of the data source. It is only applicable
		/// for data source that is valid cell range.
		///
		/// @return formula representation of the data source
		/// @throws {@code UnsupportedOperationException} if the data source is not a
		///                reference.
		string FormulaString
		{
			get;
		}

		/// Returns point value at specified index.
		///
		/// @param index index to value from
		/// @return point value at specified index.
		/// @throws {@code IndexOutOfBoundsException} if index
		///                parameter not in range {@code 0 &lt;= index &lt;= pointCount}
		T GetPointAt(int index);
	}
}
