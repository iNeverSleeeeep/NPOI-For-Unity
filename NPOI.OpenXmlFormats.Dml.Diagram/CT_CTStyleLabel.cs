using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[GeneratedCode("System.Xml", "4.0.30319.17379")]
	[DebuggerStepThrough]
	public class CT_CTStyleLabel
	{
		private CT_Colors fillClrLstField;

		private CT_Colors linClrLstField;

		private CT_Colors effectClrLstField;

		private CT_Colors txLinClrLstField;

		private CT_Colors txFillClrLstField;

		private CT_Colors txEffectClrLstField;

		private CT_OfficeArtExtensionList extLstField;

		private string nameField;

		[XmlElement(Order = 0)]
		public CT_Colors fillClrLst
		{
			get
			{
				return fillClrLstField;
			}
			set
			{
				fillClrLstField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Colors linClrLst
		{
			get
			{
				return linClrLstField;
			}
			set
			{
				linClrLstField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Colors effectClrLst
		{
			get
			{
				return effectClrLstField;
			}
			set
			{
				effectClrLstField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Colors txLinClrLst
		{
			get
			{
				return txLinClrLstField;
			}
			set
			{
				txLinClrLstField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Colors txFillClrLst
		{
			get
			{
				return txFillClrLstField;
			}
			set
			{
				txFillClrLstField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Colors txEffectClrLst
		{
			get
			{
				return txEffectClrLstField;
			}
			set
			{
				txEffectClrLstField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public CT_CTStyleLabel()
		{
			txEffectClrLstField = new CT_Colors();
			txFillClrLstField = new CT_Colors();
			txLinClrLstField = new CT_Colors();
			effectClrLstField = new CT_Colors();
			linClrLstField = new CT_Colors();
			fillClrLstField = new CT_Colors();
		}
	}
}
