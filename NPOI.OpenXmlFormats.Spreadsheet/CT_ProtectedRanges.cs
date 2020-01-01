using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ProtectedRanges
	{
		private List<CT_ProtectedRange> protectedRangeField;

		public List<CT_ProtectedRange> protectedRange
		{
			get
			{
				return protectedRangeField;
			}
			set
			{
				protectedRangeField = value;
			}
		}

		public static CT_ProtectedRanges Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ProtectedRanges cT_ProtectedRanges = new CT_ProtectedRanges();
			cT_ProtectedRanges.protectedRange = new List<CT_ProtectedRange>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "protectedRange")
				{
					cT_ProtectedRanges.protectedRange.Add(CT_ProtectedRange.Parse(childNode, namespaceManager));
				}
			}
			return cT_ProtectedRanges;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (protectedRange != null)
			{
				foreach (CT_ProtectedRange item in protectedRange)
				{
					item.Write(sw, "protectedRange");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
