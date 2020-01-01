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
	public class CT_HdrFtrRef : CT_Rel
	{
		private ST_HdrFtr typeField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_HdrFtr type
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

		public new static CT_HdrFtrRef Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_HdrFtrRef cT_HdrFtrRef = new CT_HdrFtrRef();
			if (node.Attributes["w:type"] != null)
			{
				cT_HdrFtrRef.type = (ST_HdrFtr)Enum.Parse(typeof(ST_HdrFtr), node.Attributes["w:type"].Value);
			}
			cT_HdrFtrRef.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			return cT_HdrFtrRef;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:type", type.ToString());
			XmlHelper.WriteAttribute(sw, "r:id", base.id);
			sw.Write("/>");
		}
	}
}
