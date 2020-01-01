using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_DocDefaults
	{
		private CT_RPrDefault rPrDefaultField;

		private CT_PPrDefault pPrDefaultField;

		[XmlElement(Order = 0)]
		public CT_RPrDefault rPrDefault
		{
			get
			{
				return rPrDefaultField;
			}
			set
			{
				rPrDefaultField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_PPrDefault pPrDefault
		{
			get
			{
				return pPrDefaultField;
			}
			set
			{
				pPrDefaultField = value;
			}
		}

		public static CT_DocDefaults Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DocDefaults cT_DocDefaults = new CT_DocDefaults();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPrDefault")
				{
					cT_DocDefaults.rPrDefault = CT_RPrDefault.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pPrDefault")
				{
					cT_DocDefaults.pPrDefault = CT_PPrDefault.Parse(childNode, namespaceManager);
				}
			}
			return cT_DocDefaults;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (rPrDefault != null)
			{
				rPrDefault.Write(sw, "rPrDefault");
			}
			if (pPrDefault != null)
			{
				pPrDefault.Write(sw, "pPrDefault");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public bool IsSetRPrDefault()
		{
			return rPrDefaultField != null;
		}

		public CT_RPrDefault AddNewRPrDefault()
		{
			rPrDefaultField = new CT_RPrDefault();
			return rPrDefaultField;
		}
	}
}
