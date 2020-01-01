using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_LatentStyles
	{
		private List<CT_LsdException> lsdExceptionField;

		private ST_OnOff defLockedStateField;

		private bool defLockedStateFieldSpecified;

		private string defUIPriorityField;

		private ST_OnOff defSemiHiddenField;

		private bool defSemiHiddenFieldSpecified;

		private ST_OnOff defUnhideWhenUsedField;

		private bool defUnhideWhenUsedFieldSpecified;

		private ST_OnOff defQFormatField;

		private bool defQFormatFieldSpecified;

		private string countField;

		[XmlElement("lsdException", Order = 0)]
		public List<CT_LsdException> lsdException
		{
			get
			{
				return lsdExceptionField;
			}
			set
			{
				lsdExceptionField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff defLockedState
		{
			get
			{
				return defLockedStateField;
			}
			set
			{
				defLockedStateField = value;
			}
		}

		[XmlIgnore]
		public bool defLockedStateSpecified
		{
			get
			{
				return defLockedStateFieldSpecified;
			}
			set
			{
				defLockedStateFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string defUIPriority
		{
			get
			{
				return defUIPriorityField;
			}
			set
			{
				defUIPriorityField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff defSemiHidden
		{
			get
			{
				return defSemiHiddenField;
			}
			set
			{
				defSemiHiddenField = value;
			}
		}

		[XmlIgnore]
		public bool defSemiHiddenSpecified
		{
			get
			{
				return defSemiHiddenFieldSpecified;
			}
			set
			{
				defSemiHiddenFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff defUnhideWhenUsed
		{
			get
			{
				return defUnhideWhenUsedField;
			}
			set
			{
				defUnhideWhenUsedField = value;
			}
		}

		[XmlIgnore]
		public bool defUnhideWhenUsedSpecified
		{
			get
			{
				return defUnhideWhenUsedFieldSpecified;
			}
			set
			{
				defUnhideWhenUsedFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff defQFormat
		{
			get
			{
				return defQFormatField;
			}
			set
			{
				defQFormatField = value;
			}
		}

		[XmlIgnore]
		public bool defQFormatSpecified
		{
			get
			{
				return defQFormatFieldSpecified;
			}
			set
			{
				defQFormatFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		public static CT_LatentStyles Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LatentStyles cT_LatentStyles = new CT_LatentStyles();
			if (node.Attributes["w:defLockedState"] != null)
			{
				cT_LatentStyles.defLockedState = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:defLockedState"].Value);
			}
			cT_LatentStyles.defUIPriority = XmlHelper.ReadString(node.Attributes["w:defUIPriority"]);
			if (node.Attributes["w:defSemiHidden"] != null)
			{
				cT_LatentStyles.defSemiHidden = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:defSemiHidden"].Value);
			}
			if (node.Attributes["w:defUnhideWhenUsed"] != null)
			{
				cT_LatentStyles.defUnhideWhenUsed = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:defUnhideWhenUsed"].Value);
			}
			if (node.Attributes["w:defQFormat"] != null)
			{
				cT_LatentStyles.defQFormat = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:defQFormat"].Value);
			}
			cT_LatentStyles.count = XmlHelper.ReadString(node.Attributes["w:count"]);
			cT_LatentStyles.lsdException = new List<CT_LsdException>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "lsdException")
				{
					cT_LatentStyles.lsdException.Add(CT_LsdException.Parse(childNode, namespaceManager));
				}
			}
			return cT_LatentStyles;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:defLockedState", defLockedState.ToString());
			XmlHelper.WriteAttribute(sw, "w:defUIPriority", defUIPriority);
			XmlHelper.WriteAttribute(sw, "w:defSemiHidden", defSemiHidden.ToString());
			XmlHelper.WriteAttribute(sw, "w:defUnhideWhenUsed", defUnhideWhenUsed.ToString());
			XmlHelper.WriteAttribute(sw, "w:defQFormat", defQFormat.ToString());
			XmlHelper.WriteAttribute(sw, "w:count", count);
			sw.Write(">");
			if (lsdException != null)
			{
				foreach (CT_LsdException item in lsdException)
				{
					item.Write(sw, "lsdException");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_LsdException AddNewLsdException()
		{
			CT_LsdException ex = new CT_LsdException();
			if (lsdExceptionField == null)
			{
				lsdExceptionField = new List<CT_LsdException>();
			}
			lsdExceptionField.Add(ex);
			return ex;
		}
	}
}
