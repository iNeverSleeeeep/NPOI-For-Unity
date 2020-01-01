using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_AudioCD
	{
		private CT_AudioCDTime stField;

		private CT_AudioCDTime endField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_AudioCDTime st
		{
			get
			{
				return stField;
			}
			set
			{
				stField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_AudioCDTime end
		{
			get
			{
				return endField;
			}
			set
			{
				endField = value;
			}
		}

		[XmlElement(Order = 2)]
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

		public CT_AudioCD()
		{
			extLstField = new CT_OfficeArtExtensionList();
			endField = new CT_AudioCDTime();
			stField = new CT_AudioCDTime();
		}
	}
}
