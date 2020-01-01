using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_ExtensionList
	{
		private CT_Extension[] extField;

		[XmlElement("ext")]
		public CT_Extension[] ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}
	}
}
