using System.Collections;
using System.Collections.Generic;

namespace NPOI.SS.UserModel
{
	public interface ICellRange<T> : IEnumerable<T>, IEnumerable where T : ICell
	{
		int Width
		{
			get;
		}

		int Height
		{
			get;
		}

		int Size
		{
			get;
		}

		string ReferenceText
		{
			get;
		}

		T TopLeftCell
		{
			get;
		}

		T[] FlattenedCells
		{
			get;
		}

		T[][] Cells
		{
			get;
		}

		T GetCell(int relativeRowIndex, int relativeColumnIndex);
	}
}
