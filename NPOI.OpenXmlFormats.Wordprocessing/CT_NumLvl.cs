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
	public class CT_NumLvl
	{
		private CT_DecimalNumber startOverrideField;

		private CT_Lvl lvlField;

		private string ilvlField;

		[XmlElement(Order = 0)]
		public CT_DecimalNumber startOverride
		{
			get
			{
				return startOverrideField;
			}
			set
			{
				startOverrideField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Lvl lvl
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
		public string ilvl
		{
			get
			{
				return ilvlField;
			}
			set
			{
				ilvlField = value;
			}
		}

		public static CT_NumLvl Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumLvl cT_NumLvl = new CT_NumLvl();
			cT_NumLvl.ilvl = XmlHelper.ReadString(node.Attributes["w:ilvl"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "startOverride")
				{
					cT_NumLvl.startOverride = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl")
				{
					cT_NumLvl.lvl = CT_Lvl.Parse(childNode, namespaceManager);
				}
			}
			return cT_NumLvl;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:ilvl", ilvl);
			sw.Write(">");
			if (startOverride != null)
			{
				startOverride.Write(sw, "startOverride");
			}
			if (lvl != null)
			{
				lvl.Write(sw, "lvl");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
