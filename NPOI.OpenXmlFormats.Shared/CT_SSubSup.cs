using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_SSubSup
	{
		private CT_SSubSupPr sSubSupPrField;

		private CT_OMathArg eField;

		private CT_OMathArg subField;

		private CT_OMathArg supField;

		[XmlElement(Order = 0)]
		public CT_SSubSupPr sSubSupPr
		{
			get
			{
				return sSubSupPrField;
			}
			set
			{
				sSubSupPrField = value;
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

		[XmlElement(Order = 3)]
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

		public static CT_SSubSup Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SSubSup cT_SSubSup = new CT_SSubSup();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sSubSupPr")
				{
					cT_SSubSup.sSubSupPr = CT_SSubSupPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_SSubSup.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sub")
				{
					cT_SSubSup.sub = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sup")
				{
					cT_SSubSup.sup = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_SSubSup;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (sSubSupPr != null)
			{
				sSubSupPr.Write(sw, "sSubSupPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			if (sub != null)
			{
				sub.Write(sw, "sub");
			}
			if (sup != null)
			{
				sup.Write(sw, "sup");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
