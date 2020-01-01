using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CalcCell
	{
		private string rField = "";

		private int iField;

		private bool sField;

		private bool lField;

		private bool tField;

		private bool aField;

		private bool iSpecifiedField;

		[XmlAttribute]
		public string r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int i
		{
			get
			{
				return iField;
			}
			set
			{
				iField = value;
			}
		}

		[XmlIgnore]
		public bool iSpecified
		{
			get
			{
				return iSpecifiedField;
			}
			set
			{
				iSpecifiedField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool s
		{
			get
			{
				return sField;
			}
			set
			{
				sField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool l
		{
			get
			{
				return lField;
			}
			set
			{
				lField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool a
		{
			get
			{
				return aField;
			}
			set
			{
				aField = value;
			}
		}
	}
}
