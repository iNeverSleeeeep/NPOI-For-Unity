using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_Title
	{
		private CT_Tx txField;

		private CT_Layout layoutField;

		private CT_Boolean overlayField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_Tx tx
		{
			get
			{
				return txField;
			}
			set
			{
				txField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Layout layout
		{
			get
			{
				return layoutField;
			}
			set
			{
				layoutField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Boolean overlay
		{
			get
			{
				return overlayField;
			}
			set
			{
				overlayField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
		public CT_TextBody txPr
		{
			get
			{
				return txPrField;
			}
			set
			{
				txPrField = value;
			}
		}

		[XmlElement(Order = 5)]
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

		public static CT_Title Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Title cT_Title = new CT_Title();
			cT_Title.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tx")
				{
					cT_Title.tx = CT_Tx.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "layout")
				{
					cT_Title.layout = CT_Layout.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "overlay")
				{
					cT_Title.overlay = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_Title.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_Title.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Title.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Title;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (tx != null)
			{
				tx.Write(sw, "tx");
			}
			if (layout != null)
			{
				layout.Write(sw, "layout");
			}
			if (overlay != null)
			{
				overlay.Write(sw, "overlay");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (txPr != null)
			{
				txPr.Write(sw, "txPr");
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
	}
}
