using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TextParagraphProperties
	{
		private CT_TextSpacing lnSpcField;

		private CT_TextSpacing spcBefField;

		private CT_TextSpacing spcAftField;

		private CT_TextBulletColorFollowText buClrTxField;

		private CT_Color buClrField;

		private CT_TextBulletSizeFollowText buSzTxField;

		private CT_TextBulletSizePercent buSzPctField;

		private CT_TextBulletSizePoint buSzPtsField;

		private CT_TextBulletTypefaceFollowText buFontTxField;

		private CT_TextFont buFontField;

		private CT_TextNoBullet buNoneField;

		private CT_TextAutonumberBullet buAutoNumField;

		private CT_TextCharBullet buCharField;

		private CT_TextBlipBullet buBlipField;

		private CT_TextTabStopList tabLstField;

		private CT_TextCharacterProperties defRPrField;

		private CT_OfficeArtExtensionList extLstField;

		private int marLField;

		private bool marLFieldSpecified;

		private int marRField;

		private bool marRFieldSpecified;

		private int lvlField;

		private bool lvlFieldSpecified;

		private int indentField;

		private bool indentFieldSpecified;

		private ST_TextAlignType algnField;

		private bool algnFieldSpecified;

		private int defTabSzField;

		private bool defTabSzFieldSpecified;

		private bool rtlField;

		private bool rtlFieldSpecified;

		private bool eaLnBrkField;

		private bool eaLnBrkFieldSpecified;

		private ST_TextFontAlignType fontAlgnField;

		private bool fontAlgnFieldSpecified;

		private bool latinLnBrkField;

		private bool latinLnBrkFieldSpecified;

		private bool hangingPunctField;

		private bool hangingPunctFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_TextSpacing lnSpc
		{
			get
			{
				return lnSpcField;
			}
			set
			{
				lnSpcField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TextSpacing spcBef
		{
			get
			{
				return spcBefField;
			}
			set
			{
				spcBefField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TextSpacing spcAft
		{
			get
			{
				return spcAftField;
			}
			set
			{
				spcAftField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_TextBulletColorFollowText buClrTx
		{
			get
			{
				return buClrTxField;
			}
			set
			{
				buClrTxField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Color buClr
		{
			get
			{
				return buClrField;
			}
			set
			{
				buClrField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_TextBulletSizeFollowText buSzTx
		{
			get
			{
				return buSzTxField;
			}
			set
			{
				buSzTxField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_TextBulletSizePercent buSzPct
		{
			get
			{
				return buSzPctField;
			}
			set
			{
				buSzPctField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_TextBulletSizePoint buSzPts
		{
			get
			{
				return buSzPtsField;
			}
			set
			{
				buSzPtsField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_TextBulletTypefaceFollowText buFontTx
		{
			get
			{
				return buFontTxField;
			}
			set
			{
				buFontTxField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_TextFont buFont
		{
			get
			{
				return buFontField;
			}
			set
			{
				buFontField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_TextNoBullet buNone
		{
			get
			{
				return buNoneField;
			}
			set
			{
				buNoneField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_TextAutonumberBullet buAutoNum
		{
			get
			{
				return buAutoNumField;
			}
			set
			{
				buAutoNumField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_TextCharBullet buChar
		{
			get
			{
				return buCharField;
			}
			set
			{
				buCharField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_TextBlipBullet buBlip
		{
			get
			{
				return buBlipField;
			}
			set
			{
				buBlipField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_TextTabStopList tabLst
		{
			get
			{
				return tabLstField;
			}
			set
			{
				tabLstField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_TextCharacterProperties defRPr
		{
			get
			{
				return defRPrField;
			}
			set
			{
				defRPrField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		public int marL
		{
			get
			{
				return marLField;
			}
			set
			{
				marLField = value;
			}
		}

		[XmlIgnore]
		public bool marLSpecified
		{
			get
			{
				return marLFieldSpecified;
			}
			set
			{
				marLFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int marR
		{
			get
			{
				return marRField;
			}
			set
			{
				marRField = value;
			}
		}

		[XmlIgnore]
		public bool marRSpecified
		{
			get
			{
				return marRFieldSpecified;
			}
			set
			{
				marRFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int lvl
		{
			get
			{
				return lvlField;
			}
			set
			{
				lvlField = value;
			}
		}

		[XmlIgnore]
		public bool lvlSpecified
		{
			get
			{
				return lvlFieldSpecified;
			}
			set
			{
				lvlFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int indent
		{
			get
			{
				return indentField;
			}
			set
			{
				indentField = value;
			}
		}

		[XmlIgnore]
		public bool indentSpecified
		{
			get
			{
				return indentFieldSpecified;
			}
			set
			{
				indentFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextAlignType algn
		{
			get
			{
				return algnField;
			}
			set
			{
				algnField = value;
			}
		}

		[XmlIgnore]
		public bool algnSpecified
		{
			get
			{
				return algnFieldSpecified;
			}
			set
			{
				algnFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int defTabSz
		{
			get
			{
				return defTabSzField;
			}
			set
			{
				defTabSzField = value;
			}
		}

		[XmlIgnore]
		public bool defTabSzSpecified
		{
			get
			{
				return defTabSzFieldSpecified;
			}
			set
			{
				defTabSzFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool rtl
		{
			get
			{
				return rtlField;
			}
			set
			{
				rtlField = value;
			}
		}

		[XmlIgnore]
		public bool rtlSpecified
		{
			get
			{
				return rtlFieldSpecified;
			}
			set
			{
				rtlFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool eaLnBrk
		{
			get
			{
				return eaLnBrkField;
			}
			set
			{
				eaLnBrkField = value;
			}
		}

		[XmlIgnore]
		public bool eaLnBrkSpecified
		{
			get
			{
				return eaLnBrkFieldSpecified;
			}
			set
			{
				eaLnBrkFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextFontAlignType fontAlgn
		{
			get
			{
				return fontAlgnField;
			}
			set
			{
				fontAlgnField = value;
			}
		}

		[XmlIgnore]
		public bool fontAlgnSpecified
		{
			get
			{
				return fontAlgnFieldSpecified;
			}
			set
			{
				fontAlgnFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool latinLnBrk
		{
			get
			{
				return latinLnBrkField;
			}
			set
			{
				latinLnBrkField = value;
			}
		}

		[XmlIgnore]
		public bool latinLnBrkSpecified
		{
			get
			{
				return latinLnBrkFieldSpecified;
			}
			set
			{
				latinLnBrkFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool hangingPunct
		{
			get
			{
				return hangingPunctField;
			}
			set
			{
				hangingPunctField = value;
			}
		}

		[XmlIgnore]
		public bool hangingPunctSpecified
		{
			get
			{
				return hangingPunctFieldSpecified;
			}
			set
			{
				hangingPunctFieldSpecified = value;
			}
		}

		public CT_TextParagraphProperties()
		{
			algn = ST_TextAlignType.l;
		}

		public static CT_TextParagraphProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextParagraphProperties cT_TextParagraphProperties = new CT_TextParagraphProperties();
			cT_TextParagraphProperties.marL = XmlHelper.ReadInt(node.Attributes["marL"]);
			cT_TextParagraphProperties.marR = XmlHelper.ReadInt(node.Attributes["marR"]);
			cT_TextParagraphProperties.lvl = XmlHelper.ReadInt(node.Attributes["lvl"]);
			cT_TextParagraphProperties.indent = XmlHelper.ReadInt(node.Attributes["indent"]);
			if (node.Attributes["algn"] != null)
			{
				cT_TextParagraphProperties.algn = (ST_TextAlignType)Enum.Parse(typeof(ST_TextAlignType), node.Attributes["algn"].Value);
			}
			else
			{
				cT_TextParagraphProperties.algn = ST_TextAlignType.l;
			}
			cT_TextParagraphProperties.defTabSz = XmlHelper.ReadInt(node.Attributes["defTabSz"]);
			cT_TextParagraphProperties.rtl = XmlHelper.ReadBool(node.Attributes["rtl"]);
			cT_TextParagraphProperties.eaLnBrk = XmlHelper.ReadBool(node.Attributes["eaLnBrk"]);
			if (node.Attributes["fontAlgn"] != null)
			{
				cT_TextParagraphProperties.fontAlgn = (ST_TextFontAlignType)Enum.Parse(typeof(ST_TextFontAlignType), node.Attributes["fontAlgn"].Value);
			}
			cT_TextParagraphProperties.latinLnBrk = XmlHelper.ReadBool(node.Attributes["latinLnBrk"]);
			cT_TextParagraphProperties.hangingPunct = XmlHelper.ReadBool(node.Attributes["hangingPunct"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "lnSpc")
				{
					cT_TextParagraphProperties.lnSpc = CT_TextSpacing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spcBef")
				{
					cT_TextParagraphProperties.spcBef = CT_TextSpacing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spcAft")
				{
					cT_TextParagraphProperties.spcAft = CT_TextSpacing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "buClrTx")
				{
					cT_TextParagraphProperties.buClrTx = new CT_TextBulletColorFollowText();
				}
				else if (childNode.LocalName == "buClr")
				{
					cT_TextParagraphProperties.buClr = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "buSzTx")
				{
					cT_TextParagraphProperties.buSzTx = new CT_TextBulletSizeFollowText();
				}
				else if (childNode.LocalName == "buSzPct")
				{
					cT_TextParagraphProperties.buSzPct = CT_TextBulletSizePercent.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "buSzPts")
				{
					cT_TextParagraphProperties.buSzPts = CT_TextBulletSizePoint.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "buFontTx")
				{
					cT_TextParagraphProperties.buFontTx = new CT_TextBulletTypefaceFollowText();
				}
				else if (childNode.LocalName == "buFont")
				{
					cT_TextParagraphProperties.buFont = CT_TextFont.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "buNone")
				{
					cT_TextParagraphProperties.buNone = new CT_TextNoBullet();
				}
				else if (childNode.LocalName == "buAutoNum")
				{
					cT_TextParagraphProperties.buAutoNum = CT_TextAutonumberBullet.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "buChar")
				{
					cT_TextParagraphProperties.buChar = CT_TextCharBullet.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "buBlip")
				{
					cT_TextParagraphProperties.buBlip = CT_TextBlipBullet.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "defRPr")
				{
					cT_TextParagraphProperties.defRPr = CT_TextCharacterProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_TextParagraphProperties.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tabLst")
				{
					cT_TextParagraphProperties.tabLst = CT_TextTabStopList.Parse(childNode, namespaceManager);
				}
			}
			return cT_TextParagraphProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "marL", marL);
			XmlHelper.WriteAttribute(sw, "marR", marR);
			XmlHelper.WriteAttribute(sw, "lvl", lvl, true);
			XmlHelper.WriteAttribute(sw, "indent", indent);
			if (algn != 0)
			{
				XmlHelper.WriteAttribute(sw, "algn", algn.ToString());
			}
			XmlHelper.WriteAttribute(sw, "defTabSz", defTabSz);
			XmlHelper.WriteAttribute(sw, "rtl", rtl, false);
			XmlHelper.WriteAttribute(sw, "eaLnBrk", eaLnBrk, false);
			if (fontAlgn != 0)
			{
				XmlHelper.WriteAttribute(sw, "fontAlgn", fontAlgn.ToString());
			}
			XmlHelper.WriteAttribute(sw, "latinLnBrk", latinLnBrk, false);
			XmlHelper.WriteAttribute(sw, "hangingPunct", hangingPunct, false);
			sw.Write(">");
			if (lnSpc != null)
			{
				lnSpc.Write(sw, "lnSpc");
			}
			if (spcBef != null)
			{
				spcBef.Write(sw, "spcBef");
			}
			if (spcAft != null)
			{
				spcAft.Write(sw, "spcAft");
			}
			if (buClrTx != null)
			{
				sw.Write("<a:buClrTx/>");
			}
			if (buClr != null)
			{
				buClr.Write(sw, "buClr");
			}
			if (buSzTx != null)
			{
				sw.Write("<a:buSzTx/>");
			}
			if (buSzPct != null)
			{
				buSzPct.Write(sw, "buSzPct");
			}
			if (buSzPts != null)
			{
				buSzPts.Write(sw, "buSzPts");
			}
			if (buFontTx != null)
			{
				sw.Write("<a:buFontTx/>");
			}
			if (buFont != null)
			{
				buFont.Write(sw, "buFont");
			}
			if (buNone != null)
			{
				sw.Write("<a:buNone/>");
			}
			if (buAutoNum != null)
			{
				buAutoNum.Write(sw, "buAutoNum");
			}
			if (buChar != null)
			{
				buChar.Write(sw, "buChar");
			}
			if (buBlip != null)
			{
				buBlip.Write(sw, "buBlip");
			}
			if (tabLstField != null)
			{
				tabLstField.Write(sw, "tabLst");
			}
			if (defRPr != null)
			{
				defRPr.Write(sw, "defRPr");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
