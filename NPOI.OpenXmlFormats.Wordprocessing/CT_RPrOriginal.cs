using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_RPrOriginal
	{
		private List<CT_SignedTwipsMeasure> spacingField;

		private List<CT_VerticalAlignRun> vertAlignField;

		private List<CT_TextScale> wField;

		private List<CT_OnOff> noProofField;

		private List<CT_OnOff> snapToGridField;

		private List<CT_Language> langField;

		private List<CT_HpsMeasure> kernField;

		private List<CT_OnOff> outlineField;

		private List<CT_SignedHpsMeasure> positionField;

		private List<CT_Fonts> rFontsField;

		private List<CT_String> rStyleField;

		private List<CT_OnOff> rtlField;

		private List<CT_OnOff> shadowField;

		private List<CT_OnOff> strikeField;

		private List<CT_Shd> shdField;

		private List<CT_HpsMeasure> szField;

		private List<CT_HpsMeasure> szCsField;

		private List<CT_OnOff> smallCapsField;

		private List<CT_Underline> uField;

		private List<CT_OnOff> vanishField;

		private List<CT_OnOff> oMathField;

		private List<CT_OnOff> webHiddenField;

		private List<CT_OnOff> specVanishField;

		private List<CT_OnOff> bField;

		private List<CT_OnOff> bCsField;

		private List<CT_Border> bdrField;

		private List<CT_OnOff> capsField;

		private List<CT_Color> colorField;

		private List<CT_OnOff> csField;

		private List<CT_OnOff> dstrikeField;

		private List<CT_EastAsianLayout> eastAsianLayoutField;

		private List<CT_TextEffect> effectField;

		private List<CT_Em> emField;

		private List<CT_OnOff> embossField;

		private List<CT_FitText> fitTextField;

		private List<CT_Highlight> highlightField;

		private List<CT_OnOff> iField;

		private List<CT_OnOff> iCsField;

		private List<CT_OnOff> imprintField;

		public List<CT_SignedTwipsMeasure> spacing
		{
			get
			{
				return spacingField;
			}
			set
			{
				spacingField = value;
			}
		}

		public List<CT_VerticalAlignRun> vertAlign
		{
			get
			{
				return vertAlignField;
			}
			set
			{
				vertAlignField = value;
			}
		}

		public List<CT_TextScale> w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		public List<CT_OnOff> noProof
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

		public List<CT_OnOff> snapToGrid
		{
			get
			{
				return snapToGridField;
			}
			set
			{
				snapToGridField = value;
			}
		}

		public List<CT_Language> lang
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

		public List<CT_HpsMeasure> kern
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

		public List<CT_OnOff> outline
		{
			get
			{
				return outlineField;
			}
			set
			{
				outlineField = value;
			}
		}

		public List<CT_SignedHpsMeasure> position
		{
			get
			{
				return positionField;
			}
			set
			{
				positionField = value;
			}
		}

		public List<CT_Fonts> rFonts
		{
			get
			{
				return rFontsField;
			}
			set
			{
				rFontsField = value;
			}
		}

		public List<CT_String> rStyle
		{
			get
			{
				return rStyleField;
			}
			set
			{
				rStyleField = value;
			}
		}

		public List<CT_OnOff> rtl
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

		public List<CT_OnOff> shadow
		{
			get
			{
				return shadowField;
			}
			set
			{
				shadowField = value;
			}
		}

		public List<CT_OnOff> strike
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

		public List<CT_Shd> shd
		{
			get
			{
				return shdField;
			}
			set
			{
				shdField = value;
			}
		}

		public List<CT_HpsMeasure> sz
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

		public List<CT_HpsMeasure> szCs
		{
			get
			{
				return szCsField;
			}
			set
			{
				szCsField = value;
			}
		}

		public List<CT_OnOff> smallCaps
		{
			get
			{
				return smallCapsField;
			}
			set
			{
				smallCapsField = value;
			}
		}

		public List<CT_Underline> u
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

		public List<CT_OnOff> vanish
		{
			get
			{
				return vanishField;
			}
			set
			{
				vanishField = value;
			}
		}

		public List<CT_OnOff> oMath
		{
			get
			{
				return oMathField;
			}
			set
			{
				oMathField = value;
			}
		}

		public List<CT_OnOff> webHidden
		{
			get
			{
				return webHiddenField;
			}
			set
			{
				webHiddenField = value;
			}
		}

		public List<CT_OnOff> specVanish
		{
			get
			{
				return specVanishField;
			}
			set
			{
				specVanishField = value;
			}
		}

		public List<CT_OnOff> b
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

		public List<CT_OnOff> bCs
		{
			get
			{
				return bCsField;
			}
			set
			{
				bCsField = value;
			}
		}

		public List<CT_Border> bdr
		{
			get
			{
				return bdrField;
			}
			set
			{
				bdrField = value;
			}
		}

		public List<CT_OnOff> caps
		{
			get
			{
				return capsField;
			}
			set
			{
				capsField = value;
			}
		}

		public List<CT_Color> color
		{
			get
			{
				return colorField;
			}
			set
			{
				colorField = value;
			}
		}

		public List<CT_OnOff> cs
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

		public List<CT_OnOff> dstrike
		{
			get
			{
				return dstrikeField;
			}
			set
			{
				dstrikeField = value;
			}
		}

		public List<CT_EastAsianLayout> eastAsianLayout
		{
			get
			{
				return eastAsianLayoutField;
			}
			set
			{
				eastAsianLayoutField = value;
			}
		}

		public List<CT_TextEffect> effect
		{
			get
			{
				return effectField;
			}
			set
			{
				effectField = value;
			}
		}

		public List<CT_Em> em
		{
			get
			{
				return emField;
			}
			set
			{
				emField = value;
			}
		}

		public List<CT_OnOff> emboss
		{
			get
			{
				return embossField;
			}
			set
			{
				embossField = value;
			}
		}

		public List<CT_FitText> fitText
		{
			get
			{
				return fitTextField;
			}
			set
			{
				fitTextField = value;
			}
		}

		public List<CT_Highlight> highlight
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

		public List<CT_OnOff> i
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

		public List<CT_OnOff> iCs
		{
			get
			{
				return iCsField;
			}
			set
			{
				iCsField = value;
			}
		}

		public List<CT_OnOff> imprint
		{
			get
			{
				return imprintField;
			}
			set
			{
				imprintField = value;
			}
		}

		public static CT_RPrOriginal Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RPrOriginal cT_RPrOriginal = new CT_RPrOriginal();
			cT_RPrOriginal.spacing = new List<CT_SignedTwipsMeasure>();
			cT_RPrOriginal.vertAlign = new List<CT_VerticalAlignRun>();
			cT_RPrOriginal.w = new List<CT_TextScale>();
			cT_RPrOriginal.noProof = new List<CT_OnOff>();
			cT_RPrOriginal.snapToGrid = new List<CT_OnOff>();
			cT_RPrOriginal.lang = new List<CT_Language>();
			cT_RPrOriginal.kern = new List<CT_HpsMeasure>();
			cT_RPrOriginal.outline = new List<CT_OnOff>();
			cT_RPrOriginal.position = new List<CT_SignedHpsMeasure>();
			cT_RPrOriginal.rFonts = new List<CT_Fonts>();
			cT_RPrOriginal.rStyle = new List<CT_String>();
			cT_RPrOriginal.rtl = new List<CT_OnOff>();
			cT_RPrOriginal.shadow = new List<CT_OnOff>();
			cT_RPrOriginal.strike = new List<CT_OnOff>();
			cT_RPrOriginal.shd = new List<CT_Shd>();
			cT_RPrOriginal.sz = new List<CT_HpsMeasure>();
			cT_RPrOriginal.szCs = new List<CT_HpsMeasure>();
			cT_RPrOriginal.smallCaps = new List<CT_OnOff>();
			cT_RPrOriginal.u = new List<CT_Underline>();
			cT_RPrOriginal.vanish = new List<CT_OnOff>();
			cT_RPrOriginal.oMath = new List<CT_OnOff>();
			cT_RPrOriginal.webHidden = new List<CT_OnOff>();
			cT_RPrOriginal.specVanish = new List<CT_OnOff>();
			cT_RPrOriginal.b = new List<CT_OnOff>();
			cT_RPrOriginal.bCs = new List<CT_OnOff>();
			cT_RPrOriginal.bdr = new List<CT_Border>();
			cT_RPrOriginal.caps = new List<CT_OnOff>();
			cT_RPrOriginal.color = new List<CT_Color>();
			cT_RPrOriginal.cs = new List<CT_OnOff>();
			cT_RPrOriginal.dstrike = new List<CT_OnOff>();
			cT_RPrOriginal.eastAsianLayout = new List<CT_EastAsianLayout>();
			cT_RPrOriginal.effect = new List<CT_TextEffect>();
			cT_RPrOriginal.em = new List<CT_Em>();
			cT_RPrOriginal.emboss = new List<CT_OnOff>();
			cT_RPrOriginal.fitText = new List<CT_FitText>();
			cT_RPrOriginal.highlight = new List<CT_Highlight>();
			cT_RPrOriginal.i = new List<CT_OnOff>();
			cT_RPrOriginal.iCs = new List<CT_OnOff>();
			cT_RPrOriginal.imprint = new List<CT_OnOff>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "spacing")
				{
					cT_RPrOriginal.spacing.Add(CT_SignedTwipsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "vertAlign")
				{
					cT_RPrOriginal.vertAlign.Add(CT_VerticalAlignRun.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "w")
				{
					cT_RPrOriginal.w.Add(CT_TextScale.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "noProof")
				{
					cT_RPrOriginal.noProof.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "snapToGrid")
				{
					cT_RPrOriginal.snapToGrid.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "lang")
				{
					cT_RPrOriginal.lang.Add(CT_Language.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "kern")
				{
					cT_RPrOriginal.kern.Add(CT_HpsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "outline")
				{
					cT_RPrOriginal.outline.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "position")
				{
					cT_RPrOriginal.position.Add(CT_SignedHpsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "rFonts")
				{
					cT_RPrOriginal.rFonts.Add(CT_Fonts.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "rStyle")
				{
					cT_RPrOriginal.rStyle.Add(CT_String.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "rtl")
				{
					cT_RPrOriginal.rtl.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "shadow")
				{
					cT_RPrOriginal.shadow.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "strike")
				{
					cT_RPrOriginal.strike.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "shd")
				{
					cT_RPrOriginal.shd.Add(CT_Shd.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "sz")
				{
					cT_RPrOriginal.sz.Add(CT_HpsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "szCs")
				{
					cT_RPrOriginal.szCs.Add(CT_HpsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "smallCaps")
				{
					cT_RPrOriginal.smallCaps.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "u")
				{
					cT_RPrOriginal.u.Add(CT_Underline.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "vanish")
				{
					cT_RPrOriginal.vanish.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_RPrOriginal.oMath.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "webHidden")
				{
					cT_RPrOriginal.webHidden.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "specVanish")
				{
					cT_RPrOriginal.specVanish.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "b")
				{
					cT_RPrOriginal.b.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "bCs")
				{
					cT_RPrOriginal.bCs.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "bdr")
				{
					cT_RPrOriginal.bdr.Add(CT_Border.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "caps")
				{
					cT_RPrOriginal.caps.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "color")
				{
					cT_RPrOriginal.color.Add(CT_Color.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "cs")
				{
					cT_RPrOriginal.cs.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "dstrike")
				{
					cT_RPrOriginal.dstrike.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "eastAsianLayout")
				{
					cT_RPrOriginal.eastAsianLayout.Add(CT_EastAsianLayout.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "effect")
				{
					cT_RPrOriginal.effect.Add(CT_TextEffect.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "em")
				{
					cT_RPrOriginal.em.Add(CT_Em.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "emboss")
				{
					cT_RPrOriginal.emboss.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "fitText")
				{
					cT_RPrOriginal.fitText.Add(CT_FitText.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "highlight")
				{
					cT_RPrOriginal.highlight.Add(CT_Highlight.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "i")
				{
					cT_RPrOriginal.i.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "iCs")
				{
					cT_RPrOriginal.iCs.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "imprint")
				{
					cT_RPrOriginal.imprint.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
			}
			return cT_RPrOriginal;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (spacing != null)
			{
				foreach (CT_SignedTwipsMeasure item in spacing)
				{
					item.Write(sw, "spacing");
				}
			}
			if (vertAlign != null)
			{
				foreach (CT_VerticalAlignRun item2 in vertAlign)
				{
					item2.Write(sw, "vertAlign");
				}
			}
			if (w != null)
			{
				foreach (CT_TextScale item3 in w)
				{
					item3.Write(sw, "w");
				}
			}
			if (noProof != null)
			{
				foreach (CT_OnOff item4 in noProof)
				{
					item4.Write(sw, "noProof");
				}
			}
			if (snapToGrid != null)
			{
				foreach (CT_OnOff item5 in snapToGrid)
				{
					item5.Write(sw, "snapToGrid");
				}
			}
			if (lang != null)
			{
				foreach (CT_Language item6 in lang)
				{
					item6.Write(sw, "lang");
				}
			}
			if (kern != null)
			{
				foreach (CT_HpsMeasure item7 in kern)
				{
					item7.Write(sw, "kern");
				}
			}
			if (outline != null)
			{
				foreach (CT_OnOff item8 in outline)
				{
					item8.Write(sw, "outline");
				}
			}
			if (position != null)
			{
				foreach (CT_SignedHpsMeasure item9 in position)
				{
					item9.Write(sw, "position");
				}
			}
			if (rFonts != null)
			{
				foreach (CT_Fonts rFont in rFonts)
				{
					rFont.Write(sw, "rFonts");
				}
			}
			if (rStyle != null)
			{
				foreach (CT_String item10 in rStyle)
				{
					item10.Write(sw, "rStyle");
				}
			}
			if (rtl != null)
			{
				foreach (CT_OnOff item11 in rtl)
				{
					item11.Write(sw, "rtl");
				}
			}
			if (shadow != null)
			{
				foreach (CT_OnOff item12 in shadow)
				{
					item12.Write(sw, "shadow");
				}
			}
			if (strike != null)
			{
				foreach (CT_OnOff item13 in strike)
				{
					item13.Write(sw, "strike");
				}
			}
			if (shd != null)
			{
				foreach (CT_Shd item14 in shd)
				{
					item14.Write(sw, "shd");
				}
			}
			if (sz != null)
			{
				foreach (CT_HpsMeasure item15 in sz)
				{
					item15.Write(sw, "sz");
				}
			}
			if (szCs != null)
			{
				foreach (CT_HpsMeasure szC in szCs)
				{
					szC.Write(sw, "szCs");
				}
			}
			if (smallCaps != null)
			{
				foreach (CT_OnOff smallCap in smallCaps)
				{
					smallCap.Write(sw, "smallCaps");
				}
			}
			if (u != null)
			{
				foreach (CT_Underline item16 in u)
				{
					item16.Write(sw, "u");
				}
			}
			if (vanish != null)
			{
				foreach (CT_OnOff item17 in vanish)
				{
					item17.Write(sw, "vanish");
				}
			}
			if (oMath != null)
			{
				foreach (CT_OnOff item18 in oMath)
				{
					item18.Write(sw, "oMath");
				}
			}
			if (webHidden != null)
			{
				foreach (CT_OnOff item19 in webHidden)
				{
					item19.Write(sw, "webHidden");
				}
			}
			if (specVanish != null)
			{
				foreach (CT_OnOff item20 in specVanish)
				{
					item20.Write(sw, "specVanish");
				}
			}
			if (b != null)
			{
				foreach (CT_OnOff item21 in b)
				{
					item21.Write(sw, "b");
				}
			}
			if (bCs != null)
			{
				foreach (CT_OnOff bC in bCs)
				{
					bC.Write(sw, "bCs");
				}
			}
			if (bdr != null)
			{
				foreach (CT_Border item22 in bdr)
				{
					item22.Write(sw, "bdr");
				}
			}
			if (caps != null)
			{
				foreach (CT_OnOff cap in caps)
				{
					cap.Write(sw, "caps");
				}
			}
			if (color != null)
			{
				foreach (CT_Color item23 in color)
				{
					item23.Write(sw, "color");
				}
			}
			if (cs != null)
			{
				foreach (CT_OnOff c in cs)
				{
					c.Write(sw, "cs");
				}
			}
			if (dstrike != null)
			{
				foreach (CT_OnOff item24 in dstrike)
				{
					item24.Write(sw, "dstrike");
				}
			}
			if (eastAsianLayout != null)
			{
				foreach (CT_EastAsianLayout item25 in eastAsianLayout)
				{
					item25.Write(sw, "eastAsianLayout");
				}
			}
			if (effect != null)
			{
				foreach (CT_TextEffect item26 in effect)
				{
					item26.Write(sw, "effect");
				}
			}
			if (em != null)
			{
				foreach (CT_Em item27 in em)
				{
					item27.Write(sw, "em");
				}
			}
			if (emboss != null)
			{
				foreach (CT_OnOff item28 in emboss)
				{
					item28.Write(sw, "emboss");
				}
			}
			if (fitText != null)
			{
				foreach (CT_FitText item29 in fitText)
				{
					item29.Write(sw, "fitText");
				}
			}
			if (highlight != null)
			{
				foreach (CT_Highlight item30 in highlight)
				{
					item30.Write(sw, "highlight");
				}
			}
			if (i != null)
			{
				foreach (CT_OnOff item31 in i)
				{
					item31.Write(sw, "i");
				}
			}
			if (iCs != null)
			{
				foreach (CT_OnOff iC in iCs)
				{
					iC.Write(sw, "iCs");
				}
			}
			if (imprint != null)
			{
				foreach (CT_OnOff item32 in imprint)
				{
					item32.Write(sw, "imprint");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
