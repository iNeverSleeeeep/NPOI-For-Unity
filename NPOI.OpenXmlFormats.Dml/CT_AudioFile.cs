using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_AudioFile
	{
		private CT_OfficeArtExtensionList extLstField;

		private string linkField;

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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string link
		{
			get
			{
				return linkField;
			}
			set
			{
				linkField = value;
			}
		}

		public CT_AudioFile()
		{
			extLstField = new CT_OfficeArtExtensionList();
		}
	}
}
