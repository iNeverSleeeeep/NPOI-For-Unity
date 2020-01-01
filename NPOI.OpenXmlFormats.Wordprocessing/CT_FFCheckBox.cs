using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_FFCheckBox
	{
		private object itemField;

		private CT_OnOff defaultField;

		private CT_OnOff checkedField;

		[XmlElement("sizeAuto", typeof(CT_OnOff), Order = 0)]
		[XmlElement("size", typeof(CT_HpsMeasure), Order = 0)]
		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff @default
		{
			get
			{
				return defaultField;
			}
			set
			{
				defaultField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff @checked
		{
			get
			{
				return checkedField;
			}
			set
			{
				checkedField = value;
			}
		}

		public CT_FFCheckBox()
		{
			checkedField = new CT_OnOff();
			defaultField = new CT_OnOff();
		}
	}
}
