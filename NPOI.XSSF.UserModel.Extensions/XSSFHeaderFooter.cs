using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel.Helpers;

namespace NPOI.XSSF.UserModel.Extensions
{
	/// <summary>
	/// Parent class of all XSSF headers and footers.
	/// </summary>
	public abstract class XSSFHeaderFooter : IHeaderFooter
	{
		private HeaderFooterHelper helper;

		private CT_HeaderFooter headerFooter;

		private bool stripFields;

		public abstract string Text
		{
			get;
			set;
		}

		/// get the text representing the center part of this element
		public string Center
		{
			get
			{
				string centerSection = helper.GetCenterSection(Text);
				if (stripFields)
				{
					return StripFields(centerSection);
				}
				return centerSection;
			}
			set
			{
				Text = helper.SetCenterSection(Text, value);
			}
		}

		/// get the text representing the left part of this element
		public string Left
		{
			get
			{
				string leftSection = helper.GetLeftSection(Text);
				if (stripFields)
				{
					return StripFields(leftSection);
				}
				return leftSection;
			}
			set
			{
				Text = helper.SetLeftSection(Text, value);
			}
		}

		/// get the text representing the right part of this element
		public string Right
		{
			get
			{
				string rightSection = helper.GetRightSection(Text);
				if (stripFields)
				{
					return StripFields(rightSection);
				}
				return rightSection;
			}
			set
			{
				Text = helper.SetRightSection(Text, value);
			}
		}

		/// Create an instance of XSSFHeaderFooter from the supplied XML bean
		///
		/// @param headerFooter
		public XSSFHeaderFooter(CT_HeaderFooter headerFooter)
		{
			this.headerFooter = headerFooter;
			helper = new HeaderFooterHelper();
		}

		/// Returns the underlying CTHeaderFooter xml bean
		///
		/// @return the underlying CTHeaderFooter xml bean
		public CT_HeaderFooter GetHeaderFooter()
		{
			return headerFooter;
		}

		public string GetValue()
		{
			string text = Text;
			if (text == null)
			{
				return "";
			}
			return text;
		}

		/// Are fields currently being stripped from the text that this
		/// {@link XSSFHeaderFooter} returns? Default is false, but can be Changed
		public bool AreFieldsStripped()
		{
			return stripFields;
		}

		/// Should fields (eg macros) be stripped from the text that this class
		/// returns? Default is not to strip.
		///
		/// @param StripFields
		public void SetAreFieldsStripped(bool stripFields)
		{
			this.stripFields = stripFields;
		}

		/// Removes any fields (eg macros, page markers etc) from the string.
		/// Normally used to make some text suitable for showing to humans, and the
		/// resultant text should not normally be saved back into the document!
		public static string StripFields(string text)
		{
			return HeaderFooter.StripFields(text);
		}
	}
}
