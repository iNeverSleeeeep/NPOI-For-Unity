using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DesignerCategory("code")]
	public class CT_ExternalRow
	{
		private CT_ExternalCell[] cellField;

		private uint rField;

		[XmlElement("cell")]
		public CT_ExternalCell[] cell
		{
			get
			{
				return cellField;
			}
			set
			{
				cellField = value;
			}
		}

		[XmlAttribute]
		public uint r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}
	}
}
