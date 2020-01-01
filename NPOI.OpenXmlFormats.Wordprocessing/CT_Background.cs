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
	public class CT_Background : CT_PictureBase
	{
		private string colorField;

		private ST_ThemeColor themeColorField;

		private bool themeColorFieldSpecified;

		private byte[] themeTintField;

		private byte[] themeShadeField;

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

		public CT_Background()
		{
			themeColorField = ST_ThemeColor.none;
		}

		public static CT_Background Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Background cT_Background = new CT_Background();
			cT_Background.color = XmlHelper.ReadString(node.Attributes["w:color"]);
			if (node.Attributes["w:themeColor"] != null)
			{
				cT_Background.themeColor = (ST_ThemeColor)Enum.Parse(typeof(ST_ThemeColor), node.Attributes["w:themeColor"].Value);
			}
			cT_Background.themeTint = XmlHelper.ReadBytes(node.Attributes["w:themeTint"]);
			cT_Background.themeShade = XmlHelper.ReadBytes(node.Attributes["w:themeShade"]);
			return cT_Background;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			if (themeColorField != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:themeColor", themeColor.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:themeTint", themeTint);
			XmlHelper.WriteAttribute(sw, "w:themeShade", themeShade);
			XmlHelper.WriteAttribute(sw, "w:color", color);
			sw.Write("/>");
		}
	}
}
