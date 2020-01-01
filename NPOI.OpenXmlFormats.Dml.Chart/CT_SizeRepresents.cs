using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_SizeRepresents
	{
		private ST_SizeRepresents valField;

		[XmlAttribute]
		[DefaultValue(ST_SizeRepresents.area)]
		public ST_SizeRepresents val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public CT_SizeRepresents()
		{
			valField = ST_SizeRepresents.area;
		}

		public static CT_SizeRepresents Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SizeRepresents cT_SizeRepresents = new CT_SizeRepresents();
			if (node.Attributes["val"] != null)
			{
				cT_SizeRepresents.val = (ST_SizeRepresents)Enum.Parse(typeof(ST_SizeRepresents), node.Attributes["val"].Value);
			}
			return cT_SizeRepresents;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
