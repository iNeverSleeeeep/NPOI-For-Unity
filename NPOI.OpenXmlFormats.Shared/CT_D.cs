using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_D
	{
		private CT_DPr dPrField;

		private List<CT_OMathArg> eField;

		[XmlElement(Order = 0)]
		public CT_DPr dPr
		{
			get
			{
				return dPrField;
			}
			set
			{
				dPrField = value;
			}
		}

		[XmlElement("e", Order = 1)]
		public List<CT_OMathArg> e
		{
			get
			{
				return eField;
			}
			set
			{
				eField = value;
			}
		}

		public static CT_D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_D cT_D = new CT_D();
			cT_D.e = new List<CT_OMathArg>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dPr")
				{
					cT_D.dPr = CT_DPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_D.e.Add(CT_OMathArg.Parse(childNode, namespaceManager));
				}
			}
			return cT_D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (dPr != null)
			{
				dPr.Write(sw, "dPr");
			}
			if (e != null)
			{
				foreach (CT_OMathArg item in e)
				{
					item.Write(sw, "e");
				}
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
