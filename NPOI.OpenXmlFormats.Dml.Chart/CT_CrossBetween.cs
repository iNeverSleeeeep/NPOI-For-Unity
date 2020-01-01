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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[DesignerCategory("code")]
	public class CT_CrossBetween
	{
		private ST_CrossBetween valField;

		[XmlAttribute]
		public ST_CrossBetween val
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

		public static CT_CrossBetween Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CrossBetween cT_CrossBetween = new CT_CrossBetween();
			if (node.Attributes["val"] != null)
			{
				cT_CrossBetween.val = (ST_CrossBetween)Enum.Parse(typeof(ST_CrossBetween), node.Attributes["val"].Value);
			}
			return cT_CrossBetween;
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
