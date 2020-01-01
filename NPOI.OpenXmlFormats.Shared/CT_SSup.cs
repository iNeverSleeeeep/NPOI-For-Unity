using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_SSup
	{
		private CT_SSupPr sSupPrField;

		private CT_OMathArg eField;

		private CT_OMathArg supField;

		[XmlElement(Order = 0)]
		public CT_SSupPr sSupPr
		{
			get
			{
				return sSupPrField;
			}
			set
			{
				sSupPrField = value;
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
		public CT_OMathArg sup
		{
			get
			{
				return supField;
			}
			set
			{
				supField = value;
			}
		}

		public static CT_SSup Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SSup cT_SSup = new CT_SSup();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sSupPr")
				{
					cT_SSup.sSupPr = CT_SSupPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_SSup.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sup")
				{
					cT_SSup.sup = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_SSup;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (sSupPr != null)
			{
				sSupPr.Write(sw, "sSupPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			if (sup != null)
			{
				sup.Write(sw, "sup");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
