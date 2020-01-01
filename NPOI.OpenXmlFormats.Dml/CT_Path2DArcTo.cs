using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_Path2DArcTo
	{
		private string wrField;

		private string hrField;

		private string stAngField;

		private string swAngField;

		[XmlAttribute]
		public string wR
		{
			get
			{
				return wrField;
			}
			set
			{
				wrField = value;
			}
		}

		[XmlAttribute]
		public string hR
		{
			get
			{
				return hrField;
			}
			set
			{
				hrField = value;
			}
		}

		[XmlAttribute]
		public string stAng
		{
			get
			{
				return stAngField;
			}
			set
			{
				stAngField = value;
			}
		}

		[XmlAttribute]
		public string swAng
		{
			get
			{
				return swAngField;
			}
			set
			{
				swAngField = value;
			}
		}
	}
}
