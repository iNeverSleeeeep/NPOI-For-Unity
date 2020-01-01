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
	public class CT_PersonType
	{
		private List<string> lastField;

		private List<string> firstField;

		private List<string> middleField;

		[XmlElement("Last")]
		public List<string> Last
		{
			get
			{
				return lastField;
			}
			set
			{
				lastField = value;
			}
		}

		[XmlElement("First")]
		public List<string> First
		{
			get
			{
				return firstField;
			}
			set
			{
				firstField = value;
			}
		}

		[XmlElement("Middle")]
		public List<string> Middle
		{
			get
			{
				return middleField;
			}
			set
			{
				middleField = value;
			}
		}

		public CT_PersonType()
		{
			middleField = new List<string>();
			firstField = new List<string>();
			lastField = new List<string>();
		}
	}
}
