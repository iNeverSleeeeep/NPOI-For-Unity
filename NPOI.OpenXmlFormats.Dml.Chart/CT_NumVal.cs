using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_NumVal
	{
		private string vField;

		private uint idxField;

		private string formatCodeField;

		[XmlElement(Order = 0)]
		public string v
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

		[XmlAttribute]
		public uint idx
		{
			get
			{
				return idxField;
			}
			set
			{
				idxField = value;
			}
		}

		[XmlAttribute]
		public string formatCode
		{
			get
			{
				return formatCodeField;
			}
			set
			{
				formatCodeField = value;
			}
		}

		public static CT_NumVal Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumVal cT_NumVal = new CT_NumVal();
			if (node.Attributes["idx"] != null)
			{
				cT_NumVal.idx = XmlHelper.ReadUInt(node.Attributes["idx"]);
			}
			cT_NumVal.formatCode = XmlHelper.ReadString(node.Attributes["formatCode"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "v")
				{
					cT_NumVal.v = childNode.InnerText;
				}
			}
			return cT_NumVal;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "idx", (double)idx, true);
			XmlHelper.WriteAttribute(sw, "formatCode", formatCode);
			sw.Write(">");
			if (v != null)
			{
				sw.Write(string.Format("<c:v>{0}</c:v>", v));
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
