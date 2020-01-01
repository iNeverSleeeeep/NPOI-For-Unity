using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TextTabStopList
	{
		private List<CT_TextTabStop> tabField;

		[XmlElement("tab", Order = 0)]
		public List<CT_TextTabStop> tab
		{
			get
			{
				return tabField;
			}
			set
			{
				tabField = value;
			}
		}

		public CT_TextTabStopList()
		{
			tabField = new List<CT_TextTabStop>();
		}

		internal void Write(StreamWriter sw, string p)
		{
			sw.Write("<a:{0}>", p);
			if (tab != null)
			{
				foreach (CT_TextTabStop item in tab)
				{
					item.Write(sw, "tab");
				}
			}
			sw.Write("</a:{0}>", p);
		}

		internal static CT_TextTabStopList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_TextTabStopList cT_TextTabStopList = new CT_TextTabStopList();
			cT_TextTabStopList.tab = new List<CT_TextTabStop>();
			foreach (XmlNode item in node)
			{
				if (item.LocalName == "tab")
				{
					cT_TextTabStopList.tab.Add(CT_TextTabStop.Parse(item, namespaceManager));
				}
			}
			return cT_TextTabStopList;
		}
	}
}
