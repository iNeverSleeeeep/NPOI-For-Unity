using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	public class CT_WrapTopBottom
	{
		private CT_EffectExtent effectExtentField;

		private uint distTField;

		private bool distTFieldSpecified;

		private uint distBField;

		private bool distBFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_EffectExtent effectExtent
		{
			get
			{
				return effectExtentField;
			}
			set
			{
				effectExtentField = value;
			}
		}

		[XmlAttribute]
		public uint distT
		{
			get
			{
				return distTField;
			}
			set
			{
				distTField = value;
			}
		}

		[XmlIgnore]
		public bool distTSpecified
		{
			get
			{
				return distTFieldSpecified;
			}
			set
			{
				distTFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint distB
		{
			get
			{
				return distBField;
			}
			set
			{
				distBField = value;
			}
		}

		[XmlIgnore]
		public bool distBSpecified
		{
			get
			{
				return distBFieldSpecified;
			}
			set
			{
				distBFieldSpecified = value;
			}
		}

		public CT_WrapTopBottom()
		{
			effectExtentField = new CT_EffectExtent();
		}
	}
}
