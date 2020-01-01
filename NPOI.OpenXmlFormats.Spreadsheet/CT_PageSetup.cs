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
	public class CT_PageSetup
	{
		private uint paperSizeField;

		private uint scaleField;

		private uint firstPageNumberField;

		private uint fitToWidthField;

		private uint fitToHeightField;

		private ST_PageOrder pageOrderField;

		private ST_Orientation orientationField;

		private bool usePrinterDefaultsField;

		private bool blackAndWhiteField;

		private bool draftField;

		private ST_CellComments cellCommentsField;

		private bool useFirstPageNumberField;

		private ST_PrintError errorsField;

		private uint horizontalDpiField;

		private uint verticalDpiField;

		private uint copiesField;

		private string idField;

		[DefaultValue(typeof(uint), "1")]
		[XmlAttribute]
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
		[DefaultValue(typeof(uint), "100")]
		public uint scale
		{
			get
			{
				return scaleField;
			}
			set
			{
				scaleField = value;
			}
		}

		[DefaultValue(typeof(uint), "1")]
		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint fitToWidth
		{
			get
			{
				return fitToWidthField;
			}
			set
			{
				fitToWidthField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint fitToHeight
		{
			get
			{
				return fitToHeightField;
			}
			set
			{
				fitToHeightField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_PageOrder.downThenOver)]
		public ST_PageOrder pageOrder
		{
			get
			{
				return pageOrderField;
			}
			set
			{
				pageOrderField = value;
			}
		}

		[DefaultValue(ST_Orientation.@default)]
		[XmlAttribute]
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

		[XmlAttribute]
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

		[XmlAttribute]
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

		[DefaultValue(ST_CellComments.none)]
		[XmlAttribute]
		public ST_CellComments cellComments
		{
			get
			{
				return cellCommentsField;
			}
			set
			{
				cellCommentsField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(ST_PrintError.displayed)]
		public ST_PrintError errors
		{
			get
			{
				return errorsField;
			}
			set
			{
				errorsField = value;
			}
		}

		[XmlAttribute]
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

		[XmlAttribute]
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
		[XmlAttribute]
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

		public CT_PageSetup()
		{
			paperSizeField = 1u;
			scaleField = 100u;
			firstPageNumberField = 1u;
			fitToWidthField = 1u;
			fitToHeightField = 1u;
			pageOrderField = ST_PageOrder.downThenOver;
			orientationField = ST_Orientation.@default;
			usePrinterDefaultsField = true;
			blackAndWhiteField = false;
			draftField = false;
			cellCommentsField = ST_CellComments.none;
			useFirstPageNumberField = false;
			errorsField = ST_PrintError.displayed;
			horizontalDpiField = 600u;
			verticalDpiField = 600u;
			copiesField = 1u;
		}

		public static CT_PageSetup Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageSetup cT_PageSetup = new CT_PageSetup();
			cT_PageSetup.paperSize = XmlHelper.ReadUInt(node.Attributes["paperSize"]);
			cT_PageSetup.scale = XmlHelper.ReadUInt(node.Attributes["scale"]);
			cT_PageSetup.firstPageNumber = XmlHelper.ReadUInt(node.Attributes["firstPageNumber"]);
			cT_PageSetup.fitToWidth = XmlHelper.ReadUInt(node.Attributes["fitToWidth"]);
			cT_PageSetup.fitToHeight = XmlHelper.ReadUInt(node.Attributes["fitToHeight"]);
			if (node.Attributes["pageOrder"] != null)
			{
				cT_PageSetup.pageOrder = (ST_PageOrder)Enum.Parse(typeof(ST_PageOrder), node.Attributes["pageOrder"].Value);
			}
			if (node.Attributes["orientation"] != null)
			{
				cT_PageSetup.orientation = (ST_Orientation)Enum.Parse(typeof(ST_Orientation), node.Attributes["orientation"].Value);
			}
			if (node.Attributes["usePrinterDefaults"] == null)
			{
				cT_PageSetup.usePrinterDefaults = true;
			}
			else
			{
				cT_PageSetup.usePrinterDefaults = XmlHelper.ReadBool(node.Attributes["usePrinterDefaults"]);
			}
			cT_PageSetup.blackAndWhite = XmlHelper.ReadBool(node.Attributes["blackAndWhite"]);
			cT_PageSetup.draft = XmlHelper.ReadBool(node.Attributes["draft"]);
			if (node.Attributes["cellComments"] != null)
			{
				cT_PageSetup.cellComments = (ST_CellComments)Enum.Parse(typeof(ST_CellComments), node.Attributes["cellComments"].Value);
			}
			cT_PageSetup.useFirstPageNumber = XmlHelper.ReadBool(node.Attributes["useFirstPageNumber"]);
			if (node.Attributes["errors"] != null)
			{
				cT_PageSetup.errors = (ST_PrintError)Enum.Parse(typeof(ST_PrintError), node.Attributes["errors"].Value);
			}
			cT_PageSetup.horizontalDpi = XmlHelper.ReadUInt(node.Attributes["horizontalDpi"]);
			cT_PageSetup.verticalDpi = XmlHelper.ReadUInt(node.Attributes["verticalDpi"]);
			cT_PageSetup.copies = XmlHelper.ReadUInt(node.Attributes["copies"]);
			cT_PageSetup.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			return cT_PageSetup;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "paperSize", paperSize);
			XmlHelper.WriteAttribute(sw, "scale", scale);
			XmlHelper.WriteAttribute(sw, "firstPageNumber", firstPageNumber);
			XmlHelper.WriteAttribute(sw, "fitToWidth", fitToWidth);
			XmlHelper.WriteAttribute(sw, "fitToHeight", (double)fitToHeight, true);
			if (pageOrder != ST_PageOrder.downThenOver)
			{
				XmlHelper.WriteAttribute(sw, "pageOrder", pageOrder.ToString());
			}
			XmlHelper.WriteAttribute(sw, "orientation", orientation.ToString());
			if (!usePrinterDefaults)
			{
				XmlHelper.WriteAttribute(sw, "usePrinterDefaults", usePrinterDefaults);
			}
			XmlHelper.WriteAttribute(sw, "blackAndWhite", blackAndWhite, false);
			XmlHelper.WriteAttribute(sw, "draft", draft, false);
			if (cellComments != ST_CellComments.none)
			{
				XmlHelper.WriteAttribute(sw, "cellComments", cellComments.ToString());
			}
			XmlHelper.WriteAttribute(sw, "useFirstPageNumber", useFirstPageNumber, false);
			if (errors != 0)
			{
				XmlHelper.WriteAttribute(sw, "errors", errors.ToString());
			}
			XmlHelper.WriteAttribute(sw, "horizontalDpi", horizontalDpi);
			XmlHelper.WriteAttribute(sw, "verticalDpi", verticalDpi);
			XmlHelper.WriteAttribute(sw, "copies", copies);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write("/>");
		}
	}
}
