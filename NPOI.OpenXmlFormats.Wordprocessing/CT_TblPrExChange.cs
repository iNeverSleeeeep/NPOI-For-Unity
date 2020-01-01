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
	public class CT_TblPrExChange : CT_TrackChange
	{
		private CT_TblPrExBase tblPrExField;

		[XmlElement(Order = 0)]
		public CT_TblPrExBase tblPrEx
		{
			get
			{
				return tblPrExField;
			}
			set
			{
				tblPrExField = value;
			}
		}

		public new static CT_TblPrExChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblPrExChange cT_TblPrExChange = new CT_TblPrExChange();
			cT_TblPrExChange.author = XmlHelper.ReadString(node.Attributes["w:author"]);
			cT_TblPrExChange.date = XmlHelper.ReadString(node.Attributes["w:date"]);
			cT_TblPrExChange.id = XmlHelper.ReadString(node.Attributes["w:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblPrEx")
				{
					cT_TblPrExChange.tblPrEx = CT_TblPrExBase.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblPrExChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:author", base.author);
			XmlHelper.WriteAttribute(sw, "w:date", base.date);
			XmlHelper.WriteAttribute(sw, "w:id", base.id);
			sw.Write(">");
			if (tblPrEx != null)
			{
				tblPrEx.Write(sw, "tblPrEx");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
