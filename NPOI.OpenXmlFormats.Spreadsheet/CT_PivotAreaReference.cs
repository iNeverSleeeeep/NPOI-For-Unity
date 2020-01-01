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
	public class CT_PivotAreaReference
	{
		private List<CT_Index> xField;

		private CT_ExtensionList extLstField;

		private uint fieldField;

		private bool fieldFieldSpecified;

		private uint countField;

		private bool countFieldSpecified;

		private bool selectedField;

		private bool byPositionField;

		private bool relativeField;

		private bool defaultSubtotalField;

		private bool sumSubtotalField;

		private bool countASubtotalField;

		private bool avgSubtotalField;

		private bool maxSubtotalField;

		private bool minSubtotalField;

		private bool productSubtotalField;

		private bool countSubtotalField;

		private bool stdDevSubtotalField;

		private bool stdDevPSubtotalField;

		private bool varSubtotalField;

		private bool varPSubtotalField;

		public List<CT_Index> x
		{
			get
			{
				return xField;
			}
			set
			{
				xField = value;
			}
		}

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

		public uint field
		{
			get
			{
				return fieldField;
			}
			set
			{
				fieldField = value;
			}
		}

		[XmlIgnore]
		public bool fieldSpecified
		{
			get
			{
				return fieldFieldSpecified;
			}
			set
			{
				fieldFieldSpecified = value;
			}
		}

		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		[XmlIgnore]
		public bool countSpecified
		{
			get
			{
				return countFieldSpecified;
			}
			set
			{
				countFieldSpecified = value;
			}
		}

		[DefaultValue(true)]
		public bool selected
		{
			get
			{
				return selectedField;
			}
			set
			{
				selectedField = value;
			}
		}

		[DefaultValue(false)]
		public bool byPosition
		{
			get
			{
				return byPositionField;
			}
			set
			{
				byPositionField = value;
			}
		}

		[DefaultValue(false)]
		public bool relative
		{
			get
			{
				return relativeField;
			}
			set
			{
				relativeField = value;
			}
		}

		[DefaultValue(false)]
		public bool defaultSubtotal
		{
			get
			{
				return defaultSubtotalField;
			}
			set
			{
				defaultSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool sumSubtotal
		{
			get
			{
				return sumSubtotalField;
			}
			set
			{
				sumSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool countASubtotal
		{
			get
			{
				return countASubtotalField;
			}
			set
			{
				countASubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool avgSubtotal
		{
			get
			{
				return avgSubtotalField;
			}
			set
			{
				avgSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool maxSubtotal
		{
			get
			{
				return maxSubtotalField;
			}
			set
			{
				maxSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool minSubtotal
		{
			get
			{
				return minSubtotalField;
			}
			set
			{
				minSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool productSubtotal
		{
			get
			{
				return productSubtotalField;
			}
			set
			{
				productSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool countSubtotal
		{
			get
			{
				return countSubtotalField;
			}
			set
			{
				countSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool stdDevSubtotal
		{
			get
			{
				return stdDevSubtotalField;
			}
			set
			{
				stdDevSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool stdDevPSubtotal
		{
			get
			{
				return stdDevPSubtotalField;
			}
			set
			{
				stdDevPSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool varSubtotal
		{
			get
			{
				return varSubtotalField;
			}
			set
			{
				varSubtotalField = value;
			}
		}

		[DefaultValue(false)]
		public bool varPSubtotal
		{
			get
			{
				return varPSubtotalField;
			}
			set
			{
				varPSubtotalField = value;
			}
		}

		public static CT_PivotAreaReference Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PivotAreaReference cT_PivotAreaReference = new CT_PivotAreaReference();
			cT_PivotAreaReference.field = XmlHelper.ReadUInt(node.Attributes["field"]);
			cT_PivotAreaReference.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_PivotAreaReference.selected = XmlHelper.ReadBool(node.Attributes["selected"]);
			cT_PivotAreaReference.byPosition = XmlHelper.ReadBool(node.Attributes["byPosition"]);
			cT_PivotAreaReference.relative = XmlHelper.ReadBool(node.Attributes["relative"]);
			cT_PivotAreaReference.defaultSubtotal = XmlHelper.ReadBool(node.Attributes["defaultSubtotal"]);
			cT_PivotAreaReference.sumSubtotal = XmlHelper.ReadBool(node.Attributes["sumSubtotal"]);
			cT_PivotAreaReference.countASubtotal = XmlHelper.ReadBool(node.Attributes["countASubtotal"]);
			cT_PivotAreaReference.avgSubtotal = XmlHelper.ReadBool(node.Attributes["avgSubtotal"]);
			cT_PivotAreaReference.maxSubtotal = XmlHelper.ReadBool(node.Attributes["maxSubtotal"]);
			cT_PivotAreaReference.minSubtotal = XmlHelper.ReadBool(node.Attributes["minSubtotal"]);
			cT_PivotAreaReference.productSubtotal = XmlHelper.ReadBool(node.Attributes["productSubtotal"]);
			cT_PivotAreaReference.countSubtotal = XmlHelper.ReadBool(node.Attributes["countSubtotal"]);
			cT_PivotAreaReference.stdDevSubtotal = XmlHelper.ReadBool(node.Attributes["stdDevSubtotal"]);
			cT_PivotAreaReference.stdDevPSubtotal = XmlHelper.ReadBool(node.Attributes["stdDevPSubtotal"]);
			cT_PivotAreaReference.varSubtotal = XmlHelper.ReadBool(node.Attributes["varSubtotal"]);
			cT_PivotAreaReference.varPSubtotal = XmlHelper.ReadBool(node.Attributes["varPSubtotal"]);
			cT_PivotAreaReference.x = new List<CT_Index>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_PivotAreaReference.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "x")
				{
					cT_PivotAreaReference.x.Add(CT_Index.Parse(childNode, namespaceManager));
				}
			}
			return cT_PivotAreaReference;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "field", field);
			XmlHelper.WriteAttribute(sw, "count", count);
			XmlHelper.WriteAttribute(sw, "selected", selected);
			XmlHelper.WriteAttribute(sw, "byPosition", byPosition);
			XmlHelper.WriteAttribute(sw, "relative", relative);
			XmlHelper.WriteAttribute(sw, "defaultSubtotal", defaultSubtotal);
			XmlHelper.WriteAttribute(sw, "sumSubtotal", sumSubtotal);
			XmlHelper.WriteAttribute(sw, "countASubtotal", countASubtotal);
			XmlHelper.WriteAttribute(sw, "avgSubtotal", avgSubtotal);
			XmlHelper.WriteAttribute(sw, "maxSubtotal", maxSubtotal);
			XmlHelper.WriteAttribute(sw, "minSubtotal", minSubtotal);
			XmlHelper.WriteAttribute(sw, "productSubtotal", productSubtotal);
			XmlHelper.WriteAttribute(sw, "countSubtotal", countSubtotal);
			XmlHelper.WriteAttribute(sw, "stdDevSubtotal", stdDevSubtotal);
			XmlHelper.WriteAttribute(sw, "stdDevPSubtotal", stdDevPSubtotal);
			XmlHelper.WriteAttribute(sw, "varSubtotal", varSubtotal);
			XmlHelper.WriteAttribute(sw, "varPSubtotal", varPSubtotal);
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (x != null)
			{
				foreach (CT_Index item in x)
				{
					item.Write(sw, "x");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_PivotAreaReference()
		{
			extLstField = new CT_ExtensionList();
			xField = new List<CT_Index>();
			selectedField = true;
			byPositionField = false;
			relativeField = false;
			defaultSubtotalField = false;
			sumSubtotalField = false;
			countASubtotalField = false;
			avgSubtotalField = false;
			maxSubtotalField = false;
			minSubtotalField = false;
			productSubtotalField = false;
			countSubtotalField = false;
			stdDevSubtotalField = false;
			stdDevPSubtotalField = false;
			varSubtotalField = false;
			varPSubtotalField = false;
		}
	}
}
