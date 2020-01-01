using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_Sources
	{
		private List<CT_SourceType> sourceField;

		private string selectedStyleField;

		private string styleNameField;

		private string uRIField;

		[XmlElement("Source")]
		public List<CT_SourceType> Source
		{
			get
			{
				return sourceField;
			}
			set
			{
				sourceField = value;
			}
		}

		[XmlAttribute]
		public string SelectedStyle
		{
			get
			{
				return selectedStyleField;
			}
			set
			{
				selectedStyleField = value;
			}
		}

		[XmlAttribute]
		public string StyleName
		{
			get
			{
				return styleNameField;
			}
			set
			{
				styleNameField = value;
			}
		}

		[XmlAttribute]
		public string URI
		{
			get
			{
				return uRIField;
			}
			set
			{
				uRIField = value;
			}
		}

		public CT_Sources()
		{
			sourceField = new List<CT_SourceType>();
		}
	}
}
