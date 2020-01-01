using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_Bar
	{
		private CT_BarPr barPrField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_BarPr barPr
		{
			get
			{
				return barPrField;
			}
			set
			{
				barPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OMathArg e
		{
			get
			{
				return eField;
			}
			set
			{
				eField = value;
			}
		}

		public static CT_Bar Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Bar cT_Bar = new CT_Bar();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "barPr")
				{
					cT_Bar.barPr = CT_BarPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_Bar.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_Bar;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (barPr != null)
			{
				barPr.Write(sw, "barPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
