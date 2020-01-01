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
	public class CT_CellAlignment
	{
		private ST_HorizontalAlignment horizontalField;

		private bool horizontalFieldSpecified;

		private ST_VerticalAlignment verticalField = ST_VerticalAlignment.bottom;

		private bool verticalFieldSpecified;

		private long textRotationField;

		private bool textRotationFieldSpecified;

		private bool wrapTextField;

		private bool wrapTextFieldSpecified;

		private long indentField;

		private bool indentFieldSpecified;

		private int relativeIndentField;

		private bool relativeIndentFieldSpecified;

		private bool justifyLastLineField;

		private bool justifyLastLineFieldSpecified;

		private bool shrinkToFitField;

		private bool shrinkToFitFieldSpecified;

		private long readingOrderField;

		private bool readingOrderFieldSpecified;

		[XmlAttribute]
		[DefaultValue(ST_HorizontalAlignment.general)]
		public ST_HorizontalAlignment horizontal
		{
			get
			{
				return horizontalField;
			}
			set
			{
				horizontalField = value;
				horizontalFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool horizontalSpecified
		{
			get
			{
				return horizontalFieldSpecified;
			}
			set
			{
				horizontalFieldSpecified = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_VerticalAlignment.bottom)]
		public ST_VerticalAlignment vertical
		{
			get
			{
				return verticalField;
			}
			set
			{
				verticalField = value;
				verticalFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool verticalSpecified
		{
			get
			{
				return verticalFieldSpecified;
			}
			set
			{
				verticalFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public long textRotation
		{
			get
			{
				return textRotationField;
			}
			set
			{
				textRotationField = value;
				textRotationFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool textRotationSpecified
		{
			get
			{
				return textRotationFieldSpecified;
			}
			set
			{
				textRotationFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool wrapText
		{
			get
			{
				return wrapTextField;
			}
			set
			{
				wrapTextField = value;
				wrapTextSpecified = true;
			}
		}

		[XmlIgnore]
		public bool wrapTextSpecified
		{
			get
			{
				return wrapTextFieldSpecified;
			}
			set
			{
				wrapTextFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public long indent
		{
			get
			{
				return indentField;
			}
			set
			{
				indentField = value;
				indentSpecified = true;
			}
		}

		[XmlIgnore]
		public bool indentSpecified
		{
			get
			{
				return indentFieldSpecified;
			}
			set
			{
				indentFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int relativeIndent
		{
			get
			{
				return relativeIndentField;
			}
			set
			{
				relativeIndentField = value;
				relativeIndentSpecified = true;
			}
		}

		[XmlIgnore]
		public bool relativeIndentSpecified
		{
			get
			{
				return relativeIndentFieldSpecified;
			}
			set
			{
				relativeIndentFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool justifyLastLine
		{
			get
			{
				return justifyLastLineField;
			}
			set
			{
				justifyLastLineField = value;
				justifyLastLineSpecified = true;
			}
		}

		[XmlIgnore]
		public bool justifyLastLineSpecified
		{
			get
			{
				return justifyLastLineFieldSpecified;
			}
			set
			{
				justifyLastLineFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool shrinkToFit
		{
			get
			{
				return shrinkToFitField;
			}
			set
			{
				shrinkToFitField = value;
				shrinkToFitSpecified = true;
			}
		}

		[XmlIgnore]
		public bool shrinkToFitSpecified
		{
			get
			{
				return shrinkToFitFieldSpecified;
			}
			set
			{
				shrinkToFitFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public long readingOrder
		{
			get
			{
				return readingOrderField;
			}
			set
			{
				readingOrderField = value;
				readingOrderSpecified = true;
			}
		}

		[XmlIgnore]
		public bool readingOrderSpecified
		{
			get
			{
				return readingOrderFieldSpecified;
			}
			set
			{
				readingOrderFieldSpecified = value;
			}
		}

		public static CT_CellAlignment Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellAlignment cT_CellAlignment = new CT_CellAlignment();
			if (node.Attributes["horizontal"] != null)
			{
				cT_CellAlignment.horizontal = (ST_HorizontalAlignment)Enum.Parse(typeof(ST_HorizontalAlignment), node.Attributes["horizontal"].Value);
			}
			if (node.Attributes["vertical"] != null)
			{
				cT_CellAlignment.vertical = (ST_VerticalAlignment)Enum.Parse(typeof(ST_VerticalAlignment), node.Attributes["vertical"].Value);
			}
			cT_CellAlignment.textRotation = XmlHelper.ReadLong(node.Attributes["textRotation"]);
			cT_CellAlignment.wrapText = XmlHelper.ReadBool(node.Attributes["wrapText"]);
			cT_CellAlignment.indent = XmlHelper.ReadLong(node.Attributes["indent"]);
			cT_CellAlignment.relativeIndent = XmlHelper.ReadInt(node.Attributes["relativeIndent"]);
			cT_CellAlignment.justifyLastLine = XmlHelper.ReadBool(node.Attributes["justifyLastLine"]);
			cT_CellAlignment.shrinkToFit = XmlHelper.ReadBool(node.Attributes["shrinkToFit"]);
			cT_CellAlignment.readingOrder = XmlHelper.ReadLong(node.Attributes["readingOrder"]);
			return cT_CellAlignment;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (horizontal != 0)
			{
				XmlHelper.WriteAttribute(sw, "horizontal", horizontal.ToString());
			}
			if (vertical != ST_VerticalAlignment.bottom)
			{
				XmlHelper.WriteAttribute(sw, "vertical", vertical.ToString());
			}
			XmlHelper.WriteAttribute(sw, "textRotation", (double)textRotation);
			if (wrapText)
			{
				XmlHelper.WriteAttribute(sw, "wrapText", wrapText);
			}
			XmlHelper.WriteAttribute(sw, "indent", (double)indent);
			XmlHelper.WriteAttribute(sw, "relativeIndent", relativeIndent);
			if (justifyLastLine)
			{
				XmlHelper.WriteAttribute(sw, "justifyLastLine", justifyLastLine);
			}
			if (shrinkToFit)
			{
				XmlHelper.WriteAttribute(sw, "shrinkToFit", shrinkToFit);
			}
			XmlHelper.WriteAttribute(sw, "readingOrder", (double)readingOrder);
			sw.Write("/>");
		}

		public bool IsSetHorizontal()
		{
			return horizontalFieldSpecified;
		}

		public bool IsSetVertical()
		{
			return verticalFieldSpecified;
		}

		internal CT_CellAlignment Copy()
		{
			CT_CellAlignment cT_CellAlignment = new CT_CellAlignment();
			cT_CellAlignment.horizontal = horizontal;
			cT_CellAlignment.vertical = vertical;
			cT_CellAlignment.wrapText = wrapText;
			cT_CellAlignment.shrinkToFit = shrinkToFit;
			cT_CellAlignment.textRotation = textRotation;
			cT_CellAlignment.justifyLastLine = justifyLastLine;
			cT_CellAlignment.readingOrder = readingOrder;
			cT_CellAlignment.relativeIndent = relativeIndent;
			cT_CellAlignment.indent = indent;
			return cT_CellAlignment;
		}
	}
}
