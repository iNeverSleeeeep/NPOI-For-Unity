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
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_SplitType
	{
		private ST_SplitType valField;

		[XmlAttribute]
		[DefaultValue(ST_SplitType.auto)]
		public ST_SplitType val
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

		public CT_SplitType()
		{
			valField = ST_SplitType.auto;
		}

		public static CT_SplitType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SplitType cT_SplitType = new CT_SplitType();
			if (node.Attributes["val"] != null)
			{
				cT_SplitType.val = (ST_SplitType)Enum.Parse(typeof(ST_SplitType), node.Attributes["val"].Value);
			}
			return cT_SplitType;
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
