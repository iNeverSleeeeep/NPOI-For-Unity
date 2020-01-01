using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.Util;
using System.Collections.Generic;

namespace NPOI.XWPF.UserModel
{
	/// @author Philipp Epp
	public class XWPFAbstractNum
	{
		private CT_AbstractNum ctAbstractNum;

		protected XWPFNumbering numbering;

		private char[] lvlText = new char[3]
		{
			'n',
			'l',
			'u'
		};

		/// <summary>
		/// Abstract Numbering Definition Type
		/// </summary>
		public MultiLevelType MultiLevelType
		{
			get
			{
				return EnumConverter.ValueOf<MultiLevelType, ST_MultiLevelType>(ctAbstractNum.multiLevelType.val);
			}
			set
			{
				ctAbstractNum.multiLevelType.val = EnumConverter.ValueOf<ST_MultiLevelType, MultiLevelType>(value);
			}
		}

		public string AbstractNumId
		{
			get
			{
				return ctAbstractNum.abstractNumId;
			}
			set
			{
				ctAbstractNum.abstractNumId = value;
			}
		}

		protected XWPFAbstractNum()
		{
			ctAbstractNum = null;
			numbering = null;
		}

		public XWPFAbstractNum(CT_AbstractNum abstractNum)
		{
			ctAbstractNum = abstractNum;
		}

		public XWPFAbstractNum(CT_AbstractNum ctAbstractNum, XWPFNumbering numbering)
		{
			this.ctAbstractNum = ctAbstractNum;
			this.numbering = numbering;
		}

		public CT_AbstractNum GetAbstractNum()
		{
			return ctAbstractNum;
		}

		public XWPFNumbering GetNumbering()
		{
			return numbering;
		}

		public CT_AbstractNum GetCTAbstractNum()
		{
			return ctAbstractNum;
		}

		public void SetNumbering(XWPFNumbering numbering)
		{
			this.numbering = numbering;
		}

		internal void InitLvl()
		{
			List<CT_Lvl> list = new List<CT_Lvl>();
			for (int i = 0; i < 9; i++)
			{
				CT_Lvl cT_Lvl = new CT_Lvl();
				cT_Lvl.start.val = "1";
				cT_Lvl.tentative = ((i == 0) ? ST_OnOff.on : ST_OnOff.off);
				cT_Lvl.ilvl = i.ToString();
				cT_Lvl.lvlJc.val = ST_Jc.left;
				cT_Lvl.numFmt.val = ST_NumberFormat.bullet;
				cT_Lvl.lvlText.val = lvlText[i % 3].ToString();
				CT_Ind cT_Ind = cT_Lvl.pPr.AddNewInd();
				cT_Ind.left = (420 * (i + 1)).ToString();
				cT_Ind.hanging = 420uL;
				CT_Fonts cT_Fonts = cT_Lvl.rPr.AddNewRFonts();
				cT_Fonts.ascii = "Wingdings";
				cT_Fonts.hAnsi = "Wingdings";
				cT_Fonts.hint = ST_Hint.@default;
				list.Add(cT_Lvl);
			}
			ctAbstractNum.lvl = list;
		}

		internal void SetLevelTentative(int lvl, bool tentative)
		{
			if (tentative)
			{
				ctAbstractNum.lvl[lvl].tentative = ST_OnOff.on;
			}
			else
			{
				ctAbstractNum.lvl[lvl].tentative = ST_OnOff.off;
			}
		}
	}
}
