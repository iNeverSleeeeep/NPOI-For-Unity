using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Underline
	{
		private ST_Underline valField;

		private bool valFieldSpecified;

		private string colorField;

		private ST_ThemeColor themeColorField;

		private bool themeColorFieldSpecified;

		private byte[] themeTintField;

		private byte[] themeShadeField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Underline val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlIgnore]
		public bool valSpecified
		{
			get
			{
				return valFieldSpecified;
			}
			set
			{
				valFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ThemeColor themeColor
		{
			get
			{
				return themeColorField;
			}
			set
			{
				themeColorField = value;
			}
		}

		[XmlIgnore]
		public bool themeColorSpecified
		{
			get
			{
				return themeColorFieldSpecified;
			}
			set
			{
				themeColorFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] themeTint
		{
			get
			{
				return themeTintField;
			}
			set
			{
				themeTintField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] themeShade
		{
			get
			{
				return themeShadeField;
			}
			set
			{
				themeShadeField = value;
			}
		}

		public CT_Underline()
		{
			themeColorField = ST_ThemeColor.none;
		}

		public static CT_Underline Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Underline cT_Underline = new CT_Underline();
			if (node.Attributes["w:val"] != null)
			{
				cT_Underline.val = (ST_Underline)Enum.Parse(typeof(ST_Underline), node.Attributes["w:val"].Value);
			}
			cT_Underline.color = XmlHelper.ReadString(node.Attributes["w:color"]);
			if (node.Attributes["w:themeColor"] != null)
			{
				cT_Underline.themeColor = (ST_ThemeColor)Enum.Parse(typeof(ST_ThemeColor), node.Attributes["w:themeColor"].Value);
			}
			cT_Underline.themeTint = XmlHelper.ReadBytes(node.Attributes["w:themeTint"]);
			cT_Underline.themeShade = XmlHelper.ReadBytes(node.Attributes["w:themeShade"]);
			return cT_Underline;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			XmlHelper.WriteAttribute(sw, "w:color", color);
			if (themeColor != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:themeColor", themeColor.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:themeTint", themeTint);
			XmlHelper.WriteAttribute(sw, "w:themeShade", themeShade);
			sw.Write("/>");
		}
	}
}
