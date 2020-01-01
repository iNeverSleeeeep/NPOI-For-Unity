using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_DdeItems
	{
		private CT_DdeItem[] ddeItemField;

		[XmlElement("ddeItem")]
		public CT_DdeItem[] ddeItem
		{
			get
			{
				return ddeItemField;
			}
			set
			{
				ddeItemField = value;
			}
		}
	}
}
