using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_ExternalDefinedNames
	{
		private CT_ExternalDefinedName[] definedNameField;

		[XmlElement("definedName")]
		public CT_ExternalDefinedName[] definedName
		{
			get
			{
				return definedNameField;
			}
			set
			{
				definedNameField = value;
			}
		}
	}
}
