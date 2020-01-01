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
	public class CT_ForEach
	{
		private List<object> itemsField;

		private string nameField;

		private string refField;

		private List<ST_AxisType> axisField;

		private List<ST_ElementType> ptTypeField;

		private List<bool> hideLastTransField;

		private List<int> stField;

		private List<uint> cntField;

		private List<int> stepField;

		[XmlElement("extLst", typeof(CT_OfficeArtExtensionList), Order = 0)]
		[XmlElement("forEach", typeof(CT_ForEach), Order = 0)]
		[XmlElement("layoutNode", typeof(CT_LayoutNode), Order = 0)]
		[XmlElement("alg", typeof(CT_Algorithm), Order = 0)]
		[XmlElement("choose", typeof(CT_Choose), Order = 0)]
		[XmlElement("constrLst", typeof(CT_Constraints), Order = 0)]
		[XmlElement("presOf", typeof(CT_PresentationOf), Order = 0)]
		[XmlElement("ruleLst", typeof(CT_Rules), Order = 0)]
		[XmlElement("shape", typeof(CT_Shape), Order = 0)]
		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
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

		[DefaultValue("")]
		[XmlAttribute]
		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
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

		public CT_ForEach()
		{
			stepField = new List<int>();
			cntField = new List<uint>();
			stField = new List<int>();
			hideLastTransField = new List<bool>();
			ptTypeField = new List<ST_ElementType>();
			axisField = new List<ST_AxisType>();
			itemsField = new List<object>();
			nameField = "";
			refField = "";
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
