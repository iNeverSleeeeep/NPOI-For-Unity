using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Connections
	{
		private CT_Connection[] connectionField;

		[XmlElement("connection")]
		public CT_Connection[] connection
		{
			get
			{
				return connectionField;
			}
			set
			{
				connectionField = value;
			}
		}
	}
}
