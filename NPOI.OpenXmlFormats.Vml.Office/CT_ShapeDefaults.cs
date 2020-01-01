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
	public class CT_ShapeDefaults
	{
		private CT_Fill fillField;

		private CT_Stroke strokeField;

		private CT_Textbox textboxField;

		private CT_Shadow shadowField;

		private CT_Skew skewField;

		private CT_Extrusion extrusionField;

		private CT_Callout calloutField;

		private CT_Lock lockField;

		private CT_ColorMru colormruField;

		private CT_ColorMenu colormenuField;

		private ST_Ext extField;

		private string spidmaxField;

		private string styleField;

		private ST_TrueFalse fill1Field;

		private bool fill1FieldSpecified;

		private string fillcolorField;

		private ST_TrueFalse stroke1Field;

		private bool stroke1FieldSpecified;

		private string strokecolorField;

		private ST_TrueFalse allowincellField;

		private bool allowincellFieldSpecified;

		[XmlElement(Namespace = "urn:schemas-microsoft-com:vml")]
		public CT_Fill fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

		[XmlElement(Namespace = "urn:schemas-microsoft-com:vml")]
		public CT_Stroke stroke
		{
			get
			{
				return strokeField;
			}
			set
			{
				strokeField = value;
			}
		}

		[XmlElement(Namespace = "urn:schemas-microsoft-com:vml")]
		public CT_Textbox textbox
		{
			get
			{
				return textboxField;
			}
			set
			{
				textboxField = value;
			}
		}

		[XmlElement(Namespace = "urn:schemas-microsoft-com:vml")]
		public CT_Shadow shadow
		{
			get
			{
				return shadowField;
			}
			set
			{
				shadowField = value;
			}
		}

		public CT_Skew skew
		{
			get
			{
				return skewField;
			}
			set
			{
				skewField = value;
			}
		}

		public CT_Extrusion extrusion
		{
			get
			{
				return extrusionField;
			}
			set
			{
				extrusionField = value;
			}
		}

		public CT_Callout callout
		{
			get
			{
				return calloutField;
			}
			set
			{
				calloutField = value;
			}
		}

		public CT_Lock @lock
		{
			get
			{
				return lockField;
			}
			set
			{
				lockField = value;
			}
		}

		public CT_ColorMru colormru
		{
			get
			{
				return colormruField;
			}
			set
			{
				colormruField = value;
			}
		}

		public CT_ColorMenu colormenu
		{
			get
			{
				return colormenuField;
			}
			set
			{
				colormenuField = value;
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
		public string spidmax
		{
			get
			{
				return spidmaxField;
			}
			set
			{
				spidmaxField = value;
			}
		}

		[XmlAttribute]
		public string style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlAttribute("fill")]
		public ST_TrueFalse fill1
		{
			get
			{
				return fill1Field;
			}
			set
			{
				fill1Field = value;
			}
		}

		[XmlIgnore]
		public bool fill1Specified
		{
			get
			{
				return fill1FieldSpecified;
			}
			set
			{
				fill1FieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string fillcolor
		{
			get
			{
				return fillcolorField;
			}
			set
			{
				fillcolorField = value;
			}
		}

		[XmlAttribute("stroke")]
		public ST_TrueFalse stroke1
		{
			get
			{
				return stroke1Field;
			}
			set
			{
				stroke1Field = value;
			}
		}

		[XmlIgnore]
		public bool stroke1Specified
		{
			get
			{
				return stroke1FieldSpecified;
			}
			set
			{
				stroke1FieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string strokecolor
		{
			get
			{
				return strokecolorField;
			}
			set
			{
				strokecolorField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TrueFalse allowincell
		{
			get
			{
				return allowincellField;
			}
			set
			{
				allowincellField = value;
			}
		}

		[XmlIgnore]
		public bool allowincellSpecified
		{
			get
			{
				return allowincellFieldSpecified;
			}
			set
			{
				allowincellFieldSpecified = value;
			}
		}
	}
}
