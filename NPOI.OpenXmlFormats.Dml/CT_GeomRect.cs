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
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_GeomRect
	{
		private string lField;

		private string tField;

		private string rField;

		private string bField;

		[XmlAttribute]
		public string l
		{
			get
			{
				return lField;
			}
			set
			{
				lField = value;
			}
		}

		[XmlAttribute]
		public string t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[XmlAttribute]
		public string r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlAttribute]
		public string b
		{
			get
			{
				return bField;
			}
			set
			{
				bField = value;
			}
		}

		public static CT_GeomRect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GeomRect cT_GeomRect = new CT_GeomRect();
			cT_GeomRect.l = XmlHelper.ReadString(node.Attributes["l"]);
			cT_GeomRect.t = XmlHelper.ReadString(node.Attributes["t"]);
			cT_GeomRect.r = XmlHelper.ReadString(node.Attributes["r"]);
			cT_GeomRect.b = XmlHelper.ReadString(node.Attributes["b"]);
			return cT_GeomRect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "l", l);
			XmlHelper.WriteAttribute(sw, "t", t);
			XmlHelper.WriteAttribute(sw, "r", r);
			XmlHelper.WriteAttribute(sw, "b", b);
			sw.Write("/>");
		}
	}
}
