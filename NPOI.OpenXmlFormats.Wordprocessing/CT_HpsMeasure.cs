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
	public class CT_HpsMeasure
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

		public static CT_HpsMeasure Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_HpsMeasure cT_HpsMeasure = new CT_HpsMeasure();
			cT_HpsMeasure.val = XmlHelper.ReadULong(node.Attributes["w:val"]);
			return cT_HpsMeasure;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", (double)val, true);
			sw.Write("/>");
		}
	}
}
