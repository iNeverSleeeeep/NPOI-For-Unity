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
	public class CT_LineNumber
	{
		private string countByField;

		private string startField;

		private ulong distanceField;

		private bool distanceFieldSpecified;

		private ST_LineNumberRestart restartField;

		private bool restartFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string countBy
		{
			get
			{
				return countByField;
			}
			set
			{
				countByField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string start
		{
			get
			{
				return startField;
			}
			set
			{
				startField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong distance
		{
			get
			{
				return distanceField;
			}
			set
			{
				distanceField = value;
			}
		}

		[XmlIgnore]
		public bool distanceSpecified
		{
			get
			{
				return distanceFieldSpecified;
			}
			set
			{
				distanceFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_LineNumberRestart restart
		{
			get
			{
				return restartField;
			}
			set
			{
				restartField = value;
			}
		}

		[XmlIgnore]
		public bool restartSpecified
		{
			get
			{
				return restartFieldSpecified;
			}
			set
			{
				restartFieldSpecified = value;
			}
		}

		public static CT_LineNumber Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LineNumber cT_LineNumber = new CT_LineNumber();
			cT_LineNumber.countBy = XmlHelper.ReadString(node.Attributes["w:countBy"]);
			cT_LineNumber.start = XmlHelper.ReadString(node.Attributes["w:start"]);
			cT_LineNumber.distance = XmlHelper.ReadULong(node.Attributes["w:distance"]);
			if (node.Attributes["w:restart"] != null)
			{
				cT_LineNumber.restart = (ST_LineNumberRestart)Enum.Parse(typeof(ST_LineNumberRestart), node.Attributes["w:restart"].Value);
			}
			return cT_LineNumber;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:countBy", countBy);
			XmlHelper.WriteAttribute(sw, "w:start", start);
			XmlHelper.WriteAttribute(sw, "w:distance", (double)distance);
			XmlHelper.WriteAttribute(sw, "w:restart", restart.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
