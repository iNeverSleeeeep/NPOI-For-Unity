using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_Constraint
	{
		private CT_OfficeArtExtensionList extLstField;

		private ST_ConstraintType typeField;

		private ST_ConstraintRelationship forField;

		private string forNameField;

		private List<ST_ElementType> ptTypeField;

		private ST_ConstraintType refTypeField;

		private ST_ConstraintRelationship refForField;

		private string refForNameField;

		private List<ST_ElementType> refPtTypeField;

		private ST_BoolOperator opField;

		private double valField;

		private double factField;

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

		[XmlAttribute]
		[DefaultValue(ST_ConstraintRelationship.self)]
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

		[XmlAttribute]
		[DefaultValue("")]
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

		[DefaultValue(ST_ConstraintType.none)]
		[XmlAttribute]
		public ST_ConstraintType refType
		{
			get
			{
				return refTypeField;
			}
			set
			{
				refTypeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_ConstraintRelationship.self)]
		public ST_ConstraintRelationship refFor
		{
			get
			{
				return refForField;
			}
			set
			{
				refForField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute]
		public string refForName
		{
			get
			{
				return refForNameField;
			}
			set
			{
				refForNameField = value;
			}
		}

		[XmlAttribute]
		public List<ST_ElementType> refPtType
		{
			get
			{
				return refPtTypeField;
			}
			set
			{
				refPtTypeField = value;
			}
		}

		[DefaultValue(ST_BoolOperator.none)]
		[XmlAttribute]
		public ST_BoolOperator op
		{
			get
			{
				return opField;
			}
			set
			{
				opField = value;
			}
		}

		[DefaultValue(0.0)]
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

		[DefaultValue(1.0)]
		[XmlAttribute]
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

		public CT_Constraint()
		{
			refPtTypeField = new List<ST_ElementType>();
			ptTypeField = new List<ST_ElementType>();
			forField = ST_ConstraintRelationship.self;
			forNameField = "";
			ST_ElementType[] collection = new ST_ElementType[1];
			ptTypeField = new List<ST_ElementType>(collection);
			refTypeField = ST_ConstraintType.none;
			refForField = ST_ConstraintRelationship.self;
			refForNameField = "";
			ST_ElementType[] collection2 = new ST_ElementType[1];
			refPtTypeField = new List<ST_ElementType>(collection2);
			opField = ST_BoolOperator.none;
			valField = 0.0;
			factField = 1.0;
		}
	}
}
