using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_Rad
	{
		private CT_RadPr radPrField;

		private CT_OMathArg degField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_RadPr radPr
		{
			get
			{
				return radPrField;
			}
			set
			{
				radPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OMathArg deg
		{
			get
			{
				return degField;
			}
			set
			{
				degField = value;
			}
		}

		[XmlElement(Order = 2)]
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

		public static CT_Rad Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Rad cT_Rad = new CT_Rad();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "radPr")
				{
					cT_Rad.radPr = CT_RadPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "deg")
				{
					cT_Rad.deg = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_Rad.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_Rad;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (radPr != null)
			{
				radPr.Write(sw, "radPr");
			}
			if (deg != null)
			{
				deg.Write(sw, "deg");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
