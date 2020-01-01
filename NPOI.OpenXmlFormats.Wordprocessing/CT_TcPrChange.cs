using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TcPrChange : CT_TrackChange
	{
		private CT_TcPrInner tcPrField;

		[XmlElement(Order = 0)]
		public CT_TcPrInner tcPr
		{
			get
			{
				return tcPrField;
			}
			set
			{
				tcPrField = value;
			}
		}

		public new static CT_TcPrChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TcPrChange cT_TcPrChange = new CT_TcPrChange();
			cT_TcPrChange.author = XmlHelper.ReadString(node.Attributes["w:author"]);
			cT_TcPrChange.date = XmlHelper.ReadString(node.Attributes["w:date"]);
			cT_TcPrChange.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tcPr")
				{
					cT_TcPrChange.tcPr = CT_TcPrInner.Parse(childNode, namespaceManager);
				}
			}
			return cT_TcPrChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:author", base.author);
			XmlHelper.WriteAttribute(sw, "w:date", base.date);
			XmlHelper.WriteAttribute(sw, "r:id", base.id);
			sw.Write(">");
			if (tcPr != null)
			{
				tcPr.Write(sw, "tcPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
