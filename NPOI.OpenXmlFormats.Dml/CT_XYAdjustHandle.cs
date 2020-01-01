using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_XYAdjustHandle
	{
		private CT_AdjPoint2D posField;

		private string gdRefXField;

		private string minXField;

		private string maxXField;

		private string gdRefYField;

		private string minYField;

		private string maxYField;

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
		public string gdRefX
		{
			get
			{
				return gdRefXField;
			}
			set
			{
				gdRefXField = value;
			}
		}

		[XmlAttribute]
		public string minX
		{
			get
			{
				return minXField;
			}
			set
			{
				minXField = value;
			}
		}

		[XmlAttribute]
		public string maxX
		{
			get
			{
				return maxXField;
			}
			set
			{
				maxXField = value;
			}
		}

		[XmlAttribute(DataType = "token")]
		public string gdRefY
		{
			get
			{
				return gdRefYField;
			}
			set
			{
				gdRefYField = value;
			}
		}

		[XmlAttribute]
		public string minY
		{
			get
			{
				return minYField;
			}
			set
			{
				minYField = value;
			}
		}

		[XmlAttribute]
		public string maxY
		{
			get
			{
				return maxYField;
			}
			set
			{
				maxYField = value;
			}
		}
	}
}
