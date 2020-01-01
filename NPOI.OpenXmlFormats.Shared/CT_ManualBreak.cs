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
	public class CT_ManualBreak
	{
		private string alnAtField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string alnAt
		{
			get
			{
				return alnAtField;
			}
			set
			{
				alnAtField = value;
			}
		}

		public static CT_ManualBreak Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ManualBreak cT_ManualBreak = new CT_ManualBreak();
			cT_ManualBreak.alnAt = XmlHelper.ReadString(node.Attributes["m:alnAt"]);
			return cT_ManualBreak;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "m:alnAt", alnAt);
			sw.Write(">");
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
