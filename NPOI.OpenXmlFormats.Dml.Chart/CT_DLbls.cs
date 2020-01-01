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
	public class CT_DLbls
	{
		private List<CT_DLbl> dLblField;

		private string separatorField;

		private CT_NumFmt numFmtField;

		private CT_Boolean showBubbleSizeField;

		private CT_Boolean showCatNameField;

		private CT_Boolean showLeaderLinesField;

		private CT_Boolean showLegendKeyField;

		private CT_Boolean showPercentField;

		private CT_Boolean showSerNameField;

		private CT_Boolean showValField;

		private CT_Boolean deleteField;

		private CT_DLblPos dLblPosField;

		private List<CT_Extension> extLstField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		[XmlElement("dLbl", Order = 0)]
		public List<CT_DLbl> dLbl
		{
			get
			{
				return dLblField;
			}
			set
			{
				dLblField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 9)]
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

		[XmlElement(Order = 16)]
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

		[XmlElement(Order = 17)]
		public CT_Boolean showLeaderLines
		{
			get
			{
				return showLeaderLinesField;
			}
			set
			{
				showLeaderLinesField = value;
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

		public static CT_DLbls Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DLbls cT_DLbls = new CT_DLbls();
			cT_DLbls.dLbl = new List<CT_DLbl>();
			cT_DLbls.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dLblPos")
				{
					cT_DLbls.dLblPos = CT_DLblPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_DLbls.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_DLbls.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_DLbls.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showBubbleSize")
				{
					cT_DLbls.showBubbleSize = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showCatName")
				{
					cT_DLbls.showCatName = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showLegendKey")
				{
					cT_DLbls.showLegendKey = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showPercent")
				{
					cT_DLbls.showPercent = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showSerName")
				{
					cT_DLbls.showSerName = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showVal")
				{
					cT_DLbls.showVal = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "delete")
				{
					cT_DLbls.delete = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "separator")
				{
					cT_DLbls.separator = childNode.InnerText;
				}
				else if (childNode.LocalName == "showLeaderLines")
				{
					cT_DLbls.showLeaderLines = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbl")
				{
					cT_DLbls.dLbl.Add(CT_DLbl.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_DLbls.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_DLbls;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
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
			if (delete != null)
			{
				delete.Write(sw, "delete");
			}
			if (separator != null)
			{
				sw.Write(string.Format("<separator>{0}</separator>", separator));
			}
			if (showLeaderLines != null)
			{
				showLeaderLines.Write(sw, "showLeaderLines");
			}
			if (dLbl != null)
			{
				foreach (CT_DLbl item in dLbl)
				{
					item.Write(sw, "dLbl");
				}
			}
			if (dLblPos != null)
			{
				dLblPos.Write(sw, "dLblPos");
			}
			if (showVal != null)
			{
				showVal.Write(sw, "showVal");
			}
			if (extLst != null)
			{
				foreach (CT_Extension item2 in extLst)
				{
					item2.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
