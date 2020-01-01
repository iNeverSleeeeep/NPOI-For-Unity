using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/schemaLibrary/2006/main")]
	public class CT_SchemaLibrary
	{
		private List<CT_Schema> schemaField;

		[XmlElement("schema")]
		public List<CT_Schema> schema
		{
			get
			{
				return schemaField;
			}
			set
			{
				schemaField = value;
			}
		}

		public CT_SchemaLibrary()
		{
			schemaField = new List<CT_Schema>();
		}
	}
}
