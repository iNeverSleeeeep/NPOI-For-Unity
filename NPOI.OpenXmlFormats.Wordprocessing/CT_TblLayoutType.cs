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
	public class CT_TblLayoutType
	{
		private ST_TblLayoutType typeField;

		private bool typeFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TblLayoutType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		public static CT_TblLayoutType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblLayoutType cT_TblLayoutType = new CT_TblLayoutType();
			if (node.Attributes["w:type"] != null)
			{
				cT_TblLayoutType.type = (ST_TblLayoutType)Enum.Parse(typeof(ST_TblLayoutType), node.Attributes["w:type"].Value);
			}
			return cT_TblLayoutType;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:type", type.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
