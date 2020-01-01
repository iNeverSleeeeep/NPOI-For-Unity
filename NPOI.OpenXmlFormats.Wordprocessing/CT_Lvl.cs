using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Lvl
	{
		private CT_DecimalNumber startField;

		private CT_NumFmt numFmtField;

		private CT_DecimalNumber lvlRestartField;

		private CT_String pStyleField;

		private CT_OnOff isLglField;

		private CT_LevelSuffix suffField;

		private CT_LevelText lvlTextField;

		private CT_DecimalNumber lvlPicBulletIdField;

		private CT_LvlLegacy legacyField;

		private CT_Jc lvlJcField;

		private CT_PPr pPrField;

		private CT_RPr rPrField;

		private string ilvlField;

		private byte[] tplcField;

		private ST_OnOff tentativeField;

		private bool tentativeFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_DecimalNumber start
		{
			get
			{
				return startField;
			}
			set
			{
				startField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_NumFmt numFmt
		{
			get
			{
				return numFmtField;
			}
			set
			{
				numFmtField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_DecimalNumber lvlRestart
		{
			get
			{
				return lvlRestartField;
			}
			set
			{
				lvlRestartField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_String pStyle
		{
			get
			{
				return pStyleField;
			}
			set
			{
				pStyleField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff isLgl
		{
			get
			{
				return isLglField;
			}
			set
			{
				isLglField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_LevelSuffix suff
		{
			get
			{
				return suffField;
			}
			set
			{
				suffField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_LevelText lvlText
		{
			get
			{
				return lvlTextField;
			}
			set
			{
				lvlTextField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_DecimalNumber lvlPicBulletId
		{
			get
			{
				return lvlPicBulletIdField;
			}
			set
			{
				lvlPicBulletIdField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_LvlLegacy legacy
		{
			get
			{
				return legacyField;
			}
			set
			{
				legacyField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Jc lvlJc
		{
			get
			{
				return lvlJcField;
			}
			set
			{
				lvlJcField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_PPr pPr
		{
			get
			{
				return pPrField;
			}
			set
			{
				pPrField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_RPr rPr
		{
			get
			{
				return rPrField;
			}
			set
			{
				rPrField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string ilvl
		{
			get
			{
				return ilvlField;
			}
			set
			{
				ilvlField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] tplc
		{
			get
			{
				return tplcField;
			}
			set
			{
				tplcField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff tentative
		{
			get
			{
				return tentativeField;
			}
			set
			{
				tentativeField = value;
				tentativeFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool tentativeSpecified
		{
			get
			{
				return tentativeFieldSpecified;
			}
			set
			{
				tentativeFieldSpecified = value;
			}
		}

		public CT_Lvl()
		{
			rPrField = new CT_RPr();
			pPrField = new CT_PPr();
			lvlJcField = new CT_Jc();
			lvlTextField = new CT_LevelText();
			numFmtField = new CT_NumFmt();
			startField = new CT_DecimalNumber();
		}

		public static CT_Lvl Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Lvl cT_Lvl = new CT_Lvl();
			cT_Lvl.ilvl = XmlHelper.ReadString(node.Attributes["w:ilvl"]);
			cT_Lvl.tplc = XmlHelper.ReadBytes(node.Attributes["w:tplc"]);
			if (node.Attributes["w:tentative"] != null)
			{
				cT_Lvl.tentative = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:tentative"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "start")
				{
					cT_Lvl.start = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_Lvl.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvlRestart")
				{
					cT_Lvl.lvlRestart = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pStyle")
				{
					cT_Lvl.pStyle = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "isLgl")
				{
					cT_Lvl.isLgl = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suff")
				{
					cT_Lvl.suff = CT_LevelSuffix.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvlText")
				{
					cT_Lvl.lvlText = CT_LevelText.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvlPicBulletId")
				{
					cT_Lvl.lvlPicBulletId = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "legacy")
				{
					cT_Lvl.legacy = CT_LvlLegacy.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvlJc")
				{
					cT_Lvl.lvlJc = CT_Jc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pPr")
				{
					cT_Lvl.pPr = CT_PPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rPr")
				{
					cT_Lvl.rPr = CT_RPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_Lvl;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:ilvl", ilvl);
			XmlHelper.WriteAttribute(sw, "w:tplc", tplc);
			if (tentative != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:tentative", tentative.ToString());
			}
			sw.Write(">");
			if (start != null)
			{
				start.Write(sw, "start");
			}
			if (numFmt != null)
			{
				numFmt.Write(sw, "numFmt");
			}
			if (lvlRestart != null)
			{
				lvlRestart.Write(sw, "lvlRestart");
			}
			if (pStyle != null)
			{
				pStyle.Write(sw, "pStyle");
			}
			if (isLgl != null)
			{
				isLgl.Write(sw, "isLgl");
			}
			if (suff != null)
			{
				suff.Write(sw, "suff");
			}
			if (lvlText != null)
			{
				lvlText.Write(sw, "lvlText");
			}
			if (lvlPicBulletId != null)
			{
				lvlPicBulletId.Write(sw, "lvlPicBulletId");
			}
			if (legacy != null)
			{
				legacy.Write(sw, "legacy");
			}
			if (lvlJc != null)
			{
				lvlJc.Write(sw, "lvlJc");
			}
			if (pPr != null)
			{
				pPr.Write(sw, "pPr");
			}
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
