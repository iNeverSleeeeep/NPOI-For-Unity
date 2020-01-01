using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot("styleDef", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	[DebuggerStepThrough]
	public class CT_StyleDefinition
	{
		private List<CT_SDName> titleField;

		private List<CT_SDDescription> descField;

		private List<CT_SDCategory> catLstField;

		private CT_Scene3D scene3dField;

		private List<CT_StyleLabel> styleLblField;

		private CT_OfficeArtExtensionList extLstField;

		private string uniqueIdField;

		private string minVerField;

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

		[XmlArray(Order = 2)]
		[XmlArrayItem("cat", IsNullable = false)]
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
		public CT_Scene3D scene3d
		{
			get
			{
				return scene3dField;
			}
			set
			{
				scene3dField = value;
			}
		}

		[XmlElement("styleLbl", Order = 4)]
		public List<CT_StyleLabel> styleLbl
		{
			get
			{
				return styleLblField;
			}
			set
			{
				styleLblField = value;
			}
		}

		[XmlElement(Order = 5)]
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
		[DefaultValue("")]
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

		public CT_StyleDefinition()
		{
			extLstField = new CT_OfficeArtExtensionList();
			styleLblField = new List<CT_StyleLabel>();
			scene3dField = new CT_Scene3D();
			catLstField = new List<CT_SDCategory>();
			descField = new List<CT_SDDescription>();
			titleField = new List<CT_SDName>();
			uniqueIdField = "";
			minVerField = "http://schemas.openxmlformats.org/drawingml/2006/diagram";
		}
	}
}
