using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_OMathParaPr
	{
		private CT_OMathJc jcField;

		[XmlElement(Order = 0)]
		public CT_OMathJc jc
		{
			get
			{
				return jcField;
			}
			set
			{
				jcField = value;
			}
		}

		public static CT_OMathParaPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OMathParaPr cT_OMathParaPr = new CT_OMathParaPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "jc")
				{
					cT_OMathParaPr.jc = CT_OMathJc.Parse(childNode, namespaceManager);
				}
			}
			return cT_OMathParaPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (jc != null)
			{
				jc.Write(sw, "jc");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
