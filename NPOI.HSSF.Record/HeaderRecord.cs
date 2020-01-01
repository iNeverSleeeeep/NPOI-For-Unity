using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Header Record
	/// Description:  Specifies a header for a sheet
	/// REFERENCE:  PG 321 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Shawn Laubach (slaubach at apache dot org) Modified 3/14/02
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class HeaderRecord : HeaderFooterBase
	{
		public const short sid = 20;

		public override short Sid => 20;

		public HeaderRecord(string text)
			: base(text)
		{
		}

		/// Constructs an Header record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public HeaderRecord(RecordInputStream in1)
			: base(in1)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[HEADER]\n");
			stringBuilder.Append("    .header = ").Append(base.Text).Append("\n");
			stringBuilder.Append("[/HEADER]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			return new HeaderRecord(base.Text);
		}
	}
}
