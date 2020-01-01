using System;
using System.Collections.Generic;
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
	public class CT_MetadataBlock
	{
		private List<CT_MetadataRecord> rcField;

		public List<CT_MetadataRecord> rc
		{
			get
			{
				return rcField;
			}
			set
			{
				rcField = value;
			}
		}

		public CT_MetadataBlock()
		{
			rcField = new List<CT_MetadataRecord>();
		}
	}
}
