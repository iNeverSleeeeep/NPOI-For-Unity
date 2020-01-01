using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties")]
	public class CT_Property
	{
		private object itemField;

		private ItemChoiceType itemElementNameField;

		private string fmtidField;

		private int pidField;

		private string nameField;

		private string linkTargetField;

		[XmlElement("error", typeof(string), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("ostream", typeof(byte[]), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", DataType = "base64Binary")]
		[XmlElement("array", typeof(CT_Array), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("blob", typeof(byte[]), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", DataType = "base64Binary")]
		[XmlElement("bool", typeof(bool), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("bstr", typeof(string), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("cf", typeof(CT_Cf), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("clsid", typeof(string), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("cy", typeof(string), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("date", typeof(DateTime), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("decimal", typeof(decimal), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("empty", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("filetime", typeof(DateTime), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("i1", typeof(sbyte), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("i2", typeof(short), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("i4", typeof(int), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("i8", typeof(long), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("int", typeof(int), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("lpstr", typeof(string), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("lpwstr", typeof(string), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("null", typeof(CT_Null), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("oblob", typeof(byte[]), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", DataType = "base64Binary")]
		[XmlElement("ostorage", typeof(byte[]), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", DataType = "base64Binary")]
		[XmlElement("vstream", typeof(CT_Vstream), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("r4", typeof(float), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("r8", typeof(double), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("storage", typeof(byte[]), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", DataType = "base64Binary")]
		[XmlElement("stream", typeof(byte[]), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", DataType = "base64Binary")]
		[XmlElement("ui1", typeof(byte), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("ui2", typeof(ushort), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("ui4", typeof(uint), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("ui8", typeof(ulong), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("uint", typeof(uint), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlElement("vector", typeof(CT_Vector), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		[XmlChoiceIdentifier("ItemElementName")]
		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}

		[XmlIgnore]
		public ItemChoiceType ItemElementName
		{
			get
			{
				return itemElementNameField;
			}
			set
			{
				itemElementNameField = value;
			}
		}

		[XmlAttribute]
		public string fmtid
		{
			get
			{
				return fmtidField;
			}
			set
			{
				fmtidField = value;
			}
		}

		[XmlAttribute]
		public int pid
		{
			get
			{
				return pidField;
			}
			set
			{
				pidField = value;
			}
		}

		[XmlAttribute]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlAttribute]
		public string linkTarget
		{
			get
			{
				return linkTargetField;
			}
			set
			{
				linkTargetField = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CT_Property))
			{
				return false;
			}
			CT_Property cT_Property = (CT_Property)obj;
			if (cT_Property.fmtidField != fmtidField || cT_Property.itemElementNameField != itemElementNameField || cT_Property.itemField != itemField || cT_Property.linkTargetField != linkTargetField || cT_Property.nameField != nameField || cT_Property.pidField != pidField)
			{
				return false;
			}
			return true;
		}

		public bool IsSetLpwstr()
		{
			throw new NotImplementedException();
		}
	}
}
