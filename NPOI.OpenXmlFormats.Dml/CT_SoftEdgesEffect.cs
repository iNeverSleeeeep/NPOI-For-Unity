using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_SoftEdgesEffect
	{
		private long radField;

		[XmlAttribute]
		public long rad
		{
			get
			{
				return radField;
			}
			set
			{
				radField = value;
			}
		}

		public static CT_SoftEdgesEffect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SoftEdgesEffect cT_SoftEdgesEffect = new CT_SoftEdgesEffect();
			cT_SoftEdgesEffect.rad = XmlHelper.ReadLong(node.Attributes["rad"]);
			return cT_SoftEdgesEffect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "rad", (double)rad);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
