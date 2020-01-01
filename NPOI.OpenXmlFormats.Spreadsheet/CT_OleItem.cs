using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	public class CT_OleItem
	{
		private string nameField;

		private bool iconField;

		private bool adviseField;

		private bool preferPicField;

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

		[XmlAttribute]
		[DefaultValue(false)]
		public bool icon
		{
			get
			{
				return iconField;
			}
			set
			{
				iconField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool advise
		{
			get
			{
				return adviseField;
			}
			set
			{
				adviseField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool preferPic
		{
			get
			{
				return preferPicField;
			}
			set
			{
				preferPicField = value;
			}
		}

		public CT_OleItem()
		{
			iconField = false;
			adviseField = false;
			preferPicField = false;
		}
	}
}
