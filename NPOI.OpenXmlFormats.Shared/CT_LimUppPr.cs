using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_LimUppPr
	{
		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
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

		public static CT_LimUppPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LimUppPr cT_LimUppPr = new CT_LimUppPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ctrlPr")
				{
					cT_LimUppPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_LimUppPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
