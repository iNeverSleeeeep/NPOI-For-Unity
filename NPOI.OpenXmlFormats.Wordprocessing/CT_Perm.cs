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
	[XmlInclude(typeof(CT_PermStart))]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Perm
	{
		private string idField;

		private ST_DisplacedByCustomXml displacedByCustomXmlField;

		private bool displacedByCustomXmlFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_DisplacedByCustomXml displacedByCustomXml
		{
			get
			{
				return displacedByCustomXmlField;
			}
			set
			{
				displacedByCustomXmlField = value;
			}
		}

		[XmlIgnore]
		public bool displacedByCustomXmlSpecified
		{
			get
			{
				return displacedByCustomXmlFieldSpecified;
			}
			set
			{
				displacedByCustomXmlFieldSpecified = value;
			}
		}

		public static CT_Perm Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Perm cT_Perm = new CT_Perm();
			cT_Perm.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			if (node.Attributes["w:displacedByCustomXml"] != null)
			{
				cT_Perm.displacedByCustomXml = (ST_DisplacedByCustomXml)Enum.Parse(typeof(ST_DisplacedByCustomXml), node.Attributes["w:displacedByCustomXml"].Value);
			}
			return cT_Perm;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r:id", id);
			XmlHelper.WriteAttribute(sw, "w:displacedByCustomXml", displacedByCustomXml.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
