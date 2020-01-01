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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Style
	{
		private CT_String nameField;

		private CT_String aliasesField;

		private CT_String basedOnField;

		private CT_String nextField;

		private CT_String linkField;

		private CT_OnOff autoRedefineField;

		private CT_OnOff hiddenField;

		private CT_DecimalNumber uiPriorityField;

		private CT_OnOff semiHiddenField;

		private CT_OnOff unhideWhenUsedField;

		private CT_OnOff qFormatField;

		private CT_OnOff lockedField;

		private CT_OnOff personalField;

		private CT_OnOff personalComposeField;

		private CT_OnOff personalReplyField;

		private CT_LongHexNumber rsidField;

		private CT_PPr pPrField;

		private CT_RPr rPrField;

		private CT_TblPrBase tblPrField;

		private CT_TrPr trPrField;

		private CT_TcPr tcPrField;

		private List<CT_TblStylePr> tblStylePrField;

		private ST_StyleType typeField;

		private bool typeFieldSpecified;

		private string styleIdField;

		private ST_OnOff defaultField;

		private bool defaultFieldSpecified;

		private ST_OnOff customStyleField;

		private bool customStyleFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_String name
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

		[XmlElement(Order = 1)]
		public CT_String aliases
		{
			get
			{
				return aliasesField;
			}
			set
			{
				aliasesField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_String basedOn
		{
			get
			{
				return basedOnField;
			}
			set
			{
				basedOnField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_String next
		{
			get
			{
				return nextField;
			}
			set
			{
				nextField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_String link
		{
			get
			{
				return linkField;
			}
			set
			{
				linkField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_OnOff autoRedefine
		{
			get
			{
				return autoRedefineField;
			}
			set
			{
				autoRedefineField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_OnOff hidden
		{
			get
			{
				return hiddenField;
			}
			set
			{
				hiddenField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_DecimalNumber uiPriority
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

		[XmlElement(Order = 8)]
		public CT_OnOff semiHidden
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

		[XmlElement(Order = 9)]
		public CT_OnOff unhideWhenUsed
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

		[XmlElement(Order = 10)]
		public CT_OnOff qFormat
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

		[XmlElement(Order = 11)]
		public CT_OnOff locked
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

		[XmlElement(Order = 12)]
		public CT_OnOff personal
		{
			get
			{
				return personalField;
			}
			set
			{
				personalField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_OnOff personalCompose
		{
			get
			{
				return personalComposeField;
			}
			set
			{
				personalComposeField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_OnOff personalReply
		{
			get
			{
				return personalReplyField;
			}
			set
			{
				personalReplyField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_LongHexNumber rsid
		{
			get
			{
				return rsidField;
			}
			set
			{
				rsidField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_PPr pPr
		{
			get
			{
				return pPrField;
			}
			set
			{
				pPrField = value;
			}
		}

		[XmlElement(Order = 17)]
		public CT_RPr rPr
		{
			get
			{
				return rPrField;
			}
			set
			{
				rPrField = value;
			}
		}

		[XmlElement(Order = 18)]
		public CT_TblPrBase tblPr
		{
			get
			{
				return tblPrField;
			}
			set
			{
				tblPrField = value;
			}
		}

		[XmlElement(Order = 19)]
		public CT_TrPr trPr
		{
			get
			{
				return trPrField;
			}
			set
			{
				trPrField = value;
			}
		}

		[XmlElement(Order = 20)]
		public CT_TcPr tcPr
		{
			get
			{
				return tcPrField;
			}
			set
			{
				tcPrField = value;
			}
		}

		[XmlElement("tblStylePr", Order = 21)]
		public List<CT_TblStylePr> tblStylePr
		{
			get
			{
				return tblStylePrField;
			}
			set
			{
				tblStylePrField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_StyleType type
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string styleId
		{
			get
			{
				return styleIdField;
			}
			set
			{
				styleIdField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff @default
		{
			get
			{
				return defaultField;
			}
			set
			{
				defaultField = value;
			}
		}

		[XmlIgnore]
		public bool defaultSpecified
		{
			get
			{
				return defaultFieldSpecified;
			}
			set
			{
				defaultFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff customStyle
		{
			get
			{
				return customStyleField;
			}
			set
			{
				customStyleField = value;
			}
		}

		[XmlIgnore]
		public bool customStyleSpecified
		{
			get
			{
				return customStyleFieldSpecified;
			}
			set
			{
				customStyleFieldSpecified = value;
			}
		}

		public static CT_Style Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Style cT_Style = new CT_Style();
			if (node.Attributes["w:type"] != null)
			{
				cT_Style.type = (ST_StyleType)Enum.Parse(typeof(ST_StyleType), node.Attributes["w:type"].Value);
			}
			cT_Style.styleId = XmlHelper.ReadString(node.Attributes["w:styleId"]);
			if (node.Attributes["w:default"] != null)
			{
				cT_Style.@default = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:default"].Value);
			}
			if (node.Attributes["w:customStyle"] != null)
			{
				cT_Style.customStyle = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:customStyle"].Value);
			}
			cT_Style.tblStylePr = new List<CT_TblStylePr>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "name")
				{
					cT_Style.name = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "aliases")
				{
					cT_Style.aliases = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "basedOn")
				{
					cT_Style.basedOn = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "next")
				{
					cT_Style.next = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "link")
				{
					cT_Style.link = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoRedefine")
				{
					cT_Style.autoRedefine = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hidden")
				{
					cT_Style.hidden = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "uiPriority")
				{
					cT_Style.uiPriority = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "semiHidden")
				{
					cT_Style.semiHidden = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "unhideWhenUsed")
				{
					cT_Style.unhideWhenUsed = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "qFormat")
				{
					cT_Style.qFormat = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "locked")
				{
					cT_Style.locked = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "personal")
				{
					cT_Style.personal = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "personalCompose")
				{
					cT_Style.personalCompose = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "personalReply")
				{
					cT_Style.personalReply = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rsid")
				{
					cT_Style.rsid = CT_LongHexNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pPr")
				{
					cT_Style.pPr = CT_PPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rPr")
				{
					cT_Style.rPr = CT_RPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblPr")
				{
					cT_Style.tblPr = CT_TblPrBase.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "trPr")
				{
					cT_Style.trPr = CT_TrPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcPr")
				{
					cT_Style.tcPr = CT_TcPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblStylePr")
				{
					cT_Style.tblStylePr.Add(CT_TblStylePr.Parse(childNode, namespaceManager));
				}
			}
			return cT_Style;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:type", type.ToString());
			if (@default != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:default", @default.ToString());
			}
			if (customStyle == ST_OnOff.on)
			{
				XmlHelper.WriteAttribute(sw, "w:customStyle", "1");
			}
			XmlHelper.WriteAttribute(sw, "w:styleId", styleId);
			sw.Write(">");
			if (name != null)
			{
				name.Write(sw, "name");
			}
			if (aliases != null)
			{
				aliases.Write(sw, "aliases");
			}
			if (basedOn != null)
			{
				basedOn.Write(sw, "basedOn");
			}
			if (next != null)
			{
				next.Write(sw, "next");
			}
			if (link != null)
			{
				link.Write(sw, "link");
			}
			if (autoRedefine != null)
			{
				autoRedefine.Write(sw, "autoRedefine");
			}
			if (hidden != null)
			{
				hidden.Write(sw, "hidden");
			}
			if (uiPriority != null)
			{
				uiPriority.Write(sw, "uiPriority");
			}
			if (semiHidden != null)
			{
				semiHidden.Write(sw, "semiHidden");
			}
			if (unhideWhenUsed != null)
			{
				unhideWhenUsed.Write(sw, "unhideWhenUsed");
			}
			if (qFormat != null)
			{
				qFormat.Write(sw, "qFormat");
			}
			if (locked != null)
			{
				locked.Write(sw, "locked");
			}
			if (personal != null)
			{
				personal.Write(sw, "personal");
			}
			if (personalCompose != null)
			{
				personalCompose.Write(sw, "personalCompose");
			}
			if (personalReply != null)
			{
				personalReply.Write(sw, "personalReply");
			}
			if (rsid != null)
			{
				rsid.Write(sw, "rsid");
			}
			if (pPr != null)
			{
				pPr.Write(sw, "pPr");
			}
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			if (tblPr != null)
			{
				tblPr.Write(sw, "tblPr");
			}
			if (trPr != null)
			{
				trPr.Write(sw, "trPr");
			}
			if (tcPr != null)
			{
				tcPr.Write(sw, "tcPr");
			}
			if (tblStylePr != null)
			{
				foreach (CT_TblStylePr item in tblStylePr)
				{
					item.Write(sw, "tblStylePr");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public bool IsSetName()
		{
			return name != null;
		}
	}
}
