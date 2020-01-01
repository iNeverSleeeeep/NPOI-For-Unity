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
	public class CT_Diagram
	{
		private CT_RelationTable relationtableField;

		private ST_Ext extField;

		private string dgmstyleField;

		private ST_TrueFalse autoformatField;

		private bool autoformatFieldSpecified;

		private ST_TrueFalse reverseField;

		private bool reverseFieldSpecified;

		private ST_TrueFalse autolayoutField;

		private bool autolayoutFieldSpecified;

		private string dgmscalexField;

		private string dgmscaleyField;

		private string dgmfontsizeField;

		private string constrainboundsField;

		private string dgmbasetextscaleField;

		public CT_RelationTable relationtable
		{
			get
			{
				return relationtableField;
			}
			set
			{
				relationtableField = value;
			}
		}

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

		[XmlAttribute(DataType = "integer")]
		public string dgmstyle
		{
			get
			{
				return dgmstyleField;
			}
			set
			{
				dgmstyleField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse autoformat
		{
			get
			{
				return autoformatField;
			}
			set
			{
				autoformatField = value;
			}
		}

		[XmlIgnore]
		public bool autoformatSpecified
		{
			get
			{
				return autoformatFieldSpecified;
			}
			set
			{
				autoformatFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse reverse
		{
			get
			{
				return reverseField;
			}
			set
			{
				reverseField = value;
			}
		}

		[XmlIgnore]
		public bool reverseSpecified
		{
			get
			{
				return reverseFieldSpecified;
			}
			set
			{
				reverseFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse autolayout
		{
			get
			{
				return autolayoutField;
			}
			set
			{
				autolayoutField = value;
			}
		}

		[XmlIgnore]
		public bool autolayoutSpecified
		{
			get
			{
				return autolayoutFieldSpecified;
			}
			set
			{
				autolayoutFieldSpecified = value;
			}
		}

		[XmlAttribute(DataType = "integer")]
		public string dgmscalex
		{
			get
			{
				return dgmscalexField;
			}
			set
			{
				dgmscalexField = value;
			}
		}

		[XmlAttribute(DataType = "integer")]
		public string dgmscaley
		{
			get
			{
				return dgmscaleyField;
			}
			set
			{
				dgmscaleyField = value;
			}
		}

		[XmlAttribute(DataType = "integer")]
		public string dgmfontsize
		{
			get
			{
				return dgmfontsizeField;
			}
			set
			{
				dgmfontsizeField = value;
			}
		}

		[XmlAttribute]
		public string constrainbounds
		{
			get
			{
				return constrainboundsField;
			}
			set
			{
				constrainboundsField = value;
			}
		}

		[XmlAttribute(DataType = "integer")]
		public string dgmbasetextscale
		{
			get
			{
				return dgmbasetextscaleField;
			}
			set
			{
				dgmbasetextscaleField = value;
			}
		}
	}
}
