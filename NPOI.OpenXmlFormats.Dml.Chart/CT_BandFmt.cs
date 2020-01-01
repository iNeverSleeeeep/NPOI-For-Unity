using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_BandFmt
	{
		private CT_UnsignedInt idxField;

		private CT_ShapeProperties spPrField;

		[XmlElement(Order = 0)]
		public CT_UnsignedInt idx
		{
			get
			{
				return idxField;
			}
			set
			{
				idxField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		public static CT_BandFmt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BandFmt cT_BandFmt = new CT_BandFmt();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idx")
				{
					cT_BandFmt.idx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_BandFmt.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
			}
			return cT_BandFmt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (idx != null)
			{
				idx.Write(sw, "idx");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
