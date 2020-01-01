using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Shd
	{
		private ST_Shd valField;

		private string colorField;

		private ST_ThemeColor themeColorField;

		private bool themeColorFieldSpecified;

		private byte[] themeTintField;

		private byte[] themeShadeField;

		private string fillField;

		private ST_ThemeColor themeFillField;

		private bool themeFillFieldSpecified;

		private byte[] themeFillTintField;

		private byte[] themeFillShadeField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Shd val
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ThemeColor themeFill
		{
			get
			{
				return themeFillField;
			}
			set
			{
				themeFillField = value;
			}
		}

		[XmlIgnore]
		public bool themeFillSpecified
		{
			get
			{
				return themeFillFieldSpecified;
			}
			set
			{
				themeFillFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] themeFillTint
		{
			get
			{
				return themeFillTintField;
			}
			set
			{
				themeFillTintField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] themeFillShade
		{
			get
			{
				return themeFillShadeField;
			}
			set
			{
				themeFillShadeField = value;
			}
		}

		public CT_Shd()
		{
			themeColorField = ST_ThemeColor.none;
			themeFillField = ST_ThemeColor.none;
		}

		public static CT_Shd Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Shd cT_Shd = new CT_Shd();
			if (node.Attributes["w:val"] != null)
			{
				cT_Shd.val = (ST_Shd)Enum.Parse(typeof(ST_Shd), node.Attributes["w:val"].Value);
			}
			cT_Shd.color = XmlHelper.ReadString(node.Attributes["w:color"]);
			if (node.Attributes["w:themeColor"] != null)
			{
				cT_Shd.themeColor = (ST_ThemeColor)Enum.Parse(typeof(ST_ThemeColor), node.Attributes["w:themeColor"].Value);
			}
			cT_Shd.themeTint = XmlHelper.ReadBytes(node.Attributes["w:themeTint"]);
			cT_Shd.themeShade = XmlHelper.ReadBytes(node.Attributes["w:themeShade"]);
			cT_Shd.fill = XmlHelper.ReadString(node.Attributes["w:fill"]);
			if (node.Attributes["w:themeFill"] != null)
			{
				cT_Shd.themeFill = (ST_ThemeColor)Enum.Parse(typeof(ST_ThemeColor), node.Attributes["w:themeFill"].Value);
			}
			cT_Shd.themeFillTint = XmlHelper.ReadBytes(node.Attributes["w:themeFillTint"]);
			cT_Shd.themeFillShade = XmlHelper.ReadBytes(node.Attributes["w:themeFillShade"]);
			return cT_Shd;
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
			XmlHelper.WriteAttribute(sw, "w:fill", fill);
			if (themeFill != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:themeFill", themeFill.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:themeFillTint", themeFillTint);
			XmlHelper.WriteAttribute(sw, "w:themeFillShade", themeFillShade);
			sw.Write("/>");
		}
	}
}
