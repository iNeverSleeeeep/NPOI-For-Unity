using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_ParaRPr
	{
		private CT_TrackChange insField;

		private CT_TrackChange delField;

		private CT_TrackChange moveFromField;

		private CT_TrackChange moveToField;

		private CT_String rStyleField;

		private CT_Fonts rFontsField;

		private CT_OnOff bField;

		private CT_OnOff bCsField;

		private CT_OnOff iField;

		private CT_OnOff iCsField;

		private CT_OnOff capsField;

		private CT_OnOff smallCapsField;

		private CT_OnOff strikeField;

		private CT_OnOff dstrikeField;

		private CT_OnOff outlineField;

		private CT_OnOff shadowField;

		private CT_OnOff embossField;

		private CT_OnOff imprintField;

		private CT_OnOff noProofField;

		private CT_OnOff snapToGridField;

		private CT_OnOff vanishField;

		private CT_OnOff webHiddenField;

		private CT_Color colorField;

		private CT_SignedTwipsMeasure spacingField;

		private CT_TextScale wField;

		private CT_HpsMeasure kernField;

		private CT_SignedHpsMeasure positionField;

		private CT_HpsMeasure szField;

		private CT_HpsMeasure szCsField;

		private CT_Highlight highlightField;

		private CT_Underline uField;

		private CT_TextEffect effectField;

		private CT_Border bdrField;

		private CT_Shd shdField;

		private CT_FitText fitTextField;

		private CT_VerticalAlignRun vertAlignField;

		private CT_OnOff rtlField;

		private CT_OnOff csField;

		private CT_Em emField;

		private CT_Language langField;

		private CT_EastAsianLayout eastAsianLayoutField;

		private CT_OnOff specVanishField;

		private CT_OnOff oMathField;

		private CT_ParaRPrChange rPrChangeField;

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
		public CT_String rStyle
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

		[XmlElement(Order = 5)]
		public CT_Fonts rFonts
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

		[XmlElement(Order = 6)]
		public CT_OnOff b
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

		[XmlElement(Order = 7)]
		public CT_OnOff bCs
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

		[XmlElement(Order = 8)]
		public CT_OnOff i
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

		[XmlElement(Order = 9)]
		public CT_OnOff iCs
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

		[XmlElement(Order = 10)]
		public CT_OnOff caps
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

		[XmlElement(Order = 11)]
		public CT_OnOff smallCaps
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

		[XmlElement(Order = 12)]
		public CT_OnOff strike
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

		[XmlElement(Order = 13)]
		public CT_OnOff dstrike
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

		[XmlElement(Order = 14)]
		public CT_OnOff outline
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

		[XmlElement(Order = 15)]
		public CT_OnOff shadow
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

		[XmlElement(Order = 16)]
		public CT_OnOff emboss
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

		[XmlElement(Order = 17)]
		public CT_OnOff imprint
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

		[XmlElement(Order = 18)]
		public CT_OnOff noProof
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

		[XmlElement(Order = 19)]
		public CT_OnOff snapToGrid
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

		[XmlElement(Order = 20)]
		public CT_OnOff vanish
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

		[XmlElement(Order = 21)]
		public CT_OnOff webHidden
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

		[XmlElement(Order = 22)]
		public CT_Color color
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

		[XmlElement(Order = 23)]
		public CT_SignedTwipsMeasure spacing
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

		[XmlElement(Order = 24)]
		public CT_TextScale w
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

		[XmlElement(Order = 25)]
		public CT_HpsMeasure kern
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

		[XmlElement(Order = 26)]
		public CT_SignedHpsMeasure position
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

		[XmlElement(Order = 27)]
		public CT_HpsMeasure sz
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

		[XmlElement(Order = 28)]
		public CT_HpsMeasure szCs
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

		[XmlElement(Order = 29)]
		public CT_Highlight highlight
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

		[XmlElement(Order = 30)]
		public CT_Underline u
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

		[XmlElement(Order = 31)]
		public CT_TextEffect effect
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

		[XmlElement(Order = 32)]
		public CT_Border bdr
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

		[XmlElement(Order = 33)]
		public CT_Shd shd
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

		[XmlElement(Order = 34)]
		public CT_FitText fitText
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

		[XmlElement(Order = 35)]
		public CT_VerticalAlignRun vertAlign
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

		[XmlElement(Order = 36)]
		public CT_OnOff rtl
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

		[XmlElement(Order = 37)]
		public CT_OnOff cs
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

		[XmlElement(Order = 38)]
		public CT_Em em
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

		[XmlElement(Order = 39)]
		public CT_Language lang
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

		[XmlElement(Order = 40)]
		public CT_EastAsianLayout eastAsianLayout
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

		[XmlElement(Order = 41)]
		public CT_OnOff specVanish
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

		[XmlElement(Order = 42)]
		public CT_OnOff oMath
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

		[XmlElement(Order = 43)]
		public CT_ParaRPrChange rPrChange
		{
			get
			{
				return rPrChangeField;
			}
			set
			{
				rPrChangeField = value;
			}
		}

		public CT_OnOff AddNewNoProof()
		{
			if (noProofField == null)
			{
				noProofField = new CT_OnOff();
			}
			return noProofField;
		}

		public static CT_ParaRPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ParaRPr cT_ParaRPr = new CT_ParaRPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ins")
				{
					cT_ParaRPr.ins = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "del")
				{
					cT_ParaRPr.del = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_ParaRPr.moveFrom = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_ParaRPr.moveTo = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rStyle")
				{
					cT_ParaRPr.rStyle = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rFonts")
				{
					cT_ParaRPr.rFonts = CT_Fonts.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "b")
				{
					cT_ParaRPr.b = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bCs")
				{
					cT_ParaRPr.bCs = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "i")
				{
					cT_ParaRPr.i = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "iCs")
				{
					cT_ParaRPr.iCs = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "caps")
				{
					cT_ParaRPr.caps = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "smallCaps")
				{
					cT_ParaRPr.smallCaps = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strike")
				{
					cT_ParaRPr.strike = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dstrike")
				{
					cT_ParaRPr.dstrike = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "outline")
				{
					cT_ParaRPr.outline = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shadow")
				{
					cT_ParaRPr.shadow = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "emboss")
				{
					cT_ParaRPr.emboss = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "imprint")
				{
					cT_ParaRPr.imprint = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noProof")
				{
					cT_ParaRPr.noProof = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "snapToGrid")
				{
					cT_ParaRPr.snapToGrid = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vanish")
				{
					cT_ParaRPr.vanish = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "webHidden")
				{
					cT_ParaRPr.webHidden = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "color")
				{
					cT_ParaRPr.color = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spacing")
				{
					cT_ParaRPr.spacing = CT_SignedTwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "w")
				{
					cT_ParaRPr.w = CT_TextScale.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "kern")
				{
					cT_ParaRPr.kern = CT_HpsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "position")
				{
					cT_ParaRPr.position = CT_SignedHpsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sz")
				{
					cT_ParaRPr.sz = CT_HpsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "szCs")
				{
					cT_ParaRPr.szCs = CT_HpsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "highlight")
				{
					cT_ParaRPr.highlight = CT_Highlight.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "u")
				{
					cT_ParaRPr.u = CT_Underline.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effect")
				{
					cT_ParaRPr.effect = CT_TextEffect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bdr")
				{
					cT_ParaRPr.bdr = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_ParaRPr.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fitText")
				{
					cT_ParaRPr.fitText = CT_FitText.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vertAlign")
				{
					cT_ParaRPr.vertAlign = CT_VerticalAlignRun.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rtl")
				{
					cT_ParaRPr.rtl = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cs")
				{
					cT_ParaRPr.cs = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "em")
				{
					cT_ParaRPr.em = CT_Em.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lang")
				{
					cT_ParaRPr.lang = CT_Language.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "eastAsianLayout")
				{
					cT_ParaRPr.eastAsianLayout = CT_EastAsianLayout.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "specVanish")
				{
					cT_ParaRPr.specVanish = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_ParaRPr.oMath = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rPrChange")
				{
					cT_ParaRPr.rPrChange = CT_ParaRPrChange.Parse(childNode, namespaceManager);
				}
			}
			return cT_ParaRPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
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
			if (rStyle != null)
			{
				rStyle.Write(sw, "rStyle");
			}
			if (rFonts != null)
			{
				rFonts.Write(sw, "rFonts");
			}
			if (b != null)
			{
				b.Write(sw, "b");
			}
			if (bCs != null)
			{
				bCs.Write(sw, "bCs");
			}
			if (i != null)
			{
				i.Write(sw, "i");
			}
			if (iCs != null)
			{
				iCs.Write(sw, "iCs");
			}
			if (caps != null)
			{
				caps.Write(sw, "caps");
			}
			if (smallCaps != null)
			{
				smallCaps.Write(sw, "smallCaps");
			}
			if (strike != null)
			{
				strike.Write(sw, "strike");
			}
			if (dstrike != null)
			{
				dstrike.Write(sw, "dstrike");
			}
			if (outline != null)
			{
				outline.Write(sw, "outline");
			}
			if (shadow != null)
			{
				shadow.Write(sw, "shadow");
			}
			if (emboss != null)
			{
				emboss.Write(sw, "emboss");
			}
			if (imprint != null)
			{
				imprint.Write(sw, "imprint");
			}
			if (noProof != null)
			{
				noProof.Write(sw, "noProof");
			}
			if (snapToGrid != null)
			{
				snapToGrid.Write(sw, "snapToGrid");
			}
			if (vanish != null)
			{
				vanish.Write(sw, "vanish");
			}
			if (webHidden != null)
			{
				webHidden.Write(sw, "webHidden");
			}
			if (color != null)
			{
				color.Write(sw, "color");
			}
			if (spacing != null)
			{
				spacing.Write(sw, "spacing");
			}
			if (w != null)
			{
				w.Write(sw, "w");
			}
			if (kern != null)
			{
				kern.Write(sw, "kern");
			}
			if (position != null)
			{
				position.Write(sw, "position");
			}
			if (sz != null)
			{
				sz.Write(sw, "sz");
			}
			if (szCs != null)
			{
				szCs.Write(sw, "szCs");
			}
			if (highlight != null)
			{
				highlight.Write(sw, "highlight");
			}
			if (u != null)
			{
				u.Write(sw, "u");
			}
			if (effect != null)
			{
				effect.Write(sw, "effect");
			}
			if (bdr != null)
			{
				bdr.Write(sw, "bdr");
			}
			if (shd != null)
			{
				shd.Write(sw, "shd");
			}
			if (fitText != null)
			{
				fitText.Write(sw, "fitText");
			}
			if (vertAlign != null)
			{
				vertAlign.Write(sw, "vertAlign");
			}
			if (rtl != null)
			{
				rtl.Write(sw, "rtl");
			}
			if (cs != null)
			{
				cs.Write(sw, "cs");
			}
			if (em != null)
			{
				em.Write(sw, "em");
			}
			if (lang != null)
			{
				lang.Write(sw, "lang");
			}
			if (eastAsianLayout != null)
			{
				eastAsianLayout.Write(sw, "eastAsianLayout");
			}
			if (specVanish != null)
			{
				specVanish.Write(sw, "specVanish");
			}
			if (oMath != null)
			{
				oMath.Write(sw, "oMath");
			}
			if (rPrChange != null)
			{
				rPrChange.Write(sw, "rPrChange");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
