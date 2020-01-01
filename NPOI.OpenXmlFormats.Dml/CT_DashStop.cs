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
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_DashStop
	{
		private int dField;

		private int spField;

		[XmlAttribute]
		public int d
		{
			get
			{
				return dField;
			}
			set
			{
				dField = value;
			}
		}

		[XmlAttribute]
		public int sp
		{
			get
			{
				return spField;
			}
			set
			{
				spField = value;
			}
		}

		public static CT_DashStop Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DashStop cT_DashStop = new CT_DashStop();
			cT_DashStop.d = XmlHelper.ReadInt(node.Attributes["d"]);
			cT_DashStop.sp = XmlHelper.ReadInt(node.Attributes["sp"]);
			return cT_DashStop;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "d", d);
			XmlHelper.WriteAttribute(sw, "sp", sp);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
