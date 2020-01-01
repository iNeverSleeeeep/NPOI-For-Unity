using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DebuggerStepThrough]
	public class CT_TextProps
	{
		private CT_Shape3D sp3dField;

		private CT_FlatText flatTxField;

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 0)]
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

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 1)]
		public CT_FlatText flatTx
		{
			get
			{
				return flatTxField;
			}
			set
			{
				flatTxField = value;
			}
		}

		public CT_TextProps()
		{
			sp3dField = new CT_Shape3D();
		}
	}
}
