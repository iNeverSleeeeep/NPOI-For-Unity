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
	public class CT_PivotArea
	{
		private CT_PivotAreaReferences referencesField;

		private CT_ExtensionList extLstField;

		private int fieldField;

		private bool fieldFieldSpecified;

		private ST_PivotAreaType typeField;

		private bool dataOnlyField;

		private bool labelOnlyField;

		private bool grandRowField;

		private bool grandColField;

		private bool cacheIndexField;

		private bool outlineField;

		private string offsetField;

		private bool collapsedLevelsAreSubtotalsField;

		private ST_Axis axisField;

		private bool axisFieldSpecified;

		private uint fieldPositionField;

		private bool fieldPositionFieldSpecified;

		public CT_PivotAreaReferences references
		{
			get
			{
				return referencesField;
			}
			set
			{
				referencesField = value;
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

		[XmlAttribute]
		public int field
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

		[XmlAttribute]
		[DefaultValue(ST_PivotAreaType.normal)]
		public ST_PivotAreaType type
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

		[DefaultValue(true)]
		[XmlAttribute]
		public bool dataOnly
		{
			get
			{
				return dataOnlyField;
			}
			set
			{
				dataOnlyField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool labelOnly
		{
			get
			{
				return labelOnlyField;
			}
			set
			{
				labelOnlyField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool grandRow
		{
			get
			{
				return grandRowField;
			}
			set
			{
				grandRowField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool grandCol
		{
			get
			{
				return grandColField;
			}
			set
			{
				grandColField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool cacheIndex
		{
			get
			{
				return cacheIndexField;
			}
			set
			{
				cacheIndexField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool outline
		{
			get
			{
				return outlineField;
			}
			set
			{
				outlineField = value;
			}
		}

		[XmlAttribute]
		public string offset
		{
			get
			{
				return offsetField;
			}
			set
			{
				offsetField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool collapsedLevelsAreSubtotals
		{
			get
			{
				return collapsedLevelsAreSubtotalsField;
			}
			set
			{
				collapsedLevelsAreSubtotalsField = value;
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

		[XmlAttribute]
		public uint fieldPosition
		{
			get
			{
				return fieldPositionField;
			}
			set
			{
				fieldPositionField = value;
			}
		}

		[XmlIgnore]
		public bool fieldPositionSpecified
		{
			get
			{
				return fieldPositionFieldSpecified;
			}
			set
			{
				fieldPositionFieldSpecified = value;
			}
		}

		public CT_PivotArea()
		{
			typeField = ST_PivotAreaType.normal;
			dataOnlyField = true;
			labelOnlyField = false;
			grandRowField = false;
			grandColField = false;
			cacheIndexField = false;
			outlineField = true;
			collapsedLevelsAreSubtotalsField = false;
		}

		public static CT_PivotArea Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PivotArea cT_PivotArea = new CT_PivotArea();
			cT_PivotArea.field = XmlHelper.ReadInt(node.Attributes["field"]);
			if (node.Attributes["type"] != null)
			{
				cT_PivotArea.type = (ST_PivotAreaType)Enum.Parse(typeof(ST_PivotAreaType), node.Attributes["type"].Value);
			}
			cT_PivotArea.dataOnly = XmlHelper.ReadBool(node.Attributes["dataOnly"]);
			cT_PivotArea.labelOnly = XmlHelper.ReadBool(node.Attributes["labelOnly"]);
			cT_PivotArea.grandRow = XmlHelper.ReadBool(node.Attributes["grandRow"]);
			cT_PivotArea.grandCol = XmlHelper.ReadBool(node.Attributes["grandCol"]);
			cT_PivotArea.cacheIndex = XmlHelper.ReadBool(node.Attributes["cacheIndex"]);
			cT_PivotArea.outline = XmlHelper.ReadBool(node.Attributes["outline"]);
			cT_PivotArea.offset = XmlHelper.ReadString(node.Attributes["offset"]);
			cT_PivotArea.collapsedLevelsAreSubtotals = XmlHelper.ReadBool(node.Attributes["collapsedLevelsAreSubtotals"]);
			if (node.Attributes["axis"] != null)
			{
				cT_PivotArea.axis = (ST_Axis)Enum.Parse(typeof(ST_Axis), node.Attributes["axis"].Value);
			}
			cT_PivotArea.fieldPosition = XmlHelper.ReadUInt(node.Attributes["fieldPosition"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "references")
				{
					cT_PivotArea.references = CT_PivotAreaReferences.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_PivotArea.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_PivotArea;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "field", field);
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			XmlHelper.WriteAttribute(sw, "dataOnly", dataOnly);
			XmlHelper.WriteAttribute(sw, "labelOnly", labelOnly);
			XmlHelper.WriteAttribute(sw, "grandRow", grandRow);
			XmlHelper.WriteAttribute(sw, "grandCol", grandCol);
			XmlHelper.WriteAttribute(sw, "cacheIndex", cacheIndex);
			XmlHelper.WriteAttribute(sw, "outline", outline);
			XmlHelper.WriteAttribute(sw, "offset", offset);
			XmlHelper.WriteAttribute(sw, "collapsedLevelsAreSubtotals", collapsedLevelsAreSubtotals);
			XmlHelper.WriteAttribute(sw, "axis", axis.ToString());
			XmlHelper.WriteAttribute(sw, "fieldPosition", fieldPosition);
			sw.Write(">");
			if (references != null)
			{
				references.Write(sw, "references");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
