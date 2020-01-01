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
	public class CT_Sym
	{
		private string fontField;

		private byte[] charField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string font
		{
			get
			{
				return fontField;
			}
			set
			{
				fontField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] @char
		{
			get
			{
				return charField;
			}
			set
			{
				charField = value;
			}
		}

		public static CT_Sym Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Sym cT_Sym = new CT_Sym();
			cT_Sym.font = XmlHelper.ReadString(node.Attributes["w:font"]);
			cT_Sym.@char = XmlHelper.ReadBytes(node.Attributes["w:char"]);
			return cT_Sym;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:font", font);
			XmlHelper.WriteAttribute(sw, "w:char", @char);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
