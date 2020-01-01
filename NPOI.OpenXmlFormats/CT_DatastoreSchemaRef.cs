using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_DatastoreSchemaRef
	{
		private string uriField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string uri
		{
			get
			{
				return uriField;
			}
			set
			{
				uriField = value;
			}
		}
	}
}
