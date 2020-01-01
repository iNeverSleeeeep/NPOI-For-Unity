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
	public class CT_ProofErr
	{
		private ST_ProofErr typeField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ProofErr type
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

		public static CT_ProofErr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ProofErr cT_ProofErr = new CT_ProofErr();
			if (node.Attributes["w:type"] != null)
			{
				cT_ProofErr.type = (ST_ProofErr)Enum.Parse(typeof(ST_ProofErr), node.Attributes["w:type"].Value);
			}
			return cT_ProofErr;
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
