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
	[XmlRoot("color", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Color
	{
		private bool autoField;

		private uint indexedField;

		private byte[] rgbField;

		private uint themeField;

		private double tintField;

		private bool autoSpecifiedField;

		private bool indexedSpecifiedField;

		private bool rgbSpecifiedField;

		private bool themeSpecifiedField;

		private bool tintSpecifiedField;

		[XmlAttribute]
		public bool auto
		{
			get
			{
				return autoField;
			}
			set
			{
				autoField = value;
				autoSpecified = true;
			}
		}

		[XmlIgnore]
		public bool autoSpecified
		{
			get
			{
				return autoSpecifiedField;
			}
			set
			{
				autoSpecifiedField = value;
			}
		}

		[XmlAttribute]
		public uint indexed
		{
			get
			{
				return indexedField;
			}
			set
			{
				indexedField = value;
				indexedSpecifiedField = true;
			}
		}

		[XmlIgnore]
		public bool indexedSpecified
		{
			get
			{
				return indexedSpecifiedField;
			}
			set
			{
				indexedSpecifiedField = value;
			}
		}

		[XmlAttribute(DataType = "hexBinary")]
		public byte[] rgb
		{
			get
			{
				return rgbField;
			}
			set
			{
				rgbField = value;
				rgbSpecified = true;
			}
		}

		[XmlIgnore]
		public bool rgbSpecified
		{
			get
			{
				return rgbSpecifiedField;
			}
			set
			{
				rgbSpecifiedField = value;
			}
		}

		[XmlAttribute]
		public uint theme
		{
			get
			{
				return themeField;
			}
			set
			{
				themeField = value;
				themeSpecifiedField = true;
			}
		}

		[XmlIgnore]
		public bool themeSpecified
		{
			get
			{
				return themeSpecifiedField;
			}
			set
			{
				themeSpecifiedField = value;
			}
		}

		[DefaultValue(0.0)]
		[XmlAttribute]
		public double tint
		{
			get
			{
				return tintField;
			}
			set
			{
				tintField = value;
				tintSpecified = true;
			}
		}

		[XmlIgnore]
		public bool tintSpecified
		{
			get
			{
				return tintSpecifiedField;
			}
			set
			{
				tintSpecifiedField = value;
			}
		}

		public bool IsSetAuto()
		{
			return autoSpecifiedField;
		}

		public bool IsSetIndexed()
		{
			return indexedSpecified;
		}

		public void SetRgb(byte R, byte G, byte B)
		{
			rgbField = new byte[4];
			rgbField[0] = 0;
			rgbField[1] = R;
			rgbField[2] = G;
			rgbField[3] = B;
			rgbSpecified = true;
		}

		public bool IsSetRgb()
		{
			return rgbSpecified;
		}

		public void SetRgb(byte[] rgb)
		{
			rgbField = new byte[rgb.Length];
			Array.Copy(rgb, rgbField, rgb.Length);
			rgbSpecified = true;
		}

		public byte[] GetRgb()
		{
			return rgbField;
		}

		public bool IsSetTheme()
		{
			return themeSpecified;
		}

		public bool IsSetTint()
		{
			return tintSpecified;
		}

		public static CT_Color Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Color cT_Color = new CT_Color();
			cT_Color.auto = XmlHelper.ReadBool(node.Attributes["auto"]);
			cT_Color.autoSpecified = (node.Attributes["auto"] != null);
			cT_Color.indexed = XmlHelper.ReadUInt(node.Attributes["indexed"]);
			cT_Color.indexedSpecified = (node.Attributes["indexed"] != null);
			cT_Color.rgb = XmlHelper.ReadBytes(node.Attributes["rgb"]);
			cT_Color.rgbSpecified = (node.Attributes["rgb"] != null);
			cT_Color.theme = XmlHelper.ReadUInt(node.Attributes["theme"]);
			cT_Color.themeSpecified = (node.Attributes["theme"] != null);
			cT_Color.tint = XmlHelper.ReadDouble(node.Attributes["tint"]);
			cT_Color.tintSpecified = (node.Attributes["tint"] != null);
			return cT_Color;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "auto", auto, false);
			if (indexedSpecified)
			{
				XmlHelper.WriteAttribute(sw, "indexed", (double)indexed, true);
			}
			if (rgbSpecified)
			{
				XmlHelper.WriteAttribute(sw, "rgb", rgb);
			}
			if (themeSpecified)
			{
				XmlHelper.WriteAttribute(sw, "theme", (double)theme, true);
			}
			if (tintSpecified)
			{
				XmlHelper.WriteAttribute(sw, "tint", tint);
			}
			sw.Write("/>");
		}
	}
}
