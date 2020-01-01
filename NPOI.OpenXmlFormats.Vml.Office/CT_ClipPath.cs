using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	public class CT_ClipPath
	{
		private string vField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string v
		{
			get
			{
				return vField;
			}
			set
			{
				vField = value;
			}
		}
	}
}
