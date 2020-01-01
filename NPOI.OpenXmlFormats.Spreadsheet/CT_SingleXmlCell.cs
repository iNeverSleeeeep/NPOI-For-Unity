using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SingleXmlCell
	{
		private CT_XmlCellPr xmlCellPrField;

		private CT_ExtensionList extLstField;

		private uint idField;

		private string rField;

		private uint connectionIdField;

		[XmlElement]
		public CT_XmlCellPr xmlCellPr
		{
			get
			{
				return xmlCellPrField;
			}
			set
			{
				xmlCellPrField = value;
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
		public uint connectionId
		{
			get
			{
				return connectionIdField;
			}
			set
			{
				connectionIdField = value;
			}
		}

		public CT_SingleXmlCell()
		{
			extLstField = new CT_ExtensionList();
			xmlCellPrField = new CT_XmlCellPr();
		}
	}
}
