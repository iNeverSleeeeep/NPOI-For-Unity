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
	public class CT_PPrChange : CT_TrackChange
	{
		private CT_PPrBase pPrField;

		[XmlElement(Order = 0)]
		public CT_PPrBase pPr
		{
			get
			{
				return pPrField;
			}
			set
			{
				pPrField = value;
			}
		}

		public new static CT_PPrChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PPrChange cT_PPrChange = new CT_PPrChange();
			cT_PPrChange.author = XmlHelper.ReadString(node.Attributes["w:author"]);
			cT_PPrChange.date = XmlHelper.ReadString(node.Attributes["w:date"]);
			cT_PPrChange.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pPr")
				{
					cT_PPrChange.pPr = CT_PPrBase.Parse(childNode, namespaceManager);
				}
			}
			return cT_PPrChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:author", base.author);
			XmlHelper.WriteAttribute(sw, "w:date", base.date);
			XmlHelper.WriteAttribute(sw, "r:id", base.id);
			sw.Write(">");
			if (pPr != null)
			{
				pPr.Write(sw, "pPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
