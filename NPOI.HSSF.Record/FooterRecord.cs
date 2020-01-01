using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Footer Record 
	/// Description:  Specifies the footer for a sheet
	/// REFERENCE:  PG 317 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Shawn Laubach (slaubach at apache dot org) Modified 3/14/02
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class FooterRecord : HeaderFooterBase
	{
		public const short sid = 21;

		/// <summary>
		/// </summary>
		/// <value></value>
		/// return the non static version of the id for this record.
		public override short Sid => 21;

		public FooterRecord(string text)
			: base(text)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.Record.FooterRecord" /> class.
		/// </summary>
		/// <param name="in1">the RecordInputstream to Read the record from</param>
		public FooterRecord(RecordInputStream in1)
			: base(in1)
		{
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FOOTER]\n");
			stringBuilder.Append("    .footer         = ").Append(base.Text).Append("\n");
			stringBuilder.Append("[/FOOTER]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			return new FooterRecord(base.Text);
		}
	}
}
