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
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_PresentationOf
	{
		private CT_OfficeArtExtensionList extLstField;

		private List<ST_AxisType> axisField;

		private List<ST_ElementType> ptTypeField;

		private List<bool> hideLastTransField;

		private List<int> stField;

		private List<uint> cntField;

		private List<int> stepField;

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
		public List<ST_AxisType> axis
		{
			get
			{
				return axisField;
			}
			set
			{
				axisField = value;
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

		[XmlAttribute]
		public List<bool> hideLastTrans
		{
			get
			{
				return hideLastTransField;
			}
			set
			{
				hideLastTransField = value;
			}
		}

		[XmlAttribute]
		public List<int> st
		{
			get
			{
				return stField;
			}
			set
			{
				stField = value;
			}
		}

		[XmlAttribute]
		public List<uint> cnt
		{
			get
			{
				return cntField;
			}
			set
			{
				cntField = value;
			}
		}

		[XmlAttribute]
		public List<int> step
		{
			get
			{
				return stepField;
			}
			set
			{
				stepField = value;
			}
		}

		public CT_PresentationOf()
		{
			stepField = new List<int>();
			cntField = new List<uint>();
			stField = new List<int>();
			hideLastTransField = new List<bool>();
			ptTypeField = new List<ST_ElementType>();
			axisField = new List<ST_AxisType>();
			axisField = new List<ST_AxisType>(new ST_AxisType[1]
			{
				ST_AxisType.none
			});
			ST_ElementType[] collection = new ST_ElementType[1];
			ptTypeField = new List<ST_ElementType>(collection);
			hideLastTransField = new List<bool>(new bool[1]
			{
				true
			});
			stField = new List<int>(new int[1]
			{
				1
			});
			uint[] collection2 = new uint[1];
			cntField = new List<uint>(collection2);
			stepField = new List<int>(new int[1]
			{
				1
			});
		}
	}
}
