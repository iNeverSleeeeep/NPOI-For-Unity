using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_NameListType
	{
		private List<CT_PersonType> personField;

		[XmlElement("Person")]
		public List<CT_PersonType> Person
		{
			get
			{
				return personField;
			}
			set
			{
				personField = value;
			}
		}

		public CT_NameListType()
		{
			personField = new List<CT_PersonType>();
		}
	}
}
