using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	public class CT_LayoutVariablePropertySet
	{
		private CT_OrgChart orgChartField;

		private CT_ChildMax chMaxField;

		private CT_ChildPref chPrefField;

		private CT_BulletEnabled bulletEnabledField;

		private CT_Direction dirField;

		private CT_HierBranchStyle hierBranchField;

		private CT_AnimOne animOneField;

		private CT_AnimLvl animLvlField;

		private CT_ResizeHandles resizeHandlesField;

		[XmlElement(Order = 0)]
		public CT_OrgChart orgChart
		{
			get
			{
				return orgChartField;
			}
			set
			{
				orgChartField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ChildMax chMax
		{
			get
			{
				return chMaxField;
			}
			set
			{
				chMaxField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_ChildPref chPref
		{
			get
			{
				return chPrefField;
			}
			set
			{
				chPrefField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_BulletEnabled bulletEnabled
		{
			get
			{
				return bulletEnabledField;
			}
			set
			{
				bulletEnabledField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Direction dir
		{
			get
			{
				return dirField;
			}
			set
			{
				dirField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_HierBranchStyle hierBranch
		{
			get
			{
				return hierBranchField;
			}
			set
			{
				hierBranchField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_AnimOne animOne
		{
			get
			{
				return animOneField;
			}
			set
			{
				animOneField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_AnimLvl animLvl
		{
			get
			{
				return animLvlField;
			}
			set
			{
				animLvlField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_ResizeHandles resizeHandles
		{
			get
			{
				return resizeHandlesField;
			}
			set
			{
				resizeHandlesField = value;
			}
		}

		public CT_LayoutVariablePropertySet()
		{
			resizeHandlesField = new CT_ResizeHandles();
			animLvlField = new CT_AnimLvl();
			animOneField = new CT_AnimOne();
			hierBranchField = new CT_HierBranchStyle();
			dirField = new CT_Direction();
			bulletEnabledField = new CT_BulletEnabled();
			chPrefField = new CT_ChildPref();
			chMaxField = new CT_ChildMax();
			orgChartField = new CT_OrgChart();
		}
	}
}
