using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_WebPr
	{
		private CT_Tables tablesField;

		private bool xmlField;

		private bool sourceDataField;

		private bool parsePreField;

		private bool consecutiveField;

		private bool firstRowField;

		private bool xl97Field;

		private bool textDatesField;

		private bool xl2000Field;

		private string urlField;

		private string postField;

		private bool htmlTablesField;

		private ST_HtmlFmt htmlFormatField;

		private string editPageField;

		public CT_Tables tables
		{
			get
			{
				return tablesField;
			}
			set
			{
				tablesField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool xml
		{
			get
			{
				return xmlField;
			}
			set
			{
				xmlField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool sourceData
		{
			get
			{
				return sourceDataField;
			}
			set
			{
				sourceDataField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool parsePre
		{
			get
			{
				return parsePreField;
			}
			set
			{
				parsePreField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool consecutive
		{
			get
			{
				return consecutiveField;
			}
			set
			{
				consecutiveField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool firstRow
		{
			get
			{
				return firstRowField;
			}
			set
			{
				firstRowField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool xl97
		{
			get
			{
				return xl97Field;
			}
			set
			{
				xl97Field = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool textDates
		{
			get
			{
				return textDatesField;
			}
			set
			{
				textDatesField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool xl2000
		{
			get
			{
				return xl2000Field;
			}
			set
			{
				xl2000Field = value;
			}
		}

		[XmlAttribute]
		public string url
		{
			get
			{
				return urlField;
			}
			set
			{
				urlField = value;
			}
		}

		[XmlAttribute]
		public string post
		{
			get
			{
				return postField;
			}
			set
			{
				postField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool htmlTables
		{
			get
			{
				return htmlTablesField;
			}
			set
			{
				htmlTablesField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_HtmlFmt.none)]
		public ST_HtmlFmt htmlFormat
		{
			get
			{
				return htmlFormatField;
			}
			set
			{
				htmlFormatField = value;
			}
		}

		[XmlAttribute]
		public string editPage
		{
			get
			{
				return editPageField;
			}
			set
			{
				editPageField = value;
			}
		}

		public CT_WebPr()
		{
			xmlField = false;
			sourceDataField = false;
			parsePreField = false;
			consecutiveField = false;
			firstRowField = false;
			xl97Field = false;
			textDatesField = false;
			xl2000Field = false;
			htmlTablesField = false;
			htmlFormatField = ST_HtmlFmt.none;
		}
	}
}
