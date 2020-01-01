using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_PolarAdjustHandle
	{
		private CT_AdjPoint2D posField;

		private string gdRefRField;

		private string minRField;

		private string maxRField;

		private string gdRefAngField;

		private string minAngField;

		private string maxAngField;

		[XmlElement(Order = 0)]
		public CT_AdjPoint2D pos
		{
			get
			{
				return posField;
			}
			set
			{
				posField = value;
			}
		}

		[XmlAttribute(DataType = "token")]
		public string gdRefR
		{
			get
			{
				return gdRefRField;
			}
			set
			{
				gdRefRField = value;
			}
		}

		[XmlAttribute]
		public string minR
		{
			get
			{
				return minRField;
			}
			set
			{
				minRField = value;
			}
		}

		[XmlAttribute]
		public string maxR
		{
			get
			{
				return maxRField;
			}
			set
			{
				maxRField = value;
			}
		}

		[XmlAttribute(DataType = "token")]
		public string gdRefAng
		{
			get
			{
				return gdRefAngField;
			}
			set
			{
				gdRefAngField = value;
			}
		}

		[XmlAttribute]
		public string minAng
		{
			get
			{
				return minAngField;
			}
			set
			{
				minAngField = value;
			}
		}

		[XmlAttribute]
		public string maxAng
		{
			get
			{
				return maxAngField;
			}
			set
			{
				maxAngField = value;
			}
		}
	}
}
