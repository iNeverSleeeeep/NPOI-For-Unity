using NPOI.OpenXml4Net.Util;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_FilterColumn
	{
		private uint colIdField;

		private bool hiddenButtonField;

		private bool showButtonField;

		[XmlAttribute]
		public uint colId
		{
			get
			{
				return colIdField;
			}
			set
			{
				colIdField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool hiddenButton
		{
			get
			{
				return hiddenButtonField;
			}
			set
			{
				hiddenButtonField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool showButton
		{
			get
			{
				return showButtonField;
			}
			set
			{
				showButtonField = value;
			}
		}

		public CT_FilterColumn()
		{
			hiddenButtonField = false;
			showButtonField = true;
		}

		public static CT_FilterColumn Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FilterColumn cT_FilterColumn = new CT_FilterColumn();
			cT_FilterColumn.colId = XmlHelper.ReadUInt(node.Attributes["colId"]);
			cT_FilterColumn.hiddenButton = XmlHelper.ReadBool(node.Attributes["hiddenButton"]);
			cT_FilterColumn.showButton = XmlHelper.ReadBool(node.Attributes["showButton"]);
			return cT_FilterColumn;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "colId", colId);
			XmlHelper.WriteAttribute(sw, "hiddenButton", hiddenButton);
			XmlHelper.WriteAttribute(sw, "showButton", showButton);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
