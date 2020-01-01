using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_RPrDefault
	{
		private CT_RPr rPrField;

		[XmlElement(Order = 0)]
		public CT_RPr rPr
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

		public static CT_RPrDefault Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RPrDefault cT_RPrDefault = new CT_RPrDefault();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_RPrDefault.rPr = CT_RPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_RPrDefault;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public bool IsSetRPr()
		{
			return rPrField != null;
		}

		public CT_RPr AddNewRPr()
		{
			rPrField = new CT_RPr();
			return rPrField;
		}
	}
}
