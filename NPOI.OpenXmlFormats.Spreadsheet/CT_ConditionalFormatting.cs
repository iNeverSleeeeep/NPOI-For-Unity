using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ConditionalFormatting
	{
		private List<CT_CfRule> cfRuleField;

		private CT_ExtensionList extLstField;

		private bool pivotField;

		private string sqrefField;

		[XmlElement]
		public List<CT_CfRule> cfRule
		{
			get
			{
				return cfRuleField;
			}
			set
			{
				cfRuleField = value;
			}
		}

		[XmlElement]
		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool pivot
		{
			get
			{
				return pivotField;
			}
			set
			{
				pivotField = value;
			}
		}

		[XmlAttribute]
		public string sqref
		{
			get
			{
				return sqrefField;
			}
			set
			{
				sqrefField = value;
			}
		}

		public static CT_ConditionalFormatting Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ConditionalFormatting cT_ConditionalFormatting = new CT_ConditionalFormatting();
			cT_ConditionalFormatting.pivot = XmlHelper.ReadBool(node.Attributes["pivot"]);
			cT_ConditionalFormatting.sqref = XmlHelper.ReadString(node.Attributes["sqref"]);
			cT_ConditionalFormatting.cfRule = new List<CT_CfRule>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_ConditionalFormatting.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cfRule")
				{
					cT_ConditionalFormatting.cfRule.Add(CT_CfRule.Parse(childNode, namespaceManager));
				}
			}
			return cT_ConditionalFormatting;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (pivot)
			{
				XmlHelper.WriteAttribute(sw, "pivot", pivot);
			}
			XmlHelper.WriteAttribute(sw, "sqref", sqref);
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (cfRule != null)
			{
				foreach (CT_CfRule item in cfRule)
				{
					item.Write(sw, "cfRule");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_ConditionalFormatting()
		{
			pivotField = false;
		}

		public int sizeOfCfRuleArray()
		{
			if (cfRule == null)
			{
				return 0;
			}
			return cfRule.Count;
		}

		public void Set(CT_ConditionalFormatting obj)
		{
			cfRuleField = obj.cfRuleField;
			extLstField = obj.extLstField;
			pivotField = obj.pivotField;
			sqrefField = obj.sqrefField;
		}

		public CT_CfRule GetCfRuleArray(int index)
		{
			return cfRule[index];
		}

		public CT_CfRule AddNewCfRule()
		{
			if (cfRule == null)
			{
				cfRule = new List<CT_CfRule>();
			}
			CT_CfRule cT_CfRule = new CT_CfRule();
			cfRule.Add(cT_CfRule);
			return cT_CfRule;
		}
	}
}
