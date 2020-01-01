using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot("singleXmlCells", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_SingleXmlCells
	{
		private List<CT_SingleXmlCell> singleXmlCellField;

		[XmlElement]
		public List<CT_SingleXmlCell> singleXmlCell
		{
			get
			{
				return singleXmlCellField;
			}
			set
			{
				singleXmlCellField = value;
			}
		}

		public CT_SingleXmlCells()
		{
			singleXmlCellField = new List<CT_SingleXmlCell>();
		}
	}
}
