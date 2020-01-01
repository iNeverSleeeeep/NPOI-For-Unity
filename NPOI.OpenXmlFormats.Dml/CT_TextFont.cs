using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TextFont
	{
		private string typefaceField;

		private byte[] panoseField;

		private sbyte pitchFamilyField;

		private sbyte charsetField;

		[XmlAttribute]
		public string typeface
		{
			get
			{
				return typefaceField;
			}
			set
			{
				typefaceField = value;
			}
		}

		[XmlAttribute(DataType = "hexBinary")]
		public byte[] panose
		{
			get
			{
				return panoseField;
			}
			set
			{
				panoseField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(sbyte), "0")]
		public sbyte pitchFamily
		{
			get
			{
				return pitchFamilyField;
			}
			set
			{
				pitchFamilyField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(sbyte), "1")]
		public sbyte charset
		{
			get
			{
				return charsetField;
			}
			set
			{
				charsetField = value;
			}
		}

		public CT_TextFont()
		{
			pitchFamilyField = 0;
			charsetField = 1;
		}

		public static CT_TextFont Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextFont cT_TextFont = new CT_TextFont();
			cT_TextFont.typeface = XmlHelper.ReadString(node.Attributes["typeface"]);
			cT_TextFont.panose = XmlHelper.ReadBytes(node.Attributes["panose"]);
			cT_TextFont.pitchFamily = XmlHelper.ReadSByte(node.Attributes["pitchFamily"]);
			if (node.Attributes["charset"] != null)
			{
				cT_TextFont.charsetField = XmlHelper.ReadSByte(node.Attributes["charset"]);
			}
			return cT_TextFont;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "typeface", typeface);
			XmlHelper.WriteAttribute(sw, "panose", panose);
			XmlHelper.WriteAttribute(sw, "pitchFamily", pitchFamily);
			if (charsetField != 1)
			{
				XmlHelper.WriteAttribute(sw, "charset", charset);
			}
			sw.Write("/>");
		}
	}
}
