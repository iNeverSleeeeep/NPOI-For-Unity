using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	public class CT_LineJoinMiterProperties
	{
		private int limField;

		private bool limFieldSpecified;

		[XmlAttribute]
		public int lim
		{
			get
			{
				return limField;
			}
			set
			{
				limField = value;
			}
		}

		[XmlIgnore]
		public bool limSpecified
		{
			get
			{
				return limFieldSpecified;
			}
			set
			{
				limFieldSpecified = value;
			}
		}

		public static CT_LineJoinMiterProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LineJoinMiterProperties cT_LineJoinMiterProperties = new CT_LineJoinMiterProperties();
			cT_LineJoinMiterProperties.lim = XmlHelper.ReadInt(node.Attributes["lim"]);
			return cT_LineJoinMiterProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "lim", lim);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
