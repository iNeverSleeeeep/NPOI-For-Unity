using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_Pt
	{
		private CT_ElemPropSet prSetField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody tField;

		private CT_OfficeArtExtensionList extLstField;

		private string modelIdField;

		private ST_PtType typeField;

		private string cxnIdField;

		[XmlElement(Order = 0)]
		public CT_ElemPropSet prSet
		{
			get
			{
				return prSetField;
			}
			set
			{
				prSetField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TextBody t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[XmlElement(Order = 3)]
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
		public string modelId
		{
			get
			{
				return modelIdField;
			}
			set
			{
				modelIdField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_PtType.node)]
		public ST_PtType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[DefaultValue("0")]
		[XmlAttribute]
		public string cxnId
		{
			get
			{
				return cxnIdField;
			}
			set
			{
				cxnIdField = value;
			}
		}

		public CT_Pt()
		{
			extLstField = new CT_OfficeArtExtensionList();
			tField = new CT_TextBody();
			spPrField = new CT_ShapeProperties();
			prSetField = new CT_ElemPropSet();
			typeField = ST_PtType.node;
			cxnIdField = "0";
		}
	}
}
