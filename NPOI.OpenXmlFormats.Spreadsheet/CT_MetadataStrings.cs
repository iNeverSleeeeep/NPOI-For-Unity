using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_MetadataStrings
	{
		private List<CT_XStringElement> sField;

		private uint countField;

		public List<CT_XStringElement> s
		{
			get
			{
				return sField;
			}
			set
			{
				sField = value;
			}
		}

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

		public CT_MetadataStrings()
		{
			sField = new List<CT_XStringElement>();
			countField = 0u;
		}
	}
}
