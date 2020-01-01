using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_Func
	{
		private CT_FuncPr funcPrField;

		private CT_OMathArg fNameField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_FuncPr funcPr
		{
			get
			{
				return funcPrField;
			}
			set
			{
				funcPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OMathArg fName
		{
			get
			{
				return fNameField;
			}
			set
			{
				fNameField = value;
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

		public static CT_Func Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Func cT_Func = new CT_Func();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "funcPr")
				{
					cT_Func.funcPr = CT_FuncPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fName")
				{
					cT_Func.fName = CT_OMathArg.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_Func.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_Func;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (funcPr != null)
			{
				funcPr.Write(sw, "funcPr");
			}
			if (fName != null)
			{
				fName.Write(sw, "fName");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
