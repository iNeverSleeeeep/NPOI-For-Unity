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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[DesignerCategory("code")]
	public class CT_DLbl
	{
		private CT_UnsignedInt idxField;

		private string separatorField;

		private CT_NumFmt numFmtField;

		private CT_Boolean showBubbleSizeField;

		private CT_Boolean showCatNameField;

		private CT_Boolean showLegendKeyField;

		private CT_Boolean showPercentField;

		private CT_Boolean showSerNameField;

		private CT_Boolean showValField;

		private CT_Boolean deleteField;

		private CT_DLblPos dLblPosField;

		private CT_Layout layoutField;

		private List<CT_Extension> extLstField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

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
		public CT_DLblPos dLblPos
		{
			get
			{
				return dLblPosField;
			}
			set
			{
				dLblPosField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_NumFmt numFmt
		{
			get
			{
				return numFmtField;
			}
			set
			{
				numFmtField = value;
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

		[XmlElement(Order = 8)]
		public CT_Boolean delete
		{
			get
			{
				return deleteField;
			}
			set
			{
				deleteField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Boolean showBubbleSize
		{
			get
			{
				return showBubbleSizeField;
			}
			set
			{
				showBubbleSizeField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_Boolean showCatName
		{
			get
			{
				return showCatNameField;
			}
			set
			{
				showCatNameField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_Boolean showLegendKey
		{
			get
			{
				return showLegendKeyField;
			}
			set
			{
				showLegendKeyField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_Boolean showPercent
		{
			get
			{
				return showPercentField;
			}
			set
			{
				showPercentField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_Boolean showSerName
		{
			get
			{
				return showSerNameField;
			}
			set
			{
				showSerNameField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_Boolean showVal
		{
			get
			{
				return showValField;
			}
			set
			{
				showValField = value;
			}
		}

		[XmlElement(Order = 15)]
		public string separator
		{
			get
			{
				return separatorField;
			}
			set
			{
				separatorField = value;
			}
		}

		[XmlElement(Order = 18)]
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

		public static CT_DLbl Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DLbl cT_DLbl = new CT_DLbl();
			cT_DLbl.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idx")
				{
					cT_DLbl.idx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "layout")
				{
					cT_DLbl.layout = CT_Layout.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLblPos")
				{
					cT_DLbl.dLblPos = CT_DLblPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_DLbl.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_DLbl.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_DLbl.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "delete")
				{
					cT_DLbl.delete = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showBubbleSize")
				{
					cT_DLbl.showBubbleSize = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showCatName")
				{
					cT_DLbl.showCatName = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showLegendKey")
				{
					cT_DLbl.showLegendKey = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showPercent")
				{
					cT_DLbl.showPercent = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showSerName")
				{
					cT_DLbl.showSerName = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showVal")
				{
					cT_DLbl.showVal = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "separator")
				{
					cT_DLbl.separator = childNode.InnerText;
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_DLbl.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_DLbl;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (idx != null)
			{
				idx.Write(sw, "idx");
			}
			if (layout != null)
			{
				layout.Write(sw, "layout");
			}
			if (dLblPos != null)
			{
				dLblPos.Write(sw, "dLblPos");
			}
			if (numFmt != null)
			{
				numFmt.Write(sw, "numFmt");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (txPr != null)
			{
				txPr.Write(sw, "txPr");
			}
			if (delete != null)
			{
				delete.Write(sw, "delete");
			}
			if (showBubbleSize != null)
			{
				showBubbleSize.Write(sw, "showBubbleSize");
			}
			if (showCatName != null)
			{
				showCatName.Write(sw, "showCatName");
			}
			if (showLegendKey != null)
			{
				showLegendKey.Write(sw, "showLegendKey");
			}
			if (showPercent != null)
			{
				showPercent.Write(sw, "showPercent");
			}
			if (showSerName != null)
			{
				showSerName.Write(sw, "showSerName");
			}
			if (showVal != null)
			{
				showVal.Write(sw, "showVal");
			}
			if (separator != null)
			{
				sw.Write(string.Format("<separator>{0}</separator>", separator));
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
