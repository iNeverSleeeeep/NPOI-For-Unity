using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace NPOI.HSSF.UserModel
{
	/// Utility class for creating data validation cells
	///
	///  @author Dragos Buleandra (dragos.buleandra@trade2b.ro)
	public class HSSFDataValidation : IDataValidation
	{
		private string _prompt_title;

		private string _prompt_text;

		private string _error_title;

		private string _error_text;

		private int _errorStyle;

		private bool _emptyCellAllowed = true;

		private bool _suppress_dropdown_arrow;

		private bool _ShowPromptBox = true;

		private bool _ShowErrorBox = true;

		private CellRangeAddressList _regions;

		private DVConstraint _constraint;

		public IDataValidationConstraint ValidationConstraint => _constraint;

		public DVConstraint Constraint => _constraint;

		public CellRangeAddressList Regions => _regions;

		public int ErrorStyle
		{
			get
			{
				return _errorStyle;
			}
			set
			{
				_errorStyle = value;
			}
		}

		public bool EmptyCellAllowed
		{
			get
			{
				return _emptyCellAllowed;
			}
			set
			{
				_emptyCellAllowed = value;
			}
		}

		public bool SuppressDropDownArrow
		{
			get
			{
				if (_constraint.GetValidationType() == 3)
				{
					return _suppress_dropdown_arrow;
				}
				return false;
			}
			set
			{
				_suppress_dropdown_arrow = value;
			}
		}

		public bool ShowPromptBox
		{
			get
			{
				return _ShowPromptBox;
			}
			set
			{
				_ShowPromptBox = value;
			}
		}

		public bool ShowErrorBox
		{
			get
			{
				return _ShowErrorBox;
			}
			set
			{
				_ShowErrorBox = value;
			}
		}

		public string PromptBoxTitle => _prompt_title;

		public string PromptBoxText => _prompt_text;

		public string ErrorBoxTitle => _error_title;

		public string ErrorBoxText => _error_text;

		/// Constructor which Initializes the cell range on which this object will be
		/// applied
		/// @param constraint 
		public HSSFDataValidation(CellRangeAddressList regions, IDataValidationConstraint constraint)
		{
			_regions = regions;
			_constraint = (DVConstraint)constraint;
		}

		public void CreatePromptBox(string title, string text)
		{
			_prompt_title = title;
			_prompt_text = text;
			ShowPromptBox = true;
		}

		public void CreateErrorBox(string title, string text)
		{
			_error_title = title;
			_error_text = text;
			ShowErrorBox = true;
		}

		public DVRecord CreateDVRecord(HSSFSheet sheet)
		{
			DVConstraint.FormulaPair formulaPair = _constraint.CreateFormulas(sheet);
			return new DVRecord(_constraint.GetValidationType(), _constraint.Operator, _errorStyle, _emptyCellAllowed, SuppressDropDownArrow, _constraint.GetValidationType() == 3 && _constraint.ExplicitListValues != null, _ShowPromptBox, _prompt_title, _prompt_text, _ShowErrorBox, _error_title, _error_text, formulaPair.Formula1, formulaPair.Formula2, _regions);
		}
	}
}
