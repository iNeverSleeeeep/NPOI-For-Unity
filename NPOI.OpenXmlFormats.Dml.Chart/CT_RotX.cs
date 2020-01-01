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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_RotX
	{
		private sbyte valField;

		[XmlAttribute]
		[DefaultValue(typeof(sbyte), "0")]
		public sbyte val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public CT_RotX()
		{
			valField = 0;
		}

		public static CT_RotX Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RotX cT_RotX = new CT_RotX();
			cT_RotX.val = XmlHelper.ReadSByte(node.Attributes["val"]);
			return cT_RotX;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val);
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
