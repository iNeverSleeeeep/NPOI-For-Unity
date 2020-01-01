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
	public class CT_TrPrChange : CT_TrackChange
	{
		private CT_TrPrBase trPrField;

		[XmlElement(Order = 0)]
		public CT_TrPrBase trPr
		{
			get
			{
				return trPrField;
			}
			set
			{
				trPrField = value;
			}
		}

		public new static CT_TrPrChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TrPrChange cT_TrPrChange = new CT_TrPrChange();
			cT_TrPrChange.author = XmlHelper.ReadString(node.Attributes["w:author"]);
			cT_TrPrChange.date = XmlHelper.ReadString(node.Attributes["w:date"]);
			cT_TrPrChange.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "trPr")
				{
					cT_TrPrChange.trPr = CT_TrPrBase.Parse(childNode, namespaceManager);
				}
			}
			return cT_TrPrChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:author", base.author);
			XmlHelper.WriteAttribute(sw, "w:date", base.date);
			XmlHelper.WriteAttribute(sw, "r:id", base.id);
			sw.Write(">");
			if (trPr != null)
			{
				trPr.Write(sw, "trPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
