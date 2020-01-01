using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_Nary
	{
		private CT_NaryPr naryPrField;

		private CT_OMathArg subField;

		private CT_OMathArg supField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_NaryPr naryPr
		{
			get
			{
				return naryPrField;
			}
			set
			{
				naryPrField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 3)]
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

		public static CT_Nary Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Nary cT_Nary = new CT_Nary();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "naryPr")
				{
					cT_Nary.naryPr = CT_NaryPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sub")
				{
					cT_Nary.sub = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sup")
				{
					cT_Nary.sup = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_Nary.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_Nary;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (naryPr != null)
			{
				naryPr.Write(sw, "naryPr");
			}
			if (sub != null)
			{
				sub.Write(sw, "sub");
			}
			if (sup != null)
			{
				sup.Write(sw, "sup");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
