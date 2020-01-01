using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Picture
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/picture")]
	public class CT_PictureNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField = new CT_NonVisualDrawingProps();

		private CT_NonVisualPictureProperties cNvPicPrField = new CT_NonVisualPictureProperties();

		[XmlElement(Order = 0)]
		public CT_NonVisualDrawingProps cNvPr
		{
			get
			{
				return cNvPrField;
			}
			set
			{
				cNvPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_NonVisualPictureProperties cNvPicPr
		{
			get
			{
				return cNvPicPrField;
			}
			set
			{
				cNvPicPrField = value;
			}
		}

		public CT_NonVisualDrawingProps AddNewCNvPr()
		{
			cNvPrField = new CT_NonVisualDrawingProps();
			return cNvPrField;
		}

		public CT_NonVisualPictureProperties AddNewCNvPicPr()
		{
			cNvPicPrField = new CT_NonVisualPictureProperties();
			return cNvPicPrField;
		}

		internal void Write(StreamWriter sw, string p)
		{
			sw.Write(string.Format("<{0}>", p));
			if (cNvPr != null)
			{
				cNvPr.Write(sw, "cNvPr");
			}
			if (cNvPicPr != null)
			{
				cNvPicPr.Write(sw, "cNvPicPr");
			}
			sw.Write(string.Format("</{0}>", p));
		}
	}
}
