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
	public class CT_DocRsids
	{
		private CT_LongHexNumber rsidRootField;

		private List<CT_LongHexNumber> rsidField;

		[XmlElement(Order = 0)]
		public CT_LongHexNumber rsidRoot
		{
			get
			{
				return rsidRootField;
			}
			set
			{
				rsidRootField = value;
			}
		}

		[XmlElement("rsid", Order = 1)]
		public List<CT_LongHexNumber> rsid
		{
			get
			{
				return rsidField;
			}
			set
			{
				rsidField = value;
			}
		}

		public static CT_DocRsids Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DocRsids cT_DocRsids = new CT_DocRsids();
			cT_DocRsids.rsid = new List<CT_LongHexNumber>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rsidRoot")
				{
					cT_DocRsids.rsidRoot = CT_LongHexNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rsid")
				{
					cT_DocRsids.rsid.Add(CT_LongHexNumber.Parse(childNode, namespaceManager));
				}
			}
			return cT_DocRsids;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (rsidRoot != null)
			{
				rsidRoot.Write(sw, "rsidRoot");
			}
			if (rsid != null)
			{
				foreach (CT_LongHexNumber item in rsid)
				{
					item.Write(sw, "rsid");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
