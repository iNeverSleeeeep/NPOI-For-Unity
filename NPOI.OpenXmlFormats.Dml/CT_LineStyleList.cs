using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_LineStyleList
	{
		private List<CT_LineProperties> lnField;

		[XmlElement("ln", Order = 0)]
		public List<CT_LineProperties> ln
		{
			get
			{
				return lnField;
			}
			set
			{
				lnField = value;
			}
		}

		public CT_LineStyleList()
		{
			lnField = new List<CT_LineProperties>();
		}
	}
}
