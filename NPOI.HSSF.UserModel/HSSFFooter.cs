using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Class to Read and manipulate the footer.
	/// The footer works by having a left, center, and right side.  The total cannot
	/// be more that 255 bytes long.  One uses this class by Getting the HSSFFooter
	/// from HSSFSheet and then Getting or Setting the left, center, and right side.
	/// For special things (such as page numbers and date), one can use a the methods
	/// that return the Chars used to represent these.  One can also Change the
	/// fonts by using similar methods.
	/// @author Shawn Laubach (slaubach at apache dot org)
	/// </summary>
	public class HSSFFooter : HeaderFooter, IFooter, IHeaderFooter
	{
		private PageSettingsBlock _psb;

		/// <summary>
		/// Gets the raw footer.
		/// </summary>
		/// <value>The raw footer.</value>
		public override string RawText
		{
			get
			{
				FooterRecord footer = _psb.Footer;
				if (footer == null)
				{
					return string.Empty;
				}
				return footer.Text;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFFooter" /> class.
		/// </summary>
		/// <param name="psb">Footer record to create the footer with</param>
		public HSSFFooter(PageSettingsBlock psb)
		{
			_psb = psb;
		}

		protected override void SetHeaderFooterText(string text)
		{
			FooterRecord footer = _psb.Footer;
			if (footer == null)
			{
				footer = new FooterRecord(text);
				_psb.Footer = footer;
			}
			else
			{
				footer.Text = text;
			}
		}
	}
}
