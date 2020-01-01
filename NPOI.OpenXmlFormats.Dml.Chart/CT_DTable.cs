using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_DTable
	{
		private CT_Boolean showHorzBorderField;

		private CT_Boolean showVertBorderField;

		private CT_Boolean showOutlineField;

		private CT_Boolean showKeysField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_Boolean showHorzBorder
		{
			get
			{
				return showHorzBorderField;
			}
			set
			{
				showHorzBorderField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Boolean showVertBorder
		{
			get
			{
				return showVertBorderField;
			}
			set
			{
				showVertBorderField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Boolean showOutline
		{
			get
			{
				return showOutlineField;
			}
			set
			{
				showOutlineField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Boolean showKeys
		{
			get
			{
				return showKeysField;
			}
			set
			{
				showKeysField = value;
			}
		}

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 5)]
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

		[XmlElement(Order = 6)]
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

		public static CT_DTable Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DTable cT_DTable = new CT_DTable();
			cT_DTable.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "showHorzBorder")
				{
					cT_DTable.showHorzBorder = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showVertBorder")
				{
					cT_DTable.showVertBorder = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showOutline")
				{
					cT_DTable.showOutline = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showKeys")
				{
					cT_DTable.showKeys = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_DTable.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_DTable.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_DTable.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_DTable;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (showHorzBorder != null)
			{
				showHorzBorder.Write(sw, "showHorzBorder");
			}
			if (showVertBorder != null)
			{
				showVertBorder.Write(sw, "showVertBorder");
			}
			if (showOutline != null)
			{
				showOutline.Write(sw, "showOutline");
			}
			if (showKeys != null)
			{
				showKeys.Write(sw, "showKeys");
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
