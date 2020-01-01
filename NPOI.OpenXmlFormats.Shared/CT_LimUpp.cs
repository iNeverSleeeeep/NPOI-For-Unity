using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_LimUpp
	{
		private CT_LimUppPr limUppPrField;

		private CT_OMathArg eField;

		private CT_OMathArg limField;

		[XmlElement(Order = 0)]
		public CT_LimUppPr limUppPr
		{
			get
			{
				return limUppPrField;
			}
			set
			{
				limUppPrField = value;
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

		public static CT_LimUpp Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LimUpp cT_LimUpp = new CT_LimUpp();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "limUppPr")
				{
					cT_LimUpp.limUppPr = CT_LimUppPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_LimUpp.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lim")
				{
					cT_LimUpp.lim = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_LimUpp;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (limUppPr != null)
			{
				limUppPr.Write(sw, "limUppPr");
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
