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
	public class CT_PermStart : CT_Perm
	{
		private ST_EdGrp edGrpField;

		private bool edGrpFieldSpecified;

		private string edField;

		private string colFirstField;

		private string colLastField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_EdGrp edGrp
		{
			get
			{
				return edGrpField;
			}
			set
			{
				edGrpField = value;
			}
		}

		[XmlIgnore]
		public bool edGrpSpecified
		{
			get
			{
				return edGrpFieldSpecified;
			}
			set
			{
				edGrpFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string ed
		{
			get
			{
				return edField;
			}
			set
			{
				edField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string colFirst
		{
			get
			{
				return colFirstField;
			}
			set
			{
				colFirstField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string colLast
		{
			get
			{
				return colLastField;
			}
			set
			{
				colLastField = value;
			}
		}

		public new static CT_PermStart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PermStart cT_PermStart = new CT_PermStart();
			if (node.Attributes["w:edGrp"] != null)
			{
				cT_PermStart.edGrp = (ST_EdGrp)Enum.Parse(typeof(ST_EdGrp), node.Attributes["w:edGrp"].Value);
			}
			cT_PermStart.ed = XmlHelper.ReadString(node.Attributes["w:ed"]);
			cT_PermStart.colFirst = XmlHelper.ReadString(node.Attributes["w:colFirst"]);
			cT_PermStart.colLast = XmlHelper.ReadString(node.Attributes["w:colLast"]);
			cT_PermStart.id = XmlHelper.ReadString(node.Attributes["w:id"]);
			if (node.Attributes["w:displacedByCustomXml"] != null)
			{
				cT_PermStart.displacedByCustomXml = (ST_DisplacedByCustomXml)Enum.Parse(typeof(ST_DisplacedByCustomXml), node.Attributes["w:displacedByCustomXml"].Value);
			}
			return cT_PermStart;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:edGrp", edGrp.ToString());
			XmlHelper.WriteAttribute(sw, "w:ed", ed);
			XmlHelper.WriteAttribute(sw, "w:colFirst", colFirst);
			XmlHelper.WriteAttribute(sw, "w:colLast", colLast);
			XmlHelper.WriteAttribute(sw, "w:id", base.id);
			XmlHelper.WriteAttribute(sw, "w:displacedByCustomXml", base.displacedByCustomXml.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
