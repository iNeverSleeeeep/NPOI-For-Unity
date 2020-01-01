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
	public class CT_Pane
	{
		private double xSplitField;

		private double ySplitField;

		private string topLeftCellField;

		private ST_Pane activePaneField;

		private ST_PaneState stateField;

		[XmlAttribute]
		[DefaultValue(0.0)]
		public double xSplit
		{
			get
			{
				return xSplitField;
			}
			set
			{
				xSplitField = value;
			}
		}

		[DefaultValue(0.0)]
		[XmlAttribute]
		public double ySplit
		{
			get
			{
				return ySplitField;
			}
			set
			{
				ySplitField = value;
			}
		}

		[XmlAttribute]
		public string topLeftCell
		{
			get
			{
				return topLeftCellField;
			}
			set
			{
				topLeftCellField = value;
			}
		}

		[DefaultValue(ST_Pane.topLeft)]
		[XmlAttribute]
		public ST_Pane activePane
		{
			get
			{
				return activePaneField;
			}
			set
			{
				activePaneField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_PaneState.split)]
		public ST_PaneState state
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

		public CT_Pane()
		{
			xSplitField = 0.0;
			ySplitField = 0.0;
			activePaneField = ST_Pane.topLeft;
			stateField = ST_PaneState.split;
		}

		public static CT_Pane Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Pane cT_Pane = new CT_Pane();
			cT_Pane.xSplit = XmlHelper.ReadDouble(node.Attributes["xSplit"]);
			cT_Pane.ySplit = XmlHelper.ReadDouble(node.Attributes["ySplit"]);
			cT_Pane.topLeftCell = XmlHelper.ReadString(node.Attributes["topLeftCell"]);
			if (node.Attributes["activePane"] != null)
			{
				cT_Pane.activePane = (ST_Pane)Enum.Parse(typeof(ST_Pane), node.Attributes["activePane"].Value);
			}
			if (node.Attributes["state"] != null)
			{
				cT_Pane.state = (ST_PaneState)Enum.Parse(typeof(ST_PaneState), node.Attributes["state"].Value);
			}
			return cT_Pane;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "xSplit", xSplit);
			XmlHelper.WriteAttribute(sw, "ySplit", ySplit);
			XmlHelper.WriteAttribute(sw, "topLeftCell", topLeftCell);
			XmlHelper.WriteAttribute(sw, "activePane", activePane.ToString());
			XmlHelper.WriteAttribute(sw, "state", state.ToString());
			sw.Write("/>");
		}

		public bool IsSetTopLeftCell()
		{
			return topLeftCellField != null;
		}

		public void UnsetTopLeftCell()
		{
			topLeftCellField = null;
		}

		public void UnsetYSplit()
		{
			ySplitField = 0.0;
		}

		public void UnsetXSplit()
		{
			xSplitField = 0.0;
		}

		public bool IsSetXSplit()
		{
			return xSplitField != 0.0;
		}

		public bool IsSetYSplit()
		{
			return ySplitField != 0.0;
		}
	}
}
