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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_ConnectionSiteList
	{
		private List<CT_ConnectionSite> cxnField;

		public List<CT_ConnectionSite> cxn
		{
			get
			{
				return cxnField;
			}
			set
			{
				cxnField = value;
			}
		}

		internal static CT_ConnectionSiteList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_ConnectionSiteList cT_ConnectionSiteList = new CT_ConnectionSiteList();
			cT_ConnectionSiteList.cxnField = new List<CT_ConnectionSite>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cxn")
				{
					cT_ConnectionSiteList.cxnField.Add(CT_ConnectionSite.Parse(childNode, namespaceManager));
				}
			}
			return cT_ConnectionSiteList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write("<a:{0}>", nodeName);
			if (cxnField != null)
			{
				foreach (CT_ConnectionSite item in cxnField)
				{
					item.Write(sw, "cxn");
				}
			}
			sw.Write("</a:{0}>", nodeName);
		}
	}
}
