using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_ColorChangeEffect
	{
		private CT_Color clrFromField;

		private CT_Color clrToField;

		private bool useAField;

		[XmlElement(Order = 0)]
		public CT_Color clrFrom
		{
			get
			{
				return clrFromField;
			}
			set
			{
				clrFromField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Color clrTo
		{
			get
			{
				return clrToField;
			}
			set
			{
				clrToField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool useA
		{
			get
			{
				return useAField;
			}
			set
			{
				useAField = value;
			}
		}

		public CT_ColorChangeEffect()
		{
			clrToField = new CT_Color();
			clrFromField = new CT_Color();
			useAField = true;
		}
	}
}
