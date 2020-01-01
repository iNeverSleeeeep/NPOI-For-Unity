using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_TextFields
	{
		private CT_TextField[] textFieldField;

		private uint countField;

		[XmlElement("textField")]
		public CT_TextField[] textField
		{
			get
			{
				return textFieldField;
			}
			set
			{
				textFieldField = value;
			}
		}

		[DefaultValue(typeof(uint), "1")]
		[XmlAttribute]
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

		public CT_TextFields()
		{
			countField = 1u;
		}
	}
}
