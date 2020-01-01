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
	public class CT_TblPrChange : CT_TrackChange
	{
		private CT_TblPrBase tblPrField;

		[XmlElement(Order = 0)]
		public CT_TblPrBase tblPr
		{
			get
			{
				return tblPrField;
			}
			set
			{
				tblPrField = value;
			}
		}

		public new static CT_TblPrChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblPrChange cT_TblPrChange = new CT_TblPrChange();
			cT_TblPrChange.author = XmlHelper.ReadString(node.Attributes["w:author"]);
			cT_TblPrChange.date = XmlHelper.ReadString(node.Attributes["w:date"]);
			cT_TblPrChange.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblPr")
				{
					cT_TblPrChange.tblPr = CT_TblPrBase.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblPrChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:author", base.author);
			XmlHelper.WriteAttribute(sw, "w:date", base.date);
			XmlHelper.WriteAttribute(sw, "r:id", base.id);
			sw.Write(">");
			if (tblPr != null)
			{
				tblPr.Write(sw, "tblPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
