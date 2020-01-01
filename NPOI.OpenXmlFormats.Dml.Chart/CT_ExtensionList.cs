using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_ExtensionList
	{
		private List<CT_Extension> extField;

		[XmlElement("ext", Order = 0)]
		public List<CT_Extension> ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}
	}
}
