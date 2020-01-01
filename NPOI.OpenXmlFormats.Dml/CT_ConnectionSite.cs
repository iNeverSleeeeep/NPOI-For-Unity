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
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_ConnectionSite
	{
		private CT_AdjPoint2D posField;

		private string angField;

		[XmlElement(Order = 0)]
		public CT_AdjPoint2D pos
		{
			get
			{
				return posField;
			}
			set
			{
				posField = value;
			}
		}

		[XmlAttribute]
		public string ang
		{
			get
			{
				return angField;
			}
			set
			{
				angField = value;
			}
		}

		public static CT_ConnectionSite Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ConnectionSite cT_ConnectionSite = new CT_ConnectionSite();
			cT_ConnectionSite.ang = XmlHelper.ReadString(node.Attributes["ang"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pos")
				{
					cT_ConnectionSite.pos = CT_AdjPoint2D.Parse(childNode, namespaceManager);
				}
			}
			return cT_ConnectionSite;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "ang", ang);
			sw.Write(">");
			if (pos != null)
			{
				pos.Write(sw, "pos");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
