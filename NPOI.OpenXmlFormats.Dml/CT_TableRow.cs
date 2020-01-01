using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TableRow
	{
		private List<CT_TableCell> tcField;

		private CT_OfficeArtExtensionList extLstField;

		private long hField;

		[XmlElement("tc", Order = 0)]
		public List<CT_TableCell> tc
		{
			get
			{
				return tcField;
			}
			set
			{
				tcField = value;
			}
		}

		[XmlElement(Order = 1)]
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
		public long h
		{
			get
			{
				return hField;
			}
			set
			{
				hField = value;
			}
		}

		public CT_TableRow()
		{
			extLstField = new CT_OfficeArtExtensionList();
			tcField = new List<CT_TableCell>();
		}
	}
}
