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
	public class CT_CalcPr
	{
		private uint calcIdField;

		private bool calcIdFieldSpecified;

		private ST_CalcMode calcModeField;

		private bool fullCalcOnLoadField;

		private ST_RefMode refModeField;

		private bool iterateField;

		private uint iterateCountField;

		private double iterateDeltaField;

		private bool fullPrecisionField;

		private bool calcCompletedField;

		private bool calcOnSaveField;

		private bool concurrentCalcField;

		private uint concurrentManualCountField;

		private bool concurrentManualCountFieldSpecified;

		private bool forceFullCalcField;

		private bool forceFullCalcFieldSpecified;

		[XmlAttribute]
		public uint calcId
		{
			get
			{
				return calcIdField;
			}
			set
			{
				calcIdField = value;
			}
		}

		[XmlIgnore]
		public bool calcIdSpecified
		{
			get
			{
				return calcIdFieldSpecified;
			}
			set
			{
				calcIdFieldSpecified = value;
			}
		}

		[DefaultValue(ST_CalcMode.auto)]
		[XmlAttribute]
		public ST_CalcMode calcMode
		{
			get
			{
				return calcModeField;
			}
			set
			{
				calcModeField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool fullCalcOnLoad
		{
			get
			{
				return fullCalcOnLoadField;
			}
			set
			{
				fullCalcOnLoadField = value;
			}
		}

		[DefaultValue(ST_RefMode.A1)]
		[XmlAttribute]
		public ST_RefMode refMode
		{
			get
			{
				return refModeField;
			}
			set
			{
				refModeField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool iterate
		{
			get
			{
				return iterateField;
			}
			set
			{
				iterateField = value;
			}
		}

		[DefaultValue(typeof(uint), "100")]
		[XmlAttribute]
		public uint iterateCount
		{
			get
			{
				return iterateCountField;
			}
			set
			{
				iterateCountField = value;
			}
		}

		[DefaultValue(0.001)]
		[XmlAttribute]
		public double iterateDelta
		{
			get
			{
				return iterateDeltaField;
			}
			set
			{
				iterateDeltaField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool fullPrecision
		{
			get
			{
				return fullPrecisionField;
			}
			set
			{
				fullPrecisionField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool calcCompleted
		{
			get
			{
				return calcCompletedField;
			}
			set
			{
				calcCompletedField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool calcOnSave
		{
			get
			{
				return calcOnSaveField;
			}
			set
			{
				calcOnSaveField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool concurrentCalc
		{
			get
			{
				return concurrentCalcField;
			}
			set
			{
				concurrentCalcField = value;
			}
		}

		[XmlAttribute]
		public uint concurrentManualCount
		{
			get
			{
				return concurrentManualCountField;
			}
			set
			{
				concurrentManualCountField = value;
			}
		}

		[XmlIgnore]
		public bool concurrentManualCountSpecified
		{
			get
			{
				return concurrentManualCountFieldSpecified;
			}
			set
			{
				concurrentManualCountFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool forceFullCalc
		{
			get
			{
				return forceFullCalcField;
			}
			set
			{
				forceFullCalcField = value;
			}
		}

		[XmlIgnore]
		public bool forceFullCalcSpecified
		{
			get
			{
				return forceFullCalcFieldSpecified;
			}
			set
			{
				forceFullCalcFieldSpecified = value;
			}
		}

		public static CT_CalcPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CalcPr cT_CalcPr = new CT_CalcPr();
			cT_CalcPr.calcId = XmlHelper.ReadUInt(node.Attributes["calcId"]);
			if (node.Attributes["calcMode"] != null)
			{
				cT_CalcPr.calcMode = (ST_CalcMode)Enum.Parse(typeof(ST_CalcMode), node.Attributes["calcMode"].Value);
			}
			cT_CalcPr.fullCalcOnLoad = XmlHelper.ReadBool(node.Attributes["fullCalcOnLoad"]);
			if (node.Attributes["refMode"] != null)
			{
				cT_CalcPr.refMode = (ST_RefMode)Enum.Parse(typeof(ST_RefMode), node.Attributes["refMode"].Value);
			}
			cT_CalcPr.iterate = XmlHelper.ReadBool(node.Attributes["iterate"]);
			cT_CalcPr.iterateCount = XmlHelper.ReadUInt(node.Attributes["iterateCount"]);
			cT_CalcPr.iterateDelta = XmlHelper.ReadDouble(node.Attributes["iterateDelta"]);
			cT_CalcPr.fullPrecision = XmlHelper.ReadBool(node.Attributes["fullPrecision"]);
			cT_CalcPr.calcCompleted = XmlHelper.ReadBool(node.Attributes["calcCompleted"]);
			cT_CalcPr.calcOnSave = XmlHelper.ReadBool(node.Attributes["calcOnSave"]);
			cT_CalcPr.concurrentCalc = XmlHelper.ReadBool(node.Attributes["concurrentCalc"]);
			cT_CalcPr.concurrentManualCount = XmlHelper.ReadUInt(node.Attributes["concurrentManualCount"]);
			cT_CalcPr.forceFullCalc = XmlHelper.ReadBool(node.Attributes["forceFullCalc"]);
			return cT_CalcPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "calcId", calcId);
			if (calcMode == ST_CalcMode.auto)
			{
				XmlHelper.WriteAttribute(sw, "calcMode", calcMode.ToString());
			}
			XmlHelper.WriteAttribute(sw, "fullCalcOnLoad", fullCalcOnLoad);
			XmlHelper.WriteAttribute(sw, "refMode", refMode.ToString());
			XmlHelper.WriteAttribute(sw, "iterate", iterate);
			XmlHelper.WriteAttribute(sw, "iterateCount", iterateCount);
			XmlHelper.WriteAttribute(sw, "iterateDelta", iterateDelta);
			XmlHelper.WriteAttribute(sw, "fullPrecision", fullPrecision);
			XmlHelper.WriteAttribute(sw, "calcCompleted", calcCompleted);
			XmlHelper.WriteAttribute(sw, "calcOnSave", calcOnSave);
			XmlHelper.WriteAttribute(sw, "concurrentCalc", concurrentCalc);
			XmlHelper.WriteAttribute(sw, "concurrentManualCount", concurrentManualCount);
			XmlHelper.WriteAttribute(sw, "forceFullCalc", forceFullCalc);
			sw.Write("/>");
		}

		public CT_CalcPr()
		{
			calcModeField = ST_CalcMode.auto;
			fullCalcOnLoadField = false;
			refModeField = ST_RefMode.A1;
			iterateField = false;
			iterateCountField = 100u;
			iterateDeltaField = 0.001;
			fullPrecisionField = true;
			calcCompletedField = true;
			calcOnSaveField = true;
			concurrentCalcField = true;
		}
	}
}
