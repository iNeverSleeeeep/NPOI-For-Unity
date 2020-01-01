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
	public class CT_LsdException
	{
		private string nameField;

		private ST_OnOff lockedField;

		private bool lockedFieldSpecified;

		private string uiPriorityField;

		private ST_OnOff semiHiddenField;

		private bool semiHiddenFieldSpecified;

		private ST_OnOff unhideWhenUsedField;

		private bool unhideWhenUsedFieldSpecified;

		private ST_OnOff qFormatField;

		private bool qFormatFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff locked
		{
			get
			{
				return lockedField;
			}
			set
			{
				lockedField = value;
			}
		}

		[XmlIgnore]
		public bool lockedSpecified
		{
			get
			{
				return lockedFieldSpecified;
			}
			set
			{
				lockedFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string uiPriority
		{
			get
			{
				return uiPriorityField;
			}
			set
			{
				uiPriorityField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff semiHidden
		{
			get
			{
				return semiHiddenField;
			}
			set
			{
				semiHiddenField = value;
			}
		}

		[XmlIgnore]
		public bool semiHiddenSpecified
		{
			get
			{
				return semiHiddenFieldSpecified;
			}
			set
			{
				semiHiddenFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff unhideWhenUsed
		{
			get
			{
				return unhideWhenUsedField;
			}
			set
			{
				unhideWhenUsedField = value;
			}
		}

		[XmlIgnore]
		public bool unhideWhenUsedSpecified
		{
			get
			{
				return unhideWhenUsedFieldSpecified;
			}
			set
			{
				unhideWhenUsedFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff qFormat
		{
			get
			{
				return qFormatField;
			}
			set
			{
				qFormatField = value;
			}
		}

		[XmlIgnore]
		public bool qFormatSpecified
		{
			get
			{
				return qFormatFieldSpecified;
			}
			set
			{
				qFormatFieldSpecified = value;
			}
		}

		public CT_LsdException()
		{
			semiHidden = ST_OnOff.off;
			unhideWhenUsed = ST_OnOff.off;
			locked = ST_OnOff.off;
		}

		public static CT_LsdException Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LsdException ex = new CT_LsdException();
			ex.name = XmlHelper.ReadString(node.Attributes["w:name"]);
			if (node.Attributes["w:locked"] != null)
			{
				ex.locked = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:locked"].Value);
			}
			ex.uiPriority = XmlHelper.ReadString(node.Attributes["w:uiPriority"]);
			if (node.Attributes["w:semiHidden"] != null)
			{
				ex.semiHidden = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:semiHidden"].Value);
			}
			if (node.Attributes["w:unhideWhenUsed"] != null)
			{
				ex.unhideWhenUsed = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:unhideWhenUsed"].Value);
			}
			if (node.Attributes["w:qFormat"] != null)
			{
				ex.qFormat = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:qFormat"].Value);
			}
			return ex;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:name", name);
			if (locked != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:locked", locked.ToString());
			}
			if (semiHidden == ST_OnOff.on)
			{
				XmlHelper.WriteAttribute(sw, "w:semiHidden", "1");
			}
			XmlHelper.WriteAttribute(sw, "w:uiPriority", uiPriority);
			if (unhideWhenUsed == ST_OnOff.on)
			{
				XmlHelper.WriteAttribute(sw, "w:unhideWhenUsed", "1");
			}
			if (qFormat != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:qFormat", qFormat.ToString());
			}
			sw.Write("/>");
		}
	}
}
