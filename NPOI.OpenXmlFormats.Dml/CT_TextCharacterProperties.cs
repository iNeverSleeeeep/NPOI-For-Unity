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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TextCharacterProperties
	{
		private CT_LineProperties lnField;

		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_BlipFillProperties blipFillField;

		private CT_PatternFillProperties pattFillField;

		private CT_GroupFillProperties grpFillField;

		private CT_EffectList effectLstField;

		private CT_EffectContainer effectDagField;

		private CT_Color highlightField;

		private CT_TextUnderlineLineFollowText uLnTxField;

		private CT_LineProperties uLnField;

		private CT_TextUnderlineFillFollowText uFillTxField;

		private CT_TextUnderlineFillGroupWrapper uFillField;

		private CT_TextFont latinField;

		private CT_TextFont eaField;

		private CT_TextFont csField;

		private CT_TextFont symField;

		private CT_Hyperlink hlinkClickField;

		private CT_Hyperlink hlinkMouseOverField;

		private CT_OfficeArtExtensionList extLstField;

		private bool kumimojiField;

		private bool kumimojiFieldSpecified;

		private string langField;

		private string altLangField;

		private int szField;

		private bool szFieldSpecified;

		private bool bField;

		private bool bFieldSpecified;

		private bool iField;

		private bool iFieldSpecified;

		private ST_TextUnderlineType uField;

		private bool uFieldSpecified;

		private ST_TextStrikeType strikeField;

		private bool strikeFieldSpecified;

		private int kernField;

		private bool kernFieldSpecified;

		private ST_TextCapsType capField;

		private bool capFieldSpecified;

		private int spcField;

		private bool spcFieldSpecified;

		private bool normalizeHField;

		private bool normalizeHFieldSpecified;

		private int baselineField;

		private bool baselineFieldSpecified;

		private bool noProofField;

		private bool noProofFieldSpecified;

		private bool dirtyField = true;

		private bool errField;

		private bool smtCleanField = true;

		private uint smtIdField;

		private string bmkField;

		[XmlElement(Order = 0)]
		public CT_LineProperties ln
		{
			get
			{
				return lnField;
			}
			set
			{
				lnField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_NoFillProperties noFill
		{
			get
			{
				return noFillField;
			}
			set
			{
				noFillField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_SolidColorFillProperties solidFill
		{
			get
			{
				return solidFillField;
			}
			set
			{
				solidFillField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_GradientFillProperties gradFill
		{
			get
			{
				return gradFillField;
			}
			set
			{
				gradFillField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_BlipFillProperties blipFill
		{
			get
			{
				return blipFillField;
			}
			set
			{
				blipFillField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_PatternFillProperties pattFill
		{
			get
			{
				return pattFillField;
			}
			set
			{
				pattFillField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_GroupFillProperties grpFill
		{
			get
			{
				return grpFillField;
			}
			set
			{
				grpFillField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_EffectList effectLst
		{
			get
			{
				return effectLstField;
			}
			set
			{
				effectLstField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_EffectContainer effectDag
		{
			get
			{
				return effectDagField;
			}
			set
			{
				effectDagField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Color highlight
		{
			get
			{
				return highlightField;
			}
			set
			{
				highlightField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_TextUnderlineLineFollowText uLnTx
		{
			get
			{
				return uLnTxField;
			}
			set
			{
				uLnTxField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_LineProperties uLn
		{
			get
			{
				return uLnField;
			}
			set
			{
				uLnField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_TextUnderlineFillFollowText uFillTx
		{
			get
			{
				return uFillTxField;
			}
			set
			{
				uFillTxField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_TextUnderlineFillGroupWrapper uFill
		{
			get
			{
				return uFillField;
			}
			set
			{
				uFillField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_TextFont latin
		{
			get
			{
				return latinField;
			}
			set
			{
				latinField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_TextFont ea
		{
			get
			{
				return eaField;
			}
			set
			{
				eaField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_TextFont cs
		{
			get
			{
				return csField;
			}
			set
			{
				csField = value;
			}
		}

		[XmlElement(Order = 17)]
		public CT_TextFont sym
		{
			get
			{
				return symField;
			}
			set
			{
				symField = value;
			}
		}

		[XmlElement(Order = 18)]
		public CT_Hyperlink hlinkClick
		{
			get
			{
				return hlinkClickField;
			}
			set
			{
				hlinkClickField = value;
			}
		}

		[XmlElement(Order = 19)]
		public CT_Hyperlink hlinkMouseOver
		{
			get
			{
				return hlinkMouseOverField;
			}
			set
			{
				hlinkMouseOverField = value;
			}
		}

		[XmlElement(Order = 20)]
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
		public bool kumimoji
		{
			get
			{
				return kumimojiField;
			}
			set
			{
				kumimojiField = value;
			}
		}

		[XmlIgnore]
		public bool kumimojiSpecified
		{
			get
			{
				return kumimojiFieldSpecified;
			}
			set
			{
				kumimojiFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string lang
		{
			get
			{
				return langField;
			}
			set
			{
				langField = value;
			}
		}

		[XmlAttribute]
		public string altLang
		{
			get
			{
				return altLangField;
			}
			set
			{
				altLangField = value;
			}
		}

		[XmlAttribute]
		public int sz
		{
			get
			{
				return szField;
			}
			set
			{
				szField = value;
			}
		}

		[XmlIgnore]
		public bool szSpecified
		{
			get
			{
				return szFieldSpecified;
			}
			set
			{
				szFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool b
		{
			get
			{
				return bField;
			}
			set
			{
				bField = value;
			}
		}

		[XmlIgnore]
		public bool bSpecified
		{
			get
			{
				return bFieldSpecified;
			}
			set
			{
				bFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool i
		{
			get
			{
				return iField;
			}
			set
			{
				iField = value;
			}
		}

		[XmlIgnore]
		public bool iSpecified
		{
			get
			{
				return iFieldSpecified;
			}
			set
			{
				iFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextUnderlineType u
		{
			get
			{
				return uField;
			}
			set
			{
				uField = value;
			}
		}

		[XmlIgnore]
		public bool uSpecified
		{
			get
			{
				return uFieldSpecified;
			}
			set
			{
				uFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextStrikeType strike
		{
			get
			{
				return strikeField;
			}
			set
			{
				strikeField = value;
			}
		}

		[XmlIgnore]
		public bool strikeSpecified
		{
			get
			{
				return strikeFieldSpecified;
			}
			set
			{
				strikeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int kern
		{
			get
			{
				return kernField;
			}
			set
			{
				kernField = value;
			}
		}

		[XmlIgnore]
		public bool kernSpecified
		{
			get
			{
				return kernFieldSpecified;
			}
			set
			{
				kernFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextCapsType cap
		{
			get
			{
				return capField;
			}
			set
			{
				capField = value;
			}
		}

		[XmlIgnore]
		public bool capSpecified
		{
			get
			{
				return capFieldSpecified;
			}
			set
			{
				capFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int spc
		{
			get
			{
				return spcField;
			}
			set
			{
				spcField = value;
			}
		}

		[XmlIgnore]
		public bool spcSpecified
		{
			get
			{
				return spcFieldSpecified;
			}
			set
			{
				spcFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool normalizeH
		{
			get
			{
				return normalizeHField;
			}
			set
			{
				normalizeHField = value;
			}
		}

		[XmlIgnore]
		public bool normalizeHSpecified
		{
			get
			{
				return normalizeHFieldSpecified;
			}
			set
			{
				normalizeHFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int baseline
		{
			get
			{
				return baselineField;
			}
			set
			{
				baselineField = value;
			}
		}

		[XmlIgnore]
		public bool baselineSpecified
		{
			get
			{
				return baselineFieldSpecified;
			}
			set
			{
				baselineFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool noProof
		{
			get
			{
				return noProofField;
			}
			set
			{
				noProofField = value;
			}
		}

		[XmlIgnore]
		public bool noProofSpecified
		{
			get
			{
				return noProofFieldSpecified;
			}
			set
			{
				noProofFieldSpecified = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool dirty
		{
			get
			{
				return dirtyField;
			}
			set
			{
				dirtyField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool err
		{
			get
			{
				return errField;
			}
			set
			{
				errField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool smtClean
		{
			get
			{
				return smtCleanField;
			}
			set
			{
				smtCleanField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint smtId
		{
			get
			{
				return smtIdField;
			}
			set
			{
				smtIdField = value;
			}
		}

		[XmlAttribute]
		public string bmk
		{
			get
			{
				return bmkField;
			}
			set
			{
				bmkField = value;
			}
		}

		public static CT_TextCharacterProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextCharacterProperties cT_TextCharacterProperties = new CT_TextCharacterProperties();
			cT_TextCharacterProperties.kumimoji = XmlHelper.ReadBool(node.Attributes["kumimoji"]);
			cT_TextCharacterProperties.lang = XmlHelper.ReadString(node.Attributes["lang"]);
			cT_TextCharacterProperties.altLang = XmlHelper.ReadString(node.Attributes["altLang"]);
			cT_TextCharacterProperties.sz = XmlHelper.ReadInt(node.Attributes["sz"]);
			if (node.Attributes["b"] != null)
			{
				cT_TextCharacterProperties.b = XmlHelper.ReadBool(node.Attributes["b"]);
			}
			if (node.Attributes["i"] != null)
			{
				cT_TextCharacterProperties.i = XmlHelper.ReadBool(node.Attributes["i"]);
			}
			if (node.Attributes["u"] != null)
			{
				cT_TextCharacterProperties.u = (ST_TextUnderlineType)Enum.Parse(typeof(ST_TextUnderlineType), node.Attributes["u"].Value);
			}
			if (node.Attributes["strike"] != null)
			{
				cT_TextCharacterProperties.strike = (ST_TextStrikeType)Enum.Parse(typeof(ST_TextStrikeType), node.Attributes["strike"].Value);
			}
			cT_TextCharacterProperties.kern = XmlHelper.ReadInt(node.Attributes["kern"]);
			if (node.Attributes["cap"] != null)
			{
				cT_TextCharacterProperties.cap = (ST_TextCapsType)Enum.Parse(typeof(ST_TextCapsType), node.Attributes["cap"].Value);
			}
			cT_TextCharacterProperties.spc = XmlHelper.ReadInt(node.Attributes["spc"]);
			cT_TextCharacterProperties.normalizeH = XmlHelper.ReadBool(node.Attributes["normalizeH"]);
			cT_TextCharacterProperties.baseline = XmlHelper.ReadInt(node.Attributes["baseline"]);
			cT_TextCharacterProperties.noProof = XmlHelper.ReadBool(node.Attributes["noProof"]);
			if (node.Attributes["dirty"] != null)
			{
				cT_TextCharacterProperties.dirty = XmlHelper.ReadBool(node.Attributes["dirty"]);
			}
			cT_TextCharacterProperties.err = XmlHelper.ReadBool(node.Attributes["err"]);
			if (node.Attributes["smtClean"] != null)
			{
				cT_TextCharacterProperties.smtClean = XmlHelper.ReadBool(node.Attributes["smtClean"]);
			}
			cT_TextCharacterProperties.smtId = XmlHelper.ReadUInt(node.Attributes["smtId"]);
			cT_TextCharacterProperties.bmk = XmlHelper.ReadString(node.Attributes["bmk"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ln")
				{
					cT_TextCharacterProperties.ln = CT_LineProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noFill")
				{
					cT_TextCharacterProperties.noFill = new CT_NoFillProperties();
				}
				else if (childNode.LocalName == "solidFill")
				{
					cT_TextCharacterProperties.solidFill = CT_SolidColorFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gradFill")
				{
					cT_TextCharacterProperties.gradFill = CT_GradientFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "blipFill")
				{
					cT_TextCharacterProperties.blipFill = CT_BlipFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pattFill")
				{
					cT_TextCharacterProperties.pattFill = CT_PatternFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpFill")
				{
					cT_TextCharacterProperties.grpFill = new CT_GroupFillProperties();
				}
				else if (childNode.LocalName == "effectLst")
				{
					cT_TextCharacterProperties.effectLst = CT_EffectList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effectDag")
				{
					cT_TextCharacterProperties.effectDag = CT_EffectContainer.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "highlight")
				{
					cT_TextCharacterProperties.highlight = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "uLnTx")
				{
					cT_TextCharacterProperties.uLnTx = new CT_TextUnderlineLineFollowText();
				}
				else if (childNode.LocalName == "uLn")
				{
					cT_TextCharacterProperties.uLn = CT_LineProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "uFillTx")
				{
					cT_TextCharacterProperties.uFillTx = new CT_TextUnderlineFillFollowText();
				}
				else if (childNode.LocalName == "uFill")
				{
					cT_TextCharacterProperties.uFill = CT_TextUnderlineFillGroupWrapper.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "latin")
				{
					cT_TextCharacterProperties.latin = CT_TextFont.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ea")
				{
					cT_TextCharacterProperties.ea = CT_TextFont.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cs")
				{
					cT_TextCharacterProperties.cs = CT_TextFont.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sym")
				{
					cT_TextCharacterProperties.sym = CT_TextFont.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hlinkClick")
				{
					cT_TextCharacterProperties.hlinkClick = CT_Hyperlink.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hlinkMouseOver")
				{
					cT_TextCharacterProperties.hlinkMouseOver = CT_Hyperlink.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_TextCharacterProperties.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_TextCharacterProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "kumimoji", kumimoji, false);
			XmlHelper.WriteAttribute(sw, "lang", lang);
			XmlHelper.WriteAttribute(sw, "altLang", altLang);
			XmlHelper.WriteAttribute(sw, "sz", sz);
			XmlHelper.WriteAttribute(sw, "b", b);
			if (i)
			{
				XmlHelper.WriteAttribute(sw, "i", i);
			}
			if (u != 0)
			{
				XmlHelper.WriteAttribute(sw, "u", u.ToString());
			}
			if (strike != 0)
			{
				XmlHelper.WriteAttribute(sw, "strike", strike.ToString());
			}
			XmlHelper.WriteAttribute(sw, "kern", kern);
			if (cap != 0)
			{
				XmlHelper.WriteAttribute(sw, "cap", cap.ToString());
			}
			XmlHelper.WriteAttribute(sw, "spc", spc);
			XmlHelper.WriteAttribute(sw, "normalizeH", normalizeH, false);
			XmlHelper.WriteAttribute(sw, "baseline", baseline);
			XmlHelper.WriteAttribute(sw, "noProof", noProof, false);
			if (!dirty)
			{
				XmlHelper.WriteAttribute(sw, "dirty", dirty);
			}
			XmlHelper.WriteAttribute(sw, "err", err, false);
			if (!smtCleanField)
			{
				XmlHelper.WriteAttribute(sw, "smtClean", smtClean);
			}
			XmlHelper.WriteAttribute(sw, "smtId", smtId);
			XmlHelper.WriteAttribute(sw, "bmk", bmk);
			sw.Write(">");
			if (ln != null)
			{
				ln.Write(sw, "ln");
			}
			if (noFill != null)
			{
				sw.Write("<a:noFill/>");
			}
			if (solidFill != null)
			{
				solidFill.Write(sw, "solidFill");
			}
			if (gradFill != null)
			{
				gradFill.Write(sw, "gradFill");
			}
			if (blipFill != null)
			{
				blipFill.Write(sw, "a:blipFill");
			}
			if (pattFill != null)
			{
				pattFill.Write(sw, "pattFill");
			}
			if (grpFill != null)
			{
				sw.Write("<a:grpFill/>");
			}
			if (effectLst != null)
			{
				effectLst.Write(sw, "effectLst");
			}
			if (effectDag != null)
			{
				effectDag.Write(sw, "effectDag");
			}
			if (highlight != null)
			{
				highlight.Write(sw, "highlight");
			}
			if (uLnTx != null)
			{
				sw.Write("<a:uLnTx/>");
			}
			if (uLn != null)
			{
				uLn.Write(sw, "uLn");
			}
			if (uFillTx != null)
			{
				sw.Write("<a:uFillTx/>");
			}
			if (uFill != null)
			{
				uFill.Write(sw, "uFill");
			}
			if (latin != null)
			{
				latin.Write(sw, "latin");
			}
			if (ea != null)
			{
				ea.Write(sw, "ea");
			}
			if (cs != null)
			{
				cs.Write(sw, "cs");
			}
			if (sym != null)
			{
				sym.Write(sw, "sym");
			}
			if (hlinkClick != null)
			{
				hlinkClick.Write(sw, "hlinkClick");
			}
			if (hlinkMouseOver != null)
			{
				hlinkMouseOver.Write(sw, "hlinkMouseOver");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_TextCharacterProperties()
		{
			strike = ST_TextStrikeType.noStrike;
			dirtyField = true;
			errField = false;
			smtCleanField = true;
			smtIdField = 0u;
		}

		public CT_TextFont AddNewLatin()
		{
			latinField = new CT_TextFont();
			return latinField;
		}

		public bool IsSetSolidFill()
		{
			return solidFill != null;
		}

		public CT_SolidColorFillProperties AddNewSolidFill()
		{
			solidFillField = new CT_SolidColorFillProperties();
			return solidFillField;
		}
	}
}
