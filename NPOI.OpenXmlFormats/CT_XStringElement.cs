using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DesignerCategory("code")]
	public class CT_XStringElement
	{
		private string vField;

		[XmlAttribute]
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
	}
}
