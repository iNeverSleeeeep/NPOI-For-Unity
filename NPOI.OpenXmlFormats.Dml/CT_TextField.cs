using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_TextField
	{
		private CT_TextCharacterProperties rPrField;

		private CT_TextParagraphProperties pPrField;

		private string tField;

		private string idField;

		private string typeField;

		[XmlElement(Order = 0)]
		public CT_TextCharacterProperties rPr
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

		[XmlElement(Order = 1)]
		public CT_TextParagraphProperties pPr
		{
			get
			{
				return pPrField;
			}
			set
			{
				pPrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public string t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[XmlAttribute(DataType = "token")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		public string type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		public static CT_TextField Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextField cT_TextField = new CT_TextField();
			cT_TextField.t = XmlHelper.ReadString(node.Attributes["t"]);
			cT_TextField.id = XmlHelper.ReadString(node.Attributes["id"]);
			cT_TextField.type = XmlHelper.ReadString(node.Attributes["type"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_TextField.rPr = CT_TextCharacterProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pPr")
				{
					cT_TextField.pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
			}
			return cT_TextField;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "t", t);
			XmlHelper.WriteAttribute(sw, "id", id);
			XmlHelper.WriteAttribute(sw, "type", type);
			sw.Write(">");
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			if (pPr != null)
			{
				pPr.Write(sw, "pPr");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
