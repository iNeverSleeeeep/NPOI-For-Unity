using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_TableStyleElement
	{
		private ST_TableStyleType typeField;

		private uint sizeField;

		private uint dxfIdField;

		private bool dxfIdFieldSpecified;

		[XmlAttribute]
		public ST_TableStyleType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[DefaultValue(typeof(uint), "1")]
		public uint size
		{
			get
			{
				return sizeField;
			}
			set
			{
				sizeField = value;
			}
		}

		[XmlAttribute]
		public uint dxfId
		{
			get
			{
				return dxfIdField;
			}
			set
			{
				dxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool dxfIdSpecified
		{
			get
			{
				return dxfIdFieldSpecified;
			}
			set
			{
				dxfIdFieldSpecified = value;
			}
		}

		public CT_TableStyleElement()
		{
			sizeField = 1u;
		}

		public static CT_TableStyleElement Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TableStyleElement cT_TableStyleElement = new CT_TableStyleElement();
			if (node.Attributes["type"] != null)
			{
				cT_TableStyleElement.type = (ST_TableStyleType)Enum.Parse(typeof(ST_TableStyleType), node.Attributes["type"].Value);
			}
			cT_TableStyleElement.size = XmlHelper.ReadUInt(node.Attributes["size"]);
			cT_TableStyleElement.dxfId = XmlHelper.ReadUInt(node.Attributes["dxfId"]);
			return cT_TableStyleElement;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			XmlHelper.WriteAttribute(sw, "size", size);
			XmlHelper.WriteAttribute(sw, "dxfId", dxfId);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
