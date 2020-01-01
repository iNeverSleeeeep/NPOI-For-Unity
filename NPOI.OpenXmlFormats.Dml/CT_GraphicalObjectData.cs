using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_GraphicalObjectData
	{
		private List<string> anyField = new List<string>();

		private string uriField;

		[XmlIgnore]
		public List<string> Any
		{
			get
			{
				return anyField;
			}
			set
			{
				anyField = value;
			}
		}

		[XmlAttribute(DataType = "token")]
		public string uri
		{
			get
			{
				return uriField;
			}
			set
			{
				uriField = value;
			}
		}

		public static CT_GraphicalObjectData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GraphicalObjectData cT_GraphicalObjectData = new CT_GraphicalObjectData();
			cT_GraphicalObjectData.uri = XmlHelper.ReadString(node.Attributes["uri"]);
			cT_GraphicalObjectData.Any = new List<string>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				cT_GraphicalObjectData.Any.Add(childNode.OuterXml);
			}
			return cT_GraphicalObjectData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "uri", uri);
			sw.Write(">");
			foreach (string item in Any)
			{
				sw.Write(item);
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public void AddChartElement(string el)
		{
			anyField.Add(el);
		}

		public void AddPicElement(string el)
		{
			anyField.Add(el);
		}
	}
}
