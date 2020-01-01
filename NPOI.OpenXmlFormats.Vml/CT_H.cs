using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using NPOI.OpenXmlFormats.Vml.Spreadsheet;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_H
	{
		private string positionField;

		private string polarField;

		private string mapField;

		private ST_TrueFalse invxField;

		private bool invxFieldSpecified;

		private ST_TrueFalse invyField;

		private bool invyFieldSpecified;

		private ST_TrueFalseBlank switchField;

		private bool switchFieldSpecified;

		private string xrangeField;

		private string yrangeField;

		private string radiusrangeField;

		[XmlAttribute]
		public string position
		{
			get
			{
				return positionField;
			}
			set
			{
				positionField = value;
			}
		}

		[XmlAttribute]
		public string polar
		{
			get
			{
				return polarField;
			}
			set
			{
				polarField = value;
			}
		}

		[XmlAttribute]
		public string map
		{
			get
			{
				return mapField;
			}
			set
			{
				mapField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse invx
		{
			get
			{
				return invxField;
			}
			set
			{
				invxField = value;
			}
		}

		[XmlIgnore]
		public bool invxSpecified
		{
			get
			{
				return invxFieldSpecified;
			}
			set
			{
				invxFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse invy
		{
			get
			{
				return invyField;
			}
			set
			{
				invyField = value;
			}
		}

		[XmlIgnore]
		public bool invySpecified
		{
			get
			{
				return invyFieldSpecified;
			}
			set
			{
				invyFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalseBlank @switch
		{
			get
			{
				return switchField;
			}
			set
			{
				switchField = value;
			}
		}

		[XmlIgnore]
		public bool switchSpecified
		{
			get
			{
				return switchFieldSpecified;
			}
			set
			{
				switchFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string xrange
		{
			get
			{
				return xrangeField;
			}
			set
			{
				xrangeField = value;
			}
		}

		[XmlAttribute]
		public string yrange
		{
			get
			{
				return yrangeField;
			}
			set
			{
				yrangeField = value;
			}
		}

		[XmlAttribute]
		public string radiusrange
		{
			get
			{
				return radiusrangeField;
			}
			set
			{
				radiusrangeField = value;
			}
		}

		public static CT_H Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_H cT_H = new CT_H();
			cT_H.position = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["position"]);
			cT_H.polar = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["polar"]);
			cT_H.map = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["map"]);
			if (node.Attributes["invx"] != null)
			{
				cT_H.invx = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["invx"]);
			}
			if (node.Attributes["invy"] != null)
			{
				cT_H.invy = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["invy"]);
			}
			if (node.Attributes["switch"] != null)
			{
				cT_H.@switch = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalseBlank(node.Attributes["switch"]);
			}
			cT_H.xrange = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["xrange"]);
			cT_H.yrange = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["yrange"]);
			cT_H.radiusrange = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["radiusrange"]);
			return cT_H;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "position", position);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "polar", polar);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "map", map);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "invx", invx);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "invy", invy);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "switch", @switch);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "xrange", xrange);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "yrange", yrange);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "radiusrange", radiusrange);
			sw.Write(">");
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
