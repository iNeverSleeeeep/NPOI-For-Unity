using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_AltChunkPr
	{
		private CT_OnOff matchSrcField;

		[XmlElement(Order = 0)]
		public CT_OnOff matchSrc
		{
			get
			{
				return matchSrcField;
			}
			set
			{
				matchSrcField = value;
			}
		}

		public static CT_AltChunkPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AltChunkPr cT_AltChunkPr = new CT_AltChunkPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "matchSrc")
				{
					cT_AltChunkPr.matchSrc = CT_OnOff.Parse(childNode, namespaceManager);
				}
			}
			return cT_AltChunkPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (matchSrc != null)
			{
				matchSrc.Write(sw, "matchSrc");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
