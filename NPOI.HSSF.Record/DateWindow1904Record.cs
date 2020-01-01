using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Date Window 1904 Flag record 
	/// Description:  Flag specifying whether 1904 date windowing Is used.
	///               (tick toc tick toc...BOOM!) 
	/// REFERENCE:  PG 280 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class DateWindow1904Record : StandardRecord
	{
		public const short sid = 34;

		private short field_1_window;

		/// Gets whether or not to use 1904 date windowing (which means you'll be screwed in 2004)
		/// @return window flag - 0/1 (false,true)
		public short Windowing
		{
			get
			{
				return field_1_window;
			}
			set
			{
				field_1_window = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 34;

		public DateWindow1904Record()
		{
		}

		/// Constructs a DateWindow1904 record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public DateWindow1904Record(RecordInputStream in1)
		{
			field_1_window = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[1904]\n");
			stringBuilder.Append("    .is1904          = ").Append(StringUtil.ToHexString(Windowing)).Append("\n");
			stringBuilder.Append("[/1904]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Windowing);
		}
	}
}
