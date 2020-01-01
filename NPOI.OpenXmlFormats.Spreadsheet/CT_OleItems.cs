using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_OleItems
	{
		private CT_OleItem[] oleItemField;

		[XmlElement("oleItem")]
		public CT_OleItem[] oleItem
		{
			get
			{
				return oleItemField;
			}
			set
			{
				oleItemField = value;
			}
		}
	}
}
