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
	[XmlRoot("layoutDefHdr", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	[DebuggerStepThrough]
	public class CT_DiagramDefinitionHeader
	{
		private List<CT_Name> titleField;

		private List<CT_Description> descField;

		private List<CT_Category> catLstField;

		private CT_OfficeArtExtensionList extLstField;

		private string uniqueIdField;

		private string minVerField;

		private string defStyleField;

		private int resIdField;

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

		[XmlArrayItem("cat", IsNullable = false)]
		[XmlArray(Order = 2)]
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

		[XmlAttribute]
		[DefaultValue("http://schemas.openxmlformats.org/drawingml/2006/diagram")]
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

		[XmlAttribute]
		[DefaultValue(0)]
		public int resId
		{
			get
			{
				return resIdField;
			}
			set
			{
				resIdField = value;
			}
		}

		public CT_DiagramDefinitionHeader()
		{
			catLstField = new List<CT_Category>();
			descField = new List<CT_Description>();
			titleField = new List<CT_Name>();
			minVerField = "http://schemas.openxmlformats.org/drawingml/2006/diagram";
			defStyleField = "";
			resIdField = 0;
		}
	}
}
