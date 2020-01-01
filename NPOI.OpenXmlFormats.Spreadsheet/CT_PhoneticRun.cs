using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", ElementName = "rPh", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_PhoneticRun
	{
		private string tField;

		private uint sbField;

		private uint ebField;

		[XmlAttribute]
		public string t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[XmlAttribute]
		public uint sb
		{
			get
			{
				return sbField;
			}
			set
			{
				sbField = value;
			}
		}

		[XmlAttribute]
		public uint eb
		{
			get
			{
				return ebField;
			}
			set
			{
				ebField = value;
			}
		}

		public static CT_PhoneticRun Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PhoneticRun cT_PhoneticRun = new CT_PhoneticRun();
			cT_PhoneticRun.t = XmlHelper.ReadString(node.Attributes["t"]);
			cT_PhoneticRun.sb = XmlHelper.ReadUInt(node.Attributes["sb"]);
			cT_PhoneticRun.eb = XmlHelper.ReadUInt(node.Attributes["eb"]);
			return cT_PhoneticRun;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "t", t);
			XmlHelper.WriteAttribute(sw, "sb", sb);
			XmlHelper.WriteAttribute(sw, "eb", eb);
			sw.Write("/>");
		}
	}
}
