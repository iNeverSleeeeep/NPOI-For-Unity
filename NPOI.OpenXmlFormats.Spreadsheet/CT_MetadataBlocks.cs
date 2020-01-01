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
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	public class CT_MetadataBlocks
	{
		private List<CT_MetadataRecord> bkField;

		private uint countField;

		[XmlArray(Order = 0)]
		[XmlArrayItem("rc", typeof(CT_MetadataRecord), IsNullable = false)]
		public List<CT_MetadataRecord> bk
		{
			get
			{
				return bkField;
			}
			set
			{
				bkField = value;
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

		public CT_MetadataBlocks()
		{
			bkField = new List<CT_MetadataRecord>();
			countField = 0u;
		}
	}
}
