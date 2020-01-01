using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_BuiltInUnit
	{
		private ST_BuiltInUnit valField;

		[DefaultValue(ST_BuiltInUnit.thousands)]
		[XmlAttribute]
		public ST_BuiltInUnit val
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

		public CT_BuiltInUnit()
		{
			valField = ST_BuiltInUnit.thousands;
		}
	}
}
