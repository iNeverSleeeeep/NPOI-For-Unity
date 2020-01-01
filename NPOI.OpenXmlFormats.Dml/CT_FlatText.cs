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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_FlatText
	{
		private long zField;

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
		public long z
		{
			get
			{
				return zField;
			}
			set
			{
				zField = value;
			}
		}

		public CT_FlatText()
		{
			zField = 0L;
		}

		public static CT_FlatText Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FlatText cT_FlatText = new CT_FlatText();
			cT_FlatText.z = XmlHelper.ReadLong(node.Attributes["z"]);
			return cT_FlatText;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "z", (double)z);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
