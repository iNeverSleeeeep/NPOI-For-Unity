using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot("chartSpace", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = false)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_ChartSpace
	{
		private CT_Boolean date1904Field;

		private CT_TextLanguageID langField;

		private CT_Boolean roundedCornersField;

		private CT_Style styleField;

		private CT_ColorMapping clrMapOvrField;

		private CT_PivotSource pivotSourceField;

		private CT_Protection protectionField;

		private CT_Chart chartField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		private CT_ExternalData externalDataField;

		private CT_PrintSettings printSettingsField;

		private CT_RelId userShapesField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_Boolean date1904
		{
			get
			{
				return date1904Field;
			}
			set
			{
				date1904Field = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TextLanguageID lang
		{
			get
			{
				return langField;
			}
			set
			{
				langField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Boolean roundedCorners
		{
			get
			{
				return roundedCornersField;
			}
			set
			{
				roundedCornersField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Style style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_ColorMapping clrMapOvr
		{
			get
			{
				return clrMapOvrField;
			}
			set
			{
				clrMapOvrField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_PivotSource pivotSource
		{
			get
			{
				return pivotSourceField;
			}
			set
			{
				pivotSourceField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Protection protection
		{
			get
			{
				return protectionField;
			}
			set
			{
				protectionField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_Chart chart
		{
			get
			{
				return chartField;
			}
			set
			{
				chartField = value;
			}
		}

		[XmlElement(Order = 8)]
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

		[XmlElement(Order = 9)]
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

		[XmlElement(Order = 10)]
		public CT_ExternalData externalData
		{
			get
			{
				return externalDataField;
			}
			set
			{
				externalDataField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_PrintSettings printSettings
		{
			get
			{
				return printSettingsField;
			}
			set
			{
				printSettingsField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_RelId userShapes
		{
			get
			{
				return userShapesField;
			}
			set
			{
				userShapesField = value;
			}
		}

		[XmlArray(Order = 13)]
		[XmlArrayItem("ext", IsNullable = false)]
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

		public static CT_ChartSpace Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ChartSpace cT_ChartSpace = new CT_ChartSpace();
			cT_ChartSpace.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "date1904")
				{
					cT_ChartSpace.date1904 = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lang")
				{
					cT_ChartSpace.lang = CT_TextLanguageID.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "roundedCorners")
				{
					cT_ChartSpace.roundedCorners = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "style")
				{
					cT_ChartSpace.style = CT_Style.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "clrMapOvr")
				{
					cT_ChartSpace.clrMapOvr = CT_ColorMapping.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pivotSource")
				{
					cT_ChartSpace.pivotSource = CT_PivotSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "protection")
				{
					cT_ChartSpace.protection = CT_Protection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "chart")
				{
					cT_ChartSpace.chart = CT_Chart.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_ChartSpace.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_ChartSpace.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "externalData")
				{
					cT_ChartSpace.externalData = CT_ExternalData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printSettings")
				{
					cT_ChartSpace.printSettings = CT_PrintSettings.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "userShapes")
				{
					cT_ChartSpace.userShapes = CT_RelId.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_ChartSpace.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_ChartSpace;
		}

		internal void Write(Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
				streamWriter.Write("<c:chartSpace");
				streamWriter.Write(" xmlns:c=\"http://schemas.openxmlformats.org/drawingml/2006/chart\" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\"");
				streamWriter.Write(" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\"");
				streamWriter.Write(">");
				if (date1904 != null)
				{
					date1904.Write(streamWriter, "date1904");
				}
				if (lang != null)
				{
					lang.Write(streamWriter, "lang");
				}
				if (roundedCorners != null)
				{
					roundedCorners.Write(streamWriter, "roundedCorners");
				}
				if (style != null)
				{
					style.Write(streamWriter, "style");
				}
				if (clrMapOvr != null)
				{
					clrMapOvr.Write(streamWriter, "clrMapOvr");
				}
				if (pivotSource != null)
				{
					pivotSource.Write(streamWriter, "pivotSource");
				}
				if (protection != null)
				{
					protection.Write(streamWriter, "protection");
				}
				if (chart != null)
				{
					chart.Write(streamWriter, "chart");
				}
				if (spPr != null)
				{
					spPr.Write(streamWriter, "spPr");
				}
				if (txPr != null)
				{
					txPr.Write(streamWriter, "txPr");
				}
				if (externalData != null)
				{
					externalData.Write(streamWriter, "externalData");
				}
				if (printSettings != null)
				{
					printSettings.Write(streamWriter, "printSettings");
				}
				if (userShapes != null)
				{
					userShapes.Write(streamWriter, "userShapes");
				}
				if (extLst != null)
				{
					foreach (CT_Extension item in extLst)
					{
						item.Write(streamWriter, "extLst");
					}
				}
				streamWriter.Write("</c:chartSpace>");
			}
		}

		public CT_Chart AddNewChart()
		{
			chartField = new CT_Chart();
			return chartField;
		}

		public CT_PrintSettings AddNewPrintSettings()
		{
			printSettingsField = new CT_PrintSettings();
			return printSettingsField;
		}
	}
}
