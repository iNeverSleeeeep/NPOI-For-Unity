using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml")]
	public class CT_DatastoreSchemaRefs
	{
		private List<CT_DatastoreSchemaRef> schemaRefField;

		[XmlElement("schemaRef")]
		public List<CT_DatastoreSchemaRef> schemaRef
		{
			get
			{
				return schemaRefField;
			}
			set
			{
				schemaRefField = value;
			}
		}

		public CT_DatastoreSchemaRefs()
		{
			schemaRefField = new List<CT_DatastoreSchemaRef>();
		}
	}
}
