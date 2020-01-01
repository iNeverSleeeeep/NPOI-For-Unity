using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_RPrChange : CT_TrackChange
	{
		private CT_RPrOriginal rPrField;

		[XmlElement(Order = 0)]
		public CT_RPrOriginal rPr
		{
			get
			{
				return rPrField;
			}
			set
			{
				rPrField = value;
			}
		}

		public new static CT_RPrChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RPrChange cT_RPrChange = new CT_RPrChange();
			cT_RPrChange.author = XmlHelper.ReadString(node.Attributes["w:author"]);
			cT_RPrChange.date = XmlHelper.ReadString(node.Attributes["w:date"]);
			cT_RPrChange.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_RPrChange.rPr = CT_RPrOriginal.Parse(childNode, namespaceManager);
				}
			}
			return cT_RPrChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:author", base.author);
			XmlHelper.WriteAttribute(sw, "w:date", base.date);
			XmlHelper.WriteAttribute(sw, "r:id", base.id);
			sw.Write(">");
			if (rPr != null)
			{
				rPr.Write(sw, "w:rPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
