using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	public class CT_Relation
	{
		private ST_Ext extField;

		private string idsrcField;

		private string iddestField;

		private string idcntrField;

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
		public string idsrc
		{
			get
			{
				return idsrcField;
			}
			set
			{
				idsrcField = value;
			}
		}

		[XmlAttribute]
		public string iddest
		{
			get
			{
				return iddestField;
			}
			set
			{
				iddestField = value;
			}
		}

		[XmlAttribute]
		public string idcntr
		{
			get
			{
				return idcntrField;
			}
			set
			{
				idcntrField = value;
			}
		}
	}
}
