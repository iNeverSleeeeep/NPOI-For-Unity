using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_Extension
	{
		private string anyField;

		private string uriField;

		[XmlText]
		public string Any
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

		[XmlIgnore]
		public bool uriSpecified
		{
			get
			{
				return null != uriField;
			}
		}

		public static CT_Extension Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Extension cT_Extension = new CT_Extension();
			cT_Extension.uri = XmlHelper.ReadString(node.Attributes["uri"]);
			cT_Extension.Any = node.InnerXml;
			return cT_Extension;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "uri", uri);
			sw.Write(">");
			if (Any != null)
			{
				sw.Write(Any);
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
