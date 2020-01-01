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
	public class CT_Spacing
	{
		private ulong beforeField;

		private bool beforeFieldSpecified;

		private string beforeLinesField;

		private ST_OnOff beforeAutospacingField;

		private bool beforeAutospacingFieldSpecified;

		private ulong afterField;

		private bool afterFieldSpecified;

		private string afterLinesField;

		private ST_OnOff afterAutospacingField;

		private bool afterAutospacingFieldSpecified;

		private string lineField;

		private ST_LineSpacingRule lineRuleField;

		private bool lineRuleFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong before
		{
			get
			{
				return beforeField;
			}
			set
			{
				beforeField = value;
			}
		}

		[XmlIgnore]
		public bool beforeSpecified
		{
			get
			{
				return beforeFieldSpecified;
			}
			set
			{
				beforeFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string beforeLines
		{
			get
			{
				return beforeLinesField;
			}
			set
			{
				beforeLinesField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff beforeAutospacing
		{
			get
			{
				return beforeAutospacingField;
			}
			set
			{
				beforeAutospacingField = value;
			}
		}

		[XmlIgnore]
		public bool beforeAutospacingSpecified
		{
			get
			{
				return beforeAutospacingFieldSpecified;
			}
			set
			{
				beforeAutospacingFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong after
		{
			get
			{
				return afterField;
			}
			set
			{
				afterField = value;
			}
		}

		[XmlIgnore]
		public bool afterSpecified
		{
			get
			{
				return afterFieldSpecified;
			}
			set
			{
				afterFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string afterLines
		{
			get
			{
				return afterLinesField;
			}
			set
			{
				afterLinesField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff afterAutospacing
		{
			get
			{
				return afterAutospacingField;
			}
			set
			{
				afterAutospacingField = value;
			}
		}

		[XmlIgnore]
		public bool afterAutospacingSpecified
		{
			get
			{
				return afterAutospacingFieldSpecified;
			}
			set
			{
				afterAutospacingFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string line
		{
			get
			{
				return lineField;
			}
			set
			{
				lineField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_LineSpacingRule lineRule
		{
			get
			{
				return lineRuleField;
			}
			set
			{
				lineRuleField = value;
			}
		}

		[XmlIgnore]
		public bool lineRuleSpecified
		{
			get
			{
				return lineRuleFieldSpecified;
			}
			set
			{
				lineRuleFieldSpecified = value;
			}
		}

		public static CT_Spacing Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Spacing cT_Spacing = new CT_Spacing();
			cT_Spacing.before = XmlHelper.ReadULong(node.Attributes["w:before"]);
			cT_Spacing.beforeLines = XmlHelper.ReadString(node.Attributes["w:beforeLines"]);
			if (node.Attributes["w:beforeAutospacing"] != null)
			{
				cT_Spacing.beforeAutospacing = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:beforeAutospacing"].Value);
			}
			cT_Spacing.after = XmlHelper.ReadULong(node.Attributes["w:after"]);
			cT_Spacing.afterLines = XmlHelper.ReadString(node.Attributes["w:afterLines"]);
			if (node.Attributes["w:afterAutospacing"] != null)
			{
				cT_Spacing.afterAutospacing = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:afterAutospacing"].Value);
			}
			cT_Spacing.line = XmlHelper.ReadString(node.Attributes["w:line"]);
			if (node.Attributes["w:lineRule"] != null)
			{
				cT_Spacing.lineRule = (ST_LineSpacingRule)Enum.Parse(typeof(ST_LineSpacingRule), node.Attributes["w:lineRule"].Value);
			}
			return cT_Spacing;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:before", (double)before);
			XmlHelper.WriteAttribute(sw, "w:beforeLines", beforeLines);
			if (beforeAutospacing != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:beforeAutospacing", beforeAutospacing.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:after", (double)after);
			XmlHelper.WriteAttribute(sw, "w:afterLines", afterLines);
			if (afterAutospacing != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:afterAutospacing", afterAutospacing.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:line", line);
			if (lineRule != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:lineRule", lineRule.ToString());
			}
			sw.Write("/>");
		}

		public bool IsSetBefore()
		{
			return beforeField != 0;
		}

		public bool IsSetBeforeLines()
		{
			return !string.IsNullOrEmpty(beforeLinesField);
		}

		public bool IsSetLineRule()
		{
			return lineRuleField != ST_LineSpacingRule.nil;
		}

		public bool IsSetAfter()
		{
			return afterField != 0;
		}

		public bool IsSetAfterLines()
		{
			return !string.IsNullOrEmpty(afterLinesField);
		}
	}
}
