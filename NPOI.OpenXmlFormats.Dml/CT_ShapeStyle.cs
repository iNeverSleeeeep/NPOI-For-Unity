using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_ShapeStyle
	{
		private CT_StyleMatrixReference lnRefField;

		private CT_StyleMatrixReference fillRefField;

		private CT_StyleMatrixReference effectRefField;

		private CT_FontReference fontRefField;

		[XmlElement(Order = 0)]
		public CT_StyleMatrixReference lnRef
		{
			get
			{
				return lnRefField;
			}
			set
			{
				lnRefField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_StyleMatrixReference fillRef
		{
			get
			{
				return fillRefField;
			}
			set
			{
				fillRefField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_StyleMatrixReference effectRef
		{
			get
			{
				return effectRefField;
			}
			set
			{
				effectRefField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_FontReference fontRef
		{
			get
			{
				return fontRefField;
			}
			set
			{
				fontRefField = value;
			}
		}

		public static CT_ShapeStyle Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ShapeStyle cT_ShapeStyle = new CT_ShapeStyle();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "lnRef")
				{
					cT_ShapeStyle.lnRef = CT_StyleMatrixReference.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fillRef")
				{
					cT_ShapeStyle.fillRef = CT_StyleMatrixReference.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effectRef")
				{
					cT_ShapeStyle.effectRef = CT_StyleMatrixReference.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fontRef")
				{
					cT_ShapeStyle.fontRef = CT_FontReference.Parse(childNode, namespaceManager);
				}
			}
			return cT_ShapeStyle;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (lnRef != null)
			{
				lnRef.Write(sw, "lnRef");
			}
			if (fillRef != null)
			{
				fillRef.Write(sw, "fillRef");
			}
			if (effectRef != null)
			{
				effectRef.Write(sw, "effectRef");
			}
			if (fontRef != null)
			{
				fontRef.Write(sw, "fontRef");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_StyleMatrixReference AddNewFillRef()
		{
			fillRefField = new CT_StyleMatrixReference();
			return fillRefField;
		}

		public CT_StyleMatrixReference AddNewLnRef()
		{
			lnRefField = new CT_StyleMatrixReference();
			return lnRefField;
		}

		public CT_FontReference AddNewFontRef()
		{
			fontRefField = new CT_FontReference();
			return fontRefField;
		}

		public CT_StyleMatrixReference AddNewEffectRef()
		{
			effectRefField = new CT_StyleMatrixReference();
			return effectRefField;
		}
	}
}
