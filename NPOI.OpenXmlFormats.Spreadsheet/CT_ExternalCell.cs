using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ExternalCell
	{
		private string vField;

		private string rField;

		private ST_CellType tField;

		private uint vmField;

		public string v
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

		[XmlAttribute]
		public string r
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

		[DefaultValue(ST_CellType.n)]
		[XmlAttribute]
		public ST_CellType t
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

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint vm
		{
			get
			{
				return vmField;
			}
			set
			{
				vmField = value;
			}
		}

		public CT_ExternalCell()
		{
			tField = ST_CellType.n;
			vmField = 0u;
		}
	}
}
