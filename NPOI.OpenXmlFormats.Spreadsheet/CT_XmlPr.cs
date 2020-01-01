using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_XmlPr
	{
		private CT_ExtensionList extLstField;

		private uint mapIdField;

		private string xpathField;

		private ST_XmlDataType xmlDataTypeField;

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
		public uint mapId
		{
			get
			{
				return mapIdField;
			}
			set
			{
				mapIdField = value;
			}
		}

		[XmlAttribute]
		public string xpath
		{
			get
			{
				return xpathField;
			}
			set
			{
				xpathField = value;
			}
		}

		[XmlAttribute]
		public ST_XmlDataType xmlDataType
		{
			get
			{
				return xmlDataTypeField;
			}
			set
			{
				xmlDataTypeField = value;
			}
		}

		public CT_XmlPr()
		{
			extLstField = new CT_ExtensionList();
		}
	}
}
