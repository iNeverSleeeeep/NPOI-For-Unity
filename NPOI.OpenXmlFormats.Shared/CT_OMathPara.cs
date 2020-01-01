using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_OMathPara
	{
		private CT_OMathParaPr oMathParaPrField;

		private List<CT_OMath> oMathField;

		[XmlElement(Order = 0)]
		public CT_OMathParaPr oMathParaPr
		{
			get
			{
				return oMathParaPrField;
			}
			set
			{
				oMathParaPrField = value;
			}
		}

		[XmlElement("oMath", Order = 1)]
		public List<CT_OMath> oMath
		{
			get
			{
				return oMathField;
			}
			set
			{
				oMathField = value;
			}
		}

		public static CT_OMathPara Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OMathPara cT_OMathPara = new CT_OMathPara();
			cT_OMathPara.oMath = new List<CT_OMath>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "oMathParaPr")
				{
					cT_OMathPara.oMathParaPr = CT_OMathParaPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_OMathPara.oMath.Add(CT_OMath.Parse(childNode, namespaceManager));
				}
			}
			return cT_OMathPara;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (oMathParaPr != null)
			{
				oMathParaPr.Write(sw, "oMathParaPr");
			}
			if (oMath != null)
			{
				foreach (CT_OMath item in oMath)
				{
					item.Write(sw, "oMath");
				}
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
