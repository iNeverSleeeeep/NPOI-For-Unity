using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = true)]
	public class CT_EffectExtent
	{
		private long lField;

		private long tField;

		private long rField;

		private long bField;

		[XmlAttribute]
		public long l
		{
			get
			{
				return lField;
			}
			set
			{
				lField = value;
			}
		}

		[XmlAttribute]
		public long t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[XmlAttribute]
		public long r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlAttribute]
		public long b
		{
			get
			{
				return bField;
			}
			set
			{
				bField = value;
			}
		}

		public static CT_EffectExtent Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EffectExtent cT_EffectExtent = new CT_EffectExtent();
			cT_EffectExtent.l = XmlHelper.ReadLong(node.Attributes["l"]);
			cT_EffectExtent.t = XmlHelper.ReadLong(node.Attributes["t"]);
			cT_EffectExtent.r = XmlHelper.ReadLong(node.Attributes["r"]);
			cT_EffectExtent.b = XmlHelper.ReadLong(node.Attributes["b"]);
			return cT_EffectExtent;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<wp:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "l", (double)l, true);
			XmlHelper.WriteAttribute(sw, "t", (double)t, true);
			XmlHelper.WriteAttribute(sw, "r", (double)r, true);
			XmlHelper.WriteAttribute(sw, "b", (double)b, true);
			sw.Write(">");
			sw.Write(string.Format("</wp:{0}>", nodeName));
		}
	}
}
