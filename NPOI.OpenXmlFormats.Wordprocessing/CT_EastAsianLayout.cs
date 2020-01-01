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
	public class CT_EastAsianLayout
	{
		private string idField;

		private ST_OnOff combineField;

		private bool combineFieldSpecified;

		private ST_CombineBrackets combineBracketsField;

		private bool combineBracketsFieldSpecified;

		private ST_OnOff vertField;

		private bool vertFieldSpecified;

		private ST_OnOff vertCompressField;

		private bool vertCompressFieldSpecified;

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
		public ST_OnOff combine
		{
			get
			{
				return combineField;
			}
			set
			{
				combineField = value;
			}
		}

		[XmlIgnore]
		public bool combineSpecified
		{
			get
			{
				return combineFieldSpecified;
			}
			set
			{
				combineFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_CombineBrackets combineBrackets
		{
			get
			{
				return combineBracketsField;
			}
			set
			{
				combineBracketsField = value;
			}
		}

		[XmlIgnore]
		public bool combineBracketsSpecified
		{
			get
			{
				return combineBracketsFieldSpecified;
			}
			set
			{
				combineBracketsFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff vert
		{
			get
			{
				return vertField;
			}
			set
			{
				vertField = value;
			}
		}

		[XmlIgnore]
		public bool vertSpecified
		{
			get
			{
				return vertFieldSpecified;
			}
			set
			{
				vertFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff vertCompress
		{
			get
			{
				return vertCompressField;
			}
			set
			{
				vertCompressField = value;
			}
		}

		[XmlIgnore]
		public bool vertCompressSpecified
		{
			get
			{
				return vertCompressFieldSpecified;
			}
			set
			{
				vertCompressFieldSpecified = value;
			}
		}

		public static CT_EastAsianLayout Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EastAsianLayout cT_EastAsianLayout = new CT_EastAsianLayout();
			cT_EastAsianLayout.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			if (node.Attributes["w:combine"] != null)
			{
				cT_EastAsianLayout.combine = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:combine"].Value);
			}
			if (node.Attributes["w:combineBrackets"] != null)
			{
				cT_EastAsianLayout.combineBrackets = (ST_CombineBrackets)Enum.Parse(typeof(ST_CombineBrackets), node.Attributes["w:combineBrackets"].Value);
			}
			if (node.Attributes["w:vert"] != null)
			{
				cT_EastAsianLayout.vert = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:vert"].Value);
			}
			if (node.Attributes["w:vertCompress"] != null)
			{
				cT_EastAsianLayout.vertCompress = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:vertCompress"].Value);
			}
			return cT_EastAsianLayout;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r:id", id);
			XmlHelper.WriteAttribute(sw, "w:combine", combine.ToString());
			XmlHelper.WriteAttribute(sw, "w:combineBrackets", combineBrackets.ToString());
			XmlHelper.WriteAttribute(sw, "w:vert", vert.ToString());
			XmlHelper.WriteAttribute(sw, "w:vertCompress", vertCompress.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
