using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_HeaderFooter
	{
		private string oddHeaderField;

		private string oddFooterField;

		private string evenHeaderField;

		private string evenFooterField;

		private string firstHeaderField;

		private string firstFooterField;

		private bool differentOddEvenField;

		private bool differentFirstField;

		private bool scaleWithDocField;

		private bool alignWithMarginsField;

		[XmlElement]
		public string oddHeader
		{
			get
			{
				return oddHeaderField;
			}
			set
			{
				oddHeaderField = value;
			}
		}

		[XmlElement]
		public string oddFooter
		{
			get
			{
				return oddFooterField;
			}
			set
			{
				oddFooterField = value;
			}
		}

		[XmlElement]
		public string evenHeader
		{
			get
			{
				return evenHeaderField;
			}
			set
			{
				evenHeaderField = value;
			}
		}

		[XmlElement]
		public string evenFooter
		{
			get
			{
				return evenFooterField;
			}
			set
			{
				evenFooterField = value;
			}
		}

		[XmlElement]
		public string firstHeader
		{
			get
			{
				return firstHeaderField;
			}
			set
			{
				firstHeaderField = value;
			}
		}

		[XmlElement]
		public string firstFooter
		{
			get
			{
				return firstFooterField;
			}
			set
			{
				firstFooterField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool differentOddEven
		{
			get
			{
				return differentOddEvenField;
			}
			set
			{
				differentOddEvenField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool differentFirst
		{
			get
			{
				return differentFirstField;
			}
			set
			{
				differentFirstField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool scaleWithDoc
		{
			get
			{
				return scaleWithDocField;
			}
			set
			{
				scaleWithDocField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool alignWithMargins
		{
			get
			{
				return alignWithMarginsField;
			}
			set
			{
				alignWithMarginsField = value;
			}
		}

		public CT_HeaderFooter()
		{
			differentOddEvenField = false;
			differentFirstField = false;
			scaleWithDocField = true;
			alignWithMarginsField = true;
		}

		public static CT_HeaderFooter Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_HeaderFooter cT_HeaderFooter = new CT_HeaderFooter();
			cT_HeaderFooter.differentOddEven = XmlHelper.ReadBool(node.Attributes["differentOddEven"]);
			cT_HeaderFooter.differentFirst = XmlHelper.ReadBool(node.Attributes["differentFirst"]);
			if (node.Attributes["scaleWithDoc"] != null)
			{
				cT_HeaderFooter.scaleWithDoc = XmlHelper.ReadBool(node.Attributes["scaleWithDoc"]);
			}
			if (node.Attributes["alignWithMargins"] != null)
			{
				cT_HeaderFooter.alignWithMargins = XmlHelper.ReadBool(node.Attributes["alignWithMargins"]);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "oddHeader")
				{
					cT_HeaderFooter.oddHeader = childNode.InnerText;
				}
				else if (childNode.LocalName == "oddFooter")
				{
					cT_HeaderFooter.oddFooter = childNode.InnerText;
				}
				else if (childNode.LocalName == "evenHeader")
				{
					cT_HeaderFooter.evenHeader = childNode.InnerText;
				}
				else if (childNode.LocalName == "evenFooter")
				{
					cT_HeaderFooter.evenFooter = childNode.InnerText;
				}
				else if (childNode.LocalName == "firstHeader")
				{
					cT_HeaderFooter.firstHeader = childNode.InnerText;
				}
				else if (childNode.LocalName == "firstFooter")
				{
					cT_HeaderFooter.firstFooter = childNode.InnerText;
				}
			}
			return cT_HeaderFooter;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (differentOddEven)
			{
				XmlHelper.WriteAttribute(sw, "differentOddEven", differentOddEven);
			}
			if (differentFirst)
			{
				XmlHelper.WriteAttribute(sw, "differentFirst", differentFirst);
			}
			if (!scaleWithDoc)
			{
				XmlHelper.WriteAttribute(sw, "scaleWithDoc", scaleWithDoc);
			}
			if (!alignWithMargins)
			{
				XmlHelper.WriteAttribute(sw, "alignWithMargins", alignWithMargins);
			}
			sw.Write(">");
			if (oddHeader != null)
			{
				sw.Write(string.Format("<oddHeader><![CDATA[{0}]]></oddHeader>", oddHeader));
			}
			if (oddFooter != null)
			{
				sw.Write(string.Format("<oddFooter><![CDATA[{0}]]></oddFooter>", oddFooter));
			}
			if (evenHeader != null)
			{
				sw.Write(string.Format("<evenHeader><![CDATA[{0}]]></evenHeader>", evenHeader));
			}
			if (evenFooter != null)
			{
				sw.Write(string.Format("<evenFooter><![CDATA[{0}]]></evenFooter>", evenFooter));
			}
			if (firstHeader != null)
			{
				sw.Write(string.Format("<firstHeader><![CDATA[{0}]]></firstHeader>", firstHeader));
			}
			if (firstFooter != null)
			{
				sw.Write(string.Format("<firstFooter><![CDATA[{0}]]></firstFooter>", firstFooter));
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
