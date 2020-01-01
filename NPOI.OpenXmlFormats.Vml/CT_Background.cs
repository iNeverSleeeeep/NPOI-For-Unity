using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_Background
	{
		private CT_Fill fillField;

		private string idField;

		private ST_TrueFalse filledField;

		private bool filledFieldSpecified;

		private string fillcolorField;

		public CT_Fill fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

		[XmlAttribute]
		public string id
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
		public ST_TrueFalse filled
		{
			get
			{
				return filledField;
			}
			set
			{
				filledField = value;
			}
		}

		[XmlIgnore]
		public bool filledSpecified
		{
			get
			{
				return filledFieldSpecified;
			}
			set
			{
				filledFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string fillcolor
		{
			get
			{
				return fillcolorField;
			}
			set
			{
				fillcolorField = value;
			}
		}
	}
}
