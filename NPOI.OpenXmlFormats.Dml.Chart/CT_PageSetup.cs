using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_PageSetup
	{
		private uint paperSizeField;

		private uint firstPageNumberField;

		private ST_PageSetupOrientation orientationField;

		private bool blackAndWhiteField;

		private bool draftField;

		private bool useFirstPageNumberField;

		private int horizontalDpiField;

		private int verticalDpiField;

		private uint copiesField;

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint paperSize
		{
			get
			{
				return paperSizeField;
			}
			set
			{
				paperSizeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint firstPageNumber
		{
			get
			{
				return firstPageNumberField;
			}
			set
			{
				firstPageNumberField = value;
			}
		}

		[DefaultValue(ST_PageSetupOrientation.@default)]
		[XmlAttribute]
		public ST_PageSetupOrientation orientation
		{
			get
			{
				return orientationField;
			}
			set
			{
				orientationField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool blackAndWhite
		{
			get
			{
				return blackAndWhiteField;
			}
			set
			{
				blackAndWhiteField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool draft
		{
			get
			{
				return draftField;
			}
			set
			{
				draftField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool useFirstPageNumber
		{
			get
			{
				return useFirstPageNumberField;
			}
			set
			{
				useFirstPageNumberField = value;
			}
		}

		[DefaultValue(600)]
		[XmlAttribute]
		public int horizontalDpi
		{
			get
			{
				return horizontalDpiField;
			}
			set
			{
				horizontalDpiField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(600)]
		public int verticalDpi
		{
			get
			{
				return verticalDpiField;
			}
			set
			{
				verticalDpiField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint copies
		{
			get
			{
				return copiesField;
			}
			set
			{
				copiesField = value;
			}
		}

		public CT_PageSetup()
		{
			paperSizeField = 1u;
			firstPageNumberField = 1u;
			orientationField = ST_PageSetupOrientation.@default;
			blackAndWhiteField = false;
			draftField = false;
			useFirstPageNumberField = false;
			horizontalDpiField = 600;
			verticalDpiField = 600;
			copiesField = 1u;
		}

		public static CT_PageSetup Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageSetup cT_PageSetup = new CT_PageSetup();
			if (node.Attributes["paperSize"] != null)
			{
				cT_PageSetup.paperSize = XmlHelper.ReadUInt(node.Attributes["paperSize"]);
			}
			if (node.Attributes["firstPageNumber"] != null)
			{
				cT_PageSetup.firstPageNumber = XmlHelper.ReadUInt(node.Attributes["firstPageNumber"]);
			}
			if (node.Attributes["orientation"] != null)
			{
				cT_PageSetup.orientation = (ST_PageSetupOrientation)Enum.Parse(typeof(ST_PageSetupOrientation), node.Attributes["orientation"].Value);
			}
			if (node.Attributes["blackAndWhite"] != null)
			{
				cT_PageSetup.blackAndWhite = XmlHelper.ReadBool(node.Attributes["blackAndWhite"]);
			}
			if (node.Attributes["draft"] != null)
			{
				cT_PageSetup.draft = XmlHelper.ReadBool(node.Attributes["draft"]);
			}
			if (node.Attributes["useFirstPageNumber"] != null)
			{
				cT_PageSetup.useFirstPageNumber = XmlHelper.ReadBool(node.Attributes["useFirstPageNumber"]);
			}
			if (node.Attributes["horizontalDpi"] != null)
			{
				cT_PageSetup.horizontalDpi = XmlHelper.ReadInt(node.Attributes["horizontalDpi"]);
			}
			if (node.Attributes["verticalDpi"] != null)
			{
				cT_PageSetup.verticalDpi = XmlHelper.ReadInt(node.Attributes["verticalDpi"]);
			}
			if (node.Attributes["copies"] != null)
			{
				cT_PageSetup.copies = XmlHelper.ReadUInt(node.Attributes["copies"]);
			}
			return cT_PageSetup;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "paperSize", paperSize);
			XmlHelper.WriteAttribute(sw, "firstPageNumber", firstPageNumber);
			XmlHelper.WriteAttribute(sw, "orientation", orientation.ToString());
			XmlHelper.WriteAttribute(sw, "blackAndWhite", blackAndWhite);
			XmlHelper.WriteAttribute(sw, "draft", draft);
			XmlHelper.WriteAttribute(sw, "useFirstPageNumber", useFirstPageNumber);
			XmlHelper.WriteAttribute(sw, "horizontalDpi", horizontalDpi);
			XmlHelper.WriteAttribute(sw, "verticalDpi", verticalDpi);
			XmlHelper.WriteAttribute(sw, "copies", copies);
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
