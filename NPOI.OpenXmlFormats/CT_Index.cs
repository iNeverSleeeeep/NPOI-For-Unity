using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Index
	{
		private uint vField;

		[XmlAttribute]
		public uint v
		{
			get
			{
				return vField;
			}
			set
			{
				vField = value;
			}
		}

		public static CT_Index Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Index cT_Index = new CT_Index();
			cT_Index.v = XmlHelper.ReadUInt(node.Attributes["v"]);
			return cT_Index;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "v", v);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
