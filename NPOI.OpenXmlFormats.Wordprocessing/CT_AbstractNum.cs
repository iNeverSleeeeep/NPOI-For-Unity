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
	public class CT_AbstractNum
	{
		private CT_LongHexNumber nsidField;

		private CT_MultiLevelType multiLevelTypeField;

		private CT_LongHexNumber tmplField;

		private CT_String nameField;

		private CT_String styleLinkField;

		private CT_String numStyleLinkField;

		private List<CT_Lvl> lvlField;

		private string abstractNumIdField;

		[XmlElement(Order = 0)]
		public CT_LongHexNumber nsid
		{
			get
			{
				return nsidField;
			}
			set
			{
				nsidField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_MultiLevelType multiLevelType
		{
			get
			{
				return multiLevelTypeField;
			}
			set
			{
				multiLevelTypeField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_LongHexNumber tmpl
		{
			get
			{
				return tmplField;
			}
			set
			{
				tmplField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
		public CT_String styleLink
		{
			get
			{
				return styleLinkField;
			}
			set
			{
				styleLinkField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_String numStyleLink
		{
			get
			{
				return numStyleLinkField;
			}
			set
			{
				numStyleLinkField = value;
			}
		}

		[XmlElement("lvl", Order = 6)]
		public List<CT_Lvl> lvl
		{
			get
			{
				return lvlField;
			}
			set
			{
				lvlField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string abstractNumId
		{
			get
			{
				return abstractNumIdField;
			}
			set
			{
				abstractNumIdField = value;
			}
		}

		public CT_AbstractNum()
		{
			multiLevelTypeField = new CT_MultiLevelType();
			nsidField = new CT_LongHexNumber();
			nsidField.val = new byte[4];
			Array.Copy(BitConverter.GetBytes(DateTime.Now.Ticks), 4, nsidField.val, 0, 4);
		}

		public CT_AbstractNum Copy()
		{
			CT_AbstractNum cT_AbstractNum = new CT_AbstractNum();
			cT_AbstractNum.abstractNumIdField = abstractNumIdField;
			cT_AbstractNum.lvlField = new List<CT_Lvl>(lvlField);
			cT_AbstractNum.multiLevelTypeField = multiLevelTypeField;
			cT_AbstractNum.nameField = nameField;
			cT_AbstractNum.nsidField = nsidField;
			cT_AbstractNum.numStyleLinkField = numStyleLinkField;
			cT_AbstractNum.styleLinkField = styleLinkField;
			cT_AbstractNum.tmplField = tmplField;
			return cT_AbstractNum;
		}

		public bool ValueEquals(CT_AbstractNum cT_AbstractNum)
		{
			return abstractNumIdField == cT_AbstractNum.abstractNumIdField;
		}

		public void Set(CT_AbstractNum cT_AbstractNum)
		{
			abstractNumIdField = cT_AbstractNum.abstractNumIdField;
			lvlField = new List<CT_Lvl>(cT_AbstractNum.lvlField);
			multiLevelTypeField = cT_AbstractNum.multiLevelTypeField;
			nameField = cT_AbstractNum.nameField;
			nsidField = cT_AbstractNum.nsidField;
			numStyleLinkField = cT_AbstractNum.numStyleLinkField;
			styleLinkField = cT_AbstractNum.styleLinkField;
			tmplField = cT_AbstractNum.tmplField;
		}

		public static CT_AbstractNum Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AbstractNum cT_AbstractNum = new CT_AbstractNum();
			cT_AbstractNum.abstractNumId = XmlHelper.ReadString(node.Attributes["w:abstractNumId"]);
			cT_AbstractNum.lvl = new List<CT_Lvl>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "nsid")
				{
					cT_AbstractNum.nsid = CT_LongHexNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "multiLevelType")
				{
					cT_AbstractNum.multiLevelType = CT_MultiLevelType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tmpl")
				{
					cT_AbstractNum.tmpl = CT_LongHexNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "name")
				{
					cT_AbstractNum.name = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "styleLink")
				{
					cT_AbstractNum.styleLink = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numStyleLink")
				{
					cT_AbstractNum.numStyleLink = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl")
				{
					cT_AbstractNum.lvl.Add(CT_Lvl.Parse(childNode, namespaceManager));
				}
			}
			return cT_AbstractNum;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:abstractNumId", abstractNumId);
			sw.Write(">");
			if (nsid != null)
			{
				nsid.Write(sw, "nsid");
			}
			if (multiLevelType != null)
			{
				multiLevelType.Write(sw, "multiLevelType");
			}
			if (tmpl != null)
			{
				tmpl.Write(sw, "tmpl");
			}
			if (name != null)
			{
				name.Write(sw, "name");
			}
			if (styleLink != null)
			{
				styleLink.Write(sw, "styleLink");
			}
			if (numStyleLink != null)
			{
				numStyleLink.Write(sw, "numStyleLink");
			}
			if (lvl != null)
			{
				foreach (CT_Lvl item in lvl)
				{
					item.Write(sw, "lvl");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
