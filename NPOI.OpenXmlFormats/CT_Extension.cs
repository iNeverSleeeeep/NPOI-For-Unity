using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Extension
	{
		private XmlElement anyField;

		private string uriField;

		[XmlAnyElement]
		public XmlElement Any
		{
			get
			{
				return anyField;
			}
			set
			{
				anyField = value;
			}
		}

		[XmlAttribute(DataType = "token")]
		public string uri
		{
			get
			{
				return uriField;
			}
			set
			{
				uriField = value;
			}
		}
	}
}
