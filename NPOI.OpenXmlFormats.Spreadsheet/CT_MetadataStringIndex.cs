using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	public class CT_MetadataStringIndex
	{
		private uint xField;

		private bool sField;

		public uint x
		{
			get
			{
				return xField;
			}
			set
			{
				xField = value;
			}
		}

		[DefaultValue(false)]
		public bool s
		{
			get
			{
				return sField;
			}
			set
			{
				sField = value;
			}
		}

		public CT_MetadataStringIndex()
		{
			sField = false;
		}
	}
}
