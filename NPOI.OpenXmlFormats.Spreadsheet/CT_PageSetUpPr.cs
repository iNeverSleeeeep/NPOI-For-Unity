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
	public class CT_PageSetUpPr
	{
		private bool autoPageBreaksField;

		private bool fitToPageField;

		[DefaultValue(true)]
		public bool autoPageBreaks
		{
			get
			{
				return autoPageBreaksField;
			}
			set
			{
				autoPageBreaksField = value;
			}
		}

		[DefaultValue(false)]
		public bool fitToPage
		{
			get
			{
				return fitToPageField;
			}
			set
			{
				fitToPageField = value;
			}
		}

		public static CT_PageSetUpPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageSetUpPr cT_PageSetUpPr = new CT_PageSetUpPr();
			cT_PageSetUpPr.autoPageBreaks = XmlHelper.ReadBool(node.Attributes["autoPageBreaks"]);
			cT_PageSetUpPr.fitToPage = XmlHelper.ReadBool(node.Attributes["fitToPage"]);
			return cT_PageSetUpPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "autoPageBreaks", autoPageBreaks, false);
			XmlHelper.WriteAttribute(sw, "fitToPage", fitToPage, false);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_PageSetUpPr()
		{
			autoPageBreaksField = true;
			fitToPageField = false;
		}
	}
}
