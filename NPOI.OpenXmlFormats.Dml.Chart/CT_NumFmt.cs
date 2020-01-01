using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_NumFmt
	{
		private string formatCodeField;

		private bool sourceLinkedField;

		[XmlAttribute]
		public string formatCode
		{
			get
			{
				return formatCodeField;
			}
			set
			{
				formatCodeField = value;
			}
		}

		[XmlAttribute]
		public bool sourceLinked
		{
			get
			{
				return sourceLinkedField;
			}
			set
			{
				sourceLinkedField = value;
			}
		}

		public static CT_NumFmt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumFmt cT_NumFmt = new CT_NumFmt();
			cT_NumFmt.formatCode = XmlHelper.ReadString(node.Attributes["formatCode"]);
			cT_NumFmt.sourceLinked = XmlHelper.ReadBool(node.Attributes["sourceLinked"]);
			return cT_NumFmt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "formatCode", formatCode);
			XmlHelper.WriteAttribute(sw, "sourceLinked", sourceLinked, false);
			sw.Write("/>");
		}
	}
}
