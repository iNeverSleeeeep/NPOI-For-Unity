using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_SPre
	{
		private CT_SPrePr sPrePrField;

		private CT_OMathArg subField;

		private CT_OMathArg supField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_SPrePr sPrePr
		{
			get
			{
				return sPrePrField;
			}
			set
			{
				sPrePrField = value;
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

		public static CT_SPre Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SPre cT_SPre = new CT_SPre();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sPrePr")
				{
					cT_SPre.sPrePr = CT_SPrePr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sub")
				{
					cT_SPre.sub = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sup")
				{
					cT_SPre.sup = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_SPre.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_SPre;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (sPrePr != null)
			{
				sPrePr.Write(sw, "sPrePr");
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
