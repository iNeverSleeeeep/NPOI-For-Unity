using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_IntProperty
	{
		private int valField;

		[XmlAttribute]
		public int val
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

		public static CT_IntProperty Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_IntProperty cT_IntProperty = new CT_IntProperty();
			cT_IntProperty.val = XmlHelper.ReadInt(node.Attributes["val"]);
			return cT_IntProperty;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val);
			sw.Write("/>");
		}
	}
}
