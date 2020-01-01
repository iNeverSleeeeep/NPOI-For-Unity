using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_DataBinding
	{
		private string prefixMappingsField;

		private string xpathField;

		private string storeItemIDField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string prefixMappings
		{
			get
			{
				return prefixMappingsField;
			}
			set
			{
				prefixMappingsField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string xpath
		{
			get
			{
				return xpathField;
			}
			set
			{
				xpathField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string storeItemID
		{
			get
			{
				return storeItemIDField;
			}
			set
			{
				storeItemIDField = value;
			}
		}

		public static CT_DataBinding Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DataBinding cT_DataBinding = new CT_DataBinding();
			cT_DataBinding.prefixMappings = XmlHelper.ReadString(node.Attributes["w:prefixMappings"]);
			cT_DataBinding.xpath = XmlHelper.ReadString(node.Attributes["w:xpath"]);
			cT_DataBinding.storeItemID = XmlHelper.ReadString(node.Attributes["w:storeItemID"]);
			return cT_DataBinding;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:prefixMappings", prefixMappings);
			XmlHelper.WriteAttribute(sw, "w:xpath", xpath);
			XmlHelper.WriteAttribute(sw, "w:storeItemID", storeItemID);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
