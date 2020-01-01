using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_BreakBinSub
	{
		private ST_BreakBinSub valField;

		private bool valFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_BreakBinSub val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlIgnore]
		public bool valSpecified
		{
			get
			{
				return valFieldSpecified;
			}
			set
			{
				valFieldSpecified = value;
			}
		}

		public static CT_BreakBinSub Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BreakBinSub cT_BreakBinSub = new CT_BreakBinSub();
			if (node.Attributes["m:val"] != null)
			{
				if (node.Attributes["m:val"].Value == "--")
				{
					cT_BreakBinSub.val = ST_BreakBinSub.Item;
				}
				else if (node.Attributes["m:val"].Value == "-+")
				{
					cT_BreakBinSub.val = ST_BreakBinSub.Item1;
				}
				else if (node.Attributes["m:val"].Value == "+-")
				{
					cT_BreakBinSub.val = ST_BreakBinSub.Item2;
				}
			}
			return cT_BreakBinSub;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			if (val == ST_BreakBinSub.Item)
			{
				XmlHelper.WriteAttribute(sw, "m:val", "--");
			}
			else if (val == ST_BreakBinSub.Item1)
			{
				XmlHelper.WriteAttribute(sw, "m:val", "-+");
			}
			else if (val == ST_BreakBinSub.Item2)
			{
				XmlHelper.WriteAttribute(sw, "m:val", "+-");
			}
			sw.Write(">");
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
