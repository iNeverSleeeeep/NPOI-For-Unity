using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", ElementName = "font")]
	public class CT_Font
	{
		private CT_FontName nameField;

		private List<CT_IntProperty> charsetField;

		private List<CT_IntProperty> familyField;

		private List<CT_BooleanProperty> bField;

		private List<CT_BooleanProperty> iField;

		private List<CT_BooleanProperty> strikeField;

		private CT_BooleanProperty outlineField;

		private CT_BooleanProperty shadowField;

		private CT_BooleanProperty condenseField;

		private CT_BooleanProperty extendField;

		private List<CT_Color> colorField;

		private List<CT_FontSize> szField;

		private List<CT_UnderlineProperty> uField;

		private List<CT_VerticalAlignFontProperty> vertAlignField;

		private List<CT_FontScheme> schemeField;

		[XmlElement]
		public CT_FontName name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlElement]
		public List<CT_IntProperty> charset
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

		[XmlElement]
		public List<CT_IntProperty> family
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

		[XmlElement]
		public List<CT_BooleanProperty> b
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

		[XmlElement]
		public List<CT_BooleanProperty> i
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

		[XmlElement]
		public List<CT_BooleanProperty> strike
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

		[XmlElement]
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

		[XmlElement]
		public List<CT_FontSize> sz
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

		[XmlElement]
		public List<CT_UnderlineProperty> u
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

		[XmlElement]
		public List<CT_VerticalAlignFontProperty> vertAlign
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

		[XmlElement]
		public List<CT_FontScheme> scheme
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

		public static CT_Font Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Font cT_Font = new CT_Font();
			cT_Font.charset = new List<CT_IntProperty>();
			cT_Font.family = new List<CT_IntProperty>();
			cT_Font.b = new List<CT_BooleanProperty>();
			cT_Font.i = new List<CT_BooleanProperty>();
			cT_Font.strike = new List<CT_BooleanProperty>();
			cT_Font.color = new List<CT_Color>();
			cT_Font.sz = new List<CT_FontSize>();
			cT_Font.u = new List<CT_UnderlineProperty>();
			cT_Font.vertAlign = new List<CT_VerticalAlignFontProperty>();
			cT_Font.scheme = new List<CT_FontScheme>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "outline")
				{
					cT_Font.outline = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shadow")
				{
					cT_Font.shadow = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "condense")
				{
					cT_Font.condense = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extend")
				{
					cT_Font.extend = CT_BooleanProperty.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "name")
				{
					cT_Font.name = CT_FontName.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "charset")
				{
					cT_Font.charset.Add(CT_IntProperty.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "family")
				{
					cT_Font.family.Add(CT_IntProperty.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "b")
				{
					cT_Font.b.Add(CT_BooleanProperty.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "i")
				{
					cT_Font.i.Add(CT_BooleanProperty.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "strike")
				{
					cT_Font.strike.Add(CT_BooleanProperty.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "color")
				{
					cT_Font.color.Add(CT_Color.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "sz")
				{
					cT_Font.sz.Add(CT_FontSize.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "u")
				{
					cT_Font.u.Add(CT_UnderlineProperty.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "vertAlign")
				{
					cT_Font.vertAlign.Add(CT_VerticalAlignFontProperty.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "scheme")
				{
					cT_Font.scheme.Add(CT_FontScheme.Parse(childNode, namespaceManager));
				}
			}
			return cT_Font;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (b != null)
			{
				foreach (CT_BooleanProperty item in b)
				{
					item.Write(sw, "b");
				}
			}
			if (i != null)
			{
				foreach (CT_BooleanProperty item2 in i)
				{
					item2.Write(sw, "i");
				}
			}
			if (strike != null)
			{
				foreach (CT_BooleanProperty item3 in strike)
				{
					item3.Write(sw, "strike");
				}
			}
			if (condense != null)
			{
				condense.Write(sw, "condense");
			}
			if (extend != null)
			{
				extend.Write(sw, "extend");
			}
			if (outline != null)
			{
				outline.Write(sw, "outline");
			}
			if (shadow != null)
			{
				shadow.Write(sw, "shadow");
			}
			if (u != null)
			{
				foreach (CT_UnderlineProperty item4 in u)
				{
					item4.Write(sw, "u");
				}
			}
			if (vertAlign != null)
			{
				foreach (CT_VerticalAlignFontProperty item5 in vertAlign)
				{
					item5.Write(sw, "vertAlign");
				}
			}
			if (sz != null)
			{
				foreach (CT_FontSize item6 in sz)
				{
					item6.Write(sw, "sz");
				}
			}
			if (color != null)
			{
				foreach (CT_Color item7 in color)
				{
					item7.Write(sw, "color");
				}
			}
			if (name != null)
			{
				name.Write(sw, "name");
			}
			if (family != null)
			{
				foreach (CT_IntProperty item8 in family)
				{
					item8.Write(sw, "family");
				}
			}
			if (charset != null)
			{
				foreach (CT_IntProperty item9 in charset)
				{
					item9.Write(sw, "charset");
				}
			}
			if (scheme != null)
			{
				foreach (CT_FontScheme item10 in scheme)
				{
					item10.Write(sw, "scheme");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public int sizeOfNameArray()
		{
			if (nameField == null)
			{
				return 0;
			}
			return 1;
		}

		public CT_FontName AddNewName()
		{
			nameField = new CT_FontName();
			return nameField;
		}

		public int sizeOfCharsetArray()
		{
			if (charsetField == null)
			{
				return 0;
			}
			return charsetField.Count;
		}

		public CT_IntProperty AddNewCharset()
		{
			if (charsetField == null)
			{
				charsetField = new List<CT_IntProperty>();
			}
			CT_IntProperty cT_IntProperty = new CT_IntProperty();
			charsetField.Add(cT_IntProperty);
			return cT_IntProperty;
		}

		public void SetCharsetArray(int index, CT_IntProperty value)
		{
			charsetField[index] = value;
		}

		public CT_IntProperty GetCharsetArray(int index)
		{
			return charsetField[index];
		}

		public int sizeOfFamilyArray()
		{
			if (familyField == null)
			{
				return 0;
			}
			return familyField.Count;
		}

		public CT_IntProperty AddNewFamily()
		{
			if (familyField == null)
			{
				familyField = new List<CT_IntProperty>();
			}
			CT_IntProperty cT_IntProperty = new CT_IntProperty();
			familyField.Add(cT_IntProperty);
			return cT_IntProperty;
		}

		public void SetFamilyArray(int index, CT_IntProperty value)
		{
			familyField[index] = value;
		}

		public CT_IntProperty GetFamilyArray(int index)
		{
			return familyField[index];
		}

		public int sizeOfBArray()
		{
			if (bField == null)
			{
				return 0;
			}
			return bField.Count;
		}

		public CT_BooleanProperty AddNewB()
		{
			if (bField == null)
			{
				bField = new List<CT_BooleanProperty>();
			}
			CT_BooleanProperty cT_BooleanProperty = new CT_BooleanProperty();
			bField.Add(cT_BooleanProperty);
			return cT_BooleanProperty;
		}

		public void SetBArray(int index, CT_BooleanProperty value)
		{
			bField[index] = value;
		}

		public void SetBArray(List<CT_BooleanProperty> array)
		{
			bField = array;
		}

		public CT_BooleanProperty GetBArray(int index)
		{
			return bField[index];
		}

		public int sizeOfIArray()
		{
			if (iField == null)
			{
				return 0;
			}
			return iField.Count;
		}

		public CT_BooleanProperty AddNewI()
		{
			if (iField == null)
			{
				iField = new List<CT_BooleanProperty>();
			}
			CT_BooleanProperty cT_BooleanProperty = new CT_BooleanProperty();
			iField.Add(cT_BooleanProperty);
			return cT_BooleanProperty;
		}

		public void SetIArray(int index, CT_BooleanProperty value)
		{
			iField[index] = value;
		}

		public void SetIArray(List<CT_BooleanProperty> array)
		{
			iField = array;
		}

		public CT_BooleanProperty GetIArray(int index)
		{
			return iField[index];
		}

		public int sizeOfStrikeArray()
		{
			if (strikeField == null)
			{
				return 0;
			}
			return strikeField.Count;
		}

		public CT_BooleanProperty AddNewStrike()
		{
			if (strikeField == null)
			{
				strikeField = new List<CT_BooleanProperty>();
			}
			CT_BooleanProperty cT_BooleanProperty = new CT_BooleanProperty();
			strikeField.Add(cT_BooleanProperty);
			return cT_BooleanProperty;
		}

		public void SetStrikeArray(int index, CT_BooleanProperty value)
		{
			strikeField[index] = value;
		}

		public void SetStrikeArray(List<CT_BooleanProperty> array)
		{
			strikeField = array;
		}

		public CT_BooleanProperty GetStrikeArray(int index)
		{
			return strikeField[index];
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
			if (shadowField == null)
			{
				return 0;
			}
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
			if (condenseField == null)
			{
				return 0;
			}
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
			if (colorField == null)
			{
				return 0;
			}
			return colorField.Count;
		}

		public CT_Color GetColorArray(int index)
		{
			return colorField[index];
		}

		public void SetColorArray(int index, CT_Color value)
		{
			colorField[index] = value;
		}

		public void SetColorArray(List<CT_Color> array)
		{
			colorField = array;
		}

		public CT_Color AddNewColor()
		{
			if (colorField == null)
			{
				colorField = new List<CT_Color>();
			}
			CT_Color cT_Color = new CT_Color();
			colorField.Add(cT_Color);
			return cT_Color;
		}

		public int sizeOfSzArray()
		{
			if (szField == null)
			{
				return 0;
			}
			return szField.Count;
		}

		public CT_FontSize AddNewSz()
		{
			if (szField == null)
			{
				szField = new List<CT_FontSize>();
			}
			CT_FontSize cT_FontSize = new CT_FontSize();
			szField.Add(cT_FontSize);
			return cT_FontSize;
		}

		public void SetSzArray(int index, CT_FontSize value)
		{
			szField[index] = value;
		}

		public void SetSzArray(List<CT_FontSize> array)
		{
			szField = array;
		}

		public CT_FontSize GetSzArray(int index)
		{
			return szField[index];
		}

		public int sizeOfUArray()
		{
			if (uField == null)
			{
				return 0;
			}
			return uField.Count;
		}

		public CT_UnderlineProperty AddNewU()
		{
			if (uField == null)
			{
				uField = new List<CT_UnderlineProperty>();
			}
			CT_UnderlineProperty cT_UnderlineProperty = new CT_UnderlineProperty();
			uField.Add(cT_UnderlineProperty);
			return cT_UnderlineProperty;
		}

		public void SetUArray(int index, CT_UnderlineProperty value)
		{
			if (uField[index] != null)
			{
				uField[index] = value;
			}
			else
			{
				uField.Insert(index, value);
			}
		}

		public void SetUArray(List<CT_UnderlineProperty> array)
		{
			uField = array;
		}

		public CT_UnderlineProperty GetUArray(int index)
		{
			return uField[index];
		}

		public int sizeOfVertAlignArray()
		{
			if (vertAlignField == null)
			{
				return 0;
			}
			return vertAlignField.Count;
		}

		public CT_VerticalAlignFontProperty AddNewVertAlign()
		{
			if (vertAlignField == null)
			{
				vertAlignField = new List<CT_VerticalAlignFontProperty>();
			}
			CT_VerticalAlignFontProperty cT_VerticalAlignFontProperty = new CT_VerticalAlignFontProperty();
			vertAlignField.Add(cT_VerticalAlignFontProperty);
			return cT_VerticalAlignFontProperty;
		}

		public void SetVertAlignArray(int index, CT_VerticalAlignFontProperty value)
		{
			vertAlignField[index] = value;
		}

		public void SetVertAlignArray(List<CT_VerticalAlignFontProperty> array)
		{
			vertAlignField = array;
		}

		public CT_VerticalAlignFontProperty GetVertAlignArray(int index)
		{
			return vertAlignField[index];
		}

		public int sizeOfSchemeArray()
		{
			if (schemeField == null)
			{
				return 0;
			}
			return schemeField.Count;
		}

		public CT_FontScheme AddNewScheme()
		{
			if (schemeField == null)
			{
				schemeField = new List<CT_FontScheme>();
			}
			CT_FontScheme cT_FontScheme = new CT_FontScheme();
			schemeField.Add(cT_FontScheme);
			return cT_FontScheme;
		}

		public void SetSchemeArray(int index, CT_FontScheme value)
		{
			schemeField[index] = value;
		}

		public CT_FontScheme GetSchemeArray(int index)
		{
			return schemeField[index];
		}

		public override string ToString()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				Write(streamWriter, "font");
				streamWriter.Flush();
				memoryStream.Position = 0L;
				StreamReader streamReader = new StreamReader(memoryStream);
				return streamReader.ReadToEnd();
			}
		}

		public CT_Font Clone()
		{
			CT_Font cT_Font = new CT_Font();
			if (name != null)
			{
				CT_FontName cT_FontName = cT_Font.AddNewName();
				cT_FontName.val = name.val;
			}
			if (charset != null)
			{
				foreach (CT_IntProperty item in charset)
				{
					CT_IntProperty cT_IntProperty = cT_Font.AddNewCharset();
					cT_IntProperty.val = item.val;
				}
			}
			if (family != null)
			{
				foreach (CT_IntProperty item2 in family)
				{
					CT_IntProperty cT_IntProperty2 = cT_Font.AddNewFamily();
					cT_IntProperty2.val = item2.val;
				}
			}
			if (b != null)
			{
				foreach (CT_BooleanProperty item3 in b)
				{
					CT_BooleanProperty cT_BooleanProperty = cT_Font.AddNewB();
					cT_BooleanProperty.val = item3.val;
				}
			}
			if (i != null)
			{
				foreach (CT_BooleanProperty item4 in i)
				{
					CT_BooleanProperty cT_BooleanProperty2 = cT_Font.AddNewB();
					cT_BooleanProperty2.val = item4.val;
				}
			}
			if (strike != null)
			{
				foreach (CT_BooleanProperty item5 in strike)
				{
					CT_BooleanProperty cT_BooleanProperty3 = cT_Font.AddNewStrike();
					cT_BooleanProperty3.val = item5.val;
				}
			}
			if (outline != null)
			{
				cT_Font.outline = new CT_BooleanProperty();
				cT_Font.outline.val = outline.val;
			}
			if (shadow != null)
			{
				cT_Font.shadow = new CT_BooleanProperty();
				cT_Font.shadow.val = shadow.val;
			}
			if (condense != null)
			{
				cT_Font.condense = new CT_BooleanProperty();
				cT_Font.condense.val = condense.val;
			}
			if (extend != null)
			{
				cT_Font.extend = new CT_BooleanProperty();
				cT_Font.extend.val = extend.val;
			}
			if (color != null)
			{
				foreach (CT_Color item6 in color)
				{
					CT_Color cT_Color = cT_Font.AddNewColor();
					cT_Color.theme = item6.theme;
				}
			}
			if (sz != null)
			{
				foreach (CT_FontSize item7 in sz)
				{
					CT_FontSize cT_FontSize = cT_Font.AddNewSz();
					cT_FontSize.val = item7.val;
				}
			}
			if (u != null)
			{
				foreach (CT_UnderlineProperty item8 in u)
				{
					CT_UnderlineProperty cT_UnderlineProperty = cT_Font.AddNewU();
					cT_UnderlineProperty.val = item8.val;
				}
			}
			if (vertAlign != null)
			{
				foreach (CT_VerticalAlignFontProperty item9 in vertAlign)
				{
					CT_VerticalAlignFontProperty cT_VerticalAlignFontProperty = cT_Font.AddNewVertAlign();
					cT_VerticalAlignFontProperty.val = item9.val;
				}
			}
			if (scheme != null)
			{
				foreach (CT_FontScheme item10 in scheme)
				{
					CT_FontScheme cT_FontScheme = cT_Font.AddNewScheme();
					cT_FontScheme.val = item10.val;
				}
				return cT_Font;
			}
			return cT_Font;
		}
	}
}
