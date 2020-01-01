using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	public class CT_Skew
	{
		private ST_Ext extField;

		private string idField;

		private ST_TrueFalse onField;

		private bool onFieldSpecified;

		private string offsetField;

		private string originField;

		private string matrixField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:vml")]
		public ST_Ext ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}

		[XmlIgnore]
		public bool extSpecified
		{
			get
			{
				return ST_Ext.NONE != extField;
			}
		}

		[XmlAttribute]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse on
		{
			get
			{
				return onField;
			}
			set
			{
				onField = value;
			}
		}

		[XmlIgnore]
		public bool onSpecified
		{
			get
			{
				return onFieldSpecified;
			}
			set
			{
				onFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string offset
		{
			get
			{
				return offsetField;
			}
			set
			{
				offsetField = value;
			}
		}

		[XmlAttribute]
		public string origin
		{
			get
			{
				return originField;
			}
			set
			{
				originField = value;
			}
		}

		[XmlAttribute]
		public string matrix
		{
			get
			{
				return matrixField;
			}
			set
			{
				matrixField = value;
			}
		}
	}
}
