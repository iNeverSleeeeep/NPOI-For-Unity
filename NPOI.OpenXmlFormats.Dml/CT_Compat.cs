using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/compatibility", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/compatibility")]
	public class CT_Compat
	{
		private string spidField;

		[XmlAttribute(DataType = "token")]
		public string spid
		{
			get
			{
				return spidField;
			}
			set
			{
				spidField = value;
			}
		}
	}
}
