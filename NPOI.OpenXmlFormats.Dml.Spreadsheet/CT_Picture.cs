using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_Picture
	{
		private CT_PictureNonVisual nvPicPrField = new CT_PictureNonVisual();

		private CT_BlipFillProperties blipFillField = new CT_BlipFillProperties();

		private CT_ShapeProperties spPrField = new CT_ShapeProperties();

		private CT_ShapeStyle styleField;

		private string macroField;

		private bool fPublishedField;

		private bool styleSpecifiedField;

		private bool macroSpecifiedField;

		private bool fPublishedSpecifiedField;

		[XmlElement]
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

		[XmlElement]
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

		[XmlElement]
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

		[XmlElement]
		public CT_ShapeStyle style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlIgnore]
		public bool styleSpecified
		{
			get
			{
				return styleSpecifiedField;
			}
			set
			{
				styleSpecifiedField = value;
			}
		}

		[XmlAttribute]
		public string macro
		{
			get
			{
				return macroField;
			}
			set
			{
				macroField = value;
			}
		}

		[XmlIgnore]
		public bool macroSpecified
		{
			get
			{
				return macroSpecifiedField;
			}
			set
			{
				macroSpecifiedField = value;
			}
		}

		[XmlAttribute]
		public bool fPublished
		{
			get
			{
				return fPublishedField;
			}
			set
			{
				fPublishedField = value;
			}
		}

		[XmlIgnore]
		public bool fPublishedSpecified
		{
			get
			{
				return fPublishedSpecifiedField;
			}
			set
			{
				fPublishedSpecifiedField = value;
			}
		}

		public static CT_Picture Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Picture cT_Picture = new CT_Picture();
			cT_Picture.macro = XmlHelper.ReadString(node.Attributes["macro"]);
			cT_Picture.fPublished = XmlHelper.ReadBool(node.Attributes["fPublished"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "nvPicPr")
				{
					cT_Picture.nvPicPr = CT_PictureNonVisual.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "blipFill")
				{
					cT_Picture.blipFill = CT_BlipFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_Picture.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "style")
				{
					cT_Picture.style = CT_ShapeStyle.Parse(childNode, namespaceManager);
				}
			}
			return cT_Picture;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "macro", macro);
			if (fPublished)
			{
				XmlHelper.WriteAttribute(sw, "fPublished", fPublished);
			}
			sw.Write(">");
			if (nvPicPr != null)
			{
				nvPicPr.Write(sw, "nvPicPr");
			}
			if (blipFill != null)
			{
				blipFill.Write(sw, "blipFill");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (style != null)
			{
				style.Write(sw, "style");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
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

		public void Set(CT_Picture pict)
		{
			nvPicPr = pict.nvPicPr;
			spPr = pict.spPr;
			macro = pict.macro;
			macroSpecified = macroSpecified;
			style = pict.style;
			styleSpecified = pict.styleSpecified;
			fPublished = pict.fPublished;
			fPublishedSpecified = pict.fPublishedSpecified;
			blipFill = pict.blipFill;
		}
	}
}
