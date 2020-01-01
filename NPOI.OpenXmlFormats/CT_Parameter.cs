using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_Parameter
	{
		private string nameField;

		private int sqlTypeField;

		private ST_ParameterType parameterTypeField;

		private bool refreshOnChangeField;

		private string promptField;

		private bool booleanField;

		private bool booleanFieldSpecified;

		private double doubleField;

		private bool doubleFieldSpecified;

		private int integerField;

		private bool integerFieldSpecified;

		private string stringField;

		private string cellField;

		[XmlAttribute]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[DefaultValue(0)]
		[XmlAttribute]
		public int sqlType
		{
			get
			{
				return sqlTypeField;
			}
			set
			{
				sqlTypeField = value;
			}
		}

		[DefaultValue(ST_ParameterType.prompt)]
		[XmlAttribute]
		public ST_ParameterType parameterType
		{
			get
			{
				return parameterTypeField;
			}
			set
			{
				parameterTypeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool refreshOnChange
		{
			get
			{
				return refreshOnChangeField;
			}
			set
			{
				refreshOnChangeField = value;
			}
		}

		[XmlAttribute]
		public string prompt
		{
			get
			{
				return promptField;
			}
			set
			{
				promptField = value;
			}
		}

		[XmlAttribute]
		public bool boolean
		{
			get
			{
				return booleanField;
			}
			set
			{
				booleanField = value;
			}
		}

		[XmlIgnore]
		public bool booleanSpecified
		{
			get
			{
				return booleanFieldSpecified;
			}
			set
			{
				booleanFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public double @double
		{
			get
			{
				return doubleField;
			}
			set
			{
				doubleField = value;
			}
		}

		[XmlIgnore]
		public bool doubleSpecified
		{
			get
			{
				return doubleFieldSpecified;
			}
			set
			{
				doubleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int integer
		{
			get
			{
				return integerField;
			}
			set
			{
				integerField = value;
			}
		}

		[XmlIgnore]
		public bool integerSpecified
		{
			get
			{
				return integerFieldSpecified;
			}
			set
			{
				integerFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string @string
		{
			get
			{
				return stringField;
			}
			set
			{
				stringField = value;
			}
		}

		[XmlAttribute]
		public string cell
		{
			get
			{
				return cellField;
			}
			set
			{
				cellField = value;
			}
		}

		public CT_Parameter()
		{
			sqlTypeField = 0;
			parameterTypeField = ST_ParameterType.prompt;
			refreshOnChangeField = false;
		}
	}
}
