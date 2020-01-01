using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	public class CT_TableCol
	{
		private CT_OfficeArtExtensionList extLstField;

		private long wField;

		[XmlElement(Order = 0)]
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
		public long w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		public CT_TableCol()
		{
			extLstField = new CT_OfficeArtExtensionList();
		}
	}
}
