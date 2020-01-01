using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot("tbl", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = false)]
	public class CT_Table
	{
		private CT_TableProperties tblPrField;

		private List<CT_TableCol> tblGridField;

		private List<CT_TableRow> trField;

		[XmlElement(Order = 0)]
		public CT_TableProperties tblPr
		{
			get
			{
				return tblPrField;
			}
			set
			{
				tblPrField = value;
			}
		}

		[XmlArrayItem("gridCol", IsNullable = false)]
		[XmlArray(Order = 1)]
		public List<CT_TableCol> tblGrid
		{
			get
			{
				return tblGridField;
			}
			set
			{
				tblGridField = value;
			}
		}

		[XmlElement("tr", Order = 2)]
		public List<CT_TableRow> tr
		{
			get
			{
				return trField;
			}
			set
			{
				trField = value;
			}
		}

		public CT_Table()
		{
			trField = new List<CT_TableRow>();
			tblGridField = new List<CT_TableCol>();
			tblPrField = new CT_TableProperties();
		}
	}
}
