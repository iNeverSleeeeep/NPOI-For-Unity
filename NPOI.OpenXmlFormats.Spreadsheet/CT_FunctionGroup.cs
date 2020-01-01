using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_FunctionGroup
	{
		private string nameField;

		[XmlAttribute]
		public string name
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

		public static CT_FunctionGroup Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FunctionGroup cT_FunctionGroup = new CT_FunctionGroup();
			cT_FunctionGroup.name = XmlHelper.ReadString(node.Attributes["name"]);
			return cT_FunctionGroup;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
