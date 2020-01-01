using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_Phant
	{
		private CT_PhantPr phantPrField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_PhantPr phantPr
		{
			get
			{
				return phantPrField;
			}
			set
			{
				phantPrField = value;
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

		public static CT_Phant Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Phant cT_Phant = new CT_Phant();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "phantPr")
				{
					cT_Phant.phantPr = CT_PhantPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_Phant.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_Phant;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (phantPr != null)
			{
				phantPr.Write(sw, "phantPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
