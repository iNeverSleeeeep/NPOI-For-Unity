using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties")]
	public class CT_VectorVariant
	{
		private CT_Vector vectorField;

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", Order = 0)]
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
	}
}
