using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_FontSig
	{
		private byte[] usb0Field;

		private byte[] usb1Field;

		private byte[] usb2Field;

		private byte[] usb3Field;

		private byte[] csb0Field;

		private byte[] csb1Field;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] usb0
		{
			get
			{
				return usb0Field;
			}
			set
			{
				usb0Field = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] usb1
		{
			get
			{
				return usb1Field;
			}
			set
			{
				usb1Field = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] usb2
		{
			get
			{
				return usb2Field;
			}
			set
			{
				usb2Field = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] usb3
		{
			get
			{
				return usb3Field;
			}
			set
			{
				usb3Field = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] csb0
		{
			get
			{
				return csb0Field;
			}
			set
			{
				csb0Field = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] csb1
		{
			get
			{
				return csb1Field;
			}
			set
			{
				csb1Field = value;
			}
		}
	}
}
