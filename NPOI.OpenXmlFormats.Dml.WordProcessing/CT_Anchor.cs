using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	[XmlRoot("anchor", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = false)]
	public class CT_Anchor
	{
		private CT_Point2D simplePosField;

		private CT_PosH positionHField;

		private CT_PosV positionVField;

		private CT_PositiveSize2D extentField;

		private CT_EffectExtent effectExtentField;

		private object itemField;

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

		private bool simplePos1Field;

		private bool simplePos1FieldSpecified;

		private uint relativeHeightField;

		private bool behindDocField;

		private bool lockedField;

		private bool layoutInCellField;

		private bool hiddenField;

		private bool hiddenFieldSpecified;

		private bool allowOverlapField;

		private CT_WrapNone wrapNoneField;

		private CT_WrapTight wrapTightField;

		private CT_WrapTopBottom wrapTopAndBottomField;

		private CT_WrapSquare wrapSquareField;

		private CT_WrapThrough wrapThroughField;

		[XmlElement(Order = 0)]
		public CT_Point2D simplePos
		{
			get
			{
				return simplePosField;
			}
			set
			{
				simplePosField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_PosH positionH
		{
			get
			{
				return positionHField;
			}
			set
			{
				positionHField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_PosV positionV
		{
			get
			{
				return positionVField;
			}
			set
			{
				positionVField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		public CT_WrapNone wrapNone
		{
			get
			{
				return wrapNoneField;
			}
			set
			{
				wrapNoneField = value;
			}
		}

		public CT_WrapTight wrapTight
		{
			get
			{
				return wrapTightField;
			}
			set
			{
				wrapTightField = value;
			}
		}

		public CT_WrapTopBottom wrapTopAndBottom
		{
			get
			{
				return wrapTopAndBottomField;
			}
			set
			{
				wrapTopAndBottomField = value;
			}
		}

		public CT_WrapSquare wrapSquare
		{
			get
			{
				return wrapSquareField;
			}
			set
			{
				wrapSquareField = value;
			}
		}

		public CT_WrapThrough wrapThrough
		{
			get
			{
				return wrapThroughField;
			}
			set
			{
				wrapThroughField = value;
			}
		}

		[XmlElement(Order = 6)]
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

		[XmlElement(Order = 7)]
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

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 8)]
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

		[XmlAttribute("simplePos")]
		public bool simplePos1
		{
			get
			{
				return simplePos1Field;
			}
			set
			{
				simplePos1Field = value;
			}
		}

		[XmlIgnore]
		public bool simplePos1Specified
		{
			get
			{
				return simplePos1FieldSpecified;
			}
			set
			{
				simplePos1FieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint relativeHeight
		{
			get
			{
				return relativeHeightField;
			}
			set
			{
				relativeHeightField = value;
			}
		}

		[XmlAttribute]
		public bool behindDoc
		{
			get
			{
				return behindDocField;
			}
			set
			{
				behindDocField = value;
			}
		}

		[XmlAttribute]
		public bool locked
		{
			get
			{
				return lockedField;
			}
			set
			{
				lockedField = value;
			}
		}

		[XmlAttribute]
		public bool layoutInCell
		{
			get
			{
				return layoutInCellField;
			}
			set
			{
				layoutInCellField = value;
			}
		}

		[XmlAttribute]
		public bool hidden
		{
			get
			{
				return hiddenField;
			}
			set
			{
				hiddenField = value;
			}
		}

		[XmlIgnore]
		public bool hiddenSpecified
		{
			get
			{
				return hiddenFieldSpecified;
			}
			set
			{
				hiddenFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool allowOverlap
		{
			get
			{
				return allowOverlapField;
			}
			set
			{
				allowOverlapField = value;
			}
		}

		public static CT_Anchor Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Anchor cT_Anchor = new CT_Anchor();
			cT_Anchor.distT = XmlHelper.ReadUInt(node.Attributes["wp:distT"]);
			cT_Anchor.distB = XmlHelper.ReadUInt(node.Attributes["wp:distB"]);
			cT_Anchor.distL = XmlHelper.ReadUInt(node.Attributes["wp:distL"]);
			cT_Anchor.distR = XmlHelper.ReadUInt(node.Attributes["wp:distR"]);
			cT_Anchor.simplePos1 = XmlHelper.ReadBool(node.Attributes["wp:simplePos1"]);
			cT_Anchor.relativeHeight = XmlHelper.ReadUInt(node.Attributes["wp:relativeHeight"]);
			cT_Anchor.behindDoc = XmlHelper.ReadBool(node.Attributes["wp:behindDoc"]);
			cT_Anchor.locked = XmlHelper.ReadBool(node.Attributes["wp:locked"]);
			cT_Anchor.layoutInCell = XmlHelper.ReadBool(node.Attributes["wp:layoutInCell"]);
			cT_Anchor.hidden = XmlHelper.ReadBool(node.Attributes["wp:hidden"]);
			cT_Anchor.allowOverlap = XmlHelper.ReadBool(node.Attributes["wp:allowOverlap"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "simplePos")
				{
					cT_Anchor.simplePos = CT_Point2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "positionH")
				{
					cT_Anchor.positionH = CT_PosH.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "positionV")
				{
					cT_Anchor.positionV = CT_PosV.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extent")
				{
					cT_Anchor.extent = CT_PositiveSize2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effectExtent")
				{
					cT_Anchor.effectExtent = CT_EffectExtent.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "docPr")
				{
					cT_Anchor.docPr = CT_NonVisualDrawingProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cNvGraphicFramePr")
				{
					cT_Anchor.cNvGraphicFramePr = CT_NonVisualGraphicFrameProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "graphic")
				{
					cT_Anchor.graphic = CT_GraphicalObject.Parse(childNode, namespaceManager);
				}
			}
			return cT_Anchor;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<wp:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "wp:distT", distT);
			XmlHelper.WriteAttribute(sw, "wp:distB", distB);
			XmlHelper.WriteAttribute(sw, "wp:distL", distL);
			XmlHelper.WriteAttribute(sw, "wp:distR", distR);
			XmlHelper.WriteAttribute(sw, "wp:simplePos1", simplePos1);
			XmlHelper.WriteAttribute(sw, "wp:relativeHeight", relativeHeight);
			XmlHelper.WriteAttribute(sw, "wp:behindDoc", behindDoc);
			XmlHelper.WriteAttribute(sw, "wp:locked", locked);
			XmlHelper.WriteAttribute(sw, "wp:layoutInCell", layoutInCell);
			XmlHelper.WriteAttribute(sw, "wp:hidden", hidden);
			XmlHelper.WriteAttribute(sw, "wp:allowOverlap", allowOverlap);
			sw.Write(">");
			if (simplePos != null)
			{
				simplePos.Write(sw, "simplePos");
			}
			if (positionH != null)
			{
				positionH.Write(sw, "positionH");
			}
			if (positionV != null)
			{
				positionV.Write(sw, "positionV");
			}
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
	}
}
