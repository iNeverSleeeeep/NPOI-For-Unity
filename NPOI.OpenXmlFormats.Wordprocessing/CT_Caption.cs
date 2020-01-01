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
	public class CT_Caption
	{
		private string nameField;

		private ST_CaptionPos posField;

		private bool posFieldSpecified;

		private ST_OnOff chapNumField;

		private bool chapNumFieldSpecified;

		private string headingField;

		private ST_OnOff noLabelField;

		private bool noLabelFieldSpecified;

		private ST_NumberFormat numFmtField;

		private bool numFmtFieldSpecified;

		private ST_ChapterSep sepField;

		private bool sepFieldSpecified;

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
		public ST_CaptionPos pos
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

		[XmlIgnore]
		public bool posSpecified
		{
			get
			{
				return posFieldSpecified;
			}
			set
			{
				posFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff chapNum
		{
			get
			{
				return chapNumField;
			}
			set
			{
				chapNumField = value;
			}
		}

		[XmlIgnore]
		public bool chapNumSpecified
		{
			get
			{
				return chapNumFieldSpecified;
			}
			set
			{
				chapNumFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string heading
		{
			get
			{
				return headingField;
			}
			set
			{
				headingField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff noLabel
		{
			get
			{
				return noLabelField;
			}
			set
			{
				noLabelField = value;
			}
		}

		[XmlIgnore]
		public bool noLabelSpecified
		{
			get
			{
				return noLabelFieldSpecified;
			}
			set
			{
				noLabelFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_NumberFormat numFmt
		{
			get
			{
				return numFmtField;
			}
			set
			{
				numFmtField = value;
			}
		}

		[XmlIgnore]
		public bool numFmtSpecified
		{
			get
			{
				return numFmtFieldSpecified;
			}
			set
			{
				numFmtFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ChapterSep sep
		{
			get
			{
				return sepField;
			}
			set
			{
				sepField = value;
			}
		}

		[XmlIgnore]
		public bool sepSpecified
		{
			get
			{
				return sepFieldSpecified;
			}
			set
			{
				sepFieldSpecified = value;
			}
		}

		public static CT_Caption Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Caption cT_Caption = new CT_Caption();
			cT_Caption.name = XmlHelper.ReadString(node.Attributes["w:name"]);
			if (node.Attributes["w:pos"] != null)
			{
				cT_Caption.pos = (ST_CaptionPos)Enum.Parse(typeof(ST_CaptionPos), node.Attributes["w:pos"].Value);
			}
			if (node.Attributes["w:chapNum"] != null)
			{
				cT_Caption.chapNum = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:chapNum"].Value);
			}
			cT_Caption.heading = XmlHelper.ReadString(node.Attributes["w:heading"]);
			if (node.Attributes["w:noLabel"] != null)
			{
				cT_Caption.noLabel = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:noLabel"].Value);
			}
			if (node.Attributes["w:numFmt"] != null)
			{
				cT_Caption.numFmt = (ST_NumberFormat)Enum.Parse(typeof(ST_NumberFormat), node.Attributes["w:numFmt"].Value);
			}
			if (node.Attributes["w:sep"] != null)
			{
				cT_Caption.sep = (ST_ChapterSep)Enum.Parse(typeof(ST_ChapterSep), node.Attributes["w:sep"].Value);
			}
			return cT_Caption;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:name", name);
			XmlHelper.WriteAttribute(sw, "w:pos", pos.ToString());
			XmlHelper.WriteAttribute(sw, "w:chapNum", chapNum.ToString());
			XmlHelper.WriteAttribute(sw, "w:heading", heading);
			XmlHelper.WriteAttribute(sw, "w:noLabel", noLabel.ToString());
			XmlHelper.WriteAttribute(sw, "w:numFmt", numFmt.ToString());
			XmlHelper.WriteAttribute(sw, "w:sep", sep.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
