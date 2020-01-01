using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_EqArrPr
	{
		private CT_YAlign baseJcField;

		private CT_OnOff maxDistField;

		private CT_OnOff objDistField;

		private CT_SpacingRule rSpRuleField;

		private CT_UnSignedInteger rSpField;

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
		public CT_OnOff maxDist
		{
			get
			{
				return maxDistField;
			}
			set
			{
				maxDistField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff objDist
		{
			get
			{
				return objDistField;
			}
			set
			{
				objDistField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		public static CT_EqArrPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EqArrPr cT_EqArrPr = new CT_EqArrPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "baseJc")
				{
					cT_EqArrPr.baseJc = CT_YAlign.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "maxDist")
				{
					cT_EqArrPr.maxDist = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "objDist")
				{
					cT_EqArrPr.objDist = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rSpRule")
				{
					cT_EqArrPr.rSpRule = CT_SpacingRule.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rSp")
				{
					cT_EqArrPr.rSp = CT_UnSignedInteger.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_EqArrPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_EqArrPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (baseJc != null)
			{
				baseJc.Write(sw, "baseJc");
			}
			if (maxDist != null)
			{
				maxDist.Write(sw, "maxDist");
			}
			if (objDist != null)
			{
				objDist.Write(sw, "objDist");
			}
			if (rSpRule != null)
			{
				rSpRule.Write(sw, "rSpRule");
			}
			if (rSp != null)
			{
				rSp.Write(sw, "rSp");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
