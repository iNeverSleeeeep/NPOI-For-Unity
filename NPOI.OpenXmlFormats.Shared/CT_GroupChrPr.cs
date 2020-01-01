using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_GroupChrPr
	{
		private CT_Char chrField;

		private CT_TopBot posField;

		private CT_TopBot vertJcField;

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
		public CT_TopBot pos
		{
			get
			{
				return posField;
			}
			set
			{
				posField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TopBot vertJc
		{
			get
			{
				return vertJcField;
			}
			set
			{
				vertJcField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		public static CT_GroupChrPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GroupChrPr cT_GroupChrPr = new CT_GroupChrPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "chr")
				{
					cT_GroupChrPr.chr = CT_Char.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pos")
				{
					cT_GroupChrPr.pos = CT_TopBot.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vertJc")
				{
					cT_GroupChrPr.vertJc = CT_TopBot.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_GroupChrPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_GroupChrPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (chr != null)
			{
				chr.Write(sw, "chr");
			}
			if (pos != null)
			{
				pos.Write(sw, "pos");
			}
			if (vertJc != null)
			{
				vertJc.Write(sw, "vertJc");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
