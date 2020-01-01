using System;
using System.Collections.Generic;
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
	public class CT_Marker
	{
		private CT_MarkerStyle symbolField;

		private CT_MarkerSize sizeField;

		private CT_ShapeProperties spPrField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_MarkerStyle symbol
		{
			get
			{
				return symbolField;
			}
			set
			{
				symbolField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_MarkerSize size
		{
			get
			{
				return sizeField;
			}
			set
			{
				sizeField = value;
			}
		}

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
		public List<CT_Extension> extLst
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

		public static CT_Marker Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Marker cT_Marker = new CT_Marker();
			cT_Marker.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "symbol")
				{
					cT_Marker.symbol = CT_MarkerStyle.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "size")
				{
					cT_Marker.size = CT_MarkerSize.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_Marker.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Marker.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Marker;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (symbol != null)
			{
				symbol.Write(sw, "symbol");
			}
			if (size != null)
			{
				size.Write(sw, "size");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (extLst != null)
			{
				foreach (CT_Extension item in extLst)
				{
					item.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_MarkerStyle AddNewSymbol()
		{
			symbolField = new CT_MarkerStyle();
			return symbolField;
		}
	}
}
