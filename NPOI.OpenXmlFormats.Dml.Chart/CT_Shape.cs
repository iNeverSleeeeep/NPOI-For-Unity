using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_Shape
	{
		private ST_Shape valField;

		[XmlAttribute]
		[DefaultValue(ST_Shape.box)]
		public ST_Shape val
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

		public CT_Shape()
		{
			valField = ST_Shape.box;
		}

		public static CT_Shape Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Shape cT_Shape = new CT_Shape();
			if (node.Attributes["val"] != null)
			{
				cT_Shape.val = (ST_Shape)Enum.Parse(typeof(ST_Shape), node.Attributes["val"].Value);
			}
			return cT_Shape;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
