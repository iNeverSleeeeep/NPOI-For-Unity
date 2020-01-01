using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_RubyPr
	{
		private CT_RubyAlign rubyAlignField;

		private CT_HpsMeasure hpsField;

		private CT_HpsMeasure hpsRaiseField;

		private CT_HpsMeasure hpsBaseTextField;

		private CT_Lang lidField;

		private CT_OnOff dirtyField;

		[XmlElement(Order = 0)]
		public CT_RubyAlign rubyAlign
		{
			get
			{
				return rubyAlignField;
			}
			set
			{
				rubyAlignField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_HpsMeasure hps
		{
			get
			{
				return hpsField;
			}
			set
			{
				hpsField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_HpsMeasure hpsRaise
		{
			get
			{
				return hpsRaiseField;
			}
			set
			{
				hpsRaiseField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_HpsMeasure hpsBaseText
		{
			get
			{
				return hpsBaseTextField;
			}
			set
			{
				hpsBaseTextField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Lang lid
		{
			get
			{
				return lidField;
			}
			set
			{
				lidField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_OnOff dirty
		{
			get
			{
				return dirtyField;
			}
			set
			{
				dirtyField = value;
			}
		}

		public static CT_RubyPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RubyPr cT_RubyPr = new CT_RubyPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rubyAlign")
				{
					cT_RubyPr.rubyAlign = CT_RubyAlign.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hps")
				{
					cT_RubyPr.hps = CT_HpsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hpsRaise")
				{
					cT_RubyPr.hpsRaise = CT_HpsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hpsBaseText")
				{
					cT_RubyPr.hpsBaseText = CT_HpsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lid")
				{
					cT_RubyPr.lid = CT_Lang.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dirty")
				{
					cT_RubyPr.dirty = CT_OnOff.Parse(childNode, namespaceManager);
				}
			}
			return cT_RubyPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (rubyAlign != null)
			{
				rubyAlign.Write(sw, "rubyAlign");
			}
			if (hps != null)
			{
				hps.Write(sw, "hps");
			}
			if (hpsRaise != null)
			{
				hpsRaise.Write(sw, "hpsRaise");
			}
			if (hpsBaseText != null)
			{
				hpsBaseText.Write(sw, "hpsBaseText");
			}
			if (lid != null)
			{
				lid.Write(sw, "lid");
			}
			if (dirty != null)
			{
				dirty.Write(sw, "dirty");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
