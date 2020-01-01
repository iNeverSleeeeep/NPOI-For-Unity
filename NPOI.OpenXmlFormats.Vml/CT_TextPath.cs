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
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	public class CT_TextPath
	{
		private string idField;

		private string styleField;

		private ST_TrueFalse onField;

		private bool onFieldSpecified;

		private ST_TrueFalse fitshapeField;

		private bool fitshapeFieldSpecified;

		private ST_TrueFalse fitpathField;

		private bool fitpathFieldSpecified;

		private ST_TrueFalse trimField;

		private bool trimFieldSpecified;

		private ST_TrueFalse xscaleField;

		private bool xscaleFieldSpecified;

		private string stringField;

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
		public string style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
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
		public ST_TrueFalse fitshape
		{
			get
			{
				return fitshapeField;
			}
			set
			{
				fitshapeField = value;
			}
		}

		[XmlIgnore]
		public bool fitshapeSpecified
		{
			get
			{
				return fitshapeFieldSpecified;
			}
			set
			{
				fitshapeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse fitpath
		{
			get
			{
				return fitpathField;
			}
			set
			{
				fitpathField = value;
			}
		}

		[XmlIgnore]
		public bool fitpathSpecified
		{
			get
			{
				return fitpathFieldSpecified;
			}
			set
			{
				fitpathFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse trim
		{
			get
			{
				return trimField;
			}
			set
			{
				trimField = value;
			}
		}

		[XmlIgnore]
		public bool trimSpecified
		{
			get
			{
				return trimFieldSpecified;
			}
			set
			{
				trimFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse xscale
		{
			get
			{
				return xscaleField;
			}
			set
			{
				xscaleField = value;
			}
		}

		[XmlIgnore]
		public bool xscaleSpecified
		{
			get
			{
				return xscaleFieldSpecified;
			}
			set
			{
				xscaleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string @string
		{
			get
			{
				return stringField;
			}
			set
			{
				stringField = value;
			}
		}

		public static CT_TextPath Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextPath cT_TextPath = new CT_TextPath();
			cT_TextPath.id = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["id"]);
			cT_TextPath.style = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["style"]);
			if (node.Attributes["on"] != null)
			{
				cT_TextPath.on = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["on"]);
			}
			if (node.Attributes["fitshape"] != null)
			{
				cT_TextPath.fitshape = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["fitshape"]);
			}
			if (node.Attributes["fitpath"] != null)
			{
				cT_TextPath.fitpath = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["fitpath"]);
			}
			if (node.Attributes["trim"] != null)
			{
				cT_TextPath.trim = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["trim"]);
			}
			if (node.Attributes["xscale"] != null)
			{
				cT_TextPath.xscale = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["xscale"]);
			}
			cT_TextPath.@string = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["string"]);
			return cT_TextPath;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "id", id);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "style", style);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "on", on);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "fitshape", fitshape);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "fitpath", fitpath);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "trim", trim);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "xscale", xscale);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "string", @string);
			sw.Write(">");
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
