using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Hide Object Record
	/// Description:  flag defines whether to hide placeholders and object
	/// REFERENCE:  PG 321 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class HideObjRecord : StandardRecord
	{
		public const short sid = 141;

		public const short HIDE_ALL = 2;

		public const short SHOW_PLACEHOLDERS = 1;

		public const short SHOW_ALL = 0;

		private short field_1_hide_obj;

		protected override int DataSize => 2;

		public override short Sid => 141;

		public HideObjRecord()
		{
		}

		/// Constructs an HideObj record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public HideObjRecord(RecordInputStream in1)
		{
			field_1_hide_obj = in1.ReadShort();
		}

		/// Set hide object options
		///
		/// @param hide options
		/// @see #HIDE_ALL
		/// @see #SHOW_PLACEHOLDERS
		/// @see #SHOW_ALL
		public void SetHideObj(short hide)
		{
			field_1_hide_obj = hide;
		}

		/// Get hide object options
		///
		/// @return hide options
		/// @see #HIDE_ALL
		/// @see #SHOW_PLACEHOLDERS
		/// @see #SHOW_ALL
		public short GetHideObj()
		{
			return field_1_hide_obj;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[HIDEOBJ]\n");
			stringBuilder.Append("    .hideobj         = ").Append(StringUtil.ToHexString(GetHideObj())).Append("\n");
			stringBuilder.Append("[/HIDEOBJ]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(GetHideObj());
		}
	}
}
