using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_DigSigBlob
	{
		private byte[] blobField;

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", DataType = "base64Binary")]
		public byte[] blob
		{
			get
			{
				return blobField;
			}
			set
			{
				blobField = value;
			}
		}
	}
}
