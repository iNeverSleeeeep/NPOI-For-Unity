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
	public class CT_DdeValue
	{
		private string valField;

		private ST_DdeValueType tField;

		public string val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_DdeValueType.n)]
		public ST_DdeValueType t
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

		public CT_DdeValue()
		{
			tField = ST_DdeValueType.n;
		}
	}
}
