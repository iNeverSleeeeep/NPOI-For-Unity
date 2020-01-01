using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_FPr
	{
		private CT_FType typeField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_FType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
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

		public static CT_FPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FPr cT_FPr = new CT_FPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "type")
				{
					cT_FPr.type = CT_FType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_FPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_FPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (type != null)
			{
				type.Write(sw, "type");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
