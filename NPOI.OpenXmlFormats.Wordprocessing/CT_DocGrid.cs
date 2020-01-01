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
	public class CT_DocGrid
	{
		private ST_DocGrid typeField;

		private bool typeFieldSpecified;

		private string linePitchField;

		private string charSpaceField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_DocGrid type
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string linePitch
		{
			get
			{
				return linePitchField;
			}
			set
			{
				linePitchField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string charSpace
		{
			get
			{
				return charSpaceField;
			}
			set
			{
				charSpaceField = value;
			}
		}

		public CT_DocGrid()
		{
			type = ST_DocGrid.@default;
		}

		public static CT_DocGrid Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DocGrid cT_DocGrid = new CT_DocGrid();
			if (node.Attributes["w:type"] != null)
			{
				cT_DocGrid.type = (ST_DocGrid)Enum.Parse(typeof(ST_DocGrid), node.Attributes["w:type"].Value);
			}
			cT_DocGrid.linePitch = XmlHelper.ReadString(node.Attributes["w:linePitch"]);
			cT_DocGrid.charSpace = XmlHelper.ReadString(node.Attributes["w:charSpace"]);
			return cT_DocGrid;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			if (type != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:type", type.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:linePitch", linePitch);
			XmlHelper.WriteAttribute(sw, "w:charSpace", charSpace);
			sw.Write("/>");
		}
	}
}
