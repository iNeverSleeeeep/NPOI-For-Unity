using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.OpenXmlFormats.Vml;
using NPOI.OpenXmlFormats.Vml.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Model;
using System;

namespace NPOI.XSSF.UserModel
{
	public class XSSFComment : IComment
	{
		private CT_Comment _comment;

		private CommentsTable _comments;

		private CT_Shape _vmlShape;

		/// cached reference to the string with the comment text
		private XSSFRichTextString _str;

		/// @return Name of the original comment author. Default value is blank.
		public string Author
		{
			get
			{
				return _comments.GetAuthor((int)_comment.authorId);
			}
			set
			{
				_comment.authorId = (uint)_comments.FindAuthor(value);
			}
		}

		/// @return the 0-based column of the cell that the comment is associated with.
		public int Column
		{
			get
			{
				return new CellReference(_comment.@ref).Col;
			}
			set
			{
				string @ref = _comment.@ref;
				CellReference cellReference = new CellReference(Row, value);
				_comment.@ref = cellReference.FormatAsString();
				_comments.ReferenceUpdated(@ref, _comment);
				if (_vmlShape != null)
				{
					_vmlShape.GetClientDataArray(0).SetColumnArray(0, value);
				}
			}
		}

		/// @return the 0-based row index of the cell that the comment is associated with.
		public int Row
		{
			get
			{
				return new CellReference(_comment.@ref).Row;
			}
			set
			{
				string @ref = _comment.@ref;
				string ref2 = new CellReference(value, Column).FormatAsString();
				_comment.@ref = ref2;
				_comments.ReferenceUpdated(@ref, _comment);
				if (_vmlShape != null)
				{
					_vmlShape.GetClientDataArray(0).SetRowArray(0, value);
				}
			}
		}

		/// @return whether the comment is visible
		public bool Visible
		{
			get
			{
				bool result = false;
				if (_vmlShape != null)
				{
					string style = _vmlShape.style;
					if (style != null)
					{
						result = (style.IndexOf("visibility:visible") != -1);
					}
					else
					{
						if (_vmlShape.GetClientDataArray(0) == null)
						{
							return false;
						}
						result = _vmlShape.GetClientDataArray(0).visibleSpecified;
					}
				}
				return result;
			}
			set
			{
				if (_vmlShape != null)
				{
					string style;
					if (value)
					{
						style = "position:absolute;visibility:visible";
						_vmlShape.GetClientDataArray(0).visible = ST_TrueFalseBlank.@true;
						_vmlShape.GetClientDataArray(0).visibleSpecified = true;
					}
					else
					{
						style = "position:absolute;visibility:hidden";
						_vmlShape.GetClientDataArray(0).visible = ST_TrueFalseBlank.@false;
						_vmlShape.GetClientDataArray(0).visibleSpecified = false;
					}
					_vmlShape.style = style;
				}
			}
		}

		/// @return the rich text string of the comment
		public IRichTextString String
		{
			get
			{
				if (_str == null && _comment.text != null)
				{
					_str = new XSSFRichTextString(_comment.text);
				}
				return _str;
			}
			set
			{
				if (!(value is XSSFRichTextString))
				{
					throw new ArgumentException("Only XSSFRichTextString argument is supported");
				}
				_str = (XSSFRichTextString)value;
				_comment.text = _str.GetCTRst();
			}
		}

		/// Creates a new XSSFComment, associated with a given
		///  low level comment object.
		public XSSFComment(CommentsTable comments, CT_Comment comment, CT_Shape vmlShape)
		{
			_comment = comment;
			_comments = comments;
			_vmlShape = vmlShape;
		}

		/// Sets the rich text string used by this comment.
		///
		/// @param string  the XSSFRichTextString used by this object.
		public void SetString(string str)
		{
			String = new XSSFRichTextString(str);
		}

		/// @return the xml bean holding this comment's properties
		internal CT_Comment GetCTComment()
		{
			return _comment;
		}

		internal CT_Shape GetCTShape()
		{
			return _vmlShape;
		}
	}
}
