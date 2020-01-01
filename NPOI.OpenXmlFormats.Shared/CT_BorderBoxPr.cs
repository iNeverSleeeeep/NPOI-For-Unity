using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_BorderBoxPr
	{
		private CT_OnOff hideTopField;

		private CT_OnOff hideBotField;

		private CT_OnOff hideLeftField;

		private CT_OnOff hideRightField;

		private CT_OnOff strikeHField;

		private CT_OnOff strikeVField;

		private CT_OnOff strikeBLTRField;

		private CT_OnOff strikeTLBRField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_OnOff hideTop
		{
			get
			{
				return hideTopField;
			}
			set
			{
				hideTopField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff hideBot
		{
			get
			{
				return hideBotField;
			}
			set
			{
				hideBotField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff hideLeft
		{
			get
			{
				return hideLeftField;
			}
			set
			{
				hideLeftField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff hideRight
		{
			get
			{
				return hideRightField;
			}
			set
			{
				hideRightField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff strikeH
		{
			get
			{
				return strikeHField;
			}
			set
			{
				strikeHField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_OnOff strikeV
		{
			get
			{
				return strikeVField;
			}
			set
			{
				strikeVField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_OnOff strikeBLTR
		{
			get
			{
				return strikeBLTRField;
			}
			set
			{
				strikeBLTRField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OnOff strikeTLBR
		{
			get
			{
				return strikeTLBRField;
			}
			set
			{
				strikeTLBRField = value;
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

		public static CT_BorderBoxPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BorderBoxPr cT_BorderBoxPr = new CT_BorderBoxPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "hideTop")
				{
					cT_BorderBoxPr.hideTop = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hideBot")
				{
					cT_BorderBoxPr.hideBot = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hideLeft")
				{
					cT_BorderBoxPr.hideLeft = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hideRight")
				{
					cT_BorderBoxPr.hideRight = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strikeH")
				{
					cT_BorderBoxPr.strikeH = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strikeV")
				{
					cT_BorderBoxPr.strikeV = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strikeBLTR")
				{
					cT_BorderBoxPr.strikeBLTR = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strikeTLBR")
				{
					cT_BorderBoxPr.strikeTLBR = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_BorderBoxPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_BorderBoxPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (hideTop != null)
			{
				hideTop.Write(sw, "hideTop");
			}
			if (hideBot != null)
			{
				hideBot.Write(sw, "hideBot");
			}
			if (hideLeft != null)
			{
				hideLeft.Write(sw, "hideLeft");
			}
			if (hideRight != null)
			{
				hideRight.Write(sw, "hideRight");
			}
			if (strikeH != null)
			{
				strikeH.Write(sw, "strikeH");
			}
			if (strikeV != null)
			{
				strikeV.Write(sw, "strikeV");
			}
			if (strikeBLTR != null)
			{
				strikeBLTR.Write(sw, "strikeBLTR");
			}
			if (strikeTLBR != null)
			{
				strikeTLBR.Write(sw, "strikeTLBR");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
