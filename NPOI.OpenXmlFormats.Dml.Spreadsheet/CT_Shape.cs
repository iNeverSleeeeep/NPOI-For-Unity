using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_Shape
	{
		private CT_ShapeNonVisual nvSpPrField;

		private CT_ShapeProperties spPrField;

		private CT_ShapeStyle styleField;

		private CT_TextBody txBodyField;

		private string macroField;

		private string textlinkField;

		private bool fLocksTextField;

		private bool fPublishedField;

		public CT_ShapeNonVisual nvSpPr
		{
			get
			{
				return nvSpPrField;
			}
			set
			{
				nvSpPrField = value;
			}
		}

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

		public CT_TextBody txBody
		{
			get
			{
				return txBodyField;
			}
			set
			{
				txBodyField = value;
			}
		}

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

		[XmlAttribute]
		public string textlink
		{
			get
			{
				return textlinkField;
			}
			set
			{
				textlinkField = value;
			}
		}

		[XmlAttribute]
		public bool fLocksText
		{
			get
			{
				return fLocksTextField;
			}
			set
			{
				fLocksTextField = value;
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

		public void Set(CT_Shape obj)
		{
			macroField = obj.macro;
			textlinkField = obj.textlink;
			fLocksTextField = obj.fLocksText;
			fPublishedField = obj.fPublished;
			nvSpPrField = obj.nvSpPr;
			spPrField = obj.spPr;
			styleField = obj.style;
			txBodyField = obj.txBody;
		}

		public static CT_Shape Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Shape cT_Shape = new CT_Shape();
			cT_Shape.macro = XmlHelper.ReadString(node.Attributes["macro"]);
			cT_Shape.textlink = XmlHelper.ReadString(node.Attributes["textlink"]);
			cT_Shape.fLocksText = XmlHelper.ReadBool(node.Attributes["fLocksText"]);
			cT_Shape.fPublished = XmlHelper.ReadBool(node.Attributes["fPublished"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "nvSpPr")
				{
					cT_Shape.nvSpPr = CT_ShapeNonVisual.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_Shape.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txBody")
				{
					cT_Shape.txBody = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "style")
				{
					cT_Shape.style = CT_ShapeStyle.Parse(childNode, namespaceManager);
				}
			}
			return cT_Shape;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "macro", macro, true);
			XmlHelper.WriteAttribute(sw, "textlink", textlink, true);
			XmlHelper.WriteAttribute(sw, "fLocksText", fLocksText, false);
			XmlHelper.WriteAttribute(sw, "fPublished", fPublished, false);
			sw.Write(">");
			if (nvSpPr != null)
			{
				nvSpPr.Write(sw, "nvSpPr");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (style != null)
			{
				style.Write(sw, "style");
			}
			if (txBody != null)
			{
				txBody.Write(sw, "txBody");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}

		public CT_ShapeNonVisual AddNewNvSpPr()
		{
			nvSpPrField = new CT_ShapeNonVisual();
			return nvSpPrField;
		}

		public CT_ShapeProperties AddNewSpPr()
		{
			spPrField = new CT_ShapeProperties();
			return spPrField;
		}

		public CT_ShapeStyle AddNewStyle()
		{
			styleField = new CT_ShapeStyle();
			return styleField;
		}

		public CT_TextBody AddNewTxBody()
		{
			txBodyField = new CT_TextBody();
			return txBodyField;
		}
	}
}
