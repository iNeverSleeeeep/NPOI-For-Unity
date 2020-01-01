using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_RadPr
	{
		private CT_OnOff degHideField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_OnOff degHide
		{
			get
			{
				return degHideField;
			}
			set
			{
				degHideField = value;
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

		public static CT_RadPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RadPr cT_RadPr = new CT_RadPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "degHide")
				{
					cT_RadPr.degHide = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_RadPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_RadPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (degHide != null)
			{
				degHide.Write(sw, "degHide");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
