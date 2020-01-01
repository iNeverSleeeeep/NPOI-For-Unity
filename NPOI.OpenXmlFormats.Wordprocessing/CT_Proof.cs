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
	public class CT_Proof
	{
		private ST_Proof spellingField;

		private bool spellingFieldSpecified;

		private ST_Proof grammarField;

		private bool grammarFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Proof spelling
		{
			get
			{
				return spellingField;
			}
			set
			{
				spellingField = value;
			}
		}

		[XmlIgnore]
		public bool spellingSpecified
		{
			get
			{
				return spellingFieldSpecified;
			}
			set
			{
				spellingFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Proof grammar
		{
			get
			{
				return grammarField;
			}
			set
			{
				grammarField = value;
			}
		}

		[XmlIgnore]
		public bool grammarSpecified
		{
			get
			{
				return grammarFieldSpecified;
			}
			set
			{
				grammarFieldSpecified = value;
			}
		}

		public static CT_Proof Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Proof cT_Proof = new CT_Proof();
			if (node.Attributes["w:spelling"] != null)
			{
				cT_Proof.spelling = (ST_Proof)Enum.Parse(typeof(ST_Proof), node.Attributes["w:spelling"].Value);
			}
			if (node.Attributes["w:grammar"] != null)
			{
				cT_Proof.grammar = (ST_Proof)Enum.Parse(typeof(ST_Proof), node.Attributes["w:grammar"].Value);
			}
			return cT_Proof;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:spelling", spelling.ToString());
			XmlHelper.WriteAttribute(sw, "w:grammar", grammar.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
