using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	public class CT_RoundRect
	{
		private List<object> itemsField;

		private ItemsChoiceType5[] itemsElementNameField;

		private string arcsizeField;

		[XmlAttribute]
		public string arcsize
		{
			get
			{
				return arcsizeField;
			}
			set
			{
				arcsizeField = value;
			}
		}
	}
}
