using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_PatternFill
	{
		private CT_Color fgColorField;

		private CT_Color bgColorField;

		private ST_PatternType patternTypeField;

		[XmlElement]
		public CT_Color fgColor
		{
			get
			{
				return fgColorField;
			}
			set
			{
				fgColorField = value;
			}
		}

		[XmlElement]
		public CT_Color bgColor
		{
			get
			{
				return bgColorField;
			}
			set
			{
				bgColorField = value;
			}
		}

		[XmlAttribute]
		public ST_PatternType patternType
		{
			get
			{
				return patternTypeField;
			}
			set
			{
				patternTypeField = value;
			}
		}

		public bool IsSetPatternType()
		{
			return patternTypeField != ST_PatternType.none;
		}

		public CT_Color AddNewFgColor()
		{
			fgColorField = new CT_Color();
			return fgColorField;
		}

		public CT_Color AddNewBgColor()
		{
			bgColorField = new CT_Color();
			return bgColorField;
		}

		public void UnsetPatternType()
		{
			patternTypeField = ST_PatternType.none;
		}

		public void UnsetFgColor()
		{
			fgColorField = null;
		}

		public void UnsetBgColor()
		{
			bgColorField = null;
		}

		public static CT_PatternFill Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PatternFill cT_PatternFill = new CT_PatternFill();
			if (node.Attributes["patternType"] != null)
			{
				cT_PatternFill.patternType = (ST_PatternType)Enum.Parse(typeof(ST_PatternType), node.Attributes["patternType"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "fgColor")
				{
					cT_PatternFill.fgColor = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bgColor")
				{
					cT_PatternFill.bgColor = CT_Color.Parse(childNode, namespaceManager);
				}
			}
			return cT_PatternFill;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (patternType != 0)
			{
				XmlHelper.WriteAttribute(sw, "patternType", patternType.ToString());
			}
			if (fgColor == null && bgColor == null)
			{
				sw.Write("/>");
			}
			else
			{
				sw.Write(">");
				if (fgColor != null)
				{
					fgColor.Write(sw, "fgColor");
				}
				if (bgColor != null)
				{
					bgColor.Write(sw, "bgColor");
				}
				sw.Write(string.Format("</{0}>", nodeName));
			}
		}

		public bool IsSetBgColor()
		{
			return bgColorField != null;
		}

		public bool IsSetFgColor()
		{
			return fgColorField != null;
		}
	}
}
