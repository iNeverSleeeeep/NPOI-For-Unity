using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot("relIds", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	[DesignerCategory("code")]
	public class CT_RelIds
	{
		private string dmField;

		private string loField;

		private string qsField;

		private string csField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		[DefaultValue("")]
		public string dm
		{
			get
			{
				return dmField;
			}
			set
			{
				dmField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		[DefaultValue("")]
		public string lo
		{
			get
			{
				return loField;
			}
			set
			{
				loField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		[DefaultValue("")]
		public string qs
		{
			get
			{
				return qsField;
			}
			set
			{
				qsField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string cs
		{
			get
			{
				return csField;
			}
			set
			{
				csField = value;
			}
		}

		public CT_RelIds()
		{
			dmField = "";
			loField = "";
			qsField = "";
			csField = "";
		}
	}
}
