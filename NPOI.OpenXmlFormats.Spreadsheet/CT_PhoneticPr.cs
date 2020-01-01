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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", ElementName = "phoneticPr", IsNullable = false)]
	public class CT_PhoneticPr
	{
		private uint fontIdField;

		private ST_PhoneticType typeField;

		private ST_PhoneticAlignment alignmentField;

		[XmlAttribute]
		public uint fontId
		{
			get
			{
				return fontIdField;
			}
			set
			{
				fontIdField = value;
			}
		}

		[DefaultValue(ST_PhoneticType.fullwidthKatakana)]
		[XmlAttribute]
		public ST_PhoneticType type
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

		[XmlAttribute]
		[DefaultValue(ST_PhoneticAlignment.left)]
		public ST_PhoneticAlignment alignment
		{
			get
			{
				return alignmentField;
			}
			set
			{
				alignmentField = value;
			}
		}

		public static CT_PhoneticPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PhoneticPr cT_PhoneticPr = new CT_PhoneticPr();
			cT_PhoneticPr.fontId = XmlHelper.ReadUInt(node.Attributes["fontId"]);
			if (node.Attributes["type"] != null)
			{
				cT_PhoneticPr.type = (ST_PhoneticType)Enum.Parse(typeof(ST_PhoneticType), node.Attributes["type"].Value);
			}
			if (node.Attributes["alignment"] != null)
			{
				cT_PhoneticPr.alignment = (ST_PhoneticAlignment)Enum.Parse(typeof(ST_PhoneticAlignment), node.Attributes["alignment"].Value);
			}
			return cT_PhoneticPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "fontId", (double)fontId, true);
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			if (alignment != ST_PhoneticAlignment.left)
			{
				XmlHelper.WriteAttribute(sw, "alignment", alignment.ToString());
			}
			sw.Write("/>");
		}

		public CT_PhoneticPr()
		{
			typeField = ST_PhoneticType.fullwidthKatakana;
			alignmentField = ST_PhoneticAlignment.left;
		}
	}
}
