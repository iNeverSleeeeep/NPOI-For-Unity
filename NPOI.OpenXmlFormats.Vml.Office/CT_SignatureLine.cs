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
	public class CT_SignatureLine
	{
		private ST_Ext extField;

		private ST_TrueFalse issignaturelineField;

		private bool issignaturelineFieldSpecified;

		private string idField;

		private string providField;

		private ST_TrueFalse signinginstructionssetField;

		private bool signinginstructionssetFieldSpecified;

		private ST_TrueFalse allowcommentsField;

		private bool allowcommentsFieldSpecified;

		private ST_TrueFalse showsigndateField;

		private bool showsigndateFieldSpecified;

		private string suggestedsignerField;

		private string suggestedsigner2Field;

		private string suggestedsigneremailField;

		private string signinginstructionsField;

		private string addlxmlField;

		private string sigprovurlField;

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
		public ST_TrueFalse issignatureline
		{
			get
			{
				return issignaturelineField;
			}
			set
			{
				issignaturelineField = value;
			}
		}

		[XmlIgnore]
		public bool issignaturelineSpecified
		{
			get
			{
				return issignaturelineFieldSpecified;
			}
			set
			{
				issignaturelineFieldSpecified = value;
			}
		}

		[XmlAttribute(DataType = "token")]
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

		[XmlAttribute(DataType = "token")]
		public string provid
		{
			get
			{
				return providField;
			}
			set
			{
				providField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse signinginstructionsset
		{
			get
			{
				return signinginstructionssetField;
			}
			set
			{
				signinginstructionssetField = value;
			}
		}

		[XmlIgnore]
		public bool signinginstructionssetSpecified
		{
			get
			{
				return signinginstructionssetFieldSpecified;
			}
			set
			{
				signinginstructionssetFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse allowcomments
		{
			get
			{
				return allowcommentsField;
			}
			set
			{
				allowcommentsField = value;
			}
		}

		[XmlIgnore]
		public bool allowcommentsSpecified
		{
			get
			{
				return allowcommentsFieldSpecified;
			}
			set
			{
				allowcommentsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse showsigndate
		{
			get
			{
				return showsigndateField;
			}
			set
			{
				showsigndateField = value;
			}
		}

		[XmlIgnore]
		public bool showsigndateSpecified
		{
			get
			{
				return showsigndateFieldSpecified;
			}
			set
			{
				showsigndateFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string suggestedsigner
		{
			get
			{
				return suggestedsignerField;
			}
			set
			{
				suggestedsignerField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string suggestedsigner2
		{
			get
			{
				return suggestedsigner2Field;
			}
			set
			{
				suggestedsigner2Field = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string suggestedsigneremail
		{
			get
			{
				return suggestedsigneremailField;
			}
			set
			{
				suggestedsigneremailField = value;
			}
		}

		[XmlAttribute]
		public string signinginstructions
		{
			get
			{
				return signinginstructionsField;
			}
			set
			{
				signinginstructionsField = value;
			}
		}

		[XmlAttribute]
		public string addlxml
		{
			get
			{
				return addlxmlField;
			}
			set
			{
				addlxmlField = value;
			}
		}

		[XmlAttribute]
		public string sigprovurl
		{
			get
			{
				return sigprovurlField;
			}
			set
			{
				sigprovurlField = value;
			}
		}
	}
}
