using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_NumericRule
	{
		private CT_OfficeArtExtensionList extLstField;

		private ST_ConstraintType typeField;

		private ST_ConstraintRelationship forField;

		private string forNameField;

		private List<ST_ElementType> ptTypeField;

		private double valField;

		private double factField;

		private double maxField;

		[XmlElement(Order = 0)]
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
		public ST_ConstraintType type
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

		[DefaultValue(ST_ConstraintRelationship.self)]
		[XmlAttribute]
		public ST_ConstraintRelationship @for
		{
			get
			{
				return forField;
			}
			set
			{
				forField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute]
		public string forName
		{
			get
			{
				return forNameField;
			}
			set
			{
				forNameField = value;
			}
		}

		[XmlAttribute]
		public List<ST_ElementType> ptType
		{
			get
			{
				return ptTypeField;
			}
			set
			{
				ptTypeField = value;
			}
		}

		[DefaultValue(double.NaN)]
		[XmlAttribute]
		public double val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(double.NaN)]
		public double fact
		{
			get
			{
				return factField;
			}
			set
			{
				factField = value;
			}
		}

		[DefaultValue(double.NaN)]
		[XmlAttribute]
		public double max
		{
			get
			{
				return maxField;
			}
			set
			{
				maxField = value;
			}
		}

		public CT_NumericRule()
		{
			ptTypeField = new List<ST_ElementType>();
			forField = ST_ConstraintRelationship.self;
			forNameField = "";
			ST_ElementType[] collection = new ST_ElementType[1];
			ptTypeField = new List<ST_ElementType>(collection);
			valField = double.NaN;
			factField = double.NaN;
			maxField = double.NaN;
		}
	}
}
