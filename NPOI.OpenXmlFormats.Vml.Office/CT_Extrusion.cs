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
	public class CT_Extrusion
	{
		private ST_Ext extField;

		private ST_TrueFalse onField;

		private bool onFieldSpecified;

		private ST_ExtrusionType typeField;

		private ST_ExtrusionRender renderField;

		private string viewpointoriginField;

		private string viewpointField;

		private ST_ExtrusionPlane planeField;

		private float skewangleField;

		private bool skewangleFieldSpecified;

		private string skewamtField;

		private string foredepthField;

		private string backdepthField;

		private string orientationField;

		private float orientationangleField;

		private bool orientationangleFieldSpecified;

		private ST_TrueFalse lockrotationcenterField;

		private bool lockrotationcenterFieldSpecified;

		private ST_TrueFalse autorotationcenterField;

		private bool autorotationcenterFieldSpecified;

		private string rotationcenterField;

		private string rotationangleField;

		private ST_ColorMode colormodeField;

		private bool colormodeFieldSpecified;

		private string colorField;

		private float shininessField;

		private bool shininessFieldSpecified;

		private string specularityField;

		private string diffusityField;

		private ST_TrueFalse metalField;

		private bool metalFieldSpecified;

		private string edgeField;

		private string facetField;

		private ST_TrueFalse lightfaceField;

		private bool lightfaceFieldSpecified;

		private string brightnessField;

		private string lightpositionField;

		private string lightlevelField;

		private ST_TrueFalse lightharshField;

		private bool lightharshFieldSpecified;

		private string lightposition2Field;

		private string lightlevel2Field;

		private ST_TrueFalse lightharsh2Field;

		private bool lightharsh2FieldSpecified;

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
		[DefaultValue(ST_ExtrusionType.parallel)]
		public ST_ExtrusionType type
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

		[DefaultValue(ST_ExtrusionRender.solid)]
		[XmlAttribute]
		public ST_ExtrusionRender render
		{
			get
			{
				return renderField;
			}
			set
			{
				renderField = value;
			}
		}

		[XmlAttribute]
		public string viewpointorigin
		{
			get
			{
				return viewpointoriginField;
			}
			set
			{
				viewpointoriginField = value;
			}
		}

		[XmlAttribute]
		public string viewpoint
		{
			get
			{
				return viewpointField;
			}
			set
			{
				viewpointField = value;
			}
		}

		[DefaultValue(ST_ExtrusionPlane.XY)]
		[XmlAttribute]
		public ST_ExtrusionPlane plane
		{
			get
			{
				return planeField;
			}
			set
			{
				planeField = value;
			}
		}

		[XmlAttribute]
		public float skewangle
		{
			get
			{
				return skewangleField;
			}
			set
			{
				skewangleField = value;
			}
		}

		[XmlIgnore]
		public bool skewangleSpecified
		{
			get
			{
				return skewangleFieldSpecified;
			}
			set
			{
				skewangleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string skewamt
		{
			get
			{
				return skewamtField;
			}
			set
			{
				skewamtField = value;
			}
		}

		[XmlAttribute]
		public string foredepth
		{
			get
			{
				return foredepthField;
			}
			set
			{
				foredepthField = value;
			}
		}

		[XmlAttribute]
		public string backdepth
		{
			get
			{
				return backdepthField;
			}
			set
			{
				backdepthField = value;
			}
		}

		[XmlAttribute]
		public string orientation
		{
			get
			{
				return orientationField;
			}
			set
			{
				orientationField = value;
			}
		}

		[XmlAttribute]
		public float orientationangle
		{
			get
			{
				return orientationangleField;
			}
			set
			{
				orientationangleField = value;
			}
		}

		[XmlIgnore]
		public bool orientationangleSpecified
		{
			get
			{
				return orientationangleFieldSpecified;
			}
			set
			{
				orientationangleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse lockrotationcenter
		{
			get
			{
				return lockrotationcenterField;
			}
			set
			{
				lockrotationcenterField = value;
			}
		}

		[XmlIgnore]
		public bool lockrotationcenterSpecified
		{
			get
			{
				return lockrotationcenterFieldSpecified;
			}
			set
			{
				lockrotationcenterFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse autorotationcenter
		{
			get
			{
				return autorotationcenterField;
			}
			set
			{
				autorotationcenterField = value;
			}
		}

		[XmlIgnore]
		public bool autorotationcenterSpecified
		{
			get
			{
				return autorotationcenterFieldSpecified;
			}
			set
			{
				autorotationcenterFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string rotationcenter
		{
			get
			{
				return rotationcenterField;
			}
			set
			{
				rotationcenterField = value;
			}
		}

		[XmlAttribute]
		public string rotationangle
		{
			get
			{
				return rotationangleField;
			}
			set
			{
				rotationangleField = value;
			}
		}

		[XmlAttribute]
		public ST_ColorMode colormode
		{
			get
			{
				return colormodeField;
			}
			set
			{
				colormodeField = value;
			}
		}

		[XmlIgnore]
		public bool colormodeSpecified
		{
			get
			{
				return colormodeFieldSpecified;
			}
			set
			{
				colormodeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string color
		{
			get
			{
				return colorField;
			}
			set
			{
				colorField = value;
			}
		}

		[XmlAttribute]
		public float shininess
		{
			get
			{
				return shininessField;
			}
			set
			{
				shininessField = value;
			}
		}

		[XmlIgnore]
		public bool shininessSpecified
		{
			get
			{
				return shininessFieldSpecified;
			}
			set
			{
				shininessFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string specularity
		{
			get
			{
				return specularityField;
			}
			set
			{
				specularityField = value;
			}
		}

		[XmlAttribute]
		public string diffusity
		{
			get
			{
				return diffusityField;
			}
			set
			{
				diffusityField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse metal
		{
			get
			{
				return metalField;
			}
			set
			{
				metalField = value;
			}
		}

		[XmlIgnore]
		public bool metalSpecified
		{
			get
			{
				return metalFieldSpecified;
			}
			set
			{
				metalFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string edge
		{
			get
			{
				return edgeField;
			}
			set
			{
				edgeField = value;
			}
		}

		[XmlAttribute]
		public string facet
		{
			get
			{
				return facetField;
			}
			set
			{
				facetField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse lightface
		{
			get
			{
				return lightfaceField;
			}
			set
			{
				lightfaceField = value;
			}
		}

		[XmlIgnore]
		public bool lightfaceSpecified
		{
			get
			{
				return lightfaceFieldSpecified;
			}
			set
			{
				lightfaceFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string brightness
		{
			get
			{
				return brightnessField;
			}
			set
			{
				brightnessField = value;
			}
		}

		[XmlAttribute]
		public string lightposition
		{
			get
			{
				return lightpositionField;
			}
			set
			{
				lightpositionField = value;
			}
		}

		[XmlAttribute]
		public string lightlevel
		{
			get
			{
				return lightlevelField;
			}
			set
			{
				lightlevelField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse lightharsh
		{
			get
			{
				return lightharshField;
			}
			set
			{
				lightharshField = value;
			}
		}

		[XmlIgnore]
		public bool lightharshSpecified
		{
			get
			{
				return lightharshFieldSpecified;
			}
			set
			{
				lightharshFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string lightposition2
		{
			get
			{
				return lightposition2Field;
			}
			set
			{
				lightposition2Field = value;
			}
		}

		[XmlAttribute]
		public string lightlevel2
		{
			get
			{
				return lightlevel2Field;
			}
			set
			{
				lightlevel2Field = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse lightharsh2
		{
			get
			{
				return lightharsh2Field;
			}
			set
			{
				lightharsh2Field = value;
			}
		}

		[XmlIgnore]
		public bool lightharsh2Specified
		{
			get
			{
				return lightharsh2FieldSpecified;
			}
			set
			{
				lightharsh2FieldSpecified = value;
			}
		}

		public CT_Extrusion()
		{
			typeField = ST_ExtrusionType.parallel;
			renderField = ST_ExtrusionRender.solid;
			planeField = ST_ExtrusionPlane.XY;
		}
	}
}
