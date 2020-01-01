using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using NPOI.OpenXmlFormats.Vml.Office;
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
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	public class CT_Path
	{
		private string idField;

		private string vField;

		private string limoField;

		private string textboxrectField;

		private ST_TrueFalse fillokField;

		private bool fillokFieldSpecified;

		private ST_TrueFalse strokeokField;

		private bool strokeokFieldSpecified;

		private ST_TrueFalse shadowokField;

		private bool shadowokFieldSpecified;

		private ST_TrueFalse arrowokField;

		private bool arrowokFieldSpecified;

		private ST_TrueFalse gradientshapeokField;

		private bool gradientshapeokFieldSpecified;

		private ST_TrueFalse textpathokField;

		private bool textpathokFieldSpecified;

		private ST_TrueFalse insetpenokField;

		private bool insetpenokFieldSpecified;

		private ST_ConnectType connecttypeField;

		private string connectlocsField;

		private bool connectlocsFieldSpecified;

		private string connectanglesField;

		private bool connectanglesFieldSpecified;

		private ST_TrueFalse extrusionokField;

		private bool extrusionokFieldSpecified;

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

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public ST_ConnectType connecttype
		{
			get
			{
				return connecttypeField;
			}
			set
			{
				connecttypeField = value;
			}
		}

		[XmlAttribute]
		public string v
		{
			get
			{
				return vField;
			}
			set
			{
				vField = value;
			}
		}

		[XmlAttribute]
		public string limo
		{
			get
			{
				return limoField;
			}
			set
			{
				limoField = value;
			}
		}

		[XmlAttribute]
		public string textboxrect
		{
			get
			{
				return textboxrectField;
			}
			set
			{
				textboxrectField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse fillok
		{
			get
			{
				return fillokField;
			}
			set
			{
				fillokField = value;
			}
		}

		[XmlIgnore]
		public bool fillokSpecified
		{
			get
			{
				return fillokFieldSpecified;
			}
			set
			{
				fillokFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse strokeok
		{
			get
			{
				return strokeokField;
			}
			set
			{
				strokeokField = value;
			}
		}

		[XmlIgnore]
		public bool strokeokSpecified
		{
			get
			{
				return strokeokFieldSpecified;
			}
			set
			{
				strokeokFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse shadowok
		{
			get
			{
				return shadowokField;
			}
			set
			{
				shadowokField = value;
			}
		}

		[XmlIgnore]
		public bool shadowokSpecified
		{
			get
			{
				return shadowokFieldSpecified;
			}
			set
			{
				shadowokFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse arrowok
		{
			get
			{
				return arrowokField;
			}
			set
			{
				arrowokField = value;
			}
		}

		[XmlIgnore]
		public bool arrowokSpecified
		{
			get
			{
				return arrowokFieldSpecified;
			}
			set
			{
				arrowokFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse gradientshapeok
		{
			get
			{
				return gradientshapeokField;
			}
			set
			{
				gradientshapeokField = value;
			}
		}

		[XmlIgnore]
		public bool gradientshapeokSpecified
		{
			get
			{
				return gradientshapeokFieldSpecified;
			}
			set
			{
				gradientshapeokFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse textpathok
		{
			get
			{
				return textpathokField;
			}
			set
			{
				textpathokField = value;
			}
		}

		[XmlIgnore]
		public bool textpathokSpecified
		{
			get
			{
				return textpathokFieldSpecified;
			}
			set
			{
				textpathokFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse insetpenok
		{
			get
			{
				return insetpenokField;
			}
			set
			{
				insetpenokField = value;
			}
		}

		[XmlIgnore]
		public bool insetpenokSpecified
		{
			get
			{
				return insetpenokFieldSpecified;
			}
			set
			{
				insetpenokFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string connectlocs
		{
			get
			{
				return connectlocsField;
			}
			set
			{
				connectlocsField = value;
			}
		}

		[XmlIgnore]
		public bool connectlocsSpecified
		{
			get
			{
				return connectlocsFieldSpecified;
			}
			set
			{
				connectlocsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string connectangles
		{
			get
			{
				return connectanglesField;
			}
			set
			{
				connectanglesField = value;
			}
		}

		[XmlIgnore]
		public bool connectanglesSpecified
		{
			get
			{
				return connectanglesFieldSpecified;
			}
			set
			{
				connectanglesFieldSpecified = value;
			}
		}

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public ST_TrueFalse extrusionok
		{
			get
			{
				return extrusionokField;
			}
			set
			{
				extrusionokField = value;
			}
		}

		[XmlIgnore]
		public bool extrusionokSpecified
		{
			get
			{
				return extrusionokFieldSpecified;
			}
			set
			{
				extrusionokFieldSpecified = value;
			}
		}

		public static CT_Path Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Path cT_Path = new CT_Path();
			cT_Path.id = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["id"]);
			if (node.Attributes["o:connecttype"] != null)
			{
				cT_Path.connecttype = (ST_ConnectType)Enum.Parse(typeof(ST_ConnectType), node.Attributes["o:connecttype"].Value);
			}
			cT_Path.v = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["v"]);
			cT_Path.limo = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["limo"]);
			cT_Path.textboxrect = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["textboxrect"]);
			if (node.Attributes["fillok"] != null)
			{
				cT_Path.fillok = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["fillok"]);
			}
			if (node.Attributes["strokeok"] != null)
			{
				cT_Path.strokeok = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["strokeok"]);
			}
			if (node.Attributes["shadowok"] != null)
			{
				cT_Path.shadowok = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["shadowok"]);
			}
			if (node.Attributes["arrowok"] != null)
			{
				cT_Path.arrowok = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["arrowok"]);
			}
			if (node.Attributes["gradientshapeok"] != null)
			{
				cT_Path.gradientshapeok = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["gradientshapeok"]);
			}
			if (node.Attributes["textpathok"] != null)
			{
				cT_Path.textpathok = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["textpathok"]);
			}
			if (node.Attributes["insetpenok"] != null)
			{
				cT_Path.insetpenok = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["insetpenok"]);
			}
			cT_Path.connectlocs = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["connectlocs"]);
			cT_Path.connectangles = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["connectangles"]);
			if (node.Attributes["o:extrusionok"] != null)
			{
				cT_Path.extrusionok = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["o:extrusionok"]);
			}
			return cT_Path;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "id", id);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "o:connecttype", connecttype.ToString());
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "v", v);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "limo", limo);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "textboxrect", textboxrect);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "fillok", fillok);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "strokeok", strokeok);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "shadowok", shadowok);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "arrowok", arrowok);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "gradientshapeok", gradientshapeok);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "textpathok", textpathok);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "insetpenok", insetpenok);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "connectlocs", connectlocs);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "connectangles", connectangles);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "o:extrusionok", extrusionok);
			sw.Write(">");
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
