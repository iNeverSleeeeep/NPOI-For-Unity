using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot("tblStyleLst", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	public class CT_TableStyleList
	{
		private List<CT_TableStyle> tblStyleField;

		private string defField;

		[XmlElement("tblStyle", Order = 0)]
		public List<CT_TableStyle> tblStyle
		{
			get
			{
				return tblStyleField;
			}
			set
			{
				tblStyleField = value;
			}
		}

		[XmlAttribute(DataType = "token")]
		public string def
		{
			get
			{
				return defField;
			}
			set
			{
				defField = value;
			}
		}

		public CT_TableStyleList()
		{
			tblStyleField = new List<CT_TableStyle>();
		}
	}
}
