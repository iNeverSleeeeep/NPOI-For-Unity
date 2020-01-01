using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_ExternalLink
	{
		private object itemField;

		[XmlElement("ddeLink", typeof(CT_DdeLink))]
		[XmlElement("oleLink", typeof(CT_OleLink))]
		[XmlElement("extLst", typeof(CT_ExtensionList))]
		[XmlElement("externalBook", typeof(CT_ExternalBook))]
		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}
	}
}
