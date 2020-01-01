using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Beginning Of File
	/// Description: Somewhat of a misnomer, its used for the beginning of a Set of
	///              records that have a particular pupose or subject.
	///              Used in sheets and workbooks.
	/// REFERENCE:  PG 289 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class BOFRecord : StandardRecord
	{
		/// for BIFF8 files the BOF is 0x809.  For earlier versions it was 0x09 or 0x(biffversion)09
		public const short sid = 2057;

		/// suggested default (0x06 - BIFF8)
		public const short VERSION = 6;

		/// suggested default 0x10d3
		public const short BUILD = 4307;

		/// suggested default  0x07CC (1996)
		public const short BUILD_YEAR = 1996;

		/// suggested default for a normal sheet (0x41)
		public const short HISTORY_MASK = 65;

		private int field_1_version;

		private int field_2_type;

		private int field_3_build;

		private int field_4_year;

		private int field_5_history;

		private int field_6_rversion;

		/// Version number - for BIFF8 should be 0x06
		/// @see #VERSION
		/// @param version version to be Set
		public int Version
		{
			get
			{
				return field_1_version;
			}
			set
			{
				field_1_version = value;
			}
		}

		/// Set the history bit mask (not very useful)
		/// @see #HISTORY_MASK
		/// @param bitmask bitmask to Set for the history
		public int HistoryBitMask
		{
			get
			{
				return field_5_history;
			}
			set
			{
				field_5_history = value;
			}
		}

		/// Set the minimum version required to Read this file
		///
		/// @see #VERSION
		/// @param version version to Set
		public int RequiredVersion
		{
			get
			{
				return field_6_rversion;
			}
			set
			{
				field_6_rversion = value;
			}
		}

		/// type of object this marks
		/// @see #TYPE_WORKBOOK
		/// @see #TYPE_VB_MODULE
		/// @see #TYPE_WORKSHEET
		/// @see #TYPE_CHART
		/// @see #TYPE_EXCEL_4_MACRO
		/// @see #TYPE_WORKSPACE_FILE
		/// @return short type of object
		public BOFRecordType Type
		{
			get
			{
				return (BOFRecordType)field_2_type;
			}
			set
			{
				field_2_type = (int)value;
			}
		}

		private string TypeName
		{
			get
			{
				switch (Type)
				{
				case BOFRecordType.Chart:
					return "chart";
				case BOFRecordType.Excel4Macro:
					return "excel 4 macro";
				case BOFRecordType.VBModule:
					return "vb module";
				case BOFRecordType.Workbook:
					return "workbook";
				case BOFRecordType.Worksheet:
					return "worksheet";
				case BOFRecordType.WorkspaceFile:
					return "workspace file";
				default:
					return "#error unknown type#";
				}
			}
		}

		/// Get the build that wrote this file
		/// @see #BUILD
		/// @return short build number of the generator of this file
		public int Build
		{
			get
			{
				return field_3_build;
			}
			set
			{
				field_3_build = value;
			}
		}

		/// Year of the build that wrote this file
		/// @see #BUILD_YEAR
		/// @return short build year of the generator of this file
		public int BuildYear
		{
			get
			{
				return field_4_year;
			}
			set
			{
				field_4_year = value;
			}
		}

		protected override int DataSize => 16;

		public override short Sid => 2057;

		/// Constructs an empty BOFRecord with no fields Set.
		public BOFRecord()
		{
		}

		private BOFRecord(BOFRecordType type)
		{
			field_1_version = 6;
			field_2_type = (int)type;
			field_3_build = 4307;
			field_4_year = 1996;
			field_5_history = 1;
			field_6_rversion = 6;
		}

		public static BOFRecord CreateSheetBOF()
		{
			return new BOFRecord(BOFRecordType.Worksheet);
		}

		/// Constructs a BOFRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public BOFRecord(RecordInputStream in1)
		{
			field_1_version = in1.ReadShort();
			field_2_type = in1.ReadShort();
			if (in1.Remaining >= 2)
			{
				field_3_build = in1.ReadShort();
			}
			if (in1.Remaining >= 2)
			{
				field_4_year = in1.ReadShort();
			}
			if (in1.Remaining >= 4)
			{
				field_5_history = in1.ReadInt();
			}
			if (in1.Remaining >= 4)
			{
				field_6_rversion = in1.ReadInt();
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BOF RECORD]\n");
			stringBuilder.Append("    .version         = ").Append(StringUtil.ToHexString(Version)).Append("\n");
			stringBuilder.Append("    .type            = ").Append(StringUtil.ToHexString((int)Type)).Append("\n");
			stringBuilder.Append(" (").Append(TypeName).Append(")")
				.Append("\n");
			stringBuilder.Append("    .build           = ").Append(StringUtil.ToHexString(Build)).Append("\n");
			stringBuilder.Append("    .buildyear       = ").Append(BuildYear).Append("\n");
			stringBuilder.Append("    .history         = ").Append(StringUtil.ToHexString(HistoryBitMask)).Append("\n");
			stringBuilder.Append("    .requiredversion = ").Append(StringUtil.ToHexString(RequiredVersion)).Append("\n");
			stringBuilder.Append("[/BOF RECORD]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Version);
			out1.WriteShort((int)Type);
			out1.WriteShort(Build);
			out1.WriteShort(BuildYear);
			out1.WriteInt(HistoryBitMask);
			out1.WriteInt(RequiredVersion);
		}

		public override object Clone()
		{
			BOFRecord bOFRecord = new BOFRecord();
			bOFRecord.field_1_version = field_1_version;
			bOFRecord.field_2_type = field_2_type;
			bOFRecord.field_3_build = field_3_build;
			bOFRecord.field_4_year = field_4_year;
			bOFRecord.field_5_history = field_5_history;
			bOFRecord.field_6_rversion = field_6_rversion;
			return bOFRecord;
		}
	}
}
