using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Picture
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/picture")]
	[DebuggerStepThrough]
	[XmlRoot("pic", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/picture", IsNullable = false)]
	[DesignerCategory("code")]
	public class CT_Picture
	{
		private CT_PictureNonVisual nvPicPrField = new CT_PictureNonVisual();

		private CT_BlipFillProperties blipFillField = new CT_BlipFillProperties();

		private CT_ShapeProperties spPrField = new CT_ShapeProperties();

		[XmlElement(Order = 0)]
		public CT_PictureNonVisual nvPicPr
		{
			get
			{
				return nvPicPrField;
			}
			set
			{
				nvPicPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_BlipFillProperties blipFill
		{
			get
			{
				return blipFillField;
			}
			set
			{
				blipFillField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		public CT_PictureNonVisual AddNewNvPicPr()
		{
			nvPicPrField = new CT_PictureNonVisual();
			return nvPicPrField;
		}

		public CT_BlipFillProperties AddNewBlipFill()
		{
			blipFillField = new CT_BlipFillProperties();
			return blipFillField;
		}

		public CT_ShapeProperties AddNewSpPr()
		{
			spPrField = new CT_ShapeProperties();
			return spPrField;
		}

		public void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0} xmlns:pic=\"{1}\">", nodeName, "http://schemas.openxmlformats.org/drawingml/2006/picture"));
			if (nvPicPr != null)
			{
				nvPicPr.Write(sw, "pic:nvPicPr");
			}
			if (blipFill != null)
			{
				blipFill.Write(sw, "pic:blipFill");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "pic:spPr");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
