using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_ParaRPrOriginal
	{
		private CT_TrackChange insField;

		private CT_TrackChange delField;

		private CT_TrackChange moveFromField;

		private CT_TrackChange moveToField;

		private List<CT_OnOff> webHiddenField;

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

		private List<CT_HpsMeasure> kernField;

		private List<CT_Language> langField;

		private List<CT_OnOff> noProofField;

		private List<CT_OnOff> oMathField;

		private List<CT_OnOff> outlineField;

		private List<CT_SignedHpsMeasure> positionField;

		private List<CT_Fonts> rFontsField;

		private List<CT_String> rStyleField;

		private List<CT_OnOff> rtlField;

		private List<CT_OnOff> shadowField;

		private List<CT_Shd> shdField;

		private List<CT_OnOff> smallCapsField;

		private List<CT_OnOff> snapToGridField;

		private List<CT_SignedTwipsMeasure> spacingField;

		private List<CT_OnOff> specVanishField;

		private List<CT_OnOff> strikeField;

		private List<CT_HpsMeasure> szField;

		private List<CT_HpsMeasure> szCsField;

		private List<CT_Underline> uField;

		private List<CT_OnOff> vanishField;

		private List<CT_VerticalAlignRun> vertAlignField;

		private List<CT_TextScale> wField;

		[XmlElement(Order = 0)]
		public CT_TrackChange ins
		{
			get
			{
				return insField;
			}
			set
			{
				insField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TrackChange del
		{
			get
			{
				return delField;
			}
			set
			{
				delField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TrackChange moveFrom
		{
			get
			{
				return moveFromField;
			}
			set
			{
				moveFromField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_TrackChange moveTo
		{
			get
			{
				return moveToField;
			}
			set
			{
				moveToField = value;
			}
		}

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 4)]
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

		public static CT_ParaRPrOriginal Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ParaRPrOriginal cT_ParaRPrOriginal = new CT_ParaRPrOriginal();
			cT_ParaRPrOriginal.webHidden = new List<CT_OnOff>();
			cT_ParaRPrOriginal.b = new List<CT_OnOff>();
			cT_ParaRPrOriginal.bCs = new List<CT_OnOff>();
			cT_ParaRPrOriginal.bdr = new List<CT_Border>();
			cT_ParaRPrOriginal.caps = new List<CT_OnOff>();
			cT_ParaRPrOriginal.color = new List<CT_Color>();
			cT_ParaRPrOriginal.cs = new List<CT_OnOff>();
			cT_ParaRPrOriginal.dstrike = new List<CT_OnOff>();
			cT_ParaRPrOriginal.eastAsianLayout = new List<CT_EastAsianLayout>();
			cT_ParaRPrOriginal.effect = new List<CT_TextEffect>();
			cT_ParaRPrOriginal.em = new List<CT_Em>();
			cT_ParaRPrOriginal.emboss = new List<CT_OnOff>();
			cT_ParaRPrOriginal.fitText = new List<CT_FitText>();
			cT_ParaRPrOriginal.highlight = new List<CT_Highlight>();
			cT_ParaRPrOriginal.i = new List<CT_OnOff>();
			cT_ParaRPrOriginal.iCs = new List<CT_OnOff>();
			cT_ParaRPrOriginal.imprint = new List<CT_OnOff>();
			cT_ParaRPrOriginal.kern = new List<CT_HpsMeasure>();
			cT_ParaRPrOriginal.lang = new List<CT_Language>();
			cT_ParaRPrOriginal.noProof = new List<CT_OnOff>();
			cT_ParaRPrOriginal.oMath = new List<CT_OnOff>();
			cT_ParaRPrOriginal.outline = new List<CT_OnOff>();
			cT_ParaRPrOriginal.position = new List<CT_SignedHpsMeasure>();
			cT_ParaRPrOriginal.rFonts = new List<CT_Fonts>();
			cT_ParaRPrOriginal.rStyle = new List<CT_String>();
			cT_ParaRPrOriginal.rtl = new List<CT_OnOff>();
			cT_ParaRPrOriginal.shadow = new List<CT_OnOff>();
			cT_ParaRPrOriginal.shd = new List<CT_Shd>();
			cT_ParaRPrOriginal.smallCaps = new List<CT_OnOff>();
			cT_ParaRPrOriginal.snapToGrid = new List<CT_OnOff>();
			cT_ParaRPrOriginal.spacing = new List<CT_SignedTwipsMeasure>();
			cT_ParaRPrOriginal.specVanish = new List<CT_OnOff>();
			cT_ParaRPrOriginal.strike = new List<CT_OnOff>();
			cT_ParaRPrOriginal.sz = new List<CT_HpsMeasure>();
			cT_ParaRPrOriginal.szCs = new List<CT_HpsMeasure>();
			cT_ParaRPrOriginal.u = new List<CT_Underline>();
			cT_ParaRPrOriginal.vanish = new List<CT_OnOff>();
			cT_ParaRPrOriginal.vertAlign = new List<CT_VerticalAlignRun>();
			cT_ParaRPrOriginal.w = new List<CT_TextScale>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ins")
				{
					cT_ParaRPrOriginal.ins = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "del")
				{
					cT_ParaRPrOriginal.del = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_ParaRPrOriginal.moveFrom = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_ParaRPrOriginal.moveTo = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "webHidden")
				{
					cT_ParaRPrOriginal.webHidden.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "b")
				{
					cT_ParaRPrOriginal.b.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "bCs")
				{
					cT_ParaRPrOriginal.bCs.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "bdr")
				{
					cT_ParaRPrOriginal.bdr.Add(CT_Border.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "caps")
				{
					cT_ParaRPrOriginal.caps.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "color")
				{
					cT_ParaRPrOriginal.color.Add(CT_Color.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "cs")
				{
					cT_ParaRPrOriginal.cs.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "dstrike")
				{
					cT_ParaRPrOriginal.dstrike.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "eastAsianLayout")
				{
					cT_ParaRPrOriginal.eastAsianLayout.Add(CT_EastAsianLayout.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "effect")
				{
					cT_ParaRPrOriginal.effect.Add(CT_TextEffect.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "em")
				{
					cT_ParaRPrOriginal.em.Add(CT_Em.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "emboss")
				{
					cT_ParaRPrOriginal.emboss.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "fitText")
				{
					cT_ParaRPrOriginal.fitText.Add(CT_FitText.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "highlight")
				{
					cT_ParaRPrOriginal.highlight.Add(CT_Highlight.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "i")
				{
					cT_ParaRPrOriginal.i.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "iCs")
				{
					cT_ParaRPrOriginal.iCs.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "imprint")
				{
					cT_ParaRPrOriginal.imprint.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "kern")
				{
					cT_ParaRPrOriginal.kern.Add(CT_HpsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "lang")
				{
					cT_ParaRPrOriginal.lang.Add(CT_Language.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "noProof")
				{
					cT_ParaRPrOriginal.noProof.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_ParaRPrOriginal.oMath.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "outline")
				{
					cT_ParaRPrOriginal.outline.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "position")
				{
					cT_ParaRPrOriginal.position.Add(CT_SignedHpsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "rFonts")
				{
					cT_ParaRPrOriginal.rFonts.Add(CT_Fonts.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "rStyle")
				{
					cT_ParaRPrOriginal.rStyle.Add(CT_String.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "rtl")
				{
					cT_ParaRPrOriginal.rtl.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "shadow")
				{
					cT_ParaRPrOriginal.shadow.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "shd")
				{
					cT_ParaRPrOriginal.shd.Add(CT_Shd.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "smallCaps")
				{
					cT_ParaRPrOriginal.smallCaps.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "snapToGrid")
				{
					cT_ParaRPrOriginal.snapToGrid.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "spacing")
				{
					cT_ParaRPrOriginal.spacing.Add(CT_SignedTwipsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "specVanish")
				{
					cT_ParaRPrOriginal.specVanish.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "strike")
				{
					cT_ParaRPrOriginal.strike.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "sz")
				{
					cT_ParaRPrOriginal.sz.Add(CT_HpsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "szCs")
				{
					cT_ParaRPrOriginal.szCs.Add(CT_HpsMeasure.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "u")
				{
					cT_ParaRPrOriginal.u.Add(CT_Underline.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "vanish")
				{
					cT_ParaRPrOriginal.vanish.Add(CT_OnOff.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "vertAlign")
				{
					cT_ParaRPrOriginal.vertAlign.Add(CT_VerticalAlignRun.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "w")
				{
					cT_ParaRPrOriginal.w.Add(CT_TextScale.Parse(childNode, namespaceManager));
				}
			}
			return cT_ParaRPrOriginal;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}>", nodeName));
			if (ins != null)
			{
				ins.Write(sw, "ins");
			}
			if (del != null)
			{
				del.Write(sw, "del");
			}
			if (moveFrom != null)
			{
				moveFrom.Write(sw, "moveFrom");
			}
			if (moveTo != null)
			{
				moveTo.Write(sw, "moveTo");
			}
			if (webHidden != null)
			{
				foreach (CT_OnOff item in webHidden)
				{
					item.Write(sw, "webHidden");
				}
			}
			if (b != null)
			{
				foreach (CT_OnOff item2 in b)
				{
					item2.Write(sw, "b");
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
				foreach (CT_Border item3 in bdr)
				{
					item3.Write(sw, "bdr");
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
				foreach (CT_Color item4 in color)
				{
					item4.Write(sw, "color");
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
				foreach (CT_OnOff item5 in dstrike)
				{
					item5.Write(sw, "dstrike");
				}
			}
			if (eastAsianLayout != null)
			{
				foreach (CT_EastAsianLayout item6 in eastAsianLayout)
				{
					item6.Write(sw, "eastAsianLayout");
				}
			}
			if (effect != null)
			{
				foreach (CT_TextEffect item7 in effect)
				{
					item7.Write(sw, "effect");
				}
			}
			if (em != null)
			{
				foreach (CT_Em item8 in em)
				{
					item8.Write(sw, "em");
				}
			}
			if (emboss != null)
			{
				foreach (CT_OnOff item9 in emboss)
				{
					item9.Write(sw, "emboss");
				}
			}
			if (fitText != null)
			{
				foreach (CT_FitText item10 in fitText)
				{
					item10.Write(sw, "fitText");
				}
			}
			if (highlight != null)
			{
				foreach (CT_Highlight item11 in highlight)
				{
					item11.Write(sw, "highlight");
				}
			}
			if (i != null)
			{
				foreach (CT_OnOff item12 in i)
				{
					item12.Write(sw, "i");
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
				foreach (CT_OnOff item13 in imprint)
				{
					item13.Write(sw, "imprint");
				}
			}
			if (kern != null)
			{
				foreach (CT_HpsMeasure item14 in kern)
				{
					item14.Write(sw, "kern");
				}
			}
			if (lang != null)
			{
				foreach (CT_Language item15 in lang)
				{
					item15.Write(sw, "lang");
				}
			}
			if (noProof != null)
			{
				foreach (CT_OnOff item16 in noProof)
				{
					item16.Write(sw, "noProof");
				}
			}
			if (oMath != null)
			{
				foreach (CT_OnOff item17 in oMath)
				{
					item17.Write(sw, "oMath");
				}
			}
			if (outline != null)
			{
				foreach (CT_OnOff item18 in outline)
				{
					item18.Write(sw, "outline");
				}
			}
			if (position != null)
			{
				foreach (CT_SignedHpsMeasure item19 in position)
				{
					item19.Write(sw, "position");
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
				foreach (CT_String item20 in rStyle)
				{
					item20.Write(sw, "rStyle");
				}
			}
			if (rtl != null)
			{
				foreach (CT_OnOff item21 in rtl)
				{
					item21.Write(sw, "rtl");
				}
			}
			if (shadow != null)
			{
				foreach (CT_OnOff item22 in shadow)
				{
					item22.Write(sw, "shadow");
				}
			}
			if (shd != null)
			{
				foreach (CT_Shd item23 in shd)
				{
					item23.Write(sw, "shd");
				}
			}
			if (smallCaps != null)
			{
				foreach (CT_OnOff smallCap in smallCaps)
				{
					smallCap.Write(sw, "smallCaps");
				}
			}
			if (snapToGrid != null)
			{
				foreach (CT_OnOff item24 in snapToGrid)
				{
					item24.Write(sw, "snapToGrid");
				}
			}
			if (spacing != null)
			{
				foreach (CT_SignedTwipsMeasure item25 in spacing)
				{
					item25.Write(sw, "spacing");
				}
			}
			if (specVanish != null)
			{
				foreach (CT_OnOff item26 in specVanish)
				{
					item26.Write(sw, "specVanish");
				}
			}
			if (strike != null)
			{
				foreach (CT_OnOff item27 in strike)
				{
					item27.Write(sw, "strike");
				}
			}
			if (sz != null)
			{
				foreach (CT_HpsMeasure item28 in sz)
				{
					item28.Write(sw, "sz");
				}
			}
			if (szCs != null)
			{
				foreach (CT_HpsMeasure szC in szCs)
				{
					szC.Write(sw, "szCs");
				}
			}
			if (u != null)
			{
				foreach (CT_Underline item29 in u)
				{
					item29.Write(sw, "u");
				}
			}
			if (vanish != null)
			{
				foreach (CT_OnOff item30 in vanish)
				{
					item30.Write(sw, "vanish");
				}
			}
			if (vertAlign != null)
			{
				foreach (CT_VerticalAlignRun item31 in vertAlign)
				{
					item31.Write(sw, "vertAlign");
				}
			}
			if (w != null)
			{
				foreach (CT_TextScale item32 in w)
				{
					item32.Write(sw, "w");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
