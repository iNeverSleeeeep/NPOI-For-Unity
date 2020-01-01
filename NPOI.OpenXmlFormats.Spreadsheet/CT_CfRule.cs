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
	public class CT_CfRule
	{
		private List<string> formulaField;

		private CT_ColorScale colorScaleField;

		private CT_DataBar dataBarField;

		private CT_IconSet iconSetField;

		private CT_ExtensionList extLstField;

		private ST_CfType typeField;

		private bool typeFieldSpecified;

		private uint dxfIdField;

		private bool dxfIdFieldSpecified;

		private int priorityField;

		private bool stopIfTrueField;

		private bool aboveAverageField;

		private bool percentField;

		private bool bottomField;

		private ST_ConditionalFormattingOperator? operatorField;

		private bool operatorFieldSpecified;

		private string textField;

		private ST_TimePeriod? timePeriodField;

		private bool timePeriodFieldSpecified;

		private uint rankField;

		private bool rankFieldSpecified;

		private int stdDevField;

		private bool stdDevFieldSpecified;

		private bool equalAverageField;

		[XmlElement]
		public List<string> formula
		{
			get
			{
				return formulaField;
			}
			set
			{
				formulaField = value;
			}
		}

		[XmlElement]
		public CT_ColorScale colorScale
		{
			get
			{
				return colorScaleField;
			}
			set
			{
				colorScaleField = value;
			}
		}

		[XmlElement]
		public CT_DataBar dataBar
		{
			get
			{
				return dataBarField;
			}
			set
			{
				dataBarField = value;
			}
		}

		[XmlElement]
		public CT_IconSet iconSet
		{
			get
			{
				return iconSetField;
			}
			set
			{
				iconSetField = value;
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

		[XmlAttribute]
		public ST_CfType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint dxfId
		{
			get
			{
				return dxfIdField;
			}
			set
			{
				dxfIdField = value;
				dxfIdFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool dxfIdSpecified
		{
			get
			{
				return dxfIdFieldSpecified;
			}
			set
			{
				dxfIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int priority
		{
			get
			{
				return priorityField;
			}
			set
			{
				priorityField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool stopIfTrue
		{
			get
			{
				return stopIfTrueField;
			}
			set
			{
				stopIfTrueField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool aboveAverage
		{
			get
			{
				return aboveAverageField;
			}
			set
			{
				aboveAverageField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool percent
		{
			get
			{
				return percentField;
			}
			set
			{
				percentField = value;
			}
		}

		[DefaultValue(false)]
		public bool bottom
		{
			get
			{
				return bottomField;
			}
			set
			{
				bottomField = value;
			}
		}

		[XmlAttribute]
		public ST_ConditionalFormattingOperator? @operator
		{
			get
			{
				return operatorField;
			}
			set
			{
				operatorField = value;
			}
		}

		[XmlIgnore]
		public bool operatorSpecified
		{
			get
			{
				return operatorFieldSpecified;
			}
			set
			{
				operatorFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string text
		{
			get
			{
				return textField;
			}
			set
			{
				textField = value;
			}
		}

		[XmlAttribute]
		public ST_TimePeriod? timePeriod
		{
			get
			{
				return timePeriodField;
			}
			set
			{
				timePeriodField = value;
			}
		}

		[XmlIgnore]
		public bool timePeriodSpecified
		{
			get
			{
				return timePeriodFieldSpecified;
			}
			set
			{
				timePeriodFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint rank
		{
			get
			{
				return rankField;
			}
			set
			{
				rankField = value;
			}
		}

		[XmlIgnore]
		public bool rankSpecified
		{
			get
			{
				return rankFieldSpecified;
			}
			set
			{
				rankFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int stdDev
		{
			get
			{
				return stdDevField;
			}
			set
			{
				stdDevField = value;
			}
		}

		[XmlIgnore]
		public bool stdDevSpecified
		{
			get
			{
				return stdDevFieldSpecified;
			}
			set
			{
				stdDevFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool equalAverage
		{
			get
			{
				return equalAverageField;
			}
			set
			{
				equalAverageField = value;
			}
		}

		public CT_CfRule()
		{
			dxfIdSpecified = false;
			stopIfTrueField = false;
			aboveAverageField = true;
			percentField = false;
			bottomField = false;
			equalAverageField = false;
		}

		public static CT_CfRule Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CfRule cT_CfRule = new CT_CfRule();
			if (node.Attributes["type"] != null)
			{
				cT_CfRule.type = (ST_CfType)Enum.Parse(typeof(ST_CfType), node.Attributes["type"].Value);
			}
			if (node.Attributes["dxfId"] != null)
			{
				cT_CfRule.dxfId = XmlHelper.ReadUInt(node.Attributes["dxfId"]);
				cT_CfRule.dxfIdFieldSpecified = true;
			}
			cT_CfRule.priority = XmlHelper.ReadInt(node.Attributes["priority"]);
			cT_CfRule.stopIfTrue = XmlHelper.ReadBool(node.Attributes["stopIfTrue"]);
			if (node.Attributes["aboveAverage"] != null)
			{
				cT_CfRule.aboveAverage = XmlHelper.ReadBool(node.Attributes["aboveAverage"]);
			}
			cT_CfRule.percent = XmlHelper.ReadBool(node.Attributes["percent"]);
			cT_CfRule.bottom = XmlHelper.ReadBool(node.Attributes["bottom"]);
			if (node.Attributes["operator"] != null)
			{
				cT_CfRule.@operator = (ST_ConditionalFormattingOperator)Enum.Parse(typeof(ST_ConditionalFormattingOperator), node.Attributes["operator"].Value);
			}
			cT_CfRule.text = XmlHelper.ReadString(node.Attributes["text"]);
			if (node.Attributes["timePeriod"] != null)
			{
				cT_CfRule.timePeriod = (ST_TimePeriod)Enum.Parse(typeof(ST_TimePeriod), node.Attributes["timePeriod"].Value);
			}
			cT_CfRule.rank = XmlHelper.ReadUInt(node.Attributes["rank"]);
			cT_CfRule.stdDev = XmlHelper.ReadInt(node.Attributes["stdDev"]);
			cT_CfRule.equalAverage = XmlHelper.ReadBool(node.Attributes["equalAverage"]);
			cT_CfRule.formula = new List<string>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "colorScale")
				{
					cT_CfRule.colorScale = CT_ColorScale.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dataBar")
				{
					cT_CfRule.dataBar = CT_DataBar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "iconSet")
				{
					cT_CfRule.iconSet = CT_IconSet.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_CfRule.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "formula")
				{
					cT_CfRule.formula.Add(childNode.InnerText);
				}
			}
			return cT_CfRule;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			if (dxfIdSpecified)
			{
				XmlHelper.WriteAttribute(sw, "dxfId", (double)dxfId, true);
			}
			XmlHelper.WriteAttribute(sw, "priority", priority, true);
			if (stopIfTrue)
			{
				XmlHelper.WriteAttribute(sw, "stopIfTrue", stopIfTrue);
			}
			if (aboveAverage)
			{
				XmlHelper.WriteAttribute(sw, "aboveAverage", aboveAverage, true);
			}
			if (percent)
			{
				XmlHelper.WriteAttribute(sw, "percent", percent);
			}
			if (bottom)
			{
				XmlHelper.WriteAttribute(sw, "bottom", bottom);
			}
			if (@operator.HasValue)
			{
				XmlHelper.WriteAttribute(sw, "operator", @operator.ToString());
			}
			XmlHelper.WriteAttribute(sw, "text", text);
			if (timePeriodField.HasValue)
			{
				XmlHelper.WriteAttribute(sw, "timePeriod", timePeriod.ToString());
			}
			XmlHelper.WriteAttribute(sw, "rank", rank);
			XmlHelper.WriteAttribute(sw, "stdDev", stdDev);
			if (equalAverageField)
			{
				XmlHelper.WriteAttribute(sw, "equalAverage", equalAverage);
			}
			sw.Write(">");
			if (colorScale != null)
			{
				colorScale.Write(sw, "colorScale");
			}
			if (dataBar != null)
			{
				dataBar.Write(sw, "dataBar");
			}
			if (iconSet != null)
			{
				iconSet.Write(sw, "iconSet");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (formula != null)
			{
				foreach (string item in formula)
				{
					sw.Write(string.Format("<formula>{0}</formula>", XmlHelper.EncodeXml(item)));
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public bool IsSetDxfId()
		{
			return dxfIdFieldSpecified;
		}

		public void AddFormula(string formula)
		{
			if (this.formula == null)
			{
				this.formula = new List<string>();
			}
			this.formula.Add(formula);
		}

		public void Set(CT_CfRule src)
		{
			formula = src.formula;
			stopIfTrue = src.stopIfTrue;
			bottom = src.bottom;
			percent = src.percent;
			dxfId = src.dxfId;
			priority = src.priority;
			@operator = src.@operator;
			type = src.type;
			equalAverage = src.equalAverage;
			aboveAverage = src.aboveAverage;
			colorScale = src.colorScale;
			dataBar = src.dataBar;
			iconSet = src.iconSet;
			extLst = src.extLst;
		}

		public int sizeOfFormulaArray()
		{
			return formula.Count;
		}

		public string GetFormulaArray(int index)
		{
			return formula[index];
		}
	}
}
