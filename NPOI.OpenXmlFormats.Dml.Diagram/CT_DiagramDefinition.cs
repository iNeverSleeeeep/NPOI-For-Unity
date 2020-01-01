using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DesignerCategory("code")]
	[XmlRoot("layoutDef", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	[DebuggerStepThrough]
	public class CT_DiagramDefinition
	{
		private List<CT_Name> titleField;

		private List<CT_Description> descField;

		private List<CT_Category> catLstField;

		private CT_SampleData sampDataField;

		private CT_SampleData styleDataField;

		private CT_SampleData clrDataField;

		private CT_LayoutNode layoutNodeField;

		private CT_OfficeArtExtensionList extLstField;

		private string uniqueIdField;

		private string minVerField;

		private string defStyleField;

		[XmlElement("title", Order = 0)]
		public List<CT_Name> title
		{
			get
			{
				return titleField;
			}
			set
			{
				titleField = value;
			}
		}

		[XmlElement("desc", Order = 1)]
		public List<CT_Description> desc
		{
			get
			{
				return descField;
			}
			set
			{
				descField = value;
			}
		}

		[XmlArray(Order = 2)]
		[XmlArrayItem("cat", IsNullable = false)]
		public List<CT_Category> catLst
		{
			get
			{
				return catLstField;
			}
			set
			{
				catLstField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_SampleData sampData
		{
			get
			{
				return sampDataField;
			}
			set
			{
				sampDataField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_SampleData styleData
		{
			get
			{
				return styleDataField;
			}
			set
			{
				styleDataField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_SampleData clrData
		{
			get
			{
				return clrDataField;
			}
			set
			{
				clrDataField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_LayoutNode layoutNode
		{
			get
			{
				return layoutNodeField;
			}
			set
			{
				layoutNodeField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OfficeArtExtensionList extLst
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

		[DefaultValue("")]
		[XmlAttribute]
		public string uniqueId
		{
			get
			{
				return uniqueIdField;
			}
			set
			{
				uniqueIdField = value;
			}
		}

		[DefaultValue("http://schemas.openxmlformats.org/drawingml/2006/diagram")]
		[XmlAttribute]
		public string minVer
		{
			get
			{
				return minVerField;
			}
			set
			{
				minVerField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string defStyle
		{
			get
			{
				return defStyleField;
			}
			set
			{
				defStyleField = value;
			}
		}

		public CT_DiagramDefinition()
		{
			layoutNodeField = new CT_LayoutNode();
			clrDataField = new CT_SampleData();
			styleDataField = new CT_SampleData();
			sampDataField = new CT_SampleData();
			catLstField = new List<CT_Category>();
			descField = new List<CT_Description>();
			titleField = new List<CT_Name>();
			uniqueIdField = "";
			minVerField = "http://schemas.openxmlformats.org/drawingml/2006/diagram";
			defStyleField = "";
		}
	}
}
