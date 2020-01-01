using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties")]
	public class CT_VectorLpstr
	{
		private CT_Vector vectorField;

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
		public CT_Vector vector
		{
			get
			{
				return vectorField;
			}
			set
			{
				vectorField = value;
			}
		}

		public CT_VectorLpstr()
		{
			vectorField = new CT_Vector();
		}
	}
}
