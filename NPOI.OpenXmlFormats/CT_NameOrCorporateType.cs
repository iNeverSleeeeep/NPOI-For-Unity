using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class CT_NameOrCorporateType
	{
		private object itemField;

		[XmlElement("NameList", typeof(CT_NameListType))]
		[XmlElement("Corporate", typeof(string))]
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
