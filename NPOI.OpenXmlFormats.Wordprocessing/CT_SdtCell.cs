using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_SdtCell
	{
		private CT_SdtPr sdtPrField;

		private List<CT_RPr> sdtEndPrField;

		private CT_SdtContentCell sdtContentField;

		[XmlElement(Order = 0)]
		public CT_SdtPr sdtPr
		{
			get
			{
				return sdtPrField;
			}
			set
			{
				sdtPrField = value;
			}
		}

		[XmlArrayItem("rPr", IsNullable = false)]
		[XmlArray(Order = 1)]
		public List<CT_RPr> sdtEndPr
		{
			get
			{
				return sdtEndPrField;
			}
			set
			{
				sdtEndPrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_SdtContentCell sdtContent
		{
			get
			{
				return sdtContentField;
			}
			set
			{
				sdtContentField = value;
			}
		}

		public static CT_SdtCell Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtCell cT_SdtCell = new CT_SdtCell();
			cT_SdtCell.sdtEndPr = new List<CT_RPr>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sdtPr")
				{
					cT_SdtCell.sdtPr = CT_SdtPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sdtContent")
				{
					cT_SdtCell.sdtContent = CT_SdtContentCell.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sdtEndPr")
				{
					cT_SdtCell.sdtEndPr.Add(CT_RPr.Parse(childNode, namespaceManager));
				}
			}
			return cT_SdtCell;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (sdtPr != null)
			{
				sdtPr.Write(sw, "sdtPr");
			}
			if (sdtContent != null)
			{
				sdtContent.Write(sw, "sdtContent");
			}
			if (sdtEndPr != null)
			{
				foreach (CT_RPr item in sdtEndPr)
				{
					item.Write(sw, "sdtEndPr");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
