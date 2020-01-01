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
	public class CT_OfPieType
	{
		private ST_OfPieType valField;

		[XmlAttribute]
		[DefaultValue(ST_OfPieType.pie)]
		public ST_OfPieType val
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

		public CT_OfPieType()
		{
			valField = ST_OfPieType.pie;
		}

		public static CT_OfPieType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OfPieType cT_OfPieType = new CT_OfPieType();
			if (node.Attributes["val"] != null)
			{
				cT_OfPieType.val = (ST_OfPieType)Enum.Parse(typeof(ST_OfPieType), node.Attributes["val"].Value);
			}
			return cT_OfPieType;
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
