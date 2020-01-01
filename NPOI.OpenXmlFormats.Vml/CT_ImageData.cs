using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_ImageData
	{
		private string relidField;

		private string titleField;

		private string oleidField;

		private string movieField;

		private string idField;

		private string srcField;

		private string cropleftField;

		private string croptopField;

		private string croprightField;

		private string cropbottomField;

		private string gainField;

		private string blacklevelField;

		private string gammaField;

		private ST_TrueFalse grayscaleField;

		private bool grayscaleFieldSpecified;

		private ST_TrueFalse bilevelField;

		private bool bilevelFieldSpecified;

		private string chromakeyField;

		private string embosscolorField;

		private string recolortargetField;

		private string id1Field;

		private string pictField;

		private string hrefField;

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
		public string src
		{
			get
			{
				return srcField;
			}
			set
			{
				srcField = value;
			}
		}

		[XmlAttribute]
		public string cropleft
		{
			get
			{
				return cropleftField;
			}
			set
			{
				cropleftField = value;
			}
		}

		[XmlAttribute]
		public string croptop
		{
			get
			{
				return croptopField;
			}
			set
			{
				croptopField = value;
			}
		}

		[XmlAttribute]
		public string cropright
		{
			get
			{
				return croprightField;
			}
			set
			{
				croprightField = value;
			}
		}

		[XmlAttribute]
		public string cropbottom
		{
			get
			{
				return cropbottomField;
			}
			set
			{
				cropbottomField = value;
			}
		}

		[XmlAttribute]
		public string gain
		{
			get
			{
				return gainField;
			}
			set
			{
				gainField = value;
			}
		}

		[XmlAttribute]
		public string blacklevel
		{
			get
			{
				return blacklevelField;
			}
			set
			{
				blacklevelField = value;
			}
		}

		[XmlAttribute]
		public string gamma
		{
			get
			{
				return gammaField;
			}
			set
			{
				gammaField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse grayscale
		{
			get
			{
				return grayscaleField;
			}
			set
			{
				grayscaleField = value;
			}
		}

		[XmlIgnore]
		public bool grayscaleSpecified
		{
			get
			{
				return grayscaleFieldSpecified;
			}
			set
			{
				grayscaleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse bilevel
		{
			get
			{
				return bilevelField;
			}
			set
			{
				bilevelField = value;
			}
		}

		[XmlIgnore]
		public bool bilevelSpecified
		{
			get
			{
				return bilevelFieldSpecified;
			}
			set
			{
				bilevelFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string chromakey
		{
			get
			{
				return chromakeyField;
			}
			set
			{
				chromakeyField = value;
			}
		}

		[XmlAttribute]
		public string embosscolor
		{
			get
			{
				return embosscolorField;
			}
			set
			{
				embosscolorField = value;
			}
		}

		[XmlAttribute]
		public string recolortarget
		{
			get
			{
				return recolortargetField;
			}
			set
			{
				recolortargetField = value;
			}
		}

		[XmlAttribute]
		public string pict
		{
			get
			{
				return pictField;
			}
			set
			{
				pictField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string href
		{
			get
			{
				return hrefField;
			}
			set
			{
				hrefField = value;
			}
		}

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public string relid
		{
			get
			{
				return relidField;
			}
			set
			{
				relidField = value;
			}
		}

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public string title
		{
			get
			{
				return titleField;
			}
			set
			{
				titleField = value;
			}
		}

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public string movie
		{
			get
			{
				return movieField;
			}
			set
			{
				movieField = value;
			}
		}

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public string oleid
		{
			get
			{
				return oleidField;
			}
			set
			{
				oleidField = value;
			}
		}

		public static CT_ImageData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ImageData cT_ImageData = new CT_ImageData();
			cT_ImageData.id = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["id"]);
			cT_ImageData.src = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["src"]);
			cT_ImageData.cropleft = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["cropleft"]);
			cT_ImageData.croptop = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["croptop"]);
			cT_ImageData.cropright = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["cropright"]);
			cT_ImageData.cropbottom = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["cropbottom"]);
			cT_ImageData.gain = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["gain"]);
			cT_ImageData.blacklevel = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["blacklevel"]);
			cT_ImageData.gamma = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["gamma"]);
			if (node.Attributes["grayscale"] != null)
			{
				cT_ImageData.grayscale = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["grayscale"]);
			}
			if (node.Attributes["bilevel"] != null)
			{
				cT_ImageData.bilevel = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["bilevel"]);
			}
			cT_ImageData.chromakey = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["chromakey"]);
			cT_ImageData.embosscolor = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["embosscolor"]);
			cT_ImageData.recolortarget = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["recolortarget"]);
			cT_ImageData.pict = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["pict"]);
			cT_ImageData.href = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["r:href"]);
			cT_ImageData.relid = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["o:relid"]);
			cT_ImageData.title = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["o:title"]);
			cT_ImageData.movie = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["o:movie"]);
			cT_ImageData.oleid = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["o:oleid"]);
			return cT_ImageData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "id", id);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "src", src);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "cropleft", cropleft);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "croptop", croptop);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "cropright", cropright);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "cropbottom", cropbottom);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "gain", gain);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "blacklevel", blacklevel);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "gamma", gamma);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "grayscale", grayscale);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "bilevel", bilevel);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "chromakey", chromakey);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "embosscolor", embosscolor);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "recolortarget", recolortarget);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "pict", pict);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "r:href", href);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "o:relid", relid);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "o:title", title);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "o:movie", movie);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "o:oleid", oleid);
			sw.Write(">");
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
