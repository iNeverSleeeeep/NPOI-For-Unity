using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_MPr
	{
		private CT_YAlign baseJcField;

		private CT_OnOff plcHideField;

		private CT_SpacingRule rSpRuleField;

		private CT_SpacingRule cGpRuleField;

		private CT_UnSignedInteger rSpField;

		private CT_UnSignedInteger cSpField;

		private CT_UnSignedInteger cGpField;

		private List<CT_MC> mcsField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_YAlign baseJc
		{
			get
			{
				return baseJcField;
			}
			set
			{
				baseJcField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff plcHide
		{
			get
			{
				return plcHideField;
			}
			set
			{
				plcHideField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_SpacingRule rSpRule
		{
			get
			{
				return rSpRuleField;
			}
			set
			{
				rSpRuleField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_SpacingRule cGpRule
		{
			get
			{
				return cGpRuleField;
			}
			set
			{
				cGpRuleField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_UnSignedInteger rSp
		{
			get
			{
				return rSpField;
			}
			set
			{
				rSpField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_UnSignedInteger cSp
		{
			get
			{
				return cSpField;
			}
			set
			{
				cSpField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_UnSignedInteger cGp
		{
			get
			{
				return cGpField;
			}
			set
			{
				cGpField = value;
			}
		}

		[XmlArrayItem("mc", IsNullable = false)]
		[XmlArray(Order = 7)]
		public List<CT_MC> mcs
		{
			get
			{
				return mcsField;
			}
			set
			{
				mcsField = value;
			}
		}

		[XmlElement(Order = 8)]
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

		public static CT_MPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MPr cT_MPr = new CT_MPr();
			cT_MPr.mcs = new List<CT_MC>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "baseJc")
				{
					cT_MPr.baseJc = CT_YAlign.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "plcHide")
				{
					cT_MPr.plcHide = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rSpRule")
				{
					cT_MPr.rSpRule = CT_SpacingRule.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cGpRule")
				{
					cT_MPr.cGpRule = CT_SpacingRule.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rSp")
				{
					cT_MPr.rSp = CT_UnSignedInteger.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cSp")
				{
					cT_MPr.cSp = CT_UnSignedInteger.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cGp")
				{
					cT_MPr.cGp = CT_UnSignedInteger.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_MPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mcs")
				{
					cT_MPr.mcs.Add(CT_MC.Parse(childNode, namespaceManager));
				}
			}
			return cT_MPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (baseJc != null)
			{
				baseJc.Write(sw, "baseJc");
			}
			if (plcHide != null)
			{
				plcHide.Write(sw, "plcHide");
			}
			if (rSpRule != null)
			{
				rSpRule.Write(sw, "rSpRule");
			}
			if (cGpRule != null)
			{
				cGpRule.Write(sw, "cGpRule");
			}
			if (rSp != null)
			{
				rSp.Write(sw, "rSp");
			}
			if (cSp != null)
			{
				cSp.Write(sw, "cSp");
			}
			if (cGp != null)
			{
				cGp.Write(sw, "cGp");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			if (mcs != null)
			{
				foreach (CT_MC mc in mcs)
				{
					mc.Write(sw, "mcs");
				}
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
