using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_MetadataTypes
	{
		private List<CT_MetadataType> metadataTypeField;

		private uint countField;

		[XmlElement("metadataType")]
		public List<CT_MetadataType> metadataType
		{
			get
			{
				return metadataTypeField;
			}
			set
			{
				metadataTypeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		public CT_MetadataTypes()
		{
			metadataTypeField = new List<CT_MetadataType>();
			countField = 0u;
		}
	}
}
