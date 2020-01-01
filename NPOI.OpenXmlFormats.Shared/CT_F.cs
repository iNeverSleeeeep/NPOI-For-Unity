using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_F
	{
		private CT_FPr fPrField;

		private CT_OMathArg numField;

		private CT_OMathArg denField;

		[XmlElement(Order = 0)]
		public CT_FPr fPr
		{
			get
			{
				return fPrField;
			}
			set
			{
				fPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OMathArg num
		{
			get
			{
				return numField;
			}
			set
			{
				numField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OMathArg den
		{
			get
			{
				return denField;
			}
			set
			{
				denField = value;
			}
		}

		public static CT_F Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_F cT_F = new CT_F();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "fPr")
				{
					cT_F.fPr = CT_FPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "num")
				{
					cT_F.num = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "den")
				{
					cT_F.den = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_F;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (fPr != null)
			{
				fPr.Write(sw, "fPr");
			}
			if (num != null)
			{
				num.Write(sw, "num");
			}
			if (den != null)
			{
				den.Write(sw, "den");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
