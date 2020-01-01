using NPOI.OpenXmlFormats.Dml.Spreadsheet;
using NPOI.SS.UserModel;
using System;

namespace NPOI.XSSF.UserModel
{
	/// Represents a text box in a SpreadsheetML Drawing.
	///
	/// @author Yegor Kozlov
	public class XSSFTextBox : XSSFSimpleShape, ITextbox, IShape
	{
		public short HorizontalAlignment
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int MarginBottom
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int MarginLeft
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int MarginRight
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int MarginTop
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public IRichTextString String
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public short VerticalAlignment
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public new int CountOfAllChildren
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public new int FillColor
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override LineStyle LineStyle
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override int LineStyleColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override double LineWidth
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		internal XSSFTextBox(XSSFDrawing drawing, CT_Shape ctShape)
			: base(drawing, ctShape)
		{
		}

		public new void SetLineStyleColor(int lineStyleColor)
		{
			throw new NotImplementedException();
		}
	}
}
