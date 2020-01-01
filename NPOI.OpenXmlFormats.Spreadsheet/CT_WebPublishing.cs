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
	public class CT_WebPublishing
	{
		private bool cssField;

		private bool thicketField;

		private bool longFileNamesField;

		private bool vmlField;

		private bool allowPngField;

		private ST_TargetScreenSize targetScreenSizeField;

		private uint dpiField;

		private uint codePageField;

		private bool codePageFieldSpecified;

		[DefaultValue(true)]
		public bool css
		{
			get
			{
				return cssField;
			}
			set
			{
				cssField = value;
			}
		}

		[DefaultValue(true)]
		public bool thicket
		{
			get
			{
				return thicketField;
			}
			set
			{
				thicketField = value;
			}
		}

		[DefaultValue(true)]
		public bool longFileNames
		{
			get
			{
				return longFileNamesField;
			}
			set
			{
				longFileNamesField = value;
			}
		}

		[DefaultValue(false)]
		public bool vml
		{
			get
			{
				return vmlField;
			}
			set
			{
				vmlField = value;
			}
		}

		[DefaultValue(false)]
		public bool allowPng
		{
			get
			{
				return allowPngField;
			}
			set
			{
				allowPngField = value;
			}
		}

		[DefaultValue(ST_TargetScreenSize.Item800x600)]
		public ST_TargetScreenSize targetScreenSize
		{
			get
			{
				return targetScreenSizeField;
			}
			set
			{
				targetScreenSizeField = value;
			}
		}

		[DefaultValue(typeof(uint), "96")]
		public uint dpi
		{
			get
			{
				return dpiField;
			}
			set
			{
				dpiField = value;
			}
		}

		public uint codePage
		{
			get
			{
				return codePageField;
			}
			set
			{
				codePageField = value;
			}
		}

		[XmlIgnore]
		public bool codePageSpecified
		{
			get
			{
				return codePageFieldSpecified;
			}
			set
			{
				codePageFieldSpecified = value;
			}
		}

		public static CT_WebPublishing Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_WebPublishing cT_WebPublishing = new CT_WebPublishing();
			cT_WebPublishing.css = XmlHelper.ReadBool(node.Attributes["css"]);
			cT_WebPublishing.thicket = XmlHelper.ReadBool(node.Attributes["thicket"]);
			cT_WebPublishing.longFileNames = XmlHelper.ReadBool(node.Attributes["longFileNames"]);
			cT_WebPublishing.vml = XmlHelper.ReadBool(node.Attributes["vml"]);
			cT_WebPublishing.allowPng = XmlHelper.ReadBool(node.Attributes["allowPng"]);
			if (node.Attributes["targetScreenSize"] != null)
			{
				cT_WebPublishing.targetScreenSize = (ST_TargetScreenSize)Enum.Parse(typeof(ST_TargetScreenSize), node.Attributes["targetScreenSize"].Value);
			}
			cT_WebPublishing.dpi = XmlHelper.ReadUInt(node.Attributes["dpi"]);
			cT_WebPublishing.codePage = XmlHelper.ReadUInt(node.Attributes["codePage"]);
			return cT_WebPublishing;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "css", css);
			XmlHelper.WriteAttribute(sw, "thicket", thicket);
			XmlHelper.WriteAttribute(sw, "longFileNames", longFileNames);
			XmlHelper.WriteAttribute(sw, "vml", vml);
			XmlHelper.WriteAttribute(sw, "allowPng", allowPng);
			XmlHelper.WriteAttribute(sw, "targetScreenSize", targetScreenSize.ToString());
			XmlHelper.WriteAttribute(sw, "dpi", dpi);
			XmlHelper.WriteAttribute(sw, "codePage", codePage);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_WebPublishing()
		{
			cssField = true;
			thicketField = true;
			longFileNamesField = true;
			vmlField = false;
			allowPngField = false;
			targetScreenSizeField = ST_TargetScreenSize.Item800x600;
			dpiField = 96u;
		}
	}
}
