using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_Variant
	{
		private sbyte i1Field;

		private bool i1SpecifiedField;

		private short i2Field;

		private bool i2SpecifiedField;

		private int i4Field;

		private bool i4SpecifiedField;

		private long i8Field;

		private bool i8SpecifiedField;

		private string lpstrField;

		private string lpwstrField;

		private float r4Field;

		private bool r4SpecifiedField;

		private double r8Field;

		private bool r8SpecifiedField;

		[XmlElement]
		public short i2
		{
			get
			{
				return i2Field;
			}
			set
			{
				i2Field = value;
			}
		}

		[XmlIgnore]
		public bool i2Specified
		{
			get
			{
				return i2SpecifiedField;
			}
			set
			{
				i2SpecifiedField = value;
			}
		}

		[XmlElement]
		public int i4
		{
			get
			{
				return i4Field;
			}
			set
			{
				i4Field = value;
			}
		}

		[XmlIgnore]
		public bool i4Specified
		{
			get
			{
				return i4SpecifiedField;
			}
			set
			{
				i4SpecifiedField = value;
			}
		}

		[XmlElement]
		public long i8
		{
			get
			{
				return i8Field;
			}
			set
			{
				i8Field = value;
			}
		}

		[XmlIgnore]
		public bool i8Specified
		{
			get
			{
				return i8SpecifiedField;
			}
			set
			{
				i8SpecifiedField = value;
			}
		}

		[XmlElement]
		public string lpstr
		{
			get
			{
				return lpstrField;
			}
			set
			{
				lpstrField = value;
			}
		}

		[XmlElement]
		public string lpwstr
		{
			get
			{
				return lpwstrField;
			}
			set
			{
				lpwstrField = value;
			}
		}

		[XmlElement]
		public float r4
		{
			get
			{
				return r4Field;
			}
			set
			{
				r4Field = value;
			}
		}

		[XmlIgnore]
		public bool r4Specified
		{
			get
			{
				return r4SpecifiedField;
			}
			set
			{
				r4SpecifiedField = value;
			}
		}

		[XmlElement]
		public double r8
		{
			get
			{
				return r8Field;
			}
			set
			{
				r8Field = value;
			}
		}

		[XmlIgnore]
		public bool r8Specified
		{
			get
			{
				return r8SpecifiedField;
			}
			set
			{
				r8SpecifiedField = value;
			}
		}
	}
}
