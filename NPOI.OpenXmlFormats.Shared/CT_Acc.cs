using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_Acc
	{
		private CT_AccPr accPrField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_AccPr accPr
		{
			get
			{
				return accPrField;
			}
			set
			{
				accPrField = value;
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

		public static CT_Acc Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Acc cT_Acc = new CT_Acc();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "accPr")
				{
					cT_Acc.accPr = CT_AccPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_Acc.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_Acc;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (accPr != null)
			{
				accPr.Write(sw, "accPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
