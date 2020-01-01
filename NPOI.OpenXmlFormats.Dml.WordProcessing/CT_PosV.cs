using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	public class CT_PosV
	{
		private ST_RelFromV relativeFromField;

		private int posOffsetField;

		private ST_AlignV alignField;

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

		public ST_AlignV align
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
		public ST_RelFromV relativeFrom
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

		public static CT_PosV Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PosV cT_PosV = new CT_PosV();
			cT_PosV.posOffset = XmlHelper.ReadInt(node.Attributes["wp:posOffset"]);
			if (node.Attributes["wp:align"] != null)
			{
				cT_PosV.align = (ST_AlignV)Enum.Parse(typeof(ST_AlignV), node.Attributes["wp:align"].Value);
			}
			if (node.Attributes["wp:relativeFrom"] != null)
			{
				cT_PosV.relativeFrom = (ST_RelFromV)Enum.Parse(typeof(ST_RelFromV), node.Attributes["wp:relativeFrom"].Value);
			}
			return cT_PosV;
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
