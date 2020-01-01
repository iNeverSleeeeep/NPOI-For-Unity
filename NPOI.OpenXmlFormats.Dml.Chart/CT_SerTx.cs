using NPOI.OpenXml4Net.Util;
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
	public class CT_SerTx
	{
		private CT_StrRef strRefField;

		private string vField;

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

		public string v
		{
			get
			{
				return vField;
			}
			set
			{
				vField = value;
			}
		}

		public static CT_SerTx Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SerTx cT_SerTx = new CT_SerTx();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "strRef")
				{
					cT_SerTx.strRef = CT_StrRef.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "v")
				{
					cT_SerTx.v = childNode.InnerText;
				}
			}
			return cT_SerTx;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (strRef != null)
			{
				strRef.Write(sw, "strRef");
			}
			if (v != null)
			{
				sw.Write(string.Format("<c:v>{0}</c:v>", XmlHelper.EncodeXml(v)));
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_StrRef AddNewStrRef()
		{
			strRefField = new CT_StrRef();
			return strRefField;
		}
	}
}
