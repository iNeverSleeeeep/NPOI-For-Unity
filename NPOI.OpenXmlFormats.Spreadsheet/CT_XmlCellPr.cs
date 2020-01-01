using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_XmlCellPr
	{
		private CT_XmlPr xmlPrField;

		private CT_ExtensionList extLstField;

		private uint idField;

		private string uniqueNameField;

		[XmlElement]
		public CT_XmlPr xmlPr
		{
			get
			{
				return xmlPrField;
			}
			set
			{
				xmlPrField = value;
			}
		}

		[XmlElement]
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

		[XmlAttribute]
		public uint id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		public string uniqueName
		{
			get
			{
				return uniqueNameField;
			}
			set
			{
				uniqueNameField = value;
			}
		}

		public CT_XmlCellPr()
		{
			extLstField = new CT_ExtensionList();
			xmlPrField = new CT_XmlPr();
		}
	}
}
