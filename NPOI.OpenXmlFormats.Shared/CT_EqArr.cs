using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_EqArr
	{
		private CT_EqArrPr eqArrPrField;

		private List<CT_OMathArg> eField;

		[XmlElement(Order = 0)]
		public CT_EqArrPr eqArrPr
		{
			get
			{
				return eqArrPrField;
			}
			set
			{
				eqArrPrField = value;
			}
		}

		[XmlElement("e", Order = 1)]
		public List<CT_OMathArg> e
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

		public static CT_EqArr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EqArr cT_EqArr = new CT_EqArr();
			cT_EqArr.e = new List<CT_OMathArg>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "eqArrPr")
				{
					cT_EqArr.eqArrPr = CT_EqArrPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_EqArr.e.Add(CT_OMathArg.Parse(childNode, namespaceManager));
				}
			}
			return cT_EqArr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (eqArrPr != null)
			{
				eqArrPr.Write(sw, "eqArrPr");
			}
			if (e != null)
			{
				foreach (CT_OMathArg item in e)
				{
					item.Write(sw, "e");
				}
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
