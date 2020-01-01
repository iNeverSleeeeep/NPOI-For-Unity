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
	public class CT_Color
	{
		private string valField;

		private ST_ThemeColor themeColorField;

		private bool themeColorFieldSpecified;

		private byte[] themeTintField;

		private byte[] themeShadeField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string val
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

		public CT_Color()
		{
			themeColorField = ST_ThemeColor.none;
		}

		public static CT_Color Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Color cT_Color = new CT_Color();
			cT_Color.val = XmlHelper.ReadString(node.Attributes["w:val"]);
			if (node.Attributes["w:themeColor"] != null)
			{
				cT_Color.themeColor = (ST_ThemeColor)Enum.Parse(typeof(ST_ThemeColor), node.Attributes["w:themeColor"].Value);
			}
			cT_Color.themeTint = XmlHelper.ReadBytes(node.Attributes["w:themeTint"]);
			cT_Color.themeShade = XmlHelper.ReadBytes(node.Attributes["w:themeShade"]);
			return cT_Color;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val, true);
			if (themeColorField != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:themeColor", themeColor.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:themeTint", themeTint);
			XmlHelper.WriteAttribute(sw, "w:themeShade", themeShade);
			sw.Write("/>");
		}
	}
}
