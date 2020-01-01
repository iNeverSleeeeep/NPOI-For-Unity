using System;
using System.Collections;
using System.Text;

namespace NPOI.SS.Util
{
	public class AreaReference
	{
		/// The Char (!) that Separates sheet names from cell references 
		private const char SHEET_NAME_DELIMITER = '!';

		/// The Char (:) that Separates the two cell references in a multi-cell area reference 
		private const char CELL_DELIMITER = ':';

		/// The Char (') used to quote sheet names when they contain special Chars 
		private const char SPECIAL_NAME_DELIMITER = '\'';

		private CellReference _firstCell;

		private CellReference _lastCell;

		private bool _isSingleCell;

		/// @return <c>false</c> if this area reference involves more than one cell
		public bool IsSingleCell => _isSingleCell;

		/// @return the first cell reference which defines this area. Usually this cell is in the upper
		/// left corner of the area (but this is not a requirement).
		public CellReference FirstCell => _firstCell;

		/// Note - if this area reference refers to a single cell, the return value of this method will
		/// be identical to that of <c>GetFirstCell()</c>
		/// @return the second cell reference which defines this area.  For multi-cell areas, this is 
		/// cell diagonally opposite the 'first cell'.  Usually this cell is in the lower right corner 
		/// of the area (but this is not a requirement).
		public CellReference LastCell => _lastCell;

		/// Create an area ref from a string representation.  Sheet names containing special Chars should be
		/// delimited and escaped as per normal syntax rules for formulas.<br /> 
		/// The area reference must be contiguous (i.e. represent a single rectangle, not a Union of rectangles)
		public AreaReference(string reference)
		{
			if (!IsContiguous(reference))
			{
				throw new ArgumentException("References passed to the AreaReference must be contiguous, use generateContiguous(ref) if you have non-contiguous references");
			}
			string[] array = SeparateAreaRefs(reference);
			string text = array[0];
			if (array.Length == 1)
			{
				_firstCell = new CellReference(text);
				_lastCell = _firstCell;
				_isSingleCell = true;
			}
			else
			{
				if (array.Length != 2)
				{
					throw new ArgumentException("Bad area ref '" + reference + "'");
				}
				string text2 = array[1];
				if (IsPlainColumn(text))
				{
					if (!IsPlainColumn(text2))
					{
						throw new Exception("Bad area ref '" + reference + "'");
					}
					bool pAbsCol = CellReference.IsPartAbsolute(text);
					bool pAbsCol2 = CellReference.IsPartAbsolute(text2);
					int pCol = CellReference.ConvertColStringToIndex(text);
					int pCol2 = CellReference.ConvertColStringToIndex(text2);
					_firstCell = new CellReference(0, pCol, pAbsRow: true, pAbsCol);
					_lastCell = new CellReference(65535, pCol2, pAbsRow: true, pAbsCol2);
					_isSingleCell = false;
				}
				else
				{
					_firstCell = new CellReference(text);
					_lastCell = new CellReference(text2);
					_isSingleCell = text.Equals(text2);
				}
			}
		}

		private bool IsPlainColumn(string refPart)
		{
			for (int num = refPart.Length - 1; num >= 0; num--)
			{
				int num2 = refPart[num];
				if ((num2 != 36 || num != 0) && (num2 < 65 || num2 > 90))
				{
					return false;
				}
			}
			return true;
		}

		public static AreaReference GetWholeRow(string start, string end)
		{
			return new AreaReference("$A" + start + ":$IV" + end);
		}

		public static AreaReference GetWholeColumn(string start, string end)
		{
			return new AreaReference(start + "$1:" + end + "$65536");
		}

		/// Creates an area ref from a pair of Cell References.
		public AreaReference(CellReference topLeft, CellReference botRight)
		{
			bool flag = topLeft.Row > botRight.Row;
			bool flag2 = topLeft.Col > botRight.Col;
			if (flag || flag2)
			{
				int row;
				bool isRowAbsolute;
				int row2;
				bool isRowAbsolute2;
				if (flag)
				{
					row = botRight.Row;
					isRowAbsolute = botRight.IsRowAbsolute;
					row2 = topLeft.Row;
					isRowAbsolute2 = topLeft.IsRowAbsolute;
				}
				else
				{
					row = topLeft.Row;
					isRowAbsolute = topLeft.IsRowAbsolute;
					row2 = botRight.Row;
					isRowAbsolute2 = botRight.IsRowAbsolute;
				}
				int col;
				bool isColAbsolute;
				int col2;
				bool isColAbsolute2;
				if (flag2)
				{
					col = botRight.Col;
					isColAbsolute = botRight.IsColAbsolute;
					col2 = topLeft.Col;
					isColAbsolute2 = topLeft.IsColAbsolute;
				}
				else
				{
					col = topLeft.Col;
					isColAbsolute = topLeft.IsColAbsolute;
					col2 = botRight.Col;
					isColAbsolute2 = botRight.IsColAbsolute;
				}
				_firstCell = new CellReference(row, col, isRowAbsolute, isColAbsolute);
				_lastCell = new CellReference(row2, col2, isRowAbsolute2, isColAbsolute2);
			}
			else
			{
				_firstCell = topLeft;
				_lastCell = botRight;
			}
			_isSingleCell = false;
		}

		/// is the reference for a contiguous (i.e.
		///  Unbroken) area, or is it made up of
		///  several different parts?
		/// (If it Is, you will need to call
		///  ....
		public static bool IsContiguous(string reference)
		{
			int num = reference.IndexOf('!');
			if (num != -1)
			{
				reference = reference.Substring(num);
			}
			if (reference.IndexOf(',') == -1)
			{
				return true;
			}
			return false;
		}

		/// is the reference for a whole-column reference,
		///  such as C:C or D:G ?
		public static bool IsWholeColumnReference(CellReference topLeft, CellReference botRight)
		{
			if (topLeft.Row == 0 && topLeft.IsRowAbsolute && (botRight.Row == -1 || botRight.Row == 65535) && botRight.IsRowAbsolute)
			{
				return true;
			}
			return false;
		}

		public bool IsWholeColumnReference()
		{
			return IsWholeColumnReference(_firstCell, _lastCell);
		}

		/// Takes a non-contiguous area reference, and
		///  returns an array of contiguous area references.
		public static AreaReference[] GenerateContiguous(string reference)
		{
			ArrayList arrayList = new ArrayList();
			string[] array = reference.Split(',');
			string[] array2 = array;
			foreach (string reference2 in array2)
			{
				arrayList.Add(new AreaReference(reference2));
			}
			return (AreaReference[])arrayList.ToArray(typeof(AreaReference));
		}

		/// Returns a reference to every cell covered by this area
		public CellReference[] GetAllReferencedCells()
		{
			if (_isSingleCell)
			{
				return new CellReference[1]
				{
					_firstCell
				};
			}
			int num = Math.Min(_firstCell.Row, _lastCell.Row);
			int num2 = Math.Max(_firstCell.Row, _lastCell.Row);
			int num3 = Math.Min(_firstCell.Col, _lastCell.Col);
			int num4 = Math.Max(_firstCell.Col, _lastCell.Col);
			string sheetName = _firstCell.SheetName;
			ArrayList arrayList = new ArrayList();
			for (int i = num; i <= num2; i++)
			{
				for (int j = num3; j <= num4; j++)
				{
					CellReference value = new CellReference(sheetName, i, j, _firstCell.IsRowAbsolute, _firstCell.IsColAbsolute);
					arrayList.Add(value);
				}
			}
			return (CellReference[])arrayList.ToArray(typeof(CellReference));
		}

		/// Example return values:
		///   <table border="0" cellpAdding="1" cellspacing="0" summary="Example return values">
		///     <tr><th align="left">Result</th><th align="left">Comment</th></tr>
		///     <tr><td>A1:A1</td><td>Single cell area reference without sheet</td></tr>
		///     <tr><td>A1:$C$1</td><td>Multi-cell area reference without sheet</td></tr>
		///     <tr><td>Sheet1!A$1:B4</td><td>Standard sheet name</td></tr>
		///     <tr><td>'O''Brien''s Sales'!B5:C6' </td><td>Sheet name with special Chars</td></tr>
		///   </table>
		///             @return the text representation of this area reference as it would appear in a formula.
		public string FormatAsString()
		{
			if (IsWholeColumnReference())
			{
				return CellReference.ConvertNumToColString(_firstCell.Col) + ":" + CellReference.ConvertNumToColString(_lastCell.Col);
			}
			StringBuilder stringBuilder = new StringBuilder(32);
			stringBuilder.Append(_firstCell.FormatAsString());
			if (!_isSingleCell)
			{
				stringBuilder.Append(':');
				if (_lastCell.SheetName == null)
				{
					stringBuilder.Append(_lastCell.FormatAsString());
				}
				else
				{
					_lastCell.AppendCellReference(stringBuilder);
				}
			}
			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(FormatAsString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		/// Separates Area refs in two parts and returns them as Separate elements in a String array,
		/// each qualified with the sheet name (if present)
		///
		/// @return array with one or two elements. never <c>null</c>
		private static string[] SeparateAreaRefs(string reference)
		{
			int length = reference.Length;
			int num = -1;
			bool flag = false;
			for (int i = 0; i < length; i++)
			{
				switch (reference[i])
				{
				case ':':
					if (!flag)
					{
						if (num >= 0)
						{
							throw new ArgumentException("More than one cell delimiter '" + ':' + "' appears in area reference '" + reference + "'");
						}
						num = i;
					}
					break;
				case '\'':
					if (!flag)
					{
						flag = true;
					}
					else
					{
						if (i >= length - 1)
						{
							throw new ArgumentException("Area reference '" + reference + "' ends with special name delimiter '" + '\'' + "'");
						}
						if (reference[i + 1] == '\'')
						{
							i++;
						}
						else
						{
							flag = false;
						}
					}
					break;
				}
			}
			if (num < 0)
			{
				return new string[1]
				{
					reference
				};
			}
			string text = reference.Substring(0, num);
			string text2 = reference.Substring(num + 1);
			if (text2.IndexOf('!') >= 0)
			{
				throw new Exception("Unexpected " + '!' + " in second cell reference of '" + reference + "'");
			}
			int num2 = text.LastIndexOf('!');
			if (num2 < 0)
			{
				return new string[2]
				{
					text,
					text2
				};
			}
			string str = text.Substring(0, num2 + 1);
			return new string[2]
			{
				text,
				str + text2
			};
		}
	}
}
