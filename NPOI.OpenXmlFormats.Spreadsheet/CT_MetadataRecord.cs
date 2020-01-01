using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_MetadataRecord
	{
		private uint tField;

		private uint vField;

		[XmlAttribute]
		public uint t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[XmlAttribute]
		public uint v
		{
			get
			{
				return vField;
			}
			set
			{
				vField = value;
			}
		}
	}
}
