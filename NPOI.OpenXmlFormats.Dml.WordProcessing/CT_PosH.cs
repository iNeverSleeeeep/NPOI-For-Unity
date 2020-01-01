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
	public class CT_PosH
	{
		private ST_RelFromH relativeFromField;

		private int posOffsetField;

		private ST_AlignH alignField;

		public int posOffset
		{
			get
			{
				return posOffsetField;
			}
			set
			{
				posOffsetField = value;
			}
		}

		public ST_AlignH align
		{
			get
			{
				return alignField;
			}
			set
			{
				alignField = value;
			}
		}

		[XmlAttribute]
		public ST_RelFromH relativeFrom
		{
			get
			{
				return relativeFromField;
			}
			set
			{
				relativeFromField = value;
			}
		}

		public static CT_PosH Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PosH cT_PosH = new CT_PosH();
			cT_PosH.posOffset = XmlHelper.ReadInt(node.Attributes["wp:posOffset"]);
			if (node.Attributes["wp:align"] != null)
			{
				cT_PosH.align = (ST_AlignH)Enum.Parse(typeof(ST_AlignH), node.Attributes["wp:align"].Value);
			}
			if (node.Attributes["wp:relativeFrom"] != null)
			{
				cT_PosH.relativeFrom = (ST_RelFromH)Enum.Parse(typeof(ST_RelFromH), node.Attributes["wp:relativeFrom"].Value);
			}
			return cT_PosH;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<wp:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "wp:posOffset", posOffset);
			XmlHelper.WriteAttribute(sw, "wp:align", align.ToString());
			XmlHelper.WriteAttribute(sw, "wp:relativeFrom", relativeFrom.ToString());
			sw.Write(">");
			sw.Write(string.Format("</wp:{0}>", nodeName));
		}
	}
}
