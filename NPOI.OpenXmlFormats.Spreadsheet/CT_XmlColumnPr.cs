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
	public class CT_XmlColumnPr
	{
		private CT_ExtensionList extLstField;

		private uint mapIdField;

		private string xpathField;

		private bool denormalizedField;

		private ST_XmlDataType xmlDataTypeField;

		[XmlElement]
		public CT_ExtensionList extLst
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

		[XmlAttribute]
		public uint mapId
		{
			get
			{
				return mapIdField;
			}
			set
			{
				mapIdField = value;
			}
		}

		[XmlAttribute]
		public string xpath
		{
			get
			{
				return xpathField;
			}
			set
			{
				xpathField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool denormalized
		{
			get
			{
				return denormalizedField;
			}
			set
			{
				denormalizedField = value;
			}
		}

		[XmlAttribute]
		public ST_XmlDataType xmlDataType
		{
			get
			{
				return xmlDataTypeField;
			}
			set
			{
				xmlDataTypeField = value;
			}
		}

		public static CT_XmlColumnPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_XmlColumnPr cT_XmlColumnPr = new CT_XmlColumnPr();
			if (node.Attributes["mapId"] != null)
			{
				cT_XmlColumnPr.mapId = XmlHelper.ReadUInt(node.Attributes["mapId"]);
			}
			cT_XmlColumnPr.xpath = XmlHelper.ReadString(node.Attributes["xpath"]);
			if (node.Attributes["denormalized"] != null)
			{
				cT_XmlColumnPr.denormalized = XmlHelper.ReadBool(node.Attributes["denormalized"]);
			}
			if (node.Attributes["xmlDataType"] != null)
			{
				cT_XmlColumnPr.xmlDataType = (ST_XmlDataType)Enum.Parse(typeof(ST_XmlDataType), node.Attributes["xmlDataType"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_XmlColumnPr.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_XmlColumnPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "mapId", mapId);
			XmlHelper.WriteAttribute(sw, "xpath", xpath);
			XmlHelper.WriteAttribute(sw, "denormalized", denormalized);
			XmlHelper.WriteAttribute(sw, "xmlDataType", xmlDataType.ToString());
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_XmlColumnPr()
		{
			denormalizedField = false;
		}
	}
}
