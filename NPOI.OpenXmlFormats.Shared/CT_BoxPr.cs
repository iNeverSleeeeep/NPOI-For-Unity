using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_BoxPr
	{
		private CT_OnOff opEmuField;

		private CT_OnOff noBreakField;

		private CT_OnOff diffField;

		private CT_ManualBreak brkField;

		private CT_OnOff alnField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_OnOff opEmu
		{
			get
			{
				return opEmuField;
			}
			set
			{
				opEmuField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff noBreak
		{
			get
			{
				return noBreakField;
			}
			set
			{
				noBreakField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff diff
		{
			get
			{
				return diffField;
			}
			set
			{
				diffField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_ManualBreak brk
		{
			get
			{
				return brkField;
			}
			set
			{
				brkField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff aln
		{
			get
			{
				return alnField;
			}
			set
			{
				alnField = value;
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

		public static CT_BoxPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BoxPr cT_BoxPr = new CT_BoxPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "opEmu")
				{
					cT_BoxPr.opEmu = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noBreak")
				{
					cT_BoxPr.noBreak = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "diff")
				{
					cT_BoxPr.diff = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "brk")
				{
					cT_BoxPr.brk = CT_ManualBreak.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "aln")
				{
					cT_BoxPr.aln = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_BoxPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_BoxPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (opEmu != null)
			{
				opEmu.Write(sw, "opEmu");
			}
			if (noBreak != null)
			{
				noBreak.Write(sw, "noBreak");
			}
			if (diff != null)
			{
				diff.Write(sw, "diff");
			}
			if (brk != null)
			{
				brk.Write(sw, "brk");
			}
			if (aln != null)
			{
				aln.Write(sw, "aln");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
