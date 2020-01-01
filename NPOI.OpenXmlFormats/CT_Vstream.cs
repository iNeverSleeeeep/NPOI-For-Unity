using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_Vstream
	{
		private string versionField;

		private byte[] valueField;

		[XmlAttribute]
		public string version
		{
			get
			{
				return versionField;
			}
			set
			{
				versionField = value;
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
