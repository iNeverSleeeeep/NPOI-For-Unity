using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_Callout
	{
		private ST_Ext extField;

		private ST_TrueFalse onField;

		private bool onFieldSpecified;

		private string typeField;

		private string gapField;

		private ST_Angle angleField;

		private bool angleFieldSpecified;

		private ST_TrueFalse dropautoField;

		private bool dropautoFieldSpecified;

		private string dropField;

		private string distanceField;

		private ST_TrueFalse lengthspecifiedField;

		private string lengthField;

		private ST_TrueFalse accentbarField;

		private bool accentbarFieldSpecified;

		private ST_TrueFalse textborderField;

		private bool textborderFieldSpecified;

		private ST_TrueFalse minusxField;

		private bool minusxFieldSpecified;

		private ST_TrueFalse minusyField;

		private bool minusyFieldSpecified;

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
		public string type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlAttribute]
		public string gap
		{
			get
			{
				return gapField;
			}
			set
			{
				gapField = value;
			}
		}

		[XmlAttribute]
		public ST_Angle angle
		{
			get
			{
				return angleField;
			}
			set
			{
				angleField = value;
			}
		}

		[XmlIgnore]
		public bool angleSpecified
		{
			get
			{
				return angleFieldSpecified;
			}
			set
			{
				angleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse dropauto
		{
			get
			{
				return dropautoField;
			}
			set
			{
				dropautoField = value;
			}
		}

		[XmlIgnore]
		public bool dropautoSpecified
		{
			get
			{
				return dropautoFieldSpecified;
			}
			set
			{
				dropautoFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string drop
		{
			get
			{
				return dropField;
			}
			set
			{
				dropField = value;
			}
		}

		[XmlAttribute]
		public string distance
		{
			get
			{
				return distanceField;
			}
			set
			{
				distanceField = value;
			}
		}

		[DefaultValue(ST_TrueFalse.f)]
		[XmlAttribute]
		public ST_TrueFalse lengthspecified
		{
			get
			{
				return lengthspecifiedField;
			}
			set
			{
				lengthspecifiedField = value;
			}
		}

		[XmlAttribute]
		public string length
		{
			get
			{
				return lengthField;
			}
			set
			{
				lengthField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse accentbar
		{
			get
			{
				return accentbarField;
			}
			set
			{
				accentbarField = value;
			}
		}

		[XmlIgnore]
		public bool accentbarSpecified
		{
			get
			{
				return accentbarFieldSpecified;
			}
			set
			{
				accentbarFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse textborder
		{
			get
			{
				return textborderField;
			}
			set
			{
				textborderField = value;
			}
		}

		[XmlIgnore]
		public bool textborderSpecified
		{
			get
			{
				return textborderFieldSpecified;
			}
			set
			{
				textborderFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse minusx
		{
			get
			{
				return minusxField;
			}
			set
			{
				minusxField = value;
			}
		}

		[XmlIgnore]
		public bool minusxSpecified
		{
			get
			{
				return minusxFieldSpecified;
			}
			set
			{
				minusxFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse minusy
		{
			get
			{
				return minusyField;
			}
			set
			{
				minusyField = value;
			}
		}

		[XmlIgnore]
		public bool minusySpecified
		{
			get
			{
				return minusyFieldSpecified;
			}
			set
			{
				minusyFieldSpecified = value;
			}
		}

		public CT_Callout()
		{
			lengthspecifiedField = ST_TrueFalse.f;
		}
	}
}
