using NPOI.HSSF.Record;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Represents an Excel hyperlink.
	/// </summary>
	/// <remarks>@author Yegor Kozlov (yegor at apache dot org)</remarks>
	public class HSSFHyperlink : IHyperlink
	{
		/// Low-level record object that stores the actual hyperlink data
		public HyperlinkRecord record;

		/// If we Create a new hypelrink remember its type
		protected int link_type;

		/// <summary>
		/// Gets or sets the row of the first cell that Contains the hyperlink
		/// </summary>
		/// <value>the 0-based row of the cell that Contains the hyperlink.</value>
		public int FirstRow
		{
			get
			{
				return record.FirstRow;
			}
			set
			{
				record.FirstRow = value;
			}
		}

		/// <summary>
		/// Gets or sets the row of the last cell that Contains the hyperlink
		/// </summary>
		/// <value>the 0-based row of the last cell that Contains the hyperlink</value>
		public int LastRow
		{
			get
			{
				return record.LastRow;
			}
			set
			{
				record.LastRow = value;
			}
		}

		/// <summary>
		/// Gets or sets the column of the first cell that Contains the hyperlink
		/// </summary>
		/// <value>the 0-based column of the first cell that Contains the hyperlink</value>
		public int FirstColumn
		{
			get
			{
				return record.FirstColumn;
			}
			set
			{
				record.FirstColumn = value;
			}
		}

		/// <summary>
		/// Gets or sets the column of the last cell that Contains the hyperlink
		/// </summary>
		/// <value>the 0-based column of the last cell that Contains the hyperlink</value>
		public int LastColumn
		{
			get
			{
				return record.LastColumn;
			}
			set
			{
				record.LastColumn = value;
			}
		}

		/// <summary>
		/// Gets or sets Hypelink Address. Depending on the hyperlink type it can be URL, e-mail, patrh to a file, etc.
		/// </summary>
		/// <value>the Address of this hyperlink</value>
		public string Address
		{
			get
			{
				return record.Address;
			}
			set
			{
				record.Address = value;
			}
		}

		/// <summary>
		/// Gets or sets the text mark.
		/// </summary>
		/// <value>The text mark.</value>
		public string TextMark
		{
			get
			{
				return record.TextMark;
			}
			set
			{
				record.TextMark = value;
			}
		}

		/// <summary>
		/// Gets or sets the short filename.
		/// </summary>
		/// <value>The short filename.</value>
		public string ShortFilename
		{
			get
			{
				return record.ShortFilename;
			}
			set
			{
				record.ShortFilename = value;
			}
		}

		/// <summary>
		/// Gets or sets the text label for this hyperlink
		/// </summary>
		/// <value>text to Display</value>
		public string Label
		{
			get
			{
				return record.Label;
			}
			set
			{
				record.Label = value;
			}
		}

		/// <summary>
		/// Gets the type of this hyperlink
		/// </summary>
		/// <value>the type of this hyperlink</value>
		public HyperlinkType Type => (HyperlinkType)link_type;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFHyperlink" /> class.
		/// </summary>
		/// <param name="type">The type of hyperlink to Create.</param>
		public HSSFHyperlink(HyperlinkType type)
		{
			link_type = (int)type;
			record = new HyperlinkRecord();
			switch (type)
			{
			case HyperlinkType.Url:
			case HyperlinkType.Email:
				record.CreateUrlLink();
				break;
			case HyperlinkType.File:
				record.CreateFileLink();
				break;
			case HyperlinkType.Document:
				record.CreateDocumentLink();
				break;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFHyperlink" /> class.
		/// </summary>
		/// <param name="record">The record.</param>
		public HSSFHyperlink(HyperlinkRecord record)
		{
			this.record = record;
		}
	}
}
