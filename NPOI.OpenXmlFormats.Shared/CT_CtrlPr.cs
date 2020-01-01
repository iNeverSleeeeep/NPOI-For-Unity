using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_CtrlPr
	{
		private CT_RPr rPrField;

		private CT_RPrChange insField;

		private CT_RPrChange delField;

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		public CT_RPr rPr
		{
			get
			{
				return rPrField;
			}
			set
			{
				rPrField = value;
			}
		}

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Type = typeof(CT_RPrChange), Order = 1)]
		public CT_RPrChange ins
		{
			get
			{
				return insField;
			}
			set
			{
				insField = value;
			}
		}

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Type = typeof(CT_RPrChange), Order = 2)]
		public CT_RPrChange del
		{
			get
			{
				return delField;
			}
			set
			{
				delField = value;
			}
		}

		public static CT_CtrlPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CtrlPr cT_CtrlPr = new CT_CtrlPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_CtrlPr.rPr = CT_RPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_CtrlPr.ins = CT_RPrChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "del")
				{
					cT_CtrlPr.del = CT_RPrChange.Parse(childNode, namespaceManager);
				}
			}
			return cT_CtrlPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			if (ins != null)
			{
				ins.Write(sw, "ins");
			}
			if (del != null)
			{
				del.Write(sw, "del");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
