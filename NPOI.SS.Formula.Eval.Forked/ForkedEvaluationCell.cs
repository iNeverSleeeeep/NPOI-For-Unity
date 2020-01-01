using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Eval.Forked
{
	/// Represents a cell being used for forked Evaluation that has had a value Set different from the
	/// corresponding cell in the shared master workbook.
	///
	/// @author Josh Micich
	internal class ForkedEvaluationCell : IEvaluationCell
	{
		private IEvaluationSheet _sheet;

		/// corresponding cell from master workbook 
		private IEvaluationCell _masterCell;

		private bool _boolValue;

		private CellType _cellType;

		private int _errorValue;

		private double _numberValue;

		private string _stringValue;

		public object IdentityKey => _masterCell.IdentityKey;

		public CellType CellType => _cellType;

		public bool BooleanCellValue
		{
			get
			{
				CheckCellType(CellType.Boolean);
				return _boolValue;
			}
		}

		public int ErrorCellValue
		{
			get
			{
				CheckCellType(CellType.Error);
				return _errorValue;
			}
		}

		public double NumericCellValue
		{
			get
			{
				CheckCellType(CellType.Numeric);
				return _numberValue;
			}
		}

		public string StringCellValue
		{
			get
			{
				CheckCellType(CellType.String);
				return _stringValue;
			}
		}

		public IEvaluationSheet Sheet => _sheet;

		public int RowIndex => _masterCell.RowIndex;

		public int ColumnIndex => _masterCell.ColumnIndex;

		public CellType CachedFormulaResultType => _masterCell.CachedFormulaResultType;

		public ForkedEvaluationCell(ForkedEvaluationSheet sheet, IEvaluationCell masterCell)
		{
			_sheet = sheet;
			_masterCell = masterCell;
			SetValue(BlankEval.instance);
		}

		public void SetValue(ValueEval value)
		{
			Type type = value.GetType();
			if (type == typeof(NumberEval))
			{
				_cellType = CellType.Numeric;
				_numberValue = ((NumberEval)value).NumberValue;
			}
			else if (type == typeof(StringEval))
			{
				_cellType = CellType.String;
				_stringValue = ((StringEval)value).StringValue;
			}
			else if (type == typeof(BoolEval))
			{
				_cellType = CellType.Boolean;
				_boolValue = ((BoolEval)value).BooleanValue;
			}
			else if (type == typeof(ErrorEval))
			{
				_cellType = CellType.Error;
				_errorValue = ((ErrorEval)value).ErrorCode;
			}
			else
			{
				if (!(type == typeof(BlankEval)))
				{
					throw new ArgumentException("Unexpected value class (" + type.Name + ")");
				}
				_cellType = CellType.Blank;
			}
		}

		public void CopyValue(ICell destCell)
		{
			switch (_cellType)
			{
			case CellType.Blank:
				destCell.SetCellType(CellType.Blank);
				break;
			case CellType.Numeric:
				destCell.SetCellValue(_numberValue);
				break;
			case CellType.Boolean:
				destCell.SetCellValue(_boolValue);
				break;
			case CellType.String:
				destCell.SetCellValue(_stringValue);
				break;
			case CellType.Error:
				destCell.SetCellErrorValue((byte)_errorValue);
				break;
			default:
				throw new InvalidOperationException("Unexpected data type (" + _cellType + ")");
			}
		}

		private void CheckCellType(CellType expectedCellType)
		{
			if (_cellType != expectedCellType)
			{
				throw new Exception("Wrong data type (" + _cellType + ")");
			}
		}
	}
}
