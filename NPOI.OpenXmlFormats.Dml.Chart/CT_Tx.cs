using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_Tx
	{
		private CT_StrRef strRefField;

		private CT_TextBody richField;

		public CT_StrRef strRef
		{
			get
			{
				return strRefField;
			}
			set
			{
				strRefField = value;
			}
		}

		public CT_TextBody rich
		{
			get
			{
				return richField;
			}
			set
			{
				richField = value;
			}
		}

		public static CT_Tx Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Tx cT_Tx = new CT_Tx();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "strRef")
				{
					cT_Tx.strRef = CT_StrRef.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rich")
				{
					cT_Tx.rich = CT_TextBody.Parse(childNode, namespaceManager);
				}
			}
			return cT_Tx;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (strRef != null)
			{
				strRef.Write(sw, "strRef");
			}
			if (rich != null)
			{
				rich.Write(sw, "rich");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
