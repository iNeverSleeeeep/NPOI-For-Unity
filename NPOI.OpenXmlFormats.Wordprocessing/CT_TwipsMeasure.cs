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
	public class CT_TwipsMeasure
	{
		private ulong valField;

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

		public static CT_TwipsMeasure Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TwipsMeasure cT_TwipsMeasure = new CT_TwipsMeasure();
			cT_TwipsMeasure.val = XmlHelper.ReadULong(node.Attributes["w:val"]);
			return cT_TwipsMeasure;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", (double)val);
			sw.Write("/>");
		}
	}
}
