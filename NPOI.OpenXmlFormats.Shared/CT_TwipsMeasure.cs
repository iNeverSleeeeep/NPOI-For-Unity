using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_TwipsMeasure
	{
		private uint valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public uint val
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
			cT_TwipsMeasure.val = XmlHelper.ReadUInt(node.Attributes["m:val"]);
			return cT_TwipsMeasure;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "m:val", (double)val, true);
			sw.Write("/>");
		}
	}
}
