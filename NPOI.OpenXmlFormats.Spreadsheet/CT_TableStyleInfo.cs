using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_TableStyleInfo
	{
		private string nameField;

		private bool showFirstColumnField;

		private bool showFirstColumnFieldSpecified;

		private bool showLastColumnField;

		private bool showLastColumnFieldSpecified;

		private bool showRowStripesField;

		private bool showRowStripesFieldSpecified;

		private bool showColumnStripesField;

		private bool showColumnStripesFieldSpecified;

		[XmlAttribute]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlAttribute]
		public bool showFirstColumn
		{
			get
			{
				return showFirstColumnField;
			}
			set
			{
				showFirstColumnField = value;
			}
		}

		[XmlIgnore]
		public bool showFirstColumnSpecified
		{
			get
			{
				return showFirstColumnFieldSpecified;
			}
			set
			{
				showFirstColumnFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool showLastColumn
		{
			get
			{
				return showLastColumnField;
			}
			set
			{
				showLastColumnField = value;
			}
		}

		[XmlIgnore]
		public bool showLastColumnSpecified
		{
			get
			{
				return showLastColumnFieldSpecified;
			}
			set
			{
				showLastColumnFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool showRowStripes
		{
			get
			{
				return showRowStripesField;
			}
			set
			{
				showRowStripesField = value;
			}
		}

		[XmlIgnore]
		public bool showRowStripesSpecified
		{
			get
			{
				return showRowStripesFieldSpecified;
			}
			set
			{
				showRowStripesFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool showColumnStripes
		{
			get
			{
				return showColumnStripesField;
			}
			set
			{
				showColumnStripesField = value;
			}
		}

		[XmlIgnore]
		public bool showColumnStripesSpecified
		{
			get
			{
				return showColumnStripesFieldSpecified;
			}
			set
			{
				showColumnStripesFieldSpecified = value;
			}
		}

		public static CT_TableStyleInfo Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TableStyleInfo cT_TableStyleInfo = new CT_TableStyleInfo();
			cT_TableStyleInfo.name = XmlHelper.ReadString(node.Attributes["name"]);
			if (node.Attributes["showFirstColumn"] != null)
			{
				cT_TableStyleInfo.showFirstColumn = XmlHelper.ReadBool(node.Attributes["showFirstColumn"]);
			}
			if (node.Attributes["showLastColumn"] != null)
			{
				cT_TableStyleInfo.showLastColumn = XmlHelper.ReadBool(node.Attributes["showLastColumn"]);
			}
			if (node.Attributes["showRowStripes"] != null)
			{
				cT_TableStyleInfo.showRowStripes = XmlHelper.ReadBool(node.Attributes["showRowStripes"]);
			}
			if (node.Attributes["showColumnStripes"] != null)
			{
				cT_TableStyleInfo.showColumnStripes = XmlHelper.ReadBool(node.Attributes["showColumnStripes"]);
			}
			return cT_TableStyleInfo;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "showFirstColumn", showFirstColumn);
			XmlHelper.WriteAttribute(sw, "showLastColumn", showLastColumn);
			XmlHelper.WriteAttribute(sw, "showRowStripes", showRowStripes);
			XmlHelper.WriteAttribute(sw, "showColumnStripes", showColumnStripes);
			sw.Write("/>");
		}
	}
}
