using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_TextField
	{
		private ST_ExternalConnectionType typeField;

		private uint positionField;

		[DefaultValue(ST_ExternalConnectionType.general)]
		[XmlAttribute]
		public ST_ExternalConnectionType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint position
		{
			get
			{
				return positionField;
			}
			set
			{
				positionField = value;
			}
		}

		public CT_TextField()
		{
			typeField = ST_ExternalConnectionType.general;
			positionField = 0u;
		}
	}
}
