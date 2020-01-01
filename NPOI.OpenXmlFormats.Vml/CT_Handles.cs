using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[DesignerCategory("code")]
	public class CT_Handles
	{
		private List<CT_H> hField;

		[XmlElement("h")]
		public List<CT_H> h
		{
			get
			{
				return hField;
			}
			set
			{
				hField = value;
			}
		}

		[XmlIgnore]
		public bool hSpecified
		{
			get
			{
				return null != hField;
			}
		}

		public CT_H AddNewH()
		{
			if (hField == null)
			{
				hField = new List<CT_H>();
			}
			CT_H cT_H = new CT_H();
			hField.Add(cT_H);
			return cT_H;
		}

		public static CT_Handles Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Handles cT_Handles = new CT_Handles();
			cT_Handles.h = new List<CT_H>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "h")
				{
					cT_Handles.h.Add(CT_H.Parse(childNode, namespaceManager));
				}
			}
			return cT_Handles;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			sw.Write(">");
			if (h != null)
			{
				foreach (CT_H item in h)
				{
					item.Write(sw, "h");
				}
			}
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
