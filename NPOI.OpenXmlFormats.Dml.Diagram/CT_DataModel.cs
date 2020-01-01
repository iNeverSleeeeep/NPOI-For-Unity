using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot("dataModel", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	public class CT_DataModel
	{
		private List<CT_Pt> ptLstField;

		private List<CT_Cxn> cxnLstField;

		private CT_BackgroundFormatting bgField;

		private CT_WholeE2oFormatting wholeField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlArrayItem("pt", IsNullable = false)]
		[XmlArray(Order = 0)]
		public List<CT_Pt> ptLst
		{
			get
			{
				return ptLstField;
			}
			set
			{
				ptLstField = value;
			}
		}

		[XmlArrayItem("cxn", IsNullable = false)]
		[XmlArray(Order = 1)]
		public List<CT_Cxn> cxnLst
		{
			get
			{
				return cxnLstField;
			}
			set
			{
				cxnLstField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_BackgroundFormatting bg
		{
			get
			{
				return bgField;
			}
			set
			{
				bgField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_WholeE2oFormatting whole
		{
			get
			{
				return wholeField;
			}
			set
			{
				wholeField = value;
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

		public CT_DataModel()
		{
			extLstField = new CT_OfficeArtExtensionList();
			wholeField = new CT_WholeE2oFormatting();
			bgField = new CT_BackgroundFormatting();
			cxnLstField = new List<CT_Cxn>();
			ptLstField = new List<CT_Pt>();
		}
	}
}
