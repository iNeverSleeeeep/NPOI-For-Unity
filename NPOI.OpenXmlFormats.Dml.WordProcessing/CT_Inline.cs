using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlRoot("inline", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	public class CT_Inline
	{
		private CT_PositiveSize2D extentField;

		private CT_EffectExtent effectExtentField;

		private CT_NonVisualDrawingProps docPrField;

		private CT_NonVisualGraphicFrameProperties cNvGraphicFramePrField;

		private CT_GraphicalObject graphicField;

		private uint distTField;

		private bool distTFieldSpecified;

		private uint distBField;

		private bool distBFieldSpecified;

		private uint distLField;

		private bool distLFieldSpecified;

		private uint distRField;

		private bool distRFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_PositiveSize2D extent
		{
			get
			{
				return extentField;
			}
			set
			{
				extentField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
		public CT_NonVisualDrawingProps docPr
		{
			get
			{
				return docPrField;
			}
			set
			{
				docPrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_NonVisualGraphicFrameProperties cNvGraphicFramePr
		{
			get
			{
				return cNvGraphicFramePrField;
			}
			set
			{
				cNvGraphicFramePrField = value;
			}
		}

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 4)]
		public CT_GraphicalObject graphic
		{
			get
			{
				return graphicField;
			}
			set
			{
				graphicField = value;
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

		public static CT_Inline Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Inline cT_Inline = new CT_Inline();
			cT_Inline.distT = XmlHelper.ReadUInt(node.Attributes["wp:distT"]);
			cT_Inline.distB = XmlHelper.ReadUInt(node.Attributes["wp:distB"]);
			cT_Inline.distL = XmlHelper.ReadUInt(node.Attributes["wp:distL"]);
			cT_Inline.distR = XmlHelper.ReadUInt(node.Attributes["wp:distR"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extent")
				{
					cT_Inline.extent = CT_PositiveSize2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effectExtent")
				{
					cT_Inline.effectExtent = CT_EffectExtent.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "docPr")
				{
					cT_Inline.docPr = CT_NonVisualDrawingProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cNvGraphicFramePr")
				{
					cT_Inline.cNvGraphicFramePr = CT_NonVisualGraphicFrameProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "graphic")
				{
					cT_Inline.graphic = CT_GraphicalObject.Parse(childNode, namespaceManager);
				}
			}
			return cT_Inline;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<wp:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "distT", (double)distT, true);
			XmlHelper.WriteAttribute(sw, "distB", (double)distB, true);
			XmlHelper.WriteAttribute(sw, "distL", (double)distL, true);
			XmlHelper.WriteAttribute(sw, "distR", (double)distR, true);
			sw.Write(">");
			if (extent != null)
			{
				extent.Write(sw, "extent");
			}
			if (effectExtent != null)
			{
				effectExtent.Write(sw, "effectExtent");
			}
			if (docPr != null)
			{
				docPr.Write(sw, "docPr");
			}
			if (cNvGraphicFramePr != null)
			{
				cNvGraphicFramePr.Write(sw, "cNvGraphicFramePr");
			}
			if (graphic != null)
			{
				graphic.Write(sw, "graphic");
			}
			sw.Write(string.Format("</wp:{0}>", nodeName));
		}

		public CT_PositiveSize2D AddNewExtent()
		{
			if (extentField == null)
			{
				extentField = new CT_PositiveSize2D();
			}
			return extentField;
		}

		public CT_NonVisualDrawingProps AddNewDocPr()
		{
			if (docPrField == null)
			{
				docPrField = new CT_NonVisualDrawingProps();
			}
			return docPrField;
		}
	}
}
