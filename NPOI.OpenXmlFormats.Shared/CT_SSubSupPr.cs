using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_SSubSupPr
	{
		private CT_OnOff alnScrField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_OnOff alnScr
		{
			get
			{
				return alnScrField;
			}
			set
			{
				alnScrField = value;
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

		public static CT_SSubSupPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SSubSupPr cT_SSubSupPr = new CT_SSubSupPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "alnScr")
				{
					cT_SSubSupPr.alnScr = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_SSubSupPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_SSubSupPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (alnScr != null)
			{
				alnScr.Write(sw, "alnScr");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
