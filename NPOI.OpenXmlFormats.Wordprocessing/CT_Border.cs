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
	public class CT_Border
	{
		private ST_Border valField;

		private string colorField;

		private ST_ThemeColor themeColorField;

		private bool themeColorFieldSpecified;

		private byte[] themeTintField;

		private byte[] themeShadeField;

		private ulong szField;

		private bool szFieldSpecified;

		private ulong spaceField;

		private bool spaceFieldSpecified;

		private ST_OnOff shadowField;

		private bool shadowFieldSpecified;

		private ST_OnOff frameField;

		private bool frameFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Border val
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
		public ulong sz
		{
			get
			{
				return szField;
			}
			set
			{
				szField = value;
			}
		}

		[XmlIgnore]
		public bool szSpecified
		{
			get
			{
				return szFieldSpecified;
			}
			set
			{
				szFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong space
		{
			get
			{
				return spaceField;
			}
			set
			{
				spaceField = value;
			}
		}

		[XmlIgnore]
		public bool spaceSpecified
		{
			get
			{
				return spaceFieldSpecified;
			}
			set
			{
				spaceFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff shadow
		{
			get
			{
				return shadowField;
			}
			set
			{
				shadowField = value;
			}
		}

		[XmlIgnore]
		public bool shadowSpecified
		{
			get
			{
				return shadowFieldSpecified;
			}
			set
			{
				shadowFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff frame
		{
			get
			{
				return frameField;
			}
			set
			{
				frameField = value;
			}
		}

		[XmlIgnore]
		public bool frameSpecified
		{
			get
			{
				return frameFieldSpecified;
			}
			set
			{
				frameFieldSpecified = value;
			}
		}

		public CT_Border()
		{
			themeColor = ST_ThemeColor.none;
		}

		public static CT_Border Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Border cT_Border = new CT_Border();
			if (node.Attributes["w:val"] != null)
			{
				cT_Border.val = (ST_Border)Enum.Parse(typeof(ST_Border), node.Attributes["w:val"].Value);
			}
			cT_Border.color = XmlHelper.ReadString(node.Attributes["w:color"]);
			if (node.Attributes["w:themeColor"] != null)
			{
				cT_Border.themeColor = (ST_ThemeColor)Enum.Parse(typeof(ST_ThemeColor), node.Attributes["w:themeColor"].Value);
			}
			cT_Border.themeTint = XmlHelper.ReadBytes(node.Attributes["w:themeTint"]);
			cT_Border.themeShade = XmlHelper.ReadBytes(node.Attributes["w:themeShade"]);
			cT_Border.sz = XmlHelper.ReadULong(node.Attributes["w:sz"]);
			cT_Border.space = XmlHelper.ReadULong(node.Attributes["w:space"]);
			if (node.Attributes["w:shadow"] != null)
			{
				cT_Border.shadow = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:shadow"].Value);
			}
			if (node.Attributes["w:frame"] != null)
			{
				cT_Border.frame = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:frame"].Value);
			}
			return cT_Border;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			if (themeColor != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:themeColor", themeColor.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:themeTint", themeTint);
			XmlHelper.WriteAttribute(sw, "w:themeShade", themeShade);
			XmlHelper.WriteAttribute(sw, "w:sz", (double)sz);
			XmlHelper.WriteAttribute(sw, "w:space", (double)space, true);
			XmlHelper.WriteAttribute(sw, "w:color", color);
			if (shadow != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:shadow", shadow.ToString());
			}
			if (frame != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:frame", frame.ToString());
			}
			sw.Write("/>");
		}
	}
}
