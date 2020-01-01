using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionConflict
	{
		private uint rIdField;

		private bool uaField;

		private bool raField;

		private uint sheetIdField;

		private bool sheetIdFieldSpecified;

		public uint rId
		{
			get
			{
				return rIdField;
			}
			set
			{
				rIdField = value;
			}
		}

		[DefaultValue(false)]
		public bool ua
		{
			get
			{
				return uaField;
			}
			set
			{
				uaField = value;
			}
		}

		[DefaultValue(false)]
		public bool ra
		{
			get
			{
				return raField;
			}
			set
			{
				raField = value;
			}
		}

		public uint sheetId
		{
			get
			{
				return sheetIdField;
			}
			set
			{
				sheetIdField = value;
			}
		}

		[XmlIgnore]
		public bool sheetIdSpecified
		{
			get
			{
				return sheetIdFieldSpecified;
			}
			set
			{
				sheetIdFieldSpecified = value;
			}
		}

		public CT_RevisionConflict()
		{
			uaField = false;
			raField = false;
		}
	}
}
