using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_TextLineBreak
	{
		private CT_TextCharacterProperties rPrField;

		[XmlElement(Order = 0)]
		public CT_TextCharacterProperties rPr
		{
			get
			{
				return rPrField;
			}
			set
			{
				rPrField = value;
			}
		}

		public static CT_TextLineBreak Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextLineBreak cT_TextLineBreak = new CT_TextLineBreak();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_TextLineBreak.rPr = CT_TextCharacterProperties.Parse(childNode, namespaceManager);
				}
			}
			return cT_TextLineBreak;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}>", nodeName));
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
