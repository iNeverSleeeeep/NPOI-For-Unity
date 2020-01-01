using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CustomChartsheetView
	{
		private CT_PageMargins pageMarginsField;

		private CT_CsPageSetup pageSetupField;

		private CT_HeaderFooter headerFooterField;

		private string guidField;

		private uint scaleField;

		private ST_SheetState stateField;

		private bool zoomToFitField;

		public CT_PageMargins pageMargins
		{
			get
			{
				return pageMarginsField;
			}
			set
			{
				pageMarginsField = value;
			}
		}

		public CT_CsPageSetup pageSetup
		{
			get
			{
				return pageSetupField;
			}
			set
			{
				pageSetupField = value;
			}
		}

		public CT_HeaderFooter headerFooter
		{
			get
			{
				return headerFooterField;
			}
			set
			{
				headerFooterField = value;
			}
		}

		public string guid
		{
			get
			{
				return guidField;
			}
			set
			{
				guidField = value;
			}
		}

		[DefaultValue(typeof(uint), "100")]
		public uint scale
		{
			get
			{
				return scaleField;
			}
			set
			{
				scaleField = value;
			}
		}

		[DefaultValue(ST_SheetState.visible)]
		public ST_SheetState state
		{
			get
			{
				return stateField;
			}
			set
			{
				stateField = value;
			}
		}

		[DefaultValue(false)]
		public bool zoomToFit
		{
			get
			{
				return zoomToFitField;
			}
			set
			{
				zoomToFitField = value;
			}
		}

		public CT_CustomChartsheetView()
		{
			scaleField = 100u;
			stateField = ST_SheetState.visible;
			zoomToFitField = false;
		}

		public static CT_CustomChartsheetView Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomChartsheetView cT_CustomChartsheetView = new CT_CustomChartsheetView();
			cT_CustomChartsheetView.guid = XmlHelper.ReadString(node.Attributes["guid"]);
			if (node.Attributes["scale"] != null)
			{
				cT_CustomChartsheetView.scale = XmlHelper.ReadUInt(node.Attributes["scale"]);
			}
			if (node.Attributes["state"] != null)
			{
				cT_CustomChartsheetView.state = (ST_SheetState)Enum.Parse(typeof(ST_SheetState), node.Attributes["state"].Value);
			}
			if (node.Attributes["zoomToFit"] != null)
			{
				cT_CustomChartsheetView.zoomToFit = XmlHelper.ReadBool(node.Attributes["zoomToFit"]);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pageMargins")
				{
					cT_CustomChartsheetView.pageMargins = CT_PageMargins.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageSetup")
				{
					cT_CustomChartsheetView.pageSetup = CT_CsPageSetup.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "headerFooter")
				{
					cT_CustomChartsheetView.headerFooter = CT_HeaderFooter.Parse(childNode, namespaceManager);
				}
			}
			return cT_CustomChartsheetView;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "guid", guid);
			if (scale != 100)
			{
				XmlHelper.WriteAttribute(sw, "scale", scale);
			}
			if (state != 0)
			{
				XmlHelper.WriteAttribute(sw, "state", state.ToString());
			}
			if (zoomToFit)
			{
				XmlHelper.WriteAttribute(sw, "zoomToFit", zoomToFit);
			}
			sw.Write(">");
			if (pageMargins != null)
			{
				pageMargins.Write(sw, "pageMargins");
			}
			if (pageSetup != null)
			{
				pageSetup.Write(sw, "pageSetup");
			}
			if (headerFooter != null)
			{
				headerFooter.Write(sw, "headerFooter");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
