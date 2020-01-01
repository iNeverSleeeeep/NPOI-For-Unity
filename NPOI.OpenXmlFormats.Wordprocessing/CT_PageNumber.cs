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
	public class CT_PageNumber
	{
		private ST_NumberFormat fmtField;

		private bool fmtFieldSpecified;

		private string startField;

		private string chapStyleField;

		private ST_ChapterSep chapSepField;

		private bool chapSepFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_NumberFormat fmt
		{
			get
			{
				return fmtField;
			}
			set
			{
				fmtField = value;
			}
		}

		[XmlIgnore]
		public bool fmtSpecified
		{
			get
			{
				return fmtFieldSpecified;
			}
			set
			{
				fmtFieldSpecified = value;
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string chapStyle
		{
			get
			{
				return chapStyleField;
			}
			set
			{
				chapStyleField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ChapterSep chapSep
		{
			get
			{
				return chapSepField;
			}
			set
			{
				chapSepField = value;
			}
		}

		[XmlIgnore]
		public bool chapSepSpecified
		{
			get
			{
				return chapSepFieldSpecified;
			}
			set
			{
				chapSepFieldSpecified = value;
			}
		}

		public static CT_PageNumber Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageNumber cT_PageNumber = new CT_PageNumber();
			if (node.Attributes["w:fmt"] != null)
			{
				cT_PageNumber.fmt = (ST_NumberFormat)Enum.Parse(typeof(ST_NumberFormat), node.Attributes["w:fmt"].Value);
			}
			cT_PageNumber.start = XmlHelper.ReadString(node.Attributes["w:start"]);
			cT_PageNumber.chapStyle = XmlHelper.ReadString(node.Attributes["w:chapStyle"]);
			if (node.Attributes["w:chapSep"] != null)
			{
				cT_PageNumber.chapSep = (ST_ChapterSep)Enum.Parse(typeof(ST_ChapterSep), node.Attributes["w:chapSep"].Value);
			}
			return cT_PageNumber;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:fmt", fmt.ToString());
			XmlHelper.WriteAttribute(sw, "w:start", start);
			XmlHelper.WriteAttribute(sw, "w:chapStyle", chapStyle);
			XmlHelper.WriteAttribute(sw, "w:chapSep", chapSep.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
