using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using NPOI.OpenXmlFormats.Vml.Spreadsheet;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	public class CT_Proxy
	{
		private ST_TrueFalseBlank startField;

		private ST_TrueFalseBlank endField;

		private string idrefField;

		private int? connectlocField;

		[DefaultValue(ST_TrueFalseBlank.@false)]
		[XmlAttribute]
		public ST_TrueFalseBlank start
		{
			get
			{
				return startField;
			}
			set
			{
				startField = value;
			}
		}

		[XmlIgnore]
		public bool startSpecified
		{
			get
			{
				return ST_TrueFalseBlank.NONE != startField;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_TrueFalseBlank.@false)]
		public ST_TrueFalseBlank end
		{
			get
			{
				return endField;
			}
			set
			{
				endField = value;
			}
		}

		[XmlIgnore]
		public bool endSpecified
		{
			get
			{
				return ST_TrueFalseBlank.NONE != endField;
			}
		}

		[XmlAttribute]
		public string idref
		{
			get
			{
				return idrefField;
			}
			set
			{
				idrefField = value;
			}
		}

		[XmlIgnore]
		public bool idrefSpecified
		{
			get
			{
				return null != idrefField;
			}
		}

		[XmlAttribute]
		public int connectloc
		{
			get
			{
				return connectlocField.Value;
			}
			set
			{
				connectlocField = value;
			}
		}

		[XmlIgnore]
		public bool connectlocSpecified
		{
			get
			{
				return connectlocField.HasValue;
			}
		}

		public static CT_Proxy Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Proxy cT_Proxy = new CT_Proxy();
			if (node.Attributes["start"] != null)
			{
				cT_Proxy.start = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalseBlank(node.Attributes["start"]);
			}
			if (node.Attributes["end"] != null)
			{
				cT_Proxy.end = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalseBlank(node.Attributes["end"]);
			}
			cT_Proxy.idref = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["idref"]);
			if (node.Attributes["connectloc"] != null)
			{
				cT_Proxy.connectloc = NPOI.OpenXml4Net.Util.XmlHelper.ReadInt(node.Attributes["connectloc"]);
			}
			return cT_Proxy;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<o:{0}", nodeName));
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "start", start);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "end", end);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "idref", idref);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "connectloc", connectloc);
			sw.Write(">");
			sw.Write(string.Format("</o:{0}>", nodeName));
		}
	}
}
