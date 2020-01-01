using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_LimLow
	{
		private CT_LimLowPr limLowPrField;

		private CT_OMathArg eField;

		private CT_OMathArg limField;

		[XmlElement(Order = 0)]
		public CT_LimLowPr limLowPr
		{
			get
			{
				return limLowPrField;
			}
			set
			{
				limLowPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OMathArg e
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

		[XmlElement(Order = 2)]
		public CT_OMathArg lim
		{
			get
			{
				return limField;
			}
			set
			{
				limField = value;
			}
		}

		public static CT_LimLow Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LimLow cT_LimLow = new CT_LimLow();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "limLowPr")
				{
					cT_LimLow.limLowPr = CT_LimLowPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_LimLow.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lim")
				{
					cT_LimLow.lim = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_LimLow;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (limLowPr != null)
			{
				limLowPr.Write(sw, "limLowPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			if (lim != null)
			{
				lim.Write(sw, "lim");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
