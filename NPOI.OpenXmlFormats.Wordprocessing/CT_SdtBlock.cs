using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SdtBlock
	{
		private CT_SdtPr sdtPrField;

		private List<CT_RPr> sdtEndPrField;

		private CT_SdtContentBlock sdtContentField;

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
		public CT_SdtContentBlock sdtContent
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

		public static CT_SdtBlock Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtBlock cT_SdtBlock = new CT_SdtBlock();
			cT_SdtBlock.sdtEndPr = new List<CT_RPr>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sdtPr")
				{
					cT_SdtBlock.sdtPr = CT_SdtPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sdtContent")
				{
					cT_SdtBlock.sdtContent = CT_SdtContentBlock.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sdtEndPr")
				{
					cT_SdtBlock.sdtEndPr.Add(CT_RPr.Parse(childNode, namespaceManager));
				}
			}
			return cT_SdtBlock;
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

		public CT_SdtPr AddNewSdtPr()
		{
			if (sdtPrField == null)
			{
				sdtPrField = new CT_SdtPr();
			}
			return sdtPrField;
		}

		public CT_SdtEndPr AddNewSdtEndPr()
		{
			CT_SdtEndPr cT_SdtEndPr = new CT_SdtEndPr();
			sdtEndPrField = cT_SdtEndPr.Items;
			return cT_SdtEndPr;
		}

		public CT_SdtContentBlock AddNewSdtContent()
		{
			if (sdtContentField == null)
			{
				sdtContentField = new CT_SdtContentBlock();
			}
			return sdtContentField;
		}
	}
}
