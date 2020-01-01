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
	public class CT_HeaderFooter
	{
		private string oddHeaderField;

		private string oddFooterField;

		private string evenHeaderField;

		private string evenFooterField;

		private string firstHeaderField;

		private string firstFooterField;

		private bool alignWithMarginsField;

		private bool differentOddEvenField;

		private bool differentFirstField;

		[XmlElement(Order = 0)]
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

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 5)]
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

		[DefaultValue(true)]
		[XmlAttribute]
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

		[DefaultValue(false)]
		[XmlAttribute]
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

		public CT_HeaderFooter()
		{
			alignWithMarginsField = true;
			differentOddEvenField = false;
			differentFirstField = false;
		}

		public static CT_HeaderFooter Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_HeaderFooter cT_HeaderFooter = new CT_HeaderFooter();
			if (node.Attributes["alignWithMargins"] != null)
			{
				cT_HeaderFooter.alignWithMargins = XmlHelper.ReadBool(node.Attributes["alignWithMargins"]);
			}
			else
			{
				cT_HeaderFooter.alignWithMargins = true;
			}
			if (node.Attributes["differentOddEven"] != null)
			{
				cT_HeaderFooter.differentOddEven = XmlHelper.ReadBool(node.Attributes["differentOddEven"]);
			}
			if (node.Attributes["differentFirst"] != null)
			{
				cT_HeaderFooter.differentFirst = XmlHelper.ReadBool(node.Attributes["differentFirst"]);
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
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "alignWithMargins", alignWithMargins, true);
			XmlHelper.WriteAttribute(sw, "differentOddEven", differentOddEven);
			XmlHelper.WriteAttribute(sw, "differentFirst", differentFirst);
			sw.Write(">");
			if (oddHeader != null)
			{
				sw.Write(string.Format("<oddHeader>{0}</oddHeader>", oddHeader));
			}
			if (oddFooter != null)
			{
				sw.Write(string.Format("<oddFooter>{0}</oddFooter>", oddFooter));
			}
			if (evenHeader != null)
			{
				sw.Write(string.Format("<evenHeader>{0}</evenHeader>", evenHeader));
			}
			if (evenFooter != null)
			{
				sw.Write(string.Format("<evenFooter>{0}</evenFooter>", evenFooter));
			}
			if (firstHeader != null)
			{
				sw.Write(string.Format("<firstHeader>{0}</firstHeader>", firstHeader));
			}
			if (firstFooter != null)
			{
				sw.Write(string.Format("<firstFooter>{0}</firstFooter>", firstFooter));
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
