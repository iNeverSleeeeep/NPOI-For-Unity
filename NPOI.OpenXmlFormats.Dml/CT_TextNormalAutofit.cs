using NPOI.OpenXml4Net.Util;
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
	public class CT_TextNormalAutofit
	{
		private int fontScaleField;

		private int lnSpcReductionField;

		[DefaultValue(100000)]
		[XmlAttribute]
		public int fontScale
		{
			get
			{
				return fontScaleField;
			}
			set
			{
				fontScaleField = value;
			}
		}

		[DefaultValue(0)]
		[XmlAttribute]
		public int lnSpcReduction
		{
			get
			{
				return lnSpcReductionField;
			}
			set
			{
				lnSpcReductionField = value;
			}
		}

		public CT_TextNormalAutofit()
		{
			fontScaleField = 100000;
			lnSpcReductionField = 0;
		}

		public static CT_TextNormalAutofit Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextNormalAutofit cT_TextNormalAutofit = new CT_TextNormalAutofit();
			cT_TextNormalAutofit.fontScale = XmlHelper.ReadInt(node.Attributes["fontScale"]);
			cT_TextNormalAutofit.lnSpcReduction = XmlHelper.ReadInt(node.Attributes["lnSpcReduction"]);
			return cT_TextNormalAutofit;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "fontScale", fontScale);
			XmlHelper.WriteAttribute(sw, "lnSpcReduction", lnSpcReduction);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
