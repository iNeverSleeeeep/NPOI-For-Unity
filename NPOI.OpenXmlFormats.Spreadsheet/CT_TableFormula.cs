using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_TableFormula
	{
		private bool arrayField;

		private string valueField;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool array
		{
			get
			{
				return arrayField;
			}
			set
			{
				arrayField = value;
			}
		}

		[XmlText]
		public string Value
		{
			get
			{
				return valueField;
			}
			set
			{
				valueField = value;
			}
		}

		public CT_TableFormula()
		{
			arrayField = false;
		}

		public static CT_TableFormula Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TableFormula cT_TableFormula = new CT_TableFormula();
			if (node.Attributes["array"] != null)
			{
				cT_TableFormula.array = XmlHelper.ReadBool(node.Attributes["array"]);
			}
			cT_TableFormula.Value = node.InnerText;
			return cT_TableFormula;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "array", array);
			sw.Write(">");
			sw.Write(XmlHelper.EncodeXml(valueField));
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
