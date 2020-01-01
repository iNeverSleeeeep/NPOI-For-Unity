using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot("MapInfo", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_MapInfo
	{
		private List<CT_Schema> schemaField = new List<CT_Schema>();

		private List<CT_Map> mapField = new List<CT_Map>();

		private string selectionNamespacesField = string.Empty;

		[XmlElement("Schema")]
		public List<CT_Schema> Schema
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

		[XmlElement("Map")]
		public List<CT_Map> Map
		{
			get
			{
				return mapField;
			}
			set
			{
				mapField = value;
			}
		}

		[XmlAttribute]
		public string SelectionNamespaces
		{
			get
			{
				return selectionNamespacesField;
			}
			set
			{
				selectionNamespacesField = value;
			}
		}
	}
}
