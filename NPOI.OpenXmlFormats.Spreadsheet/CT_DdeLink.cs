using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_DdeLink
	{
		private List<CT_DdeItem> ddeItemsField;

		private string ddeServiceField;

		private string ddeTopicField;

		[XmlArray("ddeItems")]
		[XmlArrayItem("ddeItem")]
		public List<CT_DdeItem> ddeItems
		{
			get
			{
				return ddeItemsField;
			}
			set
			{
				ddeItemsField = value;
			}
		}

		[XmlAttribute]
		public string ddeService
		{
			get
			{
				return ddeServiceField;
			}
			set
			{
				ddeServiceField = value;
			}
		}

		[XmlAttribute]
		public string ddeTopic
		{
			get
			{
				return ddeTopicField;
			}
			set
			{
				ddeTopicField = value;
			}
		}
	}
}
