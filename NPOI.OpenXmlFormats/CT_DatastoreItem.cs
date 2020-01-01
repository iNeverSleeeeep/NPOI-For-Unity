using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_DatastoreItem
	{
		private List<CT_DatastoreSchemaRef> schemaRefsField;

		private string itemIDField;

		[XmlArray(Order = 0)]
		[XmlArrayItem("schemaRef", IsNullable = false)]
		public List<CT_DatastoreSchemaRef> schemaRefs
		{
			get
			{
				return schemaRefsField;
			}
			set
			{
				schemaRefsField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "token")]
		public string itemID
		{
			get
			{
				return itemIDField;
			}
			set
			{
				itemIDField = value;
			}
		}

		public CT_DatastoreItem()
		{
			schemaRefsField = new List<CT_DatastoreSchemaRef>();
		}
	}
}
