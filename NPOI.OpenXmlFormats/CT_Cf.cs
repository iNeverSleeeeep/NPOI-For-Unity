using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_Cf
	{
		private string formatField;

		private byte[] valueField;

		[XmlAttribute]
		public string format
		{
			get
			{
				return formatField;
			}
			set
			{
				formatField = value;
			}
		}

		[XmlText(DataType = "base64Binary")]
		public byte[] Value
		{
			get
			{
				return valueField;
			}
			set
			{
				valueField = value;
			}
		}
	}
}
