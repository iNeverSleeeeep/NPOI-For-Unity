using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_RElt
	{
		private CT_RPrElt rPrField;

		private string tField = string.Empty;

		[XmlElement("rPr")]
		public CT_RPrElt rPr
		{
			get
			{
				return rPrField;
			}
			set
			{
				rPrField = value;
			}
		}

		[XmlElement("t")]
		public string t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		public static CT_RElt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RElt cT_RElt = new CT_RElt();
			XmlNode xmlNode = node.SelectSingleNode("d:t", namespaceManager);
			if (xmlNode != null)
			{
				cT_RElt.t = xmlNode.InnerText.Replace("\r", "");
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_RElt.rPr = CT_RPrElt.Parse(childNode, namespaceManager);
				}
			}
			return cT_RElt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}>", nodeName));
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			if (t != null)
			{
				sw.Write(string.Format("<t xml:space=\"preserve\">{0}</t>", XmlHelper.ExcelEncodeString(XmlHelper.EncodeXml(t))));
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_RPrElt AddNewRPr()
		{
			rPrField = new CT_RPrElt();
			return rPrField;
		}
	}
}
