using NPOI.SS.Util;
using System;

namespace NPOI.SS.UserModel.Charts
{
	public class DataSources
	{
		private abstract class AbstractCellRangeDataSource<T> : IChartDataSource<T>
		{
			private ISheet sheet;

			private CellRangeAddress cellRangeAddress;

			private int numOfCells;

			private IFormulaEvaluator evaluator;

			public int PointCount => numOfCells;

			public bool IsReference => true;

			public abstract bool IsNumeric
			{
				get;
			}

			public string FormulaString => cellRangeAddress.FormatAsString(sheet.SheetName, useAbsoluteAddress: true);

			protected AbstractCellRangeDataSource(ISheet sheet, CellRangeAddress cellRangeAddress)
			{
				this.sheet = sheet;
				this.cellRangeAddress = cellRangeAddress.Copy();
				numOfCells = this.cellRangeAddress.NumberOfCells;
				evaluator = sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
			}

			public abstract T GetPointAt(int index);

			protected CellValue GetCellValueAt(int index)
			{
				if (index < 0 || index >= numOfCells)
				{
					throw new IndexOutOfRangeException("Index must be between 0 and " + (numOfCells - 1) + " (inclusive), given: " + index);
				}
				int firstRow = cellRangeAddress.FirstRow;
				int firstColumn = cellRangeAddress.FirstColumn;
				int lastColumn = cellRangeAddress.LastColumn;
				int num = lastColumn - firstColumn + 1;
				int rownum = firstRow + index / num;
				int cellnum = firstColumn + index % num;
				IRow row = sheet.GetRow(rownum);
				if (row != null)
				{
					return evaluator.Evaluate(row.GetCell(cellnum));
				}
				return null;
			}
		}

		private class DoubleCellRangeDataSource : AbstractCellRangeDataSource<double>
		{
			public override bool IsNumeric => true;

			public DoubleCellRangeDataSource(ISheet sheet, CellRangeAddress cellRangeAddress)
				: base(sheet, cellRangeAddress)
			{
			}

			public override double GetPointAt(int index)
			{
				CellValue cellValueAt = GetCellValueAt(index);
				if (cellValueAt != null && cellValueAt.CellType == CellType.Numeric)
				{
					return cellValueAt.NumberValue;
				}
				return double.NaN;
			}
		}

		private class StringCellRangeDataSource : AbstractCellRangeDataSource<string>
		{
			public override bool IsNumeric => false;

			public StringCellRangeDataSource(ISheet sheet, CellRangeAddress cellRangeAddress)
				: base(sheet, cellRangeAddress)
			{
			}

			public override string GetPointAt(int index)
			{
				CellValue cellValueAt = GetCellValueAt(index);
				if (cellValueAt != null && cellValueAt.CellType == CellType.String)
				{
					return cellValueAt.StringValue;
				}
				return null;
			}
		}

		private class ArrayDataSource<T> : IChartDataSource<T>
		{
			private T[] elements;

			public int PointCount => elements.Length;

			public bool IsReference => false;

			public bool IsNumeric
			{
				get
				{
					Type typeFromHandle = typeof(double);
					T[] array = elements;
					foreach (object o in array)
					{
						if (!typeFromHandle.IsInstanceOfType(o))
						{
							return false;
						}
					}
					return true;
				}
			}

			public string FormulaString
			{
				get
				{
					throw new InvalidOperationException("Literal data source can not be expressed by reference.");
				}
			}

			public ArrayDataSource(T[] elements)
			{
				this.elements = elements;
			}

			public T GetPointAt(int index)
			{
				return elements[index];
			}
		}

		private DataSources()
		{
		}

		public static IChartDataSource<T> FromArray<T>(T[] elements)
		{
			return new ArrayDataSource<T>(elements);
		}

		public static IChartDataSource<double> FromNumericCellRange(ISheet sheet, CellRangeAddress cellRangeAddress)
		{
			return new DoubleCellRangeDataSource(sheet, cellRangeAddress);
		}

		public static IChartDataSource<string> FromStringCellRange(ISheet sheet, CellRangeAddress cellRangeAddress)
		{
			return new StringCellRangeDataSource(sheet, cellRangeAddress);
		}
	}
}
