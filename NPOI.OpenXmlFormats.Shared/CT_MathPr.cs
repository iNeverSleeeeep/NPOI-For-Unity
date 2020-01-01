using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot("mathPr", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_MathPr
	{
		private CT_String mathFontField;

		private CT_BreakBin brkBinField;

		private CT_BreakBinSub brkBinSubField;

		private CT_OnOff smallFracField;

		private CT_OnOff dispDefField;

		private CT_TwipsMeasure lMarginField;

		private CT_TwipsMeasure rMarginField;

		private CT_OMathJc defJcField;

		private CT_TwipsMeasure preSpField;

		private CT_TwipsMeasure postSpField;

		private CT_TwipsMeasure interSpField;

		private CT_TwipsMeasure intraSpField;

		private object itemField;

		private string itemElementName;

		private CT_LimLoc intLimField;

		private CT_LimLoc naryLimField;

		[XmlElement(Order = 0)]
		public CT_String mathFont
		{
			get
			{
				return mathFontField;
			}
			set
			{
				mathFontField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_BreakBin brkBin
		{
			get
			{
				return brkBinField;
			}
			set
			{
				brkBinField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_BreakBinSub brkBinSub
		{
			get
			{
				return brkBinSubField;
			}
			set
			{
				brkBinSubField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff smallFrac
		{
			get
			{
				return smallFracField;
			}
			set
			{
				smallFracField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff dispDef
		{
			get
			{
				return dispDefField;
			}
			set
			{
				dispDefField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_TwipsMeasure lMargin
		{
			get
			{
				return lMarginField;
			}
			set
			{
				lMarginField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_TwipsMeasure rMargin
		{
			get
			{
				return rMarginField;
			}
			set
			{
				rMarginField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OMathJc defJc
		{
			get
			{
				return defJcField;
			}
			set
			{
				defJcField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_TwipsMeasure preSp
		{
			get
			{
				return preSpField;
			}
			set
			{
				preSpField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_TwipsMeasure postSp
		{
			get
			{
				return postSpField;
			}
			set
			{
				postSpField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_TwipsMeasure interSp
		{
			get
			{
				return interSpField;
			}
			set
			{
				interSpField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_TwipsMeasure intraSp
		{
			get
			{
				return intraSpField;
			}
			set
			{
				intraSpField = value;
			}
		}

		[XmlElement("wrapRight", typeof(CT_OnOff), Order = 12)]
		[XmlElement("wrapIndent", typeof(CT_TwipsMeasure), Order = 12)]
		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_LimLoc intLim
		{
			get
			{
				return intLimField;
			}
			set
			{
				intLimField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_LimLoc naryLim
		{
			get
			{
				return naryLimField;
			}
			set
			{
				naryLimField = value;
			}
		}

		public CT_MathPr()
		{
			naryLimField = new CT_LimLoc();
			intLimField = new CT_LimLoc();
			defJcField = new CT_OMathJc();
			rMarginField = new CT_TwipsMeasure();
			lMarginField = new CT_TwipsMeasure();
			smallFracField = new CT_OnOff();
			brkBinSubField = new CT_BreakBinSub();
			brkBinField = new CT_BreakBin();
			mathFontField = new CT_String();
			mathFont.val = "Cambria Math";
			brkBin.val = ST_BreakBin.before;
			brkBinSub.val = ST_BreakBinSub.Item;
			smallFrac.val = ST_OnOff.off;
			lMargin.val = 0u;
			rMargin.val = 0u;
			defJc.val = ST_Jc.centerGroup;
			itemField = new CT_TwipsMeasure();
			(Item as CT_TwipsMeasure).val = 1440u;
			itemElementName = "wrapIndent";
			intLim.val = ST_LimLoc.subSup;
			naryLim.val = ST_LimLoc.undOvr;
		}

		public static CT_MathPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MathPr cT_MathPr = new CT_MathPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "mathFont")
				{
					cT_MathPr.mathFont = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "brkBin")
				{
					cT_MathPr.brkBin = CT_BreakBin.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "brkBinSub")
				{
					cT_MathPr.brkBinSub = CT_BreakBinSub.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "smallFrac")
				{
					cT_MathPr.smallFrac = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dispDef")
				{
					cT_MathPr.dispDef = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lMargin")
				{
					cT_MathPr.lMargin = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rMargin")
				{
					cT_MathPr.rMargin = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "defJc")
				{
					cT_MathPr.defJc = CT_OMathJc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "preSp")
				{
					cT_MathPr.preSp = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "postSp")
				{
					cT_MathPr.postSp = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "interSp")
				{
					cT_MathPr.interSp = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "intraSp")
				{
					cT_MathPr.intraSp = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "wrapIndent")
				{
					cT_MathPr.Item = CT_TwipsMeasure.Parse(childNode, namespaceManager);
					cT_MathPr.itemElementName = "wrapIndent";
				}
				else if (childNode.LocalName == "wrapRight")
				{
					cT_MathPr.Item = CT_OnOff.Parse(childNode, namespaceManager);
					cT_MathPr.itemElementName = "wrapRight";
				}
				else if (childNode.LocalName == "intLim")
				{
					cT_MathPr.intLim = CT_LimLoc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "naryLim")
				{
					cT_MathPr.naryLim = CT_LimLoc.Parse(childNode, namespaceManager);
				}
			}
			return cT_MathPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (mathFont != null)
			{
				mathFont.Write(sw, "mathFont");
			}
			if (brkBin != null)
			{
				brkBin.Write(sw, "brkBin");
			}
			if (brkBinSub != null)
			{
				brkBinSub.Write(sw, "brkBinSub");
			}
			if (smallFrac != null)
			{
				smallFrac.Write(sw, "smallFrac");
			}
			if (dispDef != null)
			{
				dispDef.Write(sw, "dispDef");
			}
			if (lMargin != null)
			{
				lMargin.Write(sw, "lMargin");
			}
			if (rMargin != null)
			{
				rMargin.Write(sw, "rMargin");
			}
			if (defJc != null)
			{
				defJc.Write(sw, "defJc");
			}
			if (preSp != null)
			{
				preSp.Write(sw, "preSp");
			}
			if (postSp != null)
			{
				postSp.Write(sw, "postSp");
			}
			if (interSp != null)
			{
				interSp.Write(sw, "interSp");
			}
			if (intraSp != null)
			{
				intraSp.Write(sw, "intraSp");
			}
			if (Item != null)
			{
				if (itemElementName == "wrapIndent")
				{
					((CT_TwipsMeasure)itemField).Write(sw, itemElementName);
				}
				else
				{
					((CT_OnOff)itemField).Write(sw, itemElementName);
				}
			}
			if (intLim != null)
			{
				intLim.Write(sw, "intLim");
			}
			if (naryLim != null)
			{
				naryLim.Write(sw, "naryLim");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
