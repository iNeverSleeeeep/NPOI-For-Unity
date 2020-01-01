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
	public class CT_Height
	{
		private ulong valField;

		private ST_HeightRule hRuleField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong val
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
		public ST_HeightRule hRule
		{
			get
			{
				return hRuleField;
			}
			set
			{
				hRuleField = value;
			}
		}

		public static CT_Height Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Height cT_Height = new CT_Height();
			cT_Height.val = XmlHelper.ReadULong(node.Attributes["w:val"]);
			if (node.Attributes["w:hRule"] != null)
			{
				cT_Height.hRule = (ST_HeightRule)Enum.Parse(typeof(ST_HeightRule), node.Attributes["w:hRule"].Value);
			}
			return cT_Height;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", (double)val);
			if (hRule != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:hRule", hRule.ToString());
			}
			sw.Write("/>");
		}
	}
}
