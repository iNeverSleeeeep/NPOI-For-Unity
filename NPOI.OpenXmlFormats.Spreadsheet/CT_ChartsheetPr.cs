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
	public class CT_ChartsheetPr
	{
		private CT_Color tabColorField;

		private bool publishedField;

		private string codeNameField;

		public CT_Color tabColor
		{
			get
			{
				return tabColorField;
			}
			set
			{
				tabColorField = value;
			}
		}

		[DefaultValue(true)]
		public bool published
		{
			get
			{
				return publishedField;
			}
			set
			{
				publishedField = value;
			}
		}

		public string codeName
		{
			get
			{
				return codeNameField;
			}
			set
			{
				codeNameField = value;
			}
		}

		public CT_ChartsheetPr()
		{
			publishedField = true;
		}

		public static CT_ChartsheetPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ChartsheetPr cT_ChartsheetPr = new CT_ChartsheetPr();
			if (node.Attributes["published"] != null)
			{
				cT_ChartsheetPr.published = XmlHelper.ReadBool(node.Attributes["published"]);
			}
			cT_ChartsheetPr.codeName = XmlHelper.ReadString(node.Attributes["codeName"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tabColor")
				{
					cT_ChartsheetPr.tabColor = CT_Color.Parse(childNode, namespaceManager);
				}
			}
			return cT_ChartsheetPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (!published)
			{
				XmlHelper.WriteAttribute(sw, "published", published);
			}
			XmlHelper.WriteAttribute(sw, "codeName", codeName);
			sw.Write(">");
			if (tabColor != null)
			{
				tabColor.Write(sw, "tabColor");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
