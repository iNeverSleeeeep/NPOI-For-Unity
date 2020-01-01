using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_BaseStyles
	{
		private CT_ColorScheme clrSchemeField;

		private CT_FontScheme fontSchemeField;

		private CT_StyleMatrix fmtSchemeField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_ColorScheme clrScheme
		{
			get
			{
				return clrSchemeField;
			}
			set
			{
				clrSchemeField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_FontScheme fontScheme
		{
			get
			{
				return fontSchemeField;
			}
			set
			{
				fontSchemeField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_StyleMatrix fmtScheme
		{
			get
			{
				return fmtSchemeField;
			}
			set
			{
				fmtSchemeField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public static CT_BaseStyles Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BaseStyles cT_BaseStyles = new CT_BaseStyles();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "clrScheme")
				{
					cT_BaseStyles.clrScheme = CT_ColorScheme.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fontScheme")
				{
					cT_BaseStyles.fontScheme = CT_FontScheme.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fmtScheme")
				{
					cT_BaseStyles.fmtScheme = CT_StyleMatrix.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_BaseStyles.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_BaseStyles;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (clrScheme != null)
			{
				clrScheme.Write(sw, "clrScheme");
			}
			if (fontScheme != null)
			{
				fontScheme.Write(sw, "fontScheme");
			}
			if (fmtScheme != null)
			{
				fmtScheme.Write(sw, "fmtScheme");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
