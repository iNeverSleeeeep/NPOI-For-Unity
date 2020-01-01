using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_PatternFillProperties
	{
		private CT_Color fgClrField;

		private CT_Color bgClrField;

		private ST_PresetPatternVal prstField;

		private bool prstFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_Color fgClr
		{
			get
			{
				return fgClrField;
			}
			set
			{
				fgClrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Color bgClr
		{
			get
			{
				return bgClrField;
			}
			set
			{
				bgClrField = value;
			}
		}

		[XmlAttribute]
		public ST_PresetPatternVal prst
		{
			get
			{
				return prstField;
			}
			set
			{
				prstField = value;
			}
		}

		[XmlIgnore]
		public bool prstSpecified
		{
			get
			{
				return prstFieldSpecified;
			}
			set
			{
				prstFieldSpecified = value;
			}
		}

		public static CT_PatternFillProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PatternFillProperties cT_PatternFillProperties = new CT_PatternFillProperties();
			if (node.Attributes["prst"] != null)
			{
				cT_PatternFillProperties.prst = (ST_PresetPatternVal)Enum.Parse(typeof(ST_PresetPatternVal), node.Attributes["prst"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "fgClr")
				{
					cT_PatternFillProperties.fgClr = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bgClr")
				{
					cT_PatternFillProperties.bgClr = CT_Color.Parse(childNode, namespaceManager);
				}
			}
			return cT_PatternFillProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "prst", prst.ToString());
			sw.Write(">");
			if (fgClr != null)
			{
				fgClr.Write(sw, "fgClr");
			}
			if (bgClr != null)
			{
				bgClr.Write(sw, "bgClr");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
