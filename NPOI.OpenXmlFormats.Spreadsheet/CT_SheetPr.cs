using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SheetPr
	{
		private CT_Color tabColorField;

		private CT_OutlinePr outlinePrField;

		private CT_PageSetUpPr pageSetUpPrField;

		private bool syncHorizontalField;

		private bool syncVerticalField;

		private string syncRefField;

		private bool transitionEvaluationField;

		private bool transitionEntryField;

		private bool publishedField;

		private string codeNameField;

		private bool filterModeField;

		private bool enableFormatConditionsCalculationField;

		public CT_Color tabColor
		{
			get
			{
				return tabColorField;
			}
			set
			{
				tabColorField = value;
			}
		}

		public CT_OutlinePr outlinePr
		{
			get
			{
				return outlinePrField;
			}
			set
			{
				outlinePrField = value;
			}
		}

		public CT_PageSetUpPr pageSetUpPr
		{
			get
			{
				return pageSetUpPrField;
			}
			set
			{
				pageSetUpPrField = value;
			}
		}

		[DefaultValue(false)]
		public bool syncHorizontal
		{
			get
			{
				return syncHorizontalField;
			}
			set
			{
				syncHorizontalField = value;
			}
		}

		[DefaultValue(false)]
		public bool syncVertical
		{
			get
			{
				return syncVerticalField;
			}
			set
			{
				syncVerticalField = value;
			}
		}

		public string syncRef
		{
			get
			{
				return syncRefField;
			}
			set
			{
				syncRefField = value;
			}
		}

		[DefaultValue(false)]
		public bool transitionEvaluation
		{
			get
			{
				return transitionEvaluationField;
			}
			set
			{
				transitionEvaluationField = value;
			}
		}

		[DefaultValue(false)]
		public bool transitionEntry
		{
			get
			{
				return transitionEntryField;
			}
			set
			{
				transitionEntryField = value;
			}
		}

		[DefaultValue(true)]
		public bool published
		{
			get
			{
				return publishedField;
			}
			set
			{
				publishedField = value;
			}
		}

		public string codeName
		{
			get
			{
				return codeNameField;
			}
			set
			{
				codeNameField = value;
			}
		}

		[DefaultValue(false)]
		public bool filterMode
		{
			get
			{
				return filterModeField;
			}
			set
			{
				filterModeField = value;
			}
		}

		[DefaultValue(true)]
		public bool enableFormatConditionsCalculation
		{
			get
			{
				return enableFormatConditionsCalculationField;
			}
			set
			{
				enableFormatConditionsCalculationField = value;
			}
		}

		public static CT_SheetPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SheetPr cT_SheetPr = new CT_SheetPr();
			cT_SheetPr.syncHorizontal = XmlHelper.ReadBool(node.Attributes["syncHorizontal"]);
			cT_SheetPr.syncVertical = XmlHelper.ReadBool(node.Attributes["syncVertical"]);
			cT_SheetPr.syncRef = XmlHelper.ReadString(node.Attributes["syncRef"]);
			cT_SheetPr.transitionEvaluation = XmlHelper.ReadBool(node.Attributes["transitionEvaluation"]);
			cT_SheetPr.transitionEntry = XmlHelper.ReadBool(node.Attributes["transitionEntry"]);
			cT_SheetPr.published = XmlHelper.ReadBool(node.Attributes["published"]);
			cT_SheetPr.codeName = XmlHelper.ReadString(node.Attributes["codeName"]);
			cT_SheetPr.filterMode = XmlHelper.ReadBool(node.Attributes["filterMode"]);
			cT_SheetPr.enableFormatConditionsCalculation = XmlHelper.ReadBool(node.Attributes["enableFormatConditionsCalculation"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tabColor")
				{
					cT_SheetPr.tabColor = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "outlinePr")
				{
					cT_SheetPr.outlinePr = CT_OutlinePr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageSetUpPr")
				{
					cT_SheetPr.pageSetUpPr = CT_PageSetUpPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_SheetPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "syncHorizontal", syncHorizontal, false);
			XmlHelper.WriteAttribute(sw, "syncVertical", syncVertical, false);
			XmlHelper.WriteAttribute(sw, "syncRef", syncRef);
			XmlHelper.WriteAttribute(sw, "transitionEvaluation", transitionEvaluation, false);
			XmlHelper.WriteAttribute(sw, "transitionEntry", transitionEntry, false);
			XmlHelper.WriteAttribute(sw, "published", published, false);
			XmlHelper.WriteAttribute(sw, "codeName", codeName);
			XmlHelper.WriteAttribute(sw, "filterMode", filterMode, false);
			XmlHelper.WriteAttribute(sw, "enableFormatConditionsCalculation", enableFormatConditionsCalculation, false);
			sw.Write(">");
			if (tabColor != null)
			{
				tabColor.Write(sw, "tabColor");
			}
			if (outlinePr != null)
			{
				outlinePr.Write(sw, "outlinePr");
			}
			if (pageSetUpPr != null)
			{
				pageSetUpPr.Write(sw, "pageSetUpPr");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_SheetPr()
		{
			syncHorizontalField = false;
			syncVerticalField = false;
			transitionEvaluationField = false;
			transitionEntryField = false;
			publishedField = true;
			filterModeField = false;
			enableFormatConditionsCalculationField = true;
		}

		public bool IsSetOutlinePr()
		{
			return outlinePrField != null;
		}

		public bool IsSetPageSetUpPr()
		{
			return pageSetUpPrField != null;
		}

		public CT_PageSetUpPr AddNewPageSetUpPr()
		{
			pageSetUpPrField = new CT_PageSetUpPr();
			return pageSetUpPrField;
		}

		public CT_OutlinePr AddNewOutlinePr()
		{
			outlinePrField = new CT_OutlinePr();
			return outlinePrField;
		}

		public bool IsSetTabColor()
		{
			return tabColor != null;
		}
	}
}
