using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CsPageSetup
	{
		private uint paperSizeField;

		private uint firstPageNumberField;

		private ST_Orientation orientationField;

		private bool usePrinterDefaultsField;

		private bool blackAndWhiteField;

		private bool draftField;

		private bool useFirstPageNumberField;

		private uint horizontalDpiField;

		private uint verticalDpiField;

		private uint copiesField;

		private string idField;

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

		[DefaultValue(ST_Orientation.@default)]
		public ST_Orientation orientation
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

		[DefaultValue(true)]
		public bool usePrinterDefaults
		{
			get
			{
				return usePrinterDefaultsField;
			}
			set
			{
				usePrinterDefaultsField = value;
			}
		}

		[DefaultValue(false)]
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

		[DefaultValue(typeof(uint), "600")]
		public uint horizontalDpi
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

		[DefaultValue(typeof(uint), "600")]
		public uint verticalDpi
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
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

		public CT_CsPageSetup()
		{
			paperSizeField = 1u;
			firstPageNumberField = 1u;
			orientationField = ST_Orientation.@default;
			usePrinterDefaultsField = true;
			blackAndWhiteField = false;
			draftField = false;
			useFirstPageNumberField = false;
			horizontalDpiField = 600u;
			verticalDpiField = 600u;
			copiesField = 1u;
		}

		public static CT_CsPageSetup Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CsPageSetup cT_CsPageSetup = new CT_CsPageSetup();
			if (node.Attributes["paperSize"] != null)
			{
				cT_CsPageSetup.paperSize = XmlHelper.ReadUInt(node.Attributes["paperSize"]);
			}
			if (node.Attributes["firstPageNumber"] != null)
			{
				cT_CsPageSetup.firstPageNumber = XmlHelper.ReadUInt(node.Attributes["firstPageNumber"]);
			}
			if (node.Attributes["orientation"] != null)
			{
				cT_CsPageSetup.orientation = (ST_Orientation)Enum.Parse(typeof(ST_Orientation), node.Attributes["orientation"].Value);
			}
			if (node.Attributes["usePrinterDefaults"] != null)
			{
				cT_CsPageSetup.usePrinterDefaults = XmlHelper.ReadBool(node.Attributes["usePrinterDefaults"]);
			}
			if (node.Attributes["blackAndWhite"] != null)
			{
				cT_CsPageSetup.blackAndWhite = XmlHelper.ReadBool(node.Attributes["blackAndWhite"]);
			}
			if (node.Attributes["draft"] != null)
			{
				cT_CsPageSetup.draft = XmlHelper.ReadBool(node.Attributes["draft"]);
			}
			if (node.Attributes["useFirstPageNumber"] != null)
			{
				cT_CsPageSetup.useFirstPageNumber = XmlHelper.ReadBool(node.Attributes["useFirstPageNumber"]);
			}
			if (node.Attributes["horizontalDpi"] != null)
			{
				cT_CsPageSetup.horizontalDpi = XmlHelper.ReadUInt(node.Attributes["horizontalDpi"]);
			}
			if (node.Attributes["verticalDpi"] != null)
			{
				cT_CsPageSetup.verticalDpi = XmlHelper.ReadUInt(node.Attributes["verticalDpi"]);
			}
			if (node.Attributes["copies"] != null)
			{
				cT_CsPageSetup.copies = XmlHelper.ReadUInt(node.Attributes["copies"]);
			}
			cT_CsPageSetup.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			return cT_CsPageSetup;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (paperSize != 1)
			{
				XmlHelper.WriteAttribute(sw, "paperSize", paperSize);
			}
			if (paperSize != 1)
			{
				XmlHelper.WriteAttribute(sw, "firstPageNumber", firstPageNumber);
			}
			if (orientation != ST_Orientation.@default)
			{
				XmlHelper.WriteAttribute(sw, "orientation", orientation.ToString());
			}
			if (!usePrinterDefaults)
			{
				XmlHelper.WriteAttribute(sw, "usePrinterDefaults", usePrinterDefaults);
			}
			if (blackAndWhite)
			{
				XmlHelper.WriteAttribute(sw, "blackAndWhite", blackAndWhite);
			}
			if (draft)
			{
				XmlHelper.WriteAttribute(sw, "draft", draft);
			}
			if (useFirstPageNumber)
			{
				XmlHelper.WriteAttribute(sw, "useFirstPageNumber", useFirstPageNumber);
			}
			if (horizontalDpi != 600)
			{
				XmlHelper.WriteAttribute(sw, "horizontalDpi", horizontalDpi);
			}
			if (verticalDpi != 600)
			{
				XmlHelper.WriteAttribute(sw, "verticalDpi", verticalDpi);
			}
			if (copies != 1)
			{
				XmlHelper.WriteAttribute(sw, "copies", copies);
			}
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
