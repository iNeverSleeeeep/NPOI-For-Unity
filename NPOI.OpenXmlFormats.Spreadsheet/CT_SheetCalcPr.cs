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
	public class CT_SheetCalcPr
	{
		private bool fullCalcOnLoadField;

		[DefaultValue(false)]
		public bool fullCalcOnLoad
		{
			get
			{
				return fullCalcOnLoadField;
			}
			set
			{
				fullCalcOnLoadField = value;
			}
		}

		public static CT_SheetCalcPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SheetCalcPr cT_SheetCalcPr = new CT_SheetCalcPr();
			cT_SheetCalcPr.fullCalcOnLoad = XmlHelper.ReadBool(node.Attributes["fullCalcOnLoad"]);
			return cT_SheetCalcPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "fullCalcOnLoad", fullCalcOnLoad);
			sw.Write("/>");
		}

		public CT_SheetCalcPr()
		{
			fullCalcOnLoadField = false;
		}
	}
}
