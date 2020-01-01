using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	public class CT_WrapPath
	{
		private CT_Point2D startField;

		private List<CT_Point2D> lineToField;

		private bool editedField;

		private bool editedFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_Point2D start
		{
			get
			{
				return startField;
			}
			set
			{
				startField = value;
			}
		}

		[XmlElement("lineTo", Order = 1)]
		public List<CT_Point2D> lineTo
		{
			get
			{
				return lineToField;
			}
			set
			{
				lineToField = value;
			}
		}

		[XmlAttribute]
		public bool edited
		{
			get
			{
				return editedField;
			}
			set
			{
				editedField = value;
			}
		}

		[XmlIgnore]
		public bool editedSpecified
		{
			get
			{
				return editedFieldSpecified;
			}
			set
			{
				editedFieldSpecified = value;
			}
		}

		public CT_WrapPath()
		{
			lineToField = new List<CT_Point2D>();
		}
	}
}
