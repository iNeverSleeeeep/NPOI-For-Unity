using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Save External Links record (BookBool)
	/// Description:  Contains a flag specifying whether the Gui should save externally
	///               linked values from other workbooks. 
	/// REFERENCE:  PG 289 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class BookBoolRecord : StandardRecord
	{
		public const short sid = 218;

		private short field_1_save_link_values;

		/// Get the save ext links flag
		///
		/// @return short 0/1 (off/on)
		public short SaveLinkValues
		{
			get
			{
				return field_1_save_link_values;
			}
			set
			{
				field_1_save_link_values = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 218;

		public BookBoolRecord()
		{
		}

		/// Constructs a BookBoolRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public BookBoolRecord(RecordInputStream in1)
		{
			field_1_save_link_values = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BOOKBOOL]\n");
			stringBuilder.Append("    .savelinkvalues  = ").Append(StringUtil.ToHexString(SaveLinkValues)).Append("\n");
			stringBuilder.Append("[/BOOKBOOL]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_save_link_values);
		}
	}
}
