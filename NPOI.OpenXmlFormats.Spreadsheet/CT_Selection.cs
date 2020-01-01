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
	public class CT_Selection
	{
		private ST_Pane paneField;

		private string activeCellField;

		private uint activeCellIdField;

		private string sqrefField;

		[XmlAttribute]
		[DefaultValue(ST_Pane.topLeft)]
		public ST_Pane pane
		{
			get
			{
				return paneField;
			}
			set
			{
				paneField = value;
			}
		}

		[XmlAttribute]
		public string activeCell
		{
			get
			{
				return activeCellField;
			}
			set
			{
				activeCellField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint activeCellId
		{
			get
			{
				return activeCellIdField;
			}
			set
			{
				activeCellIdField = value;
			}
		}

		[XmlAttribute]
		public string sqref
		{
			get
			{
				return sqrefField;
			}
			set
			{
				sqrefField = value;
			}
		}

		public CT_Selection()
		{
			paneField = ST_Pane.topLeft;
			activeCellIdField = 0u;
		}

		public static CT_Selection Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Selection cT_Selection = new CT_Selection();
			if (node.Attributes["pane"] != null)
			{
				cT_Selection.pane = (ST_Pane)Enum.Parse(typeof(ST_Pane), node.Attributes["pane"].Value);
			}
			cT_Selection.activeCell = XmlHelper.ReadString(node.Attributes["activeCell"]);
			cT_Selection.activeCellId = XmlHelper.ReadUInt(node.Attributes["activeCellId"]);
			cT_Selection.sqref = XmlHelper.ReadString(node.Attributes["sqref"]);
			return cT_Selection;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (pane != ST_Pane.topLeft)
			{
				XmlHelper.WriteAttribute(sw, "pane", pane.ToString());
			}
			XmlHelper.WriteAttribute(sw, "activeCell", activeCell);
			XmlHelper.WriteAttribute(sw, "activeCellId", activeCellId);
			XmlHelper.WriteAttribute(sw, "sqref", sqref);
			sw.Write("/>");
		}

		public void SetSqref(string[] array)
		{
			if (array.Length > 0)
			{
				sqrefField = array[0];
			}
		}
	}
}
