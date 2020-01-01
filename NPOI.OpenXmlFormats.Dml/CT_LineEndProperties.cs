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
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_LineEndProperties
	{
		private ST_LineEndType typeField;

		private ST_LineEndWidth wField;

		private ST_LineEndLength lenField;

		[XmlAttribute]
		public ST_LineEndType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlAttribute]
		public ST_LineEndWidth w
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

		[XmlAttribute]
		public ST_LineEndLength len
		{
			get
			{
				return lenField;
			}
			set
			{
				lenField = value;
			}
		}

		public static CT_LineEndProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LineEndProperties cT_LineEndProperties = new CT_LineEndProperties();
			if (node.Attributes["type"] != null)
			{
				cT_LineEndProperties.type = (ST_LineEndType)Enum.Parse(typeof(ST_LineEndType), node.Attributes["type"].Value);
			}
			if (node.Attributes["w"] != null)
			{
				cT_LineEndProperties.w = (ST_LineEndWidth)Enum.Parse(typeof(ST_LineEndWidth), node.Attributes["w"].Value);
			}
			if (node.Attributes["len"] != null)
			{
				cT_LineEndProperties.len = (ST_LineEndLength)Enum.Parse(typeof(ST_LineEndLength), node.Attributes["len"].Value);
			}
			return cT_LineEndProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			if (type != 0)
			{
				XmlHelper.WriteAttribute(sw, "type", type.ToString());
			}
			if (w != 0)
			{
				XmlHelper.WriteAttribute(sw, "w", w.ToString());
			}
			if (len != 0)
			{
				XmlHelper.WriteAttribute(sw, "len", len.ToString());
			}
			sw.Write("/>");
		}
	}
}
