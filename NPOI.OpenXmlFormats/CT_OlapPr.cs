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
	public class CT_OlapPr
	{
		private bool localField;

		private string localConnectionField;

		private bool localRefreshField;

		private bool sendLocaleField;

		private uint rowDrillCountField;

		private bool rowDrillCountFieldSpecified;

		private bool serverFillField;

		private bool serverNumberFormatField;

		private bool serverFontField;

		private bool serverFontColorField;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool local
		{
			get
			{
				return localField;
			}
			set
			{
				localField = value;
			}
		}

		[XmlAttribute]
		public string localConnection
		{
			get
			{
				return localConnectionField;
			}
			set
			{
				localConnectionField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool localRefresh
		{
			get
			{
				return localRefreshField;
			}
			set
			{
				localRefreshField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool sendLocale
		{
			get
			{
				return sendLocaleField;
			}
			set
			{
				sendLocaleField = value;
			}
		}

		[XmlAttribute]
		public uint rowDrillCount
		{
			get
			{
				return rowDrillCountField;
			}
			set
			{
				rowDrillCountField = value;
			}
		}

		[XmlIgnore]
		public bool rowDrillCountSpecified
		{
			get
			{
				return rowDrillCountFieldSpecified;
			}
			set
			{
				rowDrillCountFieldSpecified = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool serverFill
		{
			get
			{
				return serverFillField;
			}
			set
			{
				serverFillField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool serverNumberFormat
		{
			get
			{
				return serverNumberFormatField;
			}
			set
			{
				serverNumberFormatField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool serverFont
		{
			get
			{
				return serverFontField;
			}
			set
			{
				serverFontField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool serverFontColor
		{
			get
			{
				return serverFontColorField;
			}
			set
			{
				serverFontColorField = value;
			}
		}

		public CT_OlapPr()
		{
			localField = false;
			localRefreshField = true;
			sendLocaleField = false;
			serverFillField = true;
			serverNumberFormatField = true;
			serverFontField = true;
			serverFontColorField = true;
		}
	}
}
