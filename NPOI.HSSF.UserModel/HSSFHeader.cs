using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Class to Read and manipulate the header.
	/// The header works by having a left, center, and right side.  The total cannot
	/// be more that 255 bytes long.  One uses this class by Getting the HSSFHeader
	/// from HSSFSheet and then Getting or Setting the left, center, and right side.
	/// For special things (such as page numbers and date), one can use a the methods
	/// that return the Chars used to represent these.  One can also Change the
	/// fonts by using similar methods.
	/// @author Shawn Laubach (slaubach at apache dot org)
	/// </summary>
	public class HSSFHeader : HeaderFooter, IHeader, IHeaderFooter
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
				HeaderRecord header = _psb.Header;
				if (header == null)
				{
					return string.Empty;
				}
				return header.Text;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFFooter" /> class.
		/// </summary>
		/// <param name="psb">Footer record to Create the footer with</param>
		public HSSFHeader(PageSettingsBlock psb)
		{
			_psb = psb;
		}

		protected override void SetHeaderFooterText(string text)
		{
			HeaderRecord header = _psb.Header;
			if (header == null)
			{
				header = new HeaderRecord(text);
				_psb.Header = header;
			}
			else
			{
				header.Text = text;
			}
		}
	}
}
