using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TextBody
	{
		private CT_TextBodyProperties bodyPrField;

		private CT_TextListStyle lstStyleField;

		private List<CT_TextParagraph> pField;

		public CT_TextBodyProperties bodyPr
		{
			get
			{
				return bodyPrField;
			}
			set
			{
				bodyPrField = value;
			}
		}

		public CT_TextListStyle lstStyle
		{
			get
			{
				return lstStyleField;
			}
			set
			{
				lstStyleField = value;
			}
		}

		[XmlElement("p")]
		public List<CT_TextParagraph> p
		{
			get
			{
				return pField;
			}
			set
			{
				pField = value;
			}
		}

		public static CT_TextBody Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextBody cT_TextBody = new CT_TextBody();
			cT_TextBody.p = new List<CT_TextParagraph>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "bodyPr")
				{
					cT_TextBody.bodyPr = CT_TextBodyProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lstStyle")
				{
					cT_TextBody.lstStyle = CT_TextListStyle.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "p")
				{
					cT_TextBody.p.Add(CT_TextParagraph.Parse(childNode, namespaceManager));
				}
			}
			return cT_TextBody;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (bodyPr != null)
			{
				bodyPr.Write(sw, "bodyPr");
			}
			if (lstStyle != null)
			{
				lstStyle.Write(sw, "lstStyle");
			}
			foreach (CT_TextParagraph item in p)
			{
				item.Write(sw, "p");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public void SetPArray(CT_TextParagraph[] array)
		{
			pField = new List<CT_TextParagraph>(array);
		}

		public CT_TextParagraph AddNewP()
		{
			if (pField == null)
			{
				pField = new List<CT_TextParagraph>();
			}
			CT_TextParagraph cT_TextParagraph = new CT_TextParagraph();
			pField.Add(cT_TextParagraph);
			return cT_TextParagraph;
		}

		public CT_TextBodyProperties AddNewBodyPr()
		{
			bodyPrField = new CT_TextBodyProperties();
			return bodyPrField;
		}

		public CT_TextListStyle AddNewLstStyle()
		{
			lstStyleField = new CT_TextListStyle();
			return lstStyleField;
		}

		public override string ToString()
		{
			if (p == null || p.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (CT_TextParagraph item in p)
			{
				foreach (CT_RegularTextRun item2 in item.r)
				{
					stringBuilder.Append(item2.t);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
