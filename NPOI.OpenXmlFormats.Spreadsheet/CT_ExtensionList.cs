using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DebuggerStepThrough]
	public class CT_ExtensionList
	{
		private List<CT_Extension> extField = new List<CT_Extension>();

		[XmlElement]
		public List<CT_Extension> ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}

		public CT_ExtensionList Copy()
		{
			CT_ExtensionList cT_ExtensionList = new CT_ExtensionList();
			cT_ExtensionList.ext = new List<CT_Extension>(ext);
			return cT_ExtensionList;
		}

		public static CT_ExtensionList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ExtensionList cT_ExtensionList = new CT_ExtensionList();
			cT_ExtensionList.ext = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ext")
				{
					cT_ExtensionList.ext.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_ExtensionList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (ext != null)
			{
				foreach (CT_Extension item in ext)
				{
					item.Write(sw, "ext");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
