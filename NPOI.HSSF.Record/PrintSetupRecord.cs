using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Print Setup Record
	/// Description:  Stores print Setup options -- bogus for HSSF (and marked as such)
	/// REFERENCE:  PG 385 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class PrintSetupRecord : StandardRecord
	{
		public const short sid = 161;

		private short field_1_paper_size;

		private short field_2_scale;

		private short field_3_page_start;

		private short field_4_fit_width;

		private short field_5_fit_height;

		private short field_6_options;

		private BitField lefttoright = BitFieldFactory.GetInstance(1);

		private BitField landscape = BitFieldFactory.GetInstance(2);

		private BitField validSettings = BitFieldFactory.GetInstance(4);

		private BitField nocolor = BitFieldFactory.GetInstance(8);

		private BitField draft = BitFieldFactory.GetInstance(16);

		private BitField notes = BitFieldFactory.GetInstance(32);

		private BitField noOrientation = BitFieldFactory.GetInstance(64);

		private BitField usepage = BitFieldFactory.GetInstance(128);

		private BitField endnote = BitFieldFactory.GetInstance(512);

		private BitField ierror = BitFieldFactory.GetInstance(3072);

		private short field_7_hresolution;

		private short field_8_vresolution;

		private double field_9_headermargin;

		private double field_10_footermargin;

		private short field_11_copies;

		public short PaperSize
		{
			get
			{
				return field_1_paper_size;
			}
			set
			{
				field_1_paper_size = value;
			}
		}

		public short Scale
		{
			get
			{
				return field_2_scale;
			}
			set
			{
				field_2_scale = value;
			}
		}

		public short PageStart
		{
			get
			{
				return field_3_page_start;
			}
			set
			{
				field_3_page_start = value;
			}
		}

		public short FitWidth
		{
			get
			{
				return field_4_fit_width;
			}
			set
			{
				field_4_fit_width = value;
			}
		}

		public short FitHeight
		{
			get
			{
				return field_5_fit_height;
			}
			set
			{
				field_5_fit_height = value;
			}
		}

		public short Options
		{
			get
			{
				return field_6_options;
			}
			set
			{
				field_6_options = value;
			}
		}

		public bool LeftToRight
		{
			get
			{
				return lefttoright.IsSet(field_6_options);
			}
			set
			{
				field_6_options = lefttoright.SetShortBoolean(field_6_options, value);
			}
		}

		public bool Landscape
		{
			get
			{
				return landscape.IsSet(field_6_options);
			}
			set
			{
				field_6_options = landscape.SetShortBoolean(field_6_options, value);
			}
		}

		public bool ValidSettings
		{
			get
			{
				return validSettings.IsSet(field_6_options);
			}
			set
			{
				field_6_options = validSettings.SetShortBoolean(field_6_options, value);
			}
		}

		public bool NoColor
		{
			get
			{
				return nocolor.IsSet(field_6_options);
			}
			set
			{
				field_6_options = nocolor.SetShortBoolean(field_6_options, value);
			}
		}

		public bool Draft
		{
			get
			{
				return draft.IsSet(field_6_options);
			}
			set
			{
				field_6_options = draft.SetShortBoolean(field_6_options, value);
			}
		}

		public bool Notes
		{
			get
			{
				return notes.IsSet(field_6_options);
			}
			set
			{
				field_6_options = notes.SetShortBoolean(field_6_options, value);
			}
		}

		public bool NoOrientation
		{
			get
			{
				return noOrientation.IsSet(field_6_options);
			}
			set
			{
				field_6_options = noOrientation.SetShortBoolean(field_6_options, value);
			}
		}

		public bool UsePage
		{
			get
			{
				return usepage.IsSet(field_6_options);
			}
			set
			{
				field_6_options = usepage.SetShortBoolean(field_6_options, value);
			}
		}

		public short HResolution
		{
			get
			{
				return field_7_hresolution;
			}
			set
			{
				field_7_hresolution = value;
			}
		}

		public short VResolution
		{
			get
			{
				return field_8_vresolution;
			}
			set
			{
				field_8_vresolution = value;
			}
		}

		public double HeaderMargin
		{
			get
			{
				return field_9_headermargin;
			}
			set
			{
				field_9_headermargin = value;
			}
		}

		public double FooterMargin
		{
			get
			{
				return field_10_footermargin;
			}
			set
			{
				field_10_footermargin = value;
			}
		}

		public bool EndNote
		{
			get
			{
				return endnote.IsSet(field_6_options);
			}
			set
			{
				field_6_options = endnote.SetShortBoolean(field_6_options, value);
			}
		}

		public short CellError
		{
			get
			{
				return ierror.GetShortValue(field_6_options);
			}
			set
			{
				field_6_options = ierror.SetShortValue(field_6_options, value);
			}
		}

		public short Copies
		{
			get
			{
				return field_11_copies;
			}
			set
			{
				field_11_copies = value;
			}
		}

		protected override int DataSize => 34;

		public override short Sid => 161;

		public PrintSetupRecord()
		{
		}

		/// Constructs a PrintSetup (SetUP) record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public PrintSetupRecord(RecordInputStream in1)
		{
			field_1_paper_size = in1.ReadShort();
			field_2_scale = in1.ReadShort();
			field_3_page_start = in1.ReadShort();
			field_4_fit_width = in1.ReadShort();
			field_5_fit_height = in1.ReadShort();
			field_6_options = in1.ReadShort();
			field_7_hresolution = in1.ReadShort();
			field_8_vresolution = in1.ReadShort();
			field_9_headermargin = in1.ReadDouble();
			field_10_footermargin = in1.ReadDouble();
			field_11_copies = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PRINTSetUP]\n");
			stringBuilder.Append("    .papersize      = ").Append(PaperSize).Append("\n");
			stringBuilder.Append("    .scale          = ").Append(Scale).Append("\n");
			stringBuilder.Append("    .pagestart      = ").Append(PageStart).Append("\n");
			stringBuilder.Append("    .fitwidth       = ").Append(FitWidth).Append("\n");
			stringBuilder.Append("    .fitheight      = ").Append(FitHeight).Append("\n");
			stringBuilder.Append("    .options        = ").Append(Options).Append("\n");
			stringBuilder.Append("        .ltor       = ").Append(LeftToRight).Append("\n");
			stringBuilder.Append("        .landscape  = ").Append(Landscape).Append("\n");
			stringBuilder.Append("        .valid      = ").Append(ValidSettings).Append("\n");
			stringBuilder.Append("        .mono       = ").Append(NoColor).Append("\n");
			stringBuilder.Append("        .draft      = ").Append(Draft).Append("\n");
			stringBuilder.Append("        .notes      = ").Append(Notes).Append("\n");
			stringBuilder.Append("        .noOrientat = ").Append(NoOrientation).Append("\n");
			stringBuilder.Append("        .usepage    = ").Append(UsePage).Append("\n");
			stringBuilder.Append("    .hresolution    = ").Append(HResolution).Append("\n");
			stringBuilder.Append("    .vresolution    = ").Append(VResolution).Append("\n");
			stringBuilder.Append("    .headermargin   = ").Append(HeaderMargin).Append("\n");
			stringBuilder.Append("    .footermargin   = ").Append(FooterMargin).Append("\n");
			stringBuilder.Append("    .copies         = ").Append(Copies).Append("\n");
			stringBuilder.Append("[/PRINTSetUP]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(PaperSize);
			out1.WriteShort(Scale);
			out1.WriteShort(PageStart);
			out1.WriteShort(FitWidth);
			out1.WriteShort(FitHeight);
			out1.WriteShort(Options);
			out1.WriteShort(HResolution);
			out1.WriteShort(VResolution);
			out1.WriteDouble(HeaderMargin);
			out1.WriteDouble(FooterMargin);
			out1.WriteShort(Copies);
		}

		public override object Clone()
		{
			PrintSetupRecord printSetupRecord = new PrintSetupRecord();
			printSetupRecord.field_1_paper_size = field_1_paper_size;
			printSetupRecord.field_2_scale = field_2_scale;
			printSetupRecord.field_3_page_start = field_3_page_start;
			printSetupRecord.field_4_fit_width = field_4_fit_width;
			printSetupRecord.field_5_fit_height = field_5_fit_height;
			printSetupRecord.field_6_options = field_6_options;
			printSetupRecord.field_7_hresolution = field_7_hresolution;
			printSetupRecord.field_8_vresolution = field_8_vresolution;
			printSetupRecord.field_9_headermargin = field_9_headermargin;
			printSetupRecord.field_10_footermargin = field_10_footermargin;
			printSetupRecord.field_11_copies = field_11_copies;
			return printSetupRecord;
		}
	}
}
