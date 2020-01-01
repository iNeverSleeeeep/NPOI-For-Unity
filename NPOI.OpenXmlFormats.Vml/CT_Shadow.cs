using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	public class CT_Shadow
	{
		private string idField;

		private ST_TrueFalse onField;

		private bool onFieldSpecified;

		private ST_ShadowType typeField;

		private bool typeFieldSpecified;

		private ST_TrueFalse obscuredField;

		private bool obscuredFieldSpecified;

		private string colorField;

		private string opacityField;

		private string offsetField;

		private string color2Field;

		private string offset2Field;

		private string originField;

		private string matrixField;

		[XmlAttribute]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse on
		{
			get
			{
				return onField;
			}
			set
			{
				onField = value;
			}
		}

		[XmlIgnore]
		public bool onSpecified
		{
			get
			{
				return onFieldSpecified;
			}
			set
			{
				onFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_ShadowType type
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

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse obscured
		{
			get
			{
				return obscuredField;
			}
			set
			{
				obscuredField = value;
			}
		}

		[XmlIgnore]
		public bool obscuredSpecified
		{
			get
			{
				return obscuredFieldSpecified;
			}
			set
			{
				obscuredFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string color
		{
			get
			{
				return colorField;
			}
			set
			{
				colorField = value;
			}
		}

		[XmlAttribute]
		public string opacity
		{
			get
			{
				return opacityField;
			}
			set
			{
				opacityField = value;
			}
		}

		[XmlAttribute]
		public string offset
		{
			get
			{
				return offsetField;
			}
			set
			{
				offsetField = value;
			}
		}

		[XmlAttribute]
		public string color2
		{
			get
			{
				return color2Field;
			}
			set
			{
				color2Field = value;
			}
		}

		[XmlAttribute]
		public string offset2
		{
			get
			{
				return offset2Field;
			}
			set
			{
				offset2Field = value;
			}
		}

		[XmlAttribute]
		public string origin
		{
			get
			{
				return originField;
			}
			set
			{
				originField = value;
			}
		}

		[XmlAttribute]
		public string matrix
		{
			get
			{
				return matrixField;
			}
			set
			{
				matrixField = value;
			}
		}

		public static CT_Shadow Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Shadow cT_Shadow = new CT_Shadow();
			cT_Shadow.id = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["id"]);
			if (node.Attributes["on"] != null)
			{
				cT_Shadow.on = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["on"]);
			}
			if (node.Attributes["type"] != null)
			{
				cT_Shadow.type = (ST_ShadowType)Enum.Parse(typeof(ST_ShadowType), node.Attributes["type"].Value);
			}
			if (node.Attributes["obscured"] != null)
			{
				cT_Shadow.obscured = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["obscured"]);
			}
			cT_Shadow.color = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["color"]);
			cT_Shadow.opacity = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["opacity"]);
			cT_Shadow.offset = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["offset"]);
			cT_Shadow.color2 = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["color2"]);
			cT_Shadow.offset2 = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["offset2"]);
			cT_Shadow.origin = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["origin"]);
			cT_Shadow.matrix = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["matrix"]);
			return cT_Shadow;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "id", id);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "on", on);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "type", type.ToString());
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "obscured", obscured);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "color", color);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "opacity", opacity);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "offset", offset);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "color2", color2);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "offset2", offset2);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "origin", origin);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "matrix", matrix);
			sw.Write(">");
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
