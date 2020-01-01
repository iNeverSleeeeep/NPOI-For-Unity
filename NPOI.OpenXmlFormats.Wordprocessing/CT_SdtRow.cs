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
	public class CT_SdtRow
	{
		private CT_SdtPr sdtPrField;

		private List<CT_RPr> sdtEndPrField;

		private CT_SdtContentRow sdtContentField;

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

		[XmlArray(Order = 1)]
		[XmlArrayItem("rPr", IsNullable = false)]
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
		public CT_SdtContentRow sdtContent
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

		public static CT_SdtRow Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtRow cT_SdtRow = new CT_SdtRow();
			cT_SdtRow.sdtEndPr = new List<CT_RPr>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sdtPr")
				{
					cT_SdtRow.sdtPr = CT_SdtPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sdtContent")
				{
					cT_SdtRow.sdtContent = CT_SdtContentRow.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sdtEndPr")
				{
					cT_SdtRow.sdtEndPr.Add(CT_RPr.Parse(childNode, namespaceManager));
				}
			}
			return cT_SdtRow;
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
