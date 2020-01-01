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
	public class CT_ParaRPrChange : CT_TrackChange
	{
		private CT_ParaRPrOriginal rPrField;

		[XmlElement(Order = 0)]
		public CT_ParaRPrOriginal rPr
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

		public new static CT_ParaRPrChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ParaRPrChange cT_ParaRPrChange = new CT_ParaRPrChange();
			cT_ParaRPrChange.author = XmlHelper.ReadString(node.Attributes["author"]);
			cT_ParaRPrChange.date = XmlHelper.ReadString(node.Attributes["date"]);
			cT_ParaRPrChange.id = XmlHelper.ReadString(node.Attributes["id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_ParaRPrChange.rPr = CT_ParaRPrOriginal.Parse(childNode, namespaceManager);
				}
			}
			return cT_ParaRPrChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "author", base.author);
			XmlHelper.WriteAttribute(sw, "date", base.date);
			XmlHelper.WriteAttribute(sw, "id", base.id);
			sw.Write(">");
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
