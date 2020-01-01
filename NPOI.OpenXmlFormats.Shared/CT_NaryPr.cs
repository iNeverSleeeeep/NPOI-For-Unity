using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_NaryPr
	{
		private CT_Char chrField;

		private CT_LimLoc limLocField;

		private CT_OnOff growField;

		private CT_OnOff subHideField;

		private CT_OnOff supHideField;

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
		public CT_LimLoc limLoc
		{
			get
			{
				return limLocField;
			}
			set
			{
				limLocField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff grow
		{
			get
			{
				return growField;
			}
			set
			{
				growField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff subHide
		{
			get
			{
				return subHideField;
			}
			set
			{
				subHideField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff supHide
		{
			get
			{
				return supHideField;
			}
			set
			{
				supHideField = value;
			}
		}

		[XmlElement(Order = 5)]
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

		public static CT_NaryPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NaryPr cT_NaryPr = new CT_NaryPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "chr")
				{
					cT_NaryPr.chr = CT_Char.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "limLoc")
				{
					cT_NaryPr.limLoc = CT_LimLoc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grow")
				{
					cT_NaryPr.grow = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "subHide")
				{
					cT_NaryPr.subHide = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "supHide")
				{
					cT_NaryPr.supHide = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_NaryPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_NaryPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (chr != null)
			{
				chr.Write(sw, "chr");
			}
			if (limLoc != null)
			{
				limLoc.Write(sw, "limLoc");
			}
			if (grow != null)
			{
				grow.Write(sw, "grow");
			}
			if (subHide != null)
			{
				subHide.Write(sw, "subHide");
			}
			if (supHide != null)
			{
				supHide.Write(sw, "supHide");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
