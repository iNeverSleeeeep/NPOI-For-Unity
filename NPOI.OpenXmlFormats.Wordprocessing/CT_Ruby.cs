using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Ruby
	{
		private CT_RubyPr rubyPrField;

		private CT_RubyContent rtField;

		private CT_RubyContent rubyBaseField;

		[XmlElement(Order = 0)]
		public CT_RubyPr rubyPr
		{
			get
			{
				return rubyPrField;
			}
			set
			{
				rubyPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_RubyContent rt
		{
			get
			{
				return rtField;
			}
			set
			{
				rtField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_RubyContent rubyBase
		{
			get
			{
				return rubyBaseField;
			}
			set
			{
				rubyBaseField = value;
			}
		}

		public static CT_Ruby Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Ruby cT_Ruby = new CT_Ruby();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rubyPr")
				{
					cT_Ruby.rubyPr = CT_RubyPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rt")
				{
					cT_Ruby.rt = CT_RubyContent.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rubyBase")
				{
					cT_Ruby.rubyBase = CT_RubyContent.Parse(childNode, namespaceManager);
				}
			}
			return cT_Ruby;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (rubyPr != null)
			{
				rubyPr.Write(sw, "rubyPr");
			}
			if (rt != null)
			{
				rt.Write(sw, "rt");
			}
			if (rubyBase != null)
			{
				rubyBase.Write(sw, "rubyBase");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
