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
	public class CT_StrokeChild
	{
		private ST_Ext extField;

		private ST_TrueFalse onField;

		private bool onFieldSpecified;

		private string weightField;

		private string colorField;

		private string color2Field;

		private string opacityField;

		private ST_StrokeLineStyle linestyleField;

		private bool linestyleFieldSpecified;

		private decimal miterlimitField;

		private bool miterlimitFieldSpecified;

		private ST_StrokeJoinStyle joinstyleField;

		private bool joinstyleFieldSpecified;

		private ST_StrokeEndCap endcapField;

		private bool endcapFieldSpecified;

		private string dashstyleField;

		private ST_TrueFalse insetpenField;

		private bool insetpenFieldSpecified;

		private ST_FillType filltypeField;

		private bool filltypeFieldSpecified;

		private string srcField;

		private ST_ImageAspect imageaspectField;

		private bool imageaspectFieldSpecified;

		private string imagesizeField;

		private ST_TrueFalse imagealignshapeField;

		private bool imagealignshapeFieldSpecified;

		private ST_StrokeArrowType startarrowField;

		private bool startarrowFieldSpecified;

		private ST_StrokeArrowWidth startarrowwidthField;

		private bool startarrowwidthFieldSpecified;

		private ST_StrokeArrowLength startarrowlengthField;

		private bool startarrowlengthFieldSpecified;

		private ST_StrokeArrowType endarrowField;

		private bool endarrowFieldSpecified;

		private ST_StrokeArrowWidth endarrowwidthField;

		private bool endarrowwidthFieldSpecified;

		private ST_StrokeArrowLength endarrowlengthField;

		private bool endarrowlengthFieldSpecified;

		private string hrefField;

		private string althrefField;

		private string titleField;

		private ST_TrueFalse forcedashField;

		private bool forcedashFieldSpecified;

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
		public string weight
		{
			get
			{
				return weightField;
			}
			set
			{
				weightField = value;
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
		public string color2
		{
			get
			{
				return color2Field;
			}
			set
			{
				color2Field = value;
			}
		}

		[XmlAttribute]
		public string opacity
		{
			get
			{
				return opacityField;
			}
			set
			{
				opacityField = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeLineStyle linestyle
		{
			get
			{
				return linestyleField;
			}
			set
			{
				linestyleField = value;
			}
		}

		[XmlIgnore]
		public bool linestyleSpecified
		{
			get
			{
				return linestyleFieldSpecified;
			}
			set
			{
				linestyleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public decimal miterlimit
		{
			get
			{
				return miterlimitField;
			}
			set
			{
				miterlimitField = value;
			}
		}

		[XmlIgnore]
		public bool miterlimitSpecified
		{
			get
			{
				return miterlimitFieldSpecified;
			}
			set
			{
				miterlimitFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeJoinStyle joinstyle
		{
			get
			{
				return joinstyleField;
			}
			set
			{
				joinstyleField = value;
			}
		}

		[XmlIgnore]
		public bool joinstyleSpecified
		{
			get
			{
				return joinstyleFieldSpecified;
			}
			set
			{
				joinstyleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeEndCap endcap
		{
			get
			{
				return endcapField;
			}
			set
			{
				endcapField = value;
			}
		}

		[XmlIgnore]
		public bool endcapSpecified
		{
			get
			{
				return endcapFieldSpecified;
			}
			set
			{
				endcapFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string dashstyle
		{
			get
			{
				return dashstyleField;
			}
			set
			{
				dashstyleField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse insetpen
		{
			get
			{
				return insetpenField;
			}
			set
			{
				insetpenField = value;
			}
		}

		[XmlIgnore]
		public bool insetpenSpecified
		{
			get
			{
				return insetpenFieldSpecified;
			}
			set
			{
				insetpenFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_FillType filltype
		{
			get
			{
				return filltypeField;
			}
			set
			{
				filltypeField = value;
			}
		}

		[XmlIgnore]
		public bool filltypeSpecified
		{
			get
			{
				return filltypeFieldSpecified;
			}
			set
			{
				filltypeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string src
		{
			get
			{
				return srcField;
			}
			set
			{
				srcField = value;
			}
		}

		[XmlAttribute]
		public ST_ImageAspect imageaspect
		{
			get
			{
				return imageaspectField;
			}
			set
			{
				imageaspectField = value;
			}
		}

		[XmlIgnore]
		public bool imageaspectSpecified
		{
			get
			{
				return imageaspectFieldSpecified;
			}
			set
			{
				imageaspectFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string imagesize
		{
			get
			{
				return imagesizeField;
			}
			set
			{
				imagesizeField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse imagealignshape
		{
			get
			{
				return imagealignshapeField;
			}
			set
			{
				imagealignshapeField = value;
			}
		}

		[XmlIgnore]
		public bool imagealignshapeSpecified
		{
			get
			{
				return imagealignshapeFieldSpecified;
			}
			set
			{
				imagealignshapeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeArrowType startarrow
		{
			get
			{
				return startarrowField;
			}
			set
			{
				startarrowField = value;
			}
		}

		[XmlIgnore]
		public bool startarrowSpecified
		{
			get
			{
				return startarrowFieldSpecified;
			}
			set
			{
				startarrowFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeArrowWidth startarrowwidth
		{
			get
			{
				return startarrowwidthField;
			}
			set
			{
				startarrowwidthField = value;
			}
		}

		[XmlIgnore]
		public bool startarrowwidthSpecified
		{
			get
			{
				return startarrowwidthFieldSpecified;
			}
			set
			{
				startarrowwidthFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeArrowLength startarrowlength
		{
			get
			{
				return startarrowlengthField;
			}
			set
			{
				startarrowlengthField = value;
			}
		}

		[XmlIgnore]
		public bool startarrowlengthSpecified
		{
			get
			{
				return startarrowlengthFieldSpecified;
			}
			set
			{
				startarrowlengthFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeArrowType endarrow
		{
			get
			{
				return endarrowField;
			}
			set
			{
				endarrowField = value;
			}
		}

		[XmlIgnore]
		public bool endarrowSpecified
		{
			get
			{
				return endarrowFieldSpecified;
			}
			set
			{
				endarrowFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeArrowWidth endarrowwidth
		{
			get
			{
				return endarrowwidthField;
			}
			set
			{
				endarrowwidthField = value;
			}
		}

		[XmlIgnore]
		public bool endarrowwidthSpecified
		{
			get
			{
				return endarrowwidthFieldSpecified;
			}
			set
			{
				endarrowwidthFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_StrokeArrowLength endarrowlength
		{
			get
			{
				return endarrowlengthField;
			}
			set
			{
				endarrowlengthField = value;
			}
		}

		[XmlIgnore]
		public bool endarrowlengthSpecified
		{
			get
			{
				return endarrowlengthFieldSpecified;
			}
			set
			{
				endarrowlengthFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string href
		{
			get
			{
				return hrefField;
			}
			set
			{
				hrefField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string althref
		{
			get
			{
				return althrefField;
			}
			set
			{
				althrefField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string title
		{
			get
			{
				return titleField;
			}
			set
			{
				titleField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TrueFalse forcedash
		{
			get
			{
				return forcedashField;
			}
			set
			{
				forcedashField = value;
			}
		}

		[XmlIgnore]
		public bool forcedashSpecified
		{
			get
			{
				return forcedashFieldSpecified;
			}
			set
			{
				forcedashFieldSpecified = value;
			}
		}
	}
}
