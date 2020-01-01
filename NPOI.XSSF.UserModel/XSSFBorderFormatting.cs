using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel
{
	/// @author Yegor Kozlov
	public class XSSFBorderFormatting : IBorderFormatting
	{
		private CT_Border _border;

		public BorderStyle BorderBottom
		{
			get
			{
				if (!_border.IsSetBottom())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)_border.bottom.style;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetBottom() ? _border.bottom : _border.AddNewBottom();
				if (value == BorderStyle.None)
				{
					_border.unsetBottom();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
			}
		}

		public BorderStyle BorderDiagonal
		{
			get
			{
				if (!_border.IsSetDiagonal())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)_border.diagonal.style;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetDiagonal() ? _border.diagonal : _border.AddNewDiagonal();
				if (value == BorderStyle.None)
				{
					_border.unsetDiagonal();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)(value + 1);
				}
			}
		}

		public BorderStyle BorderLeft
		{
			get
			{
				if (!_border.IsSetLeft())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)_border.left.style;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetLeft() ? _border.left : _border.AddNewLeft();
				if (value == BorderStyle.None)
				{
					_border.unsetLeft();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
			}
		}

		public BorderStyle BorderRight
		{
			get
			{
				if (!_border.IsSetRight())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)_border.right.style;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetRight() ? _border.right : _border.AddNewRight();
				if (value == BorderStyle.None)
				{
					_border.unsetRight();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
			}
		}

		public BorderStyle BorderTop
		{
			get
			{
				if (!_border.IsSetTop())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)_border.top.style;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetTop() ? _border.top : _border.AddNewTop();
				if (value == BorderStyle.None)
				{
					_border.unsetTop();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
			}
		}

		public short BottomBorderColor
		{
			get
			{
				if (!_border.IsSetBottom())
				{
					return 0;
				}
				CT_BorderPr bottom = _border.bottom;
				if (!bottom.color.indexedSpecified)
				{
					return 0;
				}
				return (short)bottom.color.indexed;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetBottom() ? _border.bottom : _border.AddNewBottom();
				CT_Color cT_Color = new CT_Color();
				cT_Color.indexed = (uint)value;
				cT_Color.indexedSpecified = true;
				cT_BorderPr.color = cT_Color;
			}
		}

		public short DiagonalBorderColor
		{
			get
			{
				if (!_border.IsSetDiagonal())
				{
					return 0;
				}
				CT_BorderPr diagonal = _border.diagonal;
				if (!diagonal.color.indexedSpecified)
				{
					return 0;
				}
				return (short)diagonal.color.indexed;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetDiagonal() ? _border.diagonal : _border.AddNewDiagonal();
				CT_Color cT_Color = new CT_Color();
				cT_Color.indexed = (uint)value;
				cT_Color.indexedSpecified = true;
				cT_BorderPr.color = cT_Color;
			}
		}

		public short LeftBorderColor
		{
			get
			{
				if (!_border.IsSetLeft())
				{
					return 0;
				}
				CT_BorderPr left = _border.left;
				if (!left.color.indexedSpecified)
				{
					return 0;
				}
				return (short)left.color.indexed;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetLeft() ? _border.left : _border.AddNewLeft();
				CT_Color cT_Color = new CT_Color();
				cT_Color.indexed = (uint)value;
				cT_Color.indexedSpecified = true;
				cT_BorderPr.color = cT_Color;
			}
		}

		public short RightBorderColor
		{
			get
			{
				if (!_border.IsSetRight())
				{
					return 0;
				}
				CT_BorderPr right = _border.right;
				if (!right.color.indexedSpecified)
				{
					return 0;
				}
				return (short)right.color.indexed;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetRight() ? _border.right : _border.AddNewRight();
				CT_Color cT_Color = new CT_Color();
				cT_Color.indexed = (uint)value;
				cT_Color.indexedSpecified = true;
				cT_BorderPr.color = cT_Color;
			}
		}

		public short TopBorderColor
		{
			get
			{
				if (!_border.IsSetTop())
				{
					return 0;
				}
				CT_BorderPr top = _border.top;
				if (!top.color.indexedSpecified)
				{
					return 0;
				}
				return (short)top.color.indexed;
			}
			set
			{
				CT_BorderPr cT_BorderPr = _border.IsSetTop() ? _border.top : _border.AddNewTop();
				CT_Color cT_Color = new CT_Color();
				cT_Color.indexed = (uint)value;
				cT_Color.indexedSpecified = true;
				cT_BorderPr.color = cT_Color;
			}
		}

		internal XSSFBorderFormatting(CT_Border border)
		{
			_border = border;
		}
	}
}
