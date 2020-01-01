using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_GroupChr
	{
		private CT_GroupChrPr groupChrPrField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_GroupChrPr groupChrPr
		{
			get
			{
				return groupChrPrField;
			}
			set
			{
				groupChrPrField = value;
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

		public static CT_GroupChr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GroupChr cT_GroupChr = new CT_GroupChr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "groupChrPr")
				{
					cT_GroupChr.groupChrPr = CT_GroupChrPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_GroupChr.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_GroupChr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (groupChrPr != null)
			{
				groupChrPr.Write(sw, "groupChrPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
