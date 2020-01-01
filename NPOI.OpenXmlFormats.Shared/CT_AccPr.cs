using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_AccPr
	{
		private CT_Char chrField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_Char chr
		{
			get
			{
				return chrField;
			}
			set
			{
				chrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_CtrlPr ctrlPr
		{
			get
			{
				return ctrlPrField;
			}
			set
			{
				ctrlPrField = value;
			}
		}

		public static CT_AccPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AccPr cT_AccPr = new CT_AccPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "chr")
				{
					cT_AccPr.chr = CT_Char.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_AccPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_AccPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (chr != null)
			{
				chr.Write(sw, "chr");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
