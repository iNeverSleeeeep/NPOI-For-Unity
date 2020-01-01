using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_RotY
	{
		private ushort valField;

		[XmlAttribute]
		[DefaultValue(typeof(ushort), "0")]
		public ushort val
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

		public CT_RotY()
		{
			valField = 0;
		}

		public static CT_RotY Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RotY cT_RotY = new CT_RotY();
			cT_RotY.val = XmlHelper.ReadUShort(node.Attributes["val"]);
			return cT_RotY;
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
