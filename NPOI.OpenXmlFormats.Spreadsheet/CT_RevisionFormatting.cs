using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionFormatting
	{
		private CT_Dxf dxfField;

		private CT_ExtensionList extLstField;

		private uint sheetIdField;

		private bool xfDxfField;

		private bool sField;

		private List<string> sqrefField;

		private uint startField;

		private bool startFieldSpecified;

		private uint lengthField;

		private bool lengthFieldSpecified;

		public CT_Dxf dxf
		{
			get
			{
				return dxfField;
			}
			set
			{
				dxfField = value;
			}
		}

		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
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

		[DefaultValue(false)]
		public bool xfDxf
		{
			get
			{
				return xfDxfField;
			}
			set
			{
				xfDxfField = value;
			}
		}

		[DefaultValue(false)]
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

		public List<string> sqref
		{
			get
			{
				return sqrefField;
			}
			set
			{
				sqrefField = value;
			}
		}

		public uint start
		{
			get
			{
				return startField;
			}
			set
			{
				startField = value;
			}
		}

		[XmlIgnore]
		public bool startSpecified
		{
			get
			{
				return startFieldSpecified;
			}
			set
			{
				startFieldSpecified = value;
			}
		}

		public uint length
		{
			get
			{
				return lengthField;
			}
			set
			{
				lengthField = value;
			}
		}

		[XmlIgnore]
		public bool lengthSpecified
		{
			get
			{
				return lengthFieldSpecified;
			}
			set
			{
				lengthFieldSpecified = value;
			}
		}

		public CT_RevisionFormatting()
		{
			sqrefField = new List<string>();
			extLstField = new CT_ExtensionList();
			dxfField = new CT_Dxf();
			xfDxfField = false;
			sField = false;
		}
	}
}
