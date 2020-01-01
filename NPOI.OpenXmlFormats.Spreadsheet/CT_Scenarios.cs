using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Scenarios
	{
		private List<CT_Scenario> scenarioField;

		private uint currentField;

		private bool currentFieldSpecified;

		private uint showField;

		private bool showFieldSpecified;

		private string sqrefField;

		[XmlElement]
		public List<CT_Scenario> scenario
		{
			get
			{
				return scenarioField;
			}
			set
			{
				scenarioField = value;
			}
		}

		[XmlAttribute]
		public uint current
		{
			get
			{
				return currentField;
			}
			set
			{
				currentField = value;
			}
		}

		[XmlIgnore]
		public bool currentSpecified
		{
			get
			{
				return currentFieldSpecified;
			}
			set
			{
				currentFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint show
		{
			get
			{
				return showField;
			}
			set
			{
				showField = value;
			}
		}

		[XmlIgnore]
		public bool showSpecified
		{
			get
			{
				return showFieldSpecified;
			}
			set
			{
				showFieldSpecified = value;
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

		public static CT_Scenarios Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Scenarios cT_Scenarios = new CT_Scenarios();
			cT_Scenarios.current = XmlHelper.ReadUInt(node.Attributes["current"]);
			cT_Scenarios.show = XmlHelper.ReadUInt(node.Attributes["show"]);
			cT_Scenarios.scenario = new List<CT_Scenario>();
			cT_Scenarios.sqref = XmlHelper.ReadString(node.Attributes["sqref"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "scenario")
				{
					cT_Scenarios.scenario.Add(CT_Scenario.Parse(childNode, namespaceManager));
				}
			}
			return cT_Scenarios;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "current", this.current);
			XmlHelper.WriteAttribute(sw, "show", show);
			XmlHelper.WriteAttribute(sw, "sqref", sqref);
			sw.Write(">");
			if (scenario != null)
			{
				foreach (CT_Scenario item in scenario)
				{
					item.Write(sw, "scenario");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
