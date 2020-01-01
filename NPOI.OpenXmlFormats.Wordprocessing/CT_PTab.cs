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
	public class CT_PTab
	{
		private ST_PTabAlignment alignmentField;

		private ST_PTabRelativeTo relativeToField;

		private ST_PTabLeader leaderField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_PTabAlignment alignment
		{
			get
			{
				return alignmentField;
			}
			set
			{
				alignmentField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_PTabRelativeTo relativeTo
		{
			get
			{
				return relativeToField;
			}
			set
			{
				relativeToField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_PTabLeader leader
		{
			get
			{
				return leaderField;
			}
			set
			{
				leaderField = value;
			}
		}

		public static CT_PTab Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PTab cT_PTab = new CT_PTab();
			if (node.Attributes["w:alignment"] != null)
			{
				cT_PTab.alignment = (ST_PTabAlignment)Enum.Parse(typeof(ST_PTabAlignment), node.Attributes["w:alignment"].Value);
			}
			if (node.Attributes["w:relativeTo"] != null)
			{
				cT_PTab.relativeTo = (ST_PTabRelativeTo)Enum.Parse(typeof(ST_PTabRelativeTo), node.Attributes["w:relativeTo"].Value);
			}
			if (node.Attributes["w:leader"] != null)
			{
				cT_PTab.leader = (ST_PTabLeader)Enum.Parse(typeof(ST_PTabLeader), node.Attributes["w:leader"].Value);
			}
			return cT_PTab;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:alignment", alignment.ToString());
			XmlHelper.WriteAttribute(sw, "w:relativeTo", relativeTo.ToString());
			XmlHelper.WriteAttribute(sw, "w:leader", leader.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
