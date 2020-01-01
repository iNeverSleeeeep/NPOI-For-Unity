using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_StyleLabel
	{
		private CT_Scene3D scene3dField;

		private CT_Shape3D sp3dField;

		private CT_TextProps txPrField;

		private CT_ShapeStyle styleField;

		private CT_OfficeArtExtensionList extLstField;

		private string nameField;

		[XmlElement(Order = 0)]
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

		[XmlElement(Order = 1)]
		public CT_Shape3D sp3d
		{
			get
			{
				return sp3dField;
			}
			set
			{
				sp3dField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TextProps txPr
		{
			get
			{
				return txPrField;
			}
			set
			{
				txPrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_ShapeStyle style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlElement(Order = 4)]
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
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public CT_StyleLabel()
		{
			txPrField = new CT_TextProps();
			scene3dField = new CT_Scene3D();
		}
	}
}
