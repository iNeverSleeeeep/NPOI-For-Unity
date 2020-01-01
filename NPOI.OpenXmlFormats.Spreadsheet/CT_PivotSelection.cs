using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_PivotSelection
	{
		private CT_PivotArea pivotAreaField;

		private ST_Pane paneField;

		private bool showHeaderField;

		private bool labelField;

		private bool dataField;

		private bool extendableField;

		private uint countField;

		private ST_Axis axisField;

		private bool axisFieldSpecified;

		private uint dimensionField;

		private uint startField;

		private uint minField;

		private uint maxField;

		private uint activeRowField;

		private uint activeColField;

		private uint previousRowField;

		private uint previousColField;

		private uint clickField;

		private string idField;

		public CT_PivotArea pivotArea
		{
			get
			{
				return pivotAreaField;
			}
			set
			{
				pivotAreaField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_Pane.topLeft)]
		public ST_Pane pane
		{
			get
			{
				return paneField;
			}
			set
			{
				paneField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool showHeader
		{
			get
			{
				return showHeaderField;
			}
			set
			{
				showHeaderField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool label
		{
			get
			{
				return labelField;
			}
			set
			{
				labelField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool data
		{
			get
			{
				return dataField;
			}
			set
			{
				dataField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool extendable
		{
			get
			{
				return extendableField;
			}
			set
			{
				extendableField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
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

		[XmlAttribute]
		public ST_Axis axis
		{
			get
			{
				return axisField;
			}
			set
			{
				axisField = value;
			}
		}

		[XmlIgnore]
		public bool axisSpecified
		{
			get
			{
				return axisFieldSpecified;
			}
			set
			{
				axisFieldSpecified = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint dimension
		{
			get
			{
				return dimensionField;
			}
			set
			{
				dimensionField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint start
		{
			get
			{
				return startField;
			}
			set
			{
				startField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint min
		{
			get
			{
				return minField;
			}
			set
			{
				minField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint max
		{
			get
			{
				return maxField;
			}
			set
			{
				maxField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint activeRow
		{
			get
			{
				return activeRowField;
			}
			set
			{
				activeRowField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint activeCol
		{
			get
			{
				return activeColField;
			}
			set
			{
				activeColField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint previousRow
		{
			get
			{
				return previousRowField;
			}
			set
			{
				previousRowField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint previousCol
		{
			get
			{
				return previousColField;
			}
			set
			{
				previousColField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint click
		{
			get
			{
				return clickField;
			}
			set
			{
				clickField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		public CT_PivotSelection()
		{
			pivotAreaField = new CT_PivotArea();
			paneField = ST_Pane.topLeft;
			showHeaderField = false;
			labelField = false;
			dataField = false;
			extendableField = false;
			countField = 0u;
			dimensionField = 0u;
			startField = 0u;
			minField = 0u;
			maxField = 0u;
			activeRowField = 0u;
			activeColField = 0u;
			previousRowField = 0u;
			previousColField = 0u;
			clickField = 0u;
		}

		public static CT_PivotSelection Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PivotSelection cT_PivotSelection = new CT_PivotSelection();
			if (node.Attributes["pane"] != null)
			{
				cT_PivotSelection.pane = (ST_Pane)Enum.Parse(typeof(ST_Pane), node.Attributes["pane"].Value);
			}
			cT_PivotSelection.showHeader = XmlHelper.ReadBool(node.Attributes["showHeader"]);
			cT_PivotSelection.label = XmlHelper.ReadBool(node.Attributes["label"]);
			cT_PivotSelection.data = XmlHelper.ReadBool(node.Attributes["data"]);
			cT_PivotSelection.extendable = XmlHelper.ReadBool(node.Attributes["extendable"]);
			cT_PivotSelection.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			if (node.Attributes["axis"] != null)
			{
				cT_PivotSelection.axis = (ST_Axis)Enum.Parse(typeof(ST_Axis), node.Attributes["axis"].Value);
			}
			cT_PivotSelection.dimension = XmlHelper.ReadUInt(node.Attributes["dimension"]);
			cT_PivotSelection.start = XmlHelper.ReadUInt(node.Attributes["start"]);
			cT_PivotSelection.min = XmlHelper.ReadUInt(node.Attributes["min"]);
			cT_PivotSelection.max = XmlHelper.ReadUInt(node.Attributes["max"]);
			cT_PivotSelection.activeRow = XmlHelper.ReadUInt(node.Attributes["activeRow"]);
			cT_PivotSelection.activeCol = XmlHelper.ReadUInt(node.Attributes["activeCol"]);
			cT_PivotSelection.previousRow = XmlHelper.ReadUInt(node.Attributes["previousRow"]);
			cT_PivotSelection.previousCol = XmlHelper.ReadUInt(node.Attributes["previousCol"]);
			cT_PivotSelection.click = XmlHelper.ReadUInt(node.Attributes["click"]);
			cT_PivotSelection.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pivotArea")
				{
					cT_PivotSelection.pivotArea = CT_PivotArea.Parse(childNode, namespaceManager);
				}
			}
			return cT_PivotSelection;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "pane", pane.ToString());
			XmlHelper.WriteAttribute(sw, "showHeader", showHeader);
			XmlHelper.WriteAttribute(sw, "label", label);
			XmlHelper.WriteAttribute(sw, "data", data);
			XmlHelper.WriteAttribute(sw, "extendable", extendable);
			XmlHelper.WriteAttribute(sw, "count", count);
			XmlHelper.WriteAttribute(sw, "axis", axis.ToString());
			XmlHelper.WriteAttribute(sw, "dimension", dimension);
			XmlHelper.WriteAttribute(sw, "start", start);
			XmlHelper.WriteAttribute(sw, "min", min);
			XmlHelper.WriteAttribute(sw, "max", max);
			XmlHelper.WriteAttribute(sw, "activeRow", activeRow);
			XmlHelper.WriteAttribute(sw, "activeCol", activeCol);
			XmlHelper.WriteAttribute(sw, "previousRow", previousRow);
			XmlHelper.WriteAttribute(sw, "previousCol", previousCol);
			XmlHelper.WriteAttribute(sw, "click", click);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			if (pivotArea != null)
			{
				pivotArea.Write(sw, "pivotArea");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
