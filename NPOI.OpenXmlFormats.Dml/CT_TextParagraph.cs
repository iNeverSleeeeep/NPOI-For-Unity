using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TextParagraph
	{
		private CT_TextParagraphProperties pPrField;

		private List<CT_RegularTextRun> rField;

		private List<CT_TextLineBreak> brField;

		private List<CT_TextField> fldField;

		private CT_TextCharacterProperties endParaRPrField;

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

		[XmlElement("r")]
		public List<CT_RegularTextRun> r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlElement("br")]
		public List<CT_TextLineBreak> br
		{
			get
			{
				return brField;
			}
			set
			{
				brField = value;
			}
		}

		[XmlElement("fld")]
		public List<CT_TextField> fld
		{
			get
			{
				return fldField;
			}
			set
			{
				fldField = value;
			}
		}

		public CT_TextCharacterProperties endParaRPr
		{
			get
			{
				return endParaRPrField;
			}
			set
			{
				endParaRPrField = value;
			}
		}

		public static CT_TextParagraph Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextParagraph cT_TextParagraph = new CT_TextParagraph();
			cT_TextParagraph.r = new List<CT_RegularTextRun>();
			cT_TextParagraph.br = new List<CT_TextLineBreak>();
			cT_TextParagraph.fld = new List<CT_TextField>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pPr")
				{
					cT_TextParagraph.pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "endParaRPr")
				{
					cT_TextParagraph.endParaRPr = CT_TextCharacterProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "r")
				{
					cT_TextParagraph.r.Add(CT_RegularTextRun.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "br")
				{
					cT_TextParagraph.br.Add(CT_TextLineBreak.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "fld")
				{
					cT_TextParagraph.fld.Add(CT_TextField.Parse(childNode, namespaceManager));
				}
			}
			return cT_TextParagraph;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (pPr != null)
			{
				pPr.Write(sw, "pPr");
			}
			if (r != null)
			{
				foreach (CT_RegularTextRun item in r)
				{
					item.Write(sw, "r");
				}
			}
			if (br != null)
			{
				foreach (CT_TextLineBreak item2 in br)
				{
					item2.Write(sw, "br");
				}
			}
			if (fld != null)
			{
				foreach (CT_TextField item3 in fld)
				{
					item3.Write(sw, "fld");
				}
			}
			if (endParaRPr != null)
			{
				endParaRPr.Write(sw, "endParaRPr");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_RegularTextRun AddNewR()
		{
			if (rField == null)
			{
				rField = new List<CT_RegularTextRun>();
			}
			CT_RegularTextRun cT_RegularTextRun = new CT_RegularTextRun();
			rField.Add(cT_RegularTextRun);
			return cT_RegularTextRun;
		}

		public CT_TextParagraphProperties AddNewPPr()
		{
			pPrField = new CT_TextParagraphProperties();
			return pPrField;
		}

		public CT_TextCharacterProperties AddNewEndParaRPr()
		{
			endParaRPrField = new CT_TextCharacterProperties();
			return endParaRPrField;
		}

		public int SizeOfRArray()
		{
			return rField.Count;
		}
	}
}
