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
	public class CT_M
	{
		private CT_MPr mPrField;

		private List<CT_OMathArg> mrField;

		[XmlElement(Order = 0)]
		public CT_MPr mPr
		{
			get
			{
				return mPrField;
			}
			set
			{
				mPrField = value;
			}
		}

		[XmlArrayItem("e", typeof(CT_OMathArg), IsNullable = false)]
		[XmlArray(Order = 1)]
		public List<CT_OMathArg> mr
		{
			get
			{
				return mrField;
			}
			set
			{
				mrField = value;
			}
		}

		public static CT_M Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_M cT_M = new CT_M();
			cT_M.mr = new List<CT_OMathArg>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "mPr")
				{
					cT_M.mPr = CT_MPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mr")
				{
					cT_M.mr.Add(CT_OMathArg.Parse(childNode, namespaceManager));
				}
			}
			return cT_M;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (mPr != null)
			{
				mPr.Write(sw, "mPr");
			}
			if (mr != null)
			{
				foreach (CT_OMathArg item in mr)
				{
					item.Write(sw, "mr");
				}
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
