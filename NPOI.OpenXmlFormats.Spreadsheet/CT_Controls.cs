using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Controls
	{
		private List<CT_Control> controlField;

		public List<CT_Control> control
		{
			get
			{
				return controlField;
			}
			set
			{
				controlField = value;
			}
		}

		public static CT_Controls Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Controls cT_Controls = new CT_Controls();
			cT_Controls.control = new List<CT_Control>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "control")
				{
					cT_Controls.control.Add(CT_Control.Parse(childNode, namespaceManager));
				}
			}
			return cT_Controls;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (control != null)
			{
				foreach (CT_Control item in control)
				{
					item.Write(sw, "control");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
