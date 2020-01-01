using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Schema
	{
		private XmlElement anyField;

		private string idField = string.Empty;

		private string schemaRefField;

		private string namespaceField;

		public string InnerXml;

		[XmlAnyElement]
		public XmlElement Any
		{
			get
			{
				return anyField;
			}
			set
			{
				anyField = value;
			}
		}

		[XmlAttribute]
		public string ID
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		public string SchemaRef
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

		[XmlIgnore]
		public bool SchemaRefSpecified
		{
			get
			{
				return null != schemaRefField;
			}
		}

		[XmlAttribute]
		public string Namespace
		{
			get
			{
				return namespaceField;
			}
			set
			{
				namespaceField = value;
			}
		}

		[XmlIgnore]
		public bool NamespaceSpecified
		{
			get
			{
				return null != namespaceField;
			}
		}
	}
}
