using NPOI.OpenXmlFormats.Wordprocessing;
using System.Text;

namespace NPOI.XWPF.UserModel
{
	public class TOC
	{
		private CT_SdtBlock block;

		public TOC()
			: this(new CT_SdtBlock())
		{
		}

		public TOC(CT_SdtBlock block)
		{
			this.block = block;
			CT_SdtPr cT_SdtPr = block.AddNewSdtPr();
			CT_DecimalNumber cT_DecimalNumber = cT_SdtPr.AddNewId();
			cT_DecimalNumber.val = "4844945";
			cT_SdtPr.AddNewDocPartObj().AddNewDocPartGallery().val = "Table of contents";
			CT_SdtEndPr cT_SdtEndPr = block.AddNewSdtEndPr();
			CT_RPr cT_RPr = cT_SdtEndPr.AddNewRPr();
			CT_Fonts cT_Fonts = cT_RPr.AddNewRFonts();
			cT_Fonts.asciiTheme = ST_Theme.minorHAnsi;
			cT_Fonts.eastAsiaTheme = ST_Theme.minorHAnsi;
			cT_Fonts.hAnsiTheme = ST_Theme.minorHAnsi;
			cT_Fonts.cstheme = ST_Theme.minorBidi;
			cT_RPr.AddNewB().val = false;
			cT_RPr.AddNewBCs().val = false;
			cT_RPr.AddNewColor().val = "auto";
			cT_RPr.AddNewSz().val = 24uL;
			cT_RPr.AddNewSzCs().val = 24uL;
			CT_SdtContentBlock cT_SdtContentBlock = block.AddNewSdtContent();
			CT_P cT_P = cT_SdtContentBlock.AddNewP();
			byte[] array2 = cT_P.rsidRDefault = (cT_P.rsidR = Encoding.Unicode.GetBytes("00EF7E24"));
			cT_P.AddNewPPr().AddNewPStyle().val = "TOCHeading";
			cT_P.AddNewR().AddNewT().Value = "Table of Contents";
		}

		public CT_SdtBlock GetBlock()
		{
			return block;
		}

		public void AddRow(int level, string title, int page, string bookmarkRef)
		{
			CT_SdtContentBlock sdtContent = block.sdtContent;
			CT_P cT_P = sdtContent.AddNewP();
			byte[] array2 = cT_P.rsidRDefault = (cT_P.rsidR = Encoding.Unicode.GetBytes("00EF7E24"));
			CT_PPr cT_PPr = cT_P.AddNewPPr();
			cT_PPr.AddNewPStyle().val = "TOC" + level;
			CT_Tabs cT_Tabs = cT_PPr.AddNewTabs();
			CT_TabStop cT_TabStop = cT_Tabs.AddNewTab();
			cT_TabStop.val = ST_TabJc.right;
			cT_TabStop.leader = ST_TabTlc.dot;
			cT_TabStop.pos = "8290";
			cT_PPr.AddNewRPr().AddNewNoProof();
			CT_R cT_R = cT_P.AddNewR();
			cT_R.AddNewRPr().AddNewNoProof();
			cT_R.AddNewT().Value = title;
			cT_R = cT_P.AddNewR();
			cT_R.AddNewRPr().AddNewNoProof();
			cT_R.AddNewTab();
			cT_R = cT_P.AddNewR();
			cT_R.AddNewRPr().AddNewNoProof();
			cT_R.AddNewFldChar().fldCharType = ST_FldCharType.begin;
			cT_R = cT_P.AddNewR();
			cT_R.AddNewRPr().AddNewNoProof();
			CT_Text cT_Text = cT_R.AddNewInstrText();
			cT_Text.space = "preserve";
			cT_Text.Value = " PAGEREF _Toc" + bookmarkRef + " \\h ";
			cT_P.AddNewR().AddNewRPr().AddNewNoProof();
			cT_R = cT_P.AddNewR();
			cT_R.AddNewRPr().AddNewNoProof();
			cT_R.AddNewFldChar().fldCharType = ST_FldCharType.separate;
			cT_R = cT_P.AddNewR();
			cT_R.AddNewRPr().AddNewNoProof();
			cT_R.AddNewT().Value = page.ToString();
			cT_R = cT_P.AddNewR();
			cT_R.AddNewRPr().AddNewNoProof();
			cT_R.AddNewFldChar().fldCharType = ST_FldCharType.end;
		}
	}
}
