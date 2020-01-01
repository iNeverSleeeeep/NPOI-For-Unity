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
	public class CT_TabStop
	{
		private ST_TabJc valField;

		private ST_TabTlc leaderField;

		private bool leaderFieldSpecified;

		private string posField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TabJc val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TabTlc leader
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

		[XmlIgnore]
		public bool leaderSpecified
		{
			get
			{
				return leaderFieldSpecified;
			}
			set
			{
				leaderFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string pos
		{
			get
			{
				return posField;
			}
			set
			{
				posField = value;
			}
		}

		public static CT_TabStop Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TabStop cT_TabStop = new CT_TabStop();
			if (node.Attributes["w:val"] != null)
			{
				cT_TabStop.val = (ST_TabJc)Enum.Parse(typeof(ST_TabJc), node.Attributes["w:val"].Value);
			}
			if (node.Attributes["w:leader"] != null)
			{
				cT_TabStop.leader = (ST_TabTlc)Enum.Parse(typeof(ST_TabTlc), node.Attributes["w:leader"].Value);
			}
			cT_TabStop.pos = XmlHelper.ReadString(node.Attributes["w:pos"]);
			return cT_TabStop;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			if (leader != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:leader", leader.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:pos", pos, true);
			sw.Write("/>");
		}
	}
}
