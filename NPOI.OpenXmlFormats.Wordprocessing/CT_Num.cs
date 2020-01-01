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
	public class CT_Num
	{
		private CT_DecimalNumber abstractNumIdField;

		private List<CT_NumLvl> lvlOverrideField;

		private string numIdField;

		[XmlElement(Order = 0)]
		public CT_DecimalNumber abstractNumId
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

		[XmlElement("lvlOverride", Order = 1)]
		public List<CT_NumLvl> lvlOverride
		{
			get
			{
				return lvlOverrideField;
			}
			set
			{
				lvlOverrideField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string numId
		{
			get
			{
				return numIdField;
			}
			set
			{
				numIdField = value;
			}
		}

		public static CT_Num Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Num cT_Num = new CT_Num();
			cT_Num.numId = XmlHelper.ReadString(node.Attributes["w:numId"]);
			cT_Num.lvlOverride = new List<CT_NumLvl>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "abstractNumId")
				{
					cT_Num.abstractNumId = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvlOverride")
				{
					cT_Num.lvlOverride.Add(CT_NumLvl.Parse(childNode, namespaceManager));
				}
			}
			return cT_Num;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:numId", numId);
			sw.Write(">");
			if (abstractNumId != null)
			{
				abstractNumId.Write(sw, "abstractNumId");
			}
			if (lvlOverride != null)
			{
				foreach (CT_NumLvl item in lvlOverride)
				{
					item.Write(sw, "lvlOverride");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_DecimalNumber AddNewAbstractNumId()
		{
			if (abstractNumIdField == null)
			{
				abstractNumIdField = new CT_DecimalNumber();
			}
			return abstractNumIdField;
		}
	}
}
