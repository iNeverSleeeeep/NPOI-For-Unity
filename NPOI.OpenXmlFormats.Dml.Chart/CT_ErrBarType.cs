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
	public class CT_ErrBarType
	{
		private ST_ErrBarType valField;

		[DefaultValue(ST_ErrBarType.both)]
		[XmlAttribute]
		public ST_ErrBarType val
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

		public static CT_ErrBarType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ErrBarType cT_ErrBarType = new CT_ErrBarType();
			if (node.Attributes["val"] != null)
			{
				cT_ErrBarType.val = (ST_ErrBarType)Enum.Parse(typeof(ST_ErrBarType), node.Attributes["val"].Value);
			}
			return cT_ErrBarType;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_ErrBarType()
		{
			valField = ST_ErrBarType.both;
		}
	}
}
