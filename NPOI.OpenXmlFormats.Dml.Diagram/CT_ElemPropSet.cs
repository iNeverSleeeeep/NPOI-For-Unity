using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_ElemPropSet
	{
		private CT_LayoutVariablePropertySet presLayoutVarsField;

		private CT_ShapeStyle styleField;

		private string presAssocIDField;

		private string presNameField;

		private string presStyleLblField;

		private int presStyleIdxField;

		private bool presStyleIdxFieldSpecified;

		private int presStyleCntField;

		private bool presStyleCntFieldSpecified;

		private string loTypeIdField;

		private string loCatIdField;

		private string qsTypeIdField;

		private string qsCatIdField;

		private string csTypeIdField;

		private string csCatIdField;

		private bool coherent3DOffField;

		private bool coherent3DOffFieldSpecified;

		private string phldrTField;

		private bool phldrField;

		private bool phldrFieldSpecified;

		private int custAngField;

		private bool custAngFieldSpecified;

		private bool custFlipVertField;

		private bool custFlipVertFieldSpecified;

		private bool custFlipHorField;

		private bool custFlipHorFieldSpecified;

		private int custSzXField;

		private bool custSzXFieldSpecified;

		private int custSzYField;

		private bool custSzYFieldSpecified;

		private int custScaleXField;

		private bool custScaleXFieldSpecified;

		private int custScaleYField;

		private bool custScaleYFieldSpecified;

		private bool custTField;

		private bool custTFieldSpecified;

		private int custLinFactXField;

		private bool custLinFactXFieldSpecified;

		private int custLinFactYField;

		private bool custLinFactYFieldSpecified;

		private int custLinFactNeighborXField;

		private bool custLinFactNeighborXFieldSpecified;

		private int custLinFactNeighborYField;

		private bool custLinFactNeighborYFieldSpecified;

		private int custRadScaleRadField;

		private bool custRadScaleRadFieldSpecified;

		private int custRadScaleIncField;

		private bool custRadScaleIncFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_LayoutVariablePropertySet presLayoutVars
		{
			get
			{
				return presLayoutVarsField;
			}
			set
			{
				presLayoutVarsField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ShapeStyle style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlAttribute]
		public string presAssocID
		{
			get
			{
				return presAssocIDField;
			}
			set
			{
				presAssocIDField = value;
			}
		}

		[XmlAttribute]
		public string presName
		{
			get
			{
				return presNameField;
			}
			set
			{
				presNameField = value;
			}
		}

		[XmlAttribute]
		public string presStyleLbl
		{
			get
			{
				return presStyleLblField;
			}
			set
			{
				presStyleLblField = value;
			}
		}

		[XmlAttribute]
		public int presStyleIdx
		{
			get
			{
				return presStyleIdxField;
			}
			set
			{
				presStyleIdxField = value;
			}
		}

		[XmlIgnore]
		public bool presStyleIdxSpecified
		{
			get
			{
				return presStyleIdxFieldSpecified;
			}
			set
			{
				presStyleIdxFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int presStyleCnt
		{
			get
			{
				return presStyleCntField;
			}
			set
			{
				presStyleCntField = value;
			}
		}

		[XmlIgnore]
		public bool presStyleCntSpecified
		{
			get
			{
				return presStyleCntFieldSpecified;
			}
			set
			{
				presStyleCntFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string loTypeId
		{
			get
			{
				return loTypeIdField;
			}
			set
			{
				loTypeIdField = value;
			}
		}

		[XmlAttribute]
		public string loCatId
		{
			get
			{
				return loCatIdField;
			}
			set
			{
				loCatIdField = value;
			}
		}

		[XmlAttribute]
		public string qsTypeId
		{
			get
			{
				return qsTypeIdField;
			}
			set
			{
				qsTypeIdField = value;
			}
		}

		[XmlAttribute]
		public string qsCatId
		{
			get
			{
				return qsCatIdField;
			}
			set
			{
				qsCatIdField = value;
			}
		}

		[XmlAttribute]
		public string csTypeId
		{
			get
			{
				return csTypeIdField;
			}
			set
			{
				csTypeIdField = value;
			}
		}

		[XmlAttribute]
		public string csCatId
		{
			get
			{
				return csCatIdField;
			}
			set
			{
				csCatIdField = value;
			}
		}

		[XmlAttribute]
		public bool coherent3DOff
		{
			get
			{
				return coherent3DOffField;
			}
			set
			{
				coherent3DOffField = value;
			}
		}

		[XmlIgnore]
		public bool coherent3DOffSpecified
		{
			get
			{
				return coherent3DOffFieldSpecified;
			}
			set
			{
				coherent3DOffFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string phldrT
		{
			get
			{
				return phldrTField;
			}
			set
			{
				phldrTField = value;
			}
		}

		[XmlAttribute]
		public bool phldr
		{
			get
			{
				return phldrField;
			}
			set
			{
				phldrField = value;
			}
		}

		[XmlIgnore]
		public bool phldrSpecified
		{
			get
			{
				return phldrFieldSpecified;
			}
			set
			{
				phldrFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custAng
		{
			get
			{
				return custAngField;
			}
			set
			{
				custAngField = value;
			}
		}

		[XmlIgnore]
		public bool custAngSpecified
		{
			get
			{
				return custAngFieldSpecified;
			}
			set
			{
				custAngFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool custFlipVert
		{
			get
			{
				return custFlipVertField;
			}
			set
			{
				custFlipVertField = value;
			}
		}

		[XmlIgnore]
		public bool custFlipVertSpecified
		{
			get
			{
				return custFlipVertFieldSpecified;
			}
			set
			{
				custFlipVertFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool custFlipHor
		{
			get
			{
				return custFlipHorField;
			}
			set
			{
				custFlipHorField = value;
			}
		}

		[XmlIgnore]
		public bool custFlipHorSpecified
		{
			get
			{
				return custFlipHorFieldSpecified;
			}
			set
			{
				custFlipHorFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custSzX
		{
			get
			{
				return custSzXField;
			}
			set
			{
				custSzXField = value;
			}
		}

		[XmlIgnore]
		public bool custSzXSpecified
		{
			get
			{
				return custSzXFieldSpecified;
			}
			set
			{
				custSzXFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custSzY
		{
			get
			{
				return custSzYField;
			}
			set
			{
				custSzYField = value;
			}
		}

		[XmlIgnore]
		public bool custSzYSpecified
		{
			get
			{
				return custSzYFieldSpecified;
			}
			set
			{
				custSzYFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custScaleX
		{
			get
			{
				return custScaleXField;
			}
			set
			{
				custScaleXField = value;
			}
		}

		[XmlIgnore]
		public bool custScaleXSpecified
		{
			get
			{
				return custScaleXFieldSpecified;
			}
			set
			{
				custScaleXFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custScaleY
		{
			get
			{
				return custScaleYField;
			}
			set
			{
				custScaleYField = value;
			}
		}

		[XmlIgnore]
		public bool custScaleYSpecified
		{
			get
			{
				return custScaleYFieldSpecified;
			}
			set
			{
				custScaleYFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool custT
		{
			get
			{
				return custTField;
			}
			set
			{
				custTField = value;
			}
		}

		[XmlIgnore]
		public bool custTSpecified
		{
			get
			{
				return custTFieldSpecified;
			}
			set
			{
				custTFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custLinFactX
		{
			get
			{
				return custLinFactXField;
			}
			set
			{
				custLinFactXField = value;
			}
		}

		[XmlIgnore]
		public bool custLinFactXSpecified
		{
			get
			{
				return custLinFactXFieldSpecified;
			}
			set
			{
				custLinFactXFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custLinFactY
		{
			get
			{
				return custLinFactYField;
			}
			set
			{
				custLinFactYField = value;
			}
		}

		[XmlIgnore]
		public bool custLinFactYSpecified
		{
			get
			{
				return custLinFactYFieldSpecified;
			}
			set
			{
				custLinFactYFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custLinFactNeighborX
		{
			get
			{
				return custLinFactNeighborXField;
			}
			set
			{
				custLinFactNeighborXField = value;
			}
		}

		[XmlIgnore]
		public bool custLinFactNeighborXSpecified
		{
			get
			{
				return custLinFactNeighborXFieldSpecified;
			}
			set
			{
				custLinFactNeighborXFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custLinFactNeighborY
		{
			get
			{
				return custLinFactNeighborYField;
			}
			set
			{
				custLinFactNeighborYField = value;
			}
		}

		[XmlIgnore]
		public bool custLinFactNeighborYSpecified
		{
			get
			{
				return custLinFactNeighborYFieldSpecified;
			}
			set
			{
				custLinFactNeighborYFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custRadScaleRad
		{
			get
			{
				return custRadScaleRadField;
			}
			set
			{
				custRadScaleRadField = value;
			}
		}

		[XmlIgnore]
		public bool custRadScaleRadSpecified
		{
			get
			{
				return custRadScaleRadFieldSpecified;
			}
			set
			{
				custRadScaleRadFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int custRadScaleInc
		{
			get
			{
				return custRadScaleIncField;
			}
			set
			{
				custRadScaleIncField = value;
			}
		}

		[XmlIgnore]
		public bool custRadScaleIncSpecified
		{
			get
			{
				return custRadScaleIncFieldSpecified;
			}
			set
			{
				custRadScaleIncFieldSpecified = value;
			}
		}

		public CT_ElemPropSet()
		{
			styleField = new CT_ShapeStyle();
			presLayoutVarsField = new CT_LayoutVariablePropertySet();
		}
	}
}
