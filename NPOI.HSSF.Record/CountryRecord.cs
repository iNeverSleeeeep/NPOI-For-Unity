using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Country Record (aka WIN.INI country)
	/// Description:  used for localization.  Currently HSSF always Sets this to 1
	/// and it seems to work fine even in Germany.  (es geht's auch fuer Deutschland)
	///
	/// REFERENCE:  PG 298 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class CountryRecord : StandardRecord
	{
		public const short sid = 140;

		private short field_1_default_country;

		private short field_2_current_country;

		/// Gets the default country
		///
		/// @return country ID (1 = US)
		public short DefaultCountry
		{
			get
			{
				return field_1_default_country;
			}
			set
			{
				field_1_default_country = value;
			}
		}

		/// Gets the current country
		///
		/// @return country ID (1 = US)
		public short CurrentCountry
		{
			get
			{
				return field_2_current_country;
			}
			set
			{
				field_2_current_country = value;
			}
		}

		protected override int DataSize => 4;

		public override short Sid => 140;

		public CountryRecord()
		{
		}

		/// Constructs a CountryRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public CountryRecord(RecordInputStream in1)
		{
			field_1_default_country = in1.ReadShort();
			field_2_current_country = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[COUNTRY]\n");
			stringBuilder.Append("    .defaultcountry  = ").Append(StringUtil.ToHexString(DefaultCountry)).Append("\n");
			stringBuilder.Append("    .currentcountry  = ").Append(StringUtil.ToHexString(CurrentCountry)).Append("\n");
			stringBuilder.Append("[/COUNTRY]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(DefaultCountry);
			out1.WriteShort(CurrentCountry);
		}
	}
}
