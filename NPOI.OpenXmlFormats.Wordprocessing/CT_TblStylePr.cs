using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TblStylePr
	{
		private CT_PPr pPrField;

		private CT_RPr rPrField;

		private CT_TblPrBase tblPrField;

		private CT_TrPr trPrField;

		private CT_TcPr tcPrField;

		private ST_TblStyleOverrideType typeField;

		[XmlElement(Order = 0)]
		public CT_PPr pPr
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

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
		public CT_TblPrBase tblPr
		{
			get
			{
				return tblPrField;
			}
			set
			{
				tblPrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_TrPr trPr
		{
			get
			{
				return trPrField;
			}
			set
			{
				trPrField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_TcPr tcPr
		{
			get
			{
				return tcPrField;
			}
			set
			{
				tcPrField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TblStyleOverrideType type
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

		public static CT_TblStylePr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblStylePr cT_TblStylePr = new CT_TblStylePr();
			if (node.Attributes["w:type"] != null)
			{
				cT_TblStylePr.type = (ST_TblStyleOverrideType)Enum.Parse(typeof(ST_TblStyleOverrideType), node.Attributes["w:type"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pPr")
				{
					cT_TblStylePr.pPr = CT_PPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rPr")
				{
					cT_TblStylePr.rPr = CT_RPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblPr")
				{
					cT_TblStylePr.tblPr = CT_TblPrBase.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "trPr")
				{
					cT_TblStylePr.trPr = CT_TrPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcPr")
				{
					cT_TblStylePr.tcPr = CT_TcPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblStylePr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:type", type.ToString());
			sw.Write(">");
			if (pPr != null)
			{
				pPr.Write(sw, "pPr");
			}
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			if (tblPr != null)
			{
				tblPr.Write(sw, "tblPr");
			}
			if (trPr != null)
			{
				trPr.Write(sw, "trPr");
			}
			if (tcPr != null)
			{
				tcPr.Write(sw, "tcPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
