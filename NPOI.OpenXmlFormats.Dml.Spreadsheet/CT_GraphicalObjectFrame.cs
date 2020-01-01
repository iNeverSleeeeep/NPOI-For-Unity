using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_GraphicalObjectFrame
	{
		private CT_GraphicalObjectFrameNonVisual nvGraphicFramePrField;

		private CT_Transform2D xfrmField;

		private CT_GraphicalObject graphicField;

		private string macroField;

		private bool fPublishedField;

		[XmlElement]
		public CT_GraphicalObjectFrameNonVisual nvGraphicFramePr
		{
			get
			{
				return nvGraphicFramePrField;
			}
			set
			{
				nvGraphicFramePrField = value;
			}
		}

		[XmlElement]
		public CT_Transform2D xfrm
		{
			get
			{
				return xfrmField;
			}
			set
			{
				xfrmField = value;
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

		[DefaultValue(false)]
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

		[XmlElement]
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

		public void Set(CT_GraphicalObjectFrame obj)
		{
			xfrmField = obj.xfrmField;
			graphicField = obj.graphicField;
			nvGraphicFramePrField = obj.nvGraphicFramePrField;
			macroField = obj.macroField;
			fPublishedField = obj.fPublishedField;
		}

		public CT_Transform2D AddNewXfrm()
		{
			xfrmField = new CT_Transform2D();
			return xfrmField;
		}

		public CT_GraphicalObject AddNewGraphic()
		{
			graphicField = new CT_GraphicalObject();
			return graphicField;
		}

		public CT_GraphicalObjectFrameNonVisual AddNewNvGraphicFramePr()
		{
			nvGraphicFramePr = new CT_GraphicalObjectFrameNonVisual();
			return nvGraphicFramePr;
		}

		public static CT_GraphicalObjectFrame Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GraphicalObjectFrame cT_GraphicalObjectFrame = new CT_GraphicalObjectFrame();
			cT_GraphicalObjectFrame.macro = XmlHelper.ReadString(node.Attributes["macro"]);
			cT_GraphicalObjectFrame.fPublished = XmlHelper.ReadBool(node.Attributes["fPublished"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "nvGraphicFramePr")
				{
					cT_GraphicalObjectFrame.nvGraphicFramePr = CT_GraphicalObjectFrameNonVisual.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "xfrm")
				{
					cT_GraphicalObjectFrame.xfrm = CT_Transform2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "graphic")
				{
					cT_GraphicalObjectFrame.graphic = CT_GraphicalObject.Parse(childNode, namespaceManager);
				}
			}
			return cT_GraphicalObjectFrame;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "macro", macro, true);
			XmlHelper.WriteAttribute(sw, "fPublished", fPublished, false);
			sw.Write(">");
			if (nvGraphicFramePr != null)
			{
				nvGraphicFramePr.Write(sw, "nvGraphicFramePr");
			}
			if (xfrm != null)
			{
				xfrm.Write(sw, "xfrm");
			}
			if (graphic != null)
			{
				graphic.Write(sw, "graphic");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}
	}
}
