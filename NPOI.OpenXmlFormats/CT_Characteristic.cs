using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/characteristics")]
	public class CT_Characteristic
	{
		private string nameField;

		private ST_Relation relationField;

		private string valField;

		private string vocabularyField;

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

		public ST_Relation relation
		{
			get
			{
				return relationField;
			}
			set
			{
				relationField = value;
			}
		}

		public string val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public string vocabulary
		{
			get
			{
				return vocabularyField;
			}
			set
			{
				vocabularyField = value;
			}
		}
	}
}
