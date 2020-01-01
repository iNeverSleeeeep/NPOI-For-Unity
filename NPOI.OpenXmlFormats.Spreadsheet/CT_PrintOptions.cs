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
	public class CT_PrintOptions
	{
		private bool horizontalCenteredField;

		private bool verticalCenteredField;

		private bool headingsField;

		private bool gridLinesField;

		private bool gridLinesSetField;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool horizontalCentered
		{
			get
			{
				return horizontalCenteredField;
			}
			set
			{
				horizontalCenteredField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool verticalCentered
		{
			get
			{
				return verticalCenteredField;
			}
			set
			{
				verticalCenteredField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool headings
		{
			get
			{
				return headingsField;
			}
			set
			{
				headingsField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool gridLines
		{
			get
			{
				return gridLinesField;
			}
			set
			{
				gridLinesField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool gridLinesSet
		{
			get
			{
				return gridLinesSetField;
			}
			set
			{
				gridLinesSetField = value;
			}
		}

		public static CT_PrintOptions Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PrintOptions cT_PrintOptions = new CT_PrintOptions();
			cT_PrintOptions.horizontalCentered = XmlHelper.ReadBool(node.Attributes["horizontalCentered"]);
			cT_PrintOptions.verticalCentered = XmlHelper.ReadBool(node.Attributes["verticalCentered"]);
			cT_PrintOptions.headings = XmlHelper.ReadBool(node.Attributes["headings"]);
			cT_PrintOptions.gridLines = XmlHelper.ReadBool(node.Attributes["gridLines"]);
			cT_PrintOptions.gridLinesSet = XmlHelper.ReadBool(node.Attributes["gridLinesSet"]);
			return cT_PrintOptions;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "horizontalCentered", horizontalCentered);
			XmlHelper.WriteAttribute(sw, "verticalCentered", verticalCentered);
			XmlHelper.WriteAttribute(sw, "headings", headings);
			XmlHelper.WriteAttribute(sw, "gridLines", gridLines);
			XmlHelper.WriteAttribute(sw, "gridLinesSet", gridLinesSet);
			sw.Write("/>");
		}

		public CT_PrintOptions()
		{
			horizontalCenteredField = false;
			verticalCenteredField = false;
			headingsField = false;
			gridLinesField = false;
			gridLinesSetField = true;
		}
	}
}
