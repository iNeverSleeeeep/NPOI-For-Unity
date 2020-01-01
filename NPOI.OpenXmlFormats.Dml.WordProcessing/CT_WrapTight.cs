using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	public class CT_WrapTight
	{
		private CT_WrapPath wrapPolygonField;

		private ST_WrapText wrapTextField;

		private uint distLField;

		private bool distLFieldSpecified;

		private uint distRField;

		private bool distRFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_WrapPath wrapPolygon
		{
			get
			{
				return wrapPolygonField;
			}
			set
			{
				wrapPolygonField = value;
			}
		}

		[XmlAttribute]
		public ST_WrapText wrapText
		{
			get
			{
				return wrapTextField;
			}
			set
			{
				wrapTextField = value;
			}
		}

		[XmlAttribute]
		public uint distL
		{
			get
			{
				return distLField;
			}
			set
			{
				distLField = value;
			}
		}

		[XmlIgnore]
		public bool distLSpecified
		{
			get
			{
				return distLFieldSpecified;
			}
			set
			{
				distLFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint distR
		{
			get
			{
				return distRField;
			}
			set
			{
				distRField = value;
			}
		}

		[XmlIgnore]
		public bool distRSpecified
		{
			get
			{
				return distRFieldSpecified;
			}
			set
			{
				distRFieldSpecified = value;
			}
		}

		public CT_WrapTight()
		{
			wrapPolygonField = new CT_WrapPath();
		}
	}
}
