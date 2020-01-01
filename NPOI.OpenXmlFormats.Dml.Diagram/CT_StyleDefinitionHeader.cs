using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot("styleDefHdr", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	[DesignerCategory("code")]
	public class CT_StyleDefinitionHeader
	{
		private List<CT_SDName> titleField;

		private List<CT_SDDescription> descField;

		private List<CT_SDCategory> catLstField;

		private CT_OfficeArtExtensionList extLstField;

		private string uniqueIdField;

		private string minVerField;

		private int resIdField;

		[XmlElement("title", Order = 0)]
		public List<CT_SDName> title
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
		public List<CT_SDDescription> desc
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
		public List<CT_SDCategory> catLst
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

		public CT_StyleDefinitionHeader()
		{
			catLstField = new List<CT_SDCategory>();
			descField = new List<CT_SDDescription>();
			titleField = new List<CT_SDName>();
			minVerField = "http://schemas.openxmlformats.org/drawingml/2006/diagram";
			resIdField = 0;
		}
	}
}
