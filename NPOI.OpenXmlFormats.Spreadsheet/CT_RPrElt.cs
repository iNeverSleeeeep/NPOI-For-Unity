using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_RPrElt
	{
		private CT_FontName rFontField;

		private CT_IntProperty charsetField;

		private CT_IntProperty familyField;

		private CT_BooleanProperty bField;

		private CT_BooleanProperty iField;

		private CT_BooleanProperty strikeField;

		private CT_BooleanProperty outlineField;

		private CT_BooleanProperty shadowField;

		private CT_BooleanProperty condenseField;

		private CT_BooleanProperty extendField;

		private CT_Color colorField;

		private CT_FontSize szField;

		private CT_UnderlineProperty uField;

		private CT_VerticalAlignFontProperty vertAlignField;

		private CT_FontScheme schemeField;

		[XmlElement]
		public CT_FontName rFont
		{
			get
			{
				return rFontField;
			}
			set
			{
				rFontField = value;
			}
		}

		[XmlIgnore]
		public bool rFontSpecified
		{
			get
			{
				return null != rFontField;
			}
		}

		[XmlElement]
		public CT_IntProperty charset
		{
			get
			{
				return charsetField;
			}
			set
			{
				charsetField = value;
			}
		}

		[XmlIgnore]
		public bool charsetSpecified
		{
			get
			{
				return null != charsetField;
			}
		}

		[XmlElement]
		public CT_IntProperty family
		{
			get
			{
				return familyField;
			}
			set
			{
				familyField = value;
			}
		}

		[XmlIgnore]
		public bool familySpecified
		{
			get
			{
				return null != familyField;
			}
		}

		[XmlElement]
		public CT_BooleanProperty b
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
				return null != bField;
			}
		}

		[XmlElement]
		public CT_BooleanProperty i
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
				return null != iField;
			}
		}

		[XmlElement]
		public CT_BooleanProperty strike
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
				return null != strikeField;
			}
		}

		[XmlElement]
		public CT_BooleanProperty outline
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

		[XmlIgnore]
		public bool outlineSpecified
		{
			get
			{
				return null != outlineField;
			}
		}

		[XmlElement]
		public CT_BooleanProperty shadow
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

		[XmlIgnore]
		public bool shadowSpecified
		{
			get
			{
				return null != shadowField;
			}
		}

		[XmlElement]
		public CT_BooleanProperty condense
		{
			get
			{
				return condenseField;
			}
			set
			{
				condenseField = value;
			}
		}

		[XmlIgnore]
		public bool condenseSpecified
		{
			get
			{
				return null != condenseField;
			}
		}

		[XmlElement]
		public CT_BooleanProperty extend
		{
			get
			{
				return extendField;
			}
			set
			{
				extendField = value;
			}
		}

		[XmlIgnore]
		public bool extendSpecified
		{
			get
			{
				return null != extendField;
			}
		}

		[XmlElement]
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

		[XmlIgnore]
		public bool colorSpecified
		{
			get
			{
				return null != colorField;
			}
		}

		[XmlElement]
		public CT_FontSize sz
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
				return null != szField;
			}
		}

		[XmlElement]
		public CT_UnderlineProperty u
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
				return null != uField;
			}
		}

		[XmlElement]
		public CT_VerticalAlignFontProperty vertAlign
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

		[XmlIgnore]
		public bool vertAlignSpecified
		{
			get
			{
				return null != vertAlignField;
			}
		}

		[XmlElement]
		public CT_FontScheme scheme
		{
			get
			{
				return schemeField;
			}
			set
			{
				schemeField = value;
			}
		}

		[XmlIgnore]
		public bool schemeSpecified
		{
			get
			{
				return null != schemeField;
			}
		}

		public static CT_RPrElt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RPrElt cT_RPrElt = new CT_RPrElt();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rFont")
				{
					cT_RPrElt.rFont = CT_FontName.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "charset")
				{
					cT_RPrElt.charset = CT_IntProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "family")
				{
					cT_RPrElt.family = CT_IntProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "b")
				{
					cT_RPrElt.b = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "i")
				{
					cT_RPrElt.i = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strike")
				{
					cT_RPrElt.strike = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "outline")
				{
					cT_RPrElt.outline = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shadow")
				{
					cT_RPrElt.shadow = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "condense")
				{
					cT_RPrElt.condense = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extend")
				{
					cT_RPrElt.extend = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "color")
				{
					cT_RPrElt.color = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sz")
				{
					cT_RPrElt.sz = CT_FontSize.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "u")
				{
					cT_RPrElt.u = CT_UnderlineProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vertAlign")
				{
					cT_RPrElt.vertAlign = CT_VerticalAlignFontProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "scheme")
				{
					cT_RPrElt.scheme = CT_FontScheme.Parse(childNode, namespaceManager);
				}
			}
			return cT_RPrElt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (sz != null)
			{
				sz.Write(sw, "sz");
			}
			if (color != null)
			{
				color.Write(sw, "color");
			}
			if (rFont != null)
			{
				rFont.Write(sw, "rFont");
			}
			if (family != null)
			{
				family.Write(sw, "family");
			}
			if (charset != null)
			{
				charset.Write(sw, "charset");
			}
			if (b != null)
			{
				b.Write(sw, "b");
			}
			if (i != null)
			{
				i.Write(sw, "i");
			}
			if (strike != null)
			{
				strike.Write(sw, "strike");
			}
			if (outline != null)
			{
				outline.Write(sw, "outline");
			}
			if (shadow != null)
			{
				shadow.Write(sw, "shadow");
			}
			if (condense != null)
			{
				condense.Write(sw, "condense");
			}
			if (extend != null)
			{
				extend.Write(sw, "extend");
			}
			if (u != null)
			{
				u.Write(sw, "u");
			}
			if (vertAlign != null)
			{
				vertAlign.Write(sw, "vertAlign");
			}
			if (scheme != null)
			{
				scheme.Write(sw, "scheme");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public int sizeOfRFontArray()
		{
			if (rFontField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_FontName AddNewRFont()
		{
			rFontField = new CT_FontName();
			return rFontField;
		}

		public CT_FontName GetRFontArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return rFontField;
		}

		public int sizeOfCharsetArray()
		{
			if (charsetField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_IntProperty AddNewCharset()
		{
			charsetField = new CT_IntProperty();
			return charsetField;
		}

		public CT_IntProperty GetCharsetArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return charsetField;
		}

		public int sizeOfFamilyArray()
		{
			if (familyField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_IntProperty AddNewFamily()
		{
			familyField = new CT_IntProperty();
			return familyField;
		}

		public CT_IntProperty GetFamilyArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return familyField;
		}

		public int sizeOfBArray()
		{
			if (bField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_BooleanProperty AddNewB()
		{
			bField = new CT_BooleanProperty();
			return bField;
		}

		public void SetBArray(CT_BooleanProperty[] array)
		{
			bField = ((array.Length > 0) ? array[0] : null);
		}

		public CT_BooleanProperty GetBArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return bField;
		}

		public int sizeOfIArray()
		{
			if (iField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_BooleanProperty AddNewI()
		{
			iField = new CT_BooleanProperty();
			return iField;
		}

		public void SetIArray(CT_BooleanProperty[] array)
		{
			iField = ((array.Length > 0) ? array[0] : null);
		}

		public CT_BooleanProperty GetIArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return iField;
		}

		public int sizeOfStrikeArray()
		{
			if (strikeField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_BooleanProperty AddNewStrike()
		{
			strikeField = new CT_BooleanProperty();
			return strikeField;
		}

		public void SetStrikeArray(CT_BooleanProperty[] array)
		{
			strikeField = ((array.Length > 0) ? array[0] : null);
		}

		public CT_BooleanProperty GetStrikeArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return strikeField;
		}

		public int sizeOfOutlineArray()
		{
			if (outlineField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_BooleanProperty AddNewOutline()
		{
			outlineField = new CT_BooleanProperty();
			return outlineField;
		}

		public void SetOutlineArray(CT_BooleanProperty[] array)
		{
			outlineField = ((array.Length > 0) ? array[0] : null);
		}

		public CT_BooleanProperty GetOutlineArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return outlineField;
		}

		public int sizeOfShadowArray()
		{
			if (shadowField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_BooleanProperty AddNewShadow()
		{
			shadowField = new CT_BooleanProperty();
			return shadowField;
		}

		public CT_BooleanProperty GetShadowArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return shadowField;
		}

		public int sizeOfCondenseArray()
		{
			if (condenseField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_BooleanProperty AddNewCondense()
		{
			condenseField = new CT_BooleanProperty();
			return condenseField;
		}

		public CT_BooleanProperty GetCondenseArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return condenseField;
		}

		public int sizeOfExtendArray()
		{
			if (extendField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_BooleanProperty AddNewExtend()
		{
			extendField = new CT_BooleanProperty();
			return extendField;
		}

		public CT_BooleanProperty GetExtendArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return extendField;
		}

		public int sizeOfColorArray()
		{
			if (colorField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_Color GetColorArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return colorField;
		}

		public void SetColorArray(CT_Color[] array)
		{
			colorField = ((array.Length > 0) ? array[0] : null);
		}

		public CT_Color AddNewColor()
		{
			colorField = new CT_Color();
			return colorField;
		}

		public int sizeOfSzArray()
		{
			if (szField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_FontSize AddNewSz()
		{
			szField = new CT_FontSize();
			return szField;
		}

		public void SetSzArray(CT_FontSize[] array)
		{
			szField = ((array.Length > 0) ? array[0] : null);
		}

		public CT_FontSize GetSzArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return szField;
		}

		public int sizeOfUArray()
		{
			if (uField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_UnderlineProperty AddNewU()
		{
			uField = new CT_UnderlineProperty();
			return uField;
		}

		public void SetUArray(CT_UnderlineProperty[] array)
		{
			uField = ((array.Length > 0) ? array[0] : null);
		}

		public CT_UnderlineProperty GetUArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return uField;
		}

		public int sizeOfVertAlignArray()
		{
			if (vertAlignField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_VerticalAlignFontProperty AddNewVertAlign()
		{
			vertAlignField = new CT_VerticalAlignFontProperty();
			return vertAlignField;
		}

		public void SetVertAlignArray(CT_VerticalAlignFontProperty[] array)
		{
			vertAlignField = ((array.Length > 0) ? array[0] : null);
		}

		public CT_VerticalAlignFontProperty GetVertAlignArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return vertAlignField;
		}

		public int sizeOfSchemeArray()
		{
			if (schemeField != null)
			{
				return 1;
			}
			return 0;
		}

		public CT_FontScheme AddNewScheme()
		{
			schemeField = new CT_FontScheme();
			return schemeField;
		}

		public CT_FontScheme GetSchemeArray(int index)
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException("Only an index of 0 is supported");
			}
			return schemeField;
		}
	}
}
