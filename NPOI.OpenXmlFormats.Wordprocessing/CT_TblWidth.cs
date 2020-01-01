using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TblWidth
	{
		private string wField;

		private ST_TblWidth typeField = ST_TblWidth.auto;

		private bool typeFieldSpecified = true;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TblWidth type
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

		public static CT_TblWidth Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblWidth cT_TblWidth = new CT_TblWidth();
			cT_TblWidth.w = XmlHelper.ReadString(node.Attributes["w:w"]);
			if (node.Attributes["w:type"] != null)
			{
				cT_TblWidth.type = (ST_TblWidth)Enum.Parse(typeof(ST_TblWidth), node.Attributes["w:type"].Value);
			}
			return cT_TblWidth;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:w", w);
			XmlHelper.WriteAttribute(sw, "w:type", type.ToString());
			sw.Write("/>");
		}
	}
}
