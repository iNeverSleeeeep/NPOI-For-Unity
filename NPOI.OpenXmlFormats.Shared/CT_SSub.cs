using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_SSub
	{
		private CT_SSubPr sSubPrField;

		private CT_OMathArg eField;

		private CT_OMathArg subField;

		[XmlElement(Order = 0)]
		public CT_SSubPr sSubPr
		{
			get
			{
				return sSubPrField;
			}
			set
			{
				sSubPrField = value;
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
		public CT_OMathArg sub
		{
			get
			{
				return subField;
			}
			set
			{
				subField = value;
			}
		}

		public static CT_SSub Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SSub cT_SSub = new CT_SSub();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sSubPr")
				{
					cT_SSub.sSubPr = CT_SSubPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_SSub.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sub")
				{
					cT_SSub.sub = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_SSub;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (sSubPr != null)
			{
				sSubPr.Write(sw, "sSubPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			if (sub != null)
			{
				sub.Write(sw, "sub");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
