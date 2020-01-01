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
	public class CT_ColorSchemeMapping
	{
		private ST_ColorSchemeIndex bg1Field;

		private bool bg1FieldSpecified;

		private ST_ColorSchemeIndex t1Field;

		private bool t1FieldSpecified;

		private ST_ColorSchemeIndex bg2Field;

		private bool bg2FieldSpecified;

		private ST_ColorSchemeIndex t2Field;

		private bool t2FieldSpecified;

		private ST_ColorSchemeIndex accent1Field;

		private bool accent1FieldSpecified;

		private ST_ColorSchemeIndex accent2Field;

		private bool accent2FieldSpecified;

		private ST_ColorSchemeIndex accent3Field;

		private bool accent3FieldSpecified;

		private ST_ColorSchemeIndex accent4Field;

		private bool accent4FieldSpecified;

		private ST_ColorSchemeIndex accent5Field;

		private bool accent5FieldSpecified;

		private ST_ColorSchemeIndex accent6Field;

		private bool accent6FieldSpecified;

		private ST_ColorSchemeIndex hyperlinkField;

		private bool hyperlinkFieldSpecified;

		private ST_ColorSchemeIndex followedHyperlinkField;

		private bool followedHyperlinkFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex bg1
		{
			get
			{
				return bg1Field;
			}
			set
			{
				bg1Field = value;
			}
		}

		[XmlIgnore]
		public bool bg1Specified
		{
			get
			{
				return bg1FieldSpecified;
			}
			set
			{
				bg1FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex t1
		{
			get
			{
				return t1Field;
			}
			set
			{
				t1Field = value;
			}
		}

		[XmlIgnore]
		public bool t1Specified
		{
			get
			{
				return t1FieldSpecified;
			}
			set
			{
				t1FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex bg2
		{
			get
			{
				return bg2Field;
			}
			set
			{
				bg2Field = value;
			}
		}

		[XmlIgnore]
		public bool bg2Specified
		{
			get
			{
				return bg2FieldSpecified;
			}
			set
			{
				bg2FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex t2
		{
			get
			{
				return t2Field;
			}
			set
			{
				t2Field = value;
			}
		}

		[XmlIgnore]
		public bool t2Specified
		{
			get
			{
				return t2FieldSpecified;
			}
			set
			{
				t2FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex accent1
		{
			get
			{
				return accent1Field;
			}
			set
			{
				accent1Field = value;
			}
		}

		[XmlIgnore]
		public bool accent1Specified
		{
			get
			{
				return accent1FieldSpecified;
			}
			set
			{
				accent1FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex accent2
		{
			get
			{
				return accent2Field;
			}
			set
			{
				accent2Field = value;
			}
		}

		[XmlIgnore]
		public bool accent2Specified
		{
			get
			{
				return accent2FieldSpecified;
			}
			set
			{
				accent2FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex accent3
		{
			get
			{
				return accent3Field;
			}
			set
			{
				accent3Field = value;
			}
		}

		[XmlIgnore]
		public bool accent3Specified
		{
			get
			{
				return accent3FieldSpecified;
			}
			set
			{
				accent3FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex accent4
		{
			get
			{
				return accent4Field;
			}
			set
			{
				accent4Field = value;
			}
		}

		[XmlIgnore]
		public bool accent4Specified
		{
			get
			{
				return accent4FieldSpecified;
			}
			set
			{
				accent4FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex accent5
		{
			get
			{
				return accent5Field;
			}
			set
			{
				accent5Field = value;
			}
		}

		[XmlIgnore]
		public bool accent5Specified
		{
			get
			{
				return accent5FieldSpecified;
			}
			set
			{
				accent5FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex accent6
		{
			get
			{
				return accent6Field;
			}
			set
			{
				accent6Field = value;
			}
		}

		[XmlIgnore]
		public bool accent6Specified
		{
			get
			{
				return accent6FieldSpecified;
			}
			set
			{
				accent6FieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex hyperlink
		{
			get
			{
				return hyperlinkField;
			}
			set
			{
				hyperlinkField = value;
			}
		}

		[XmlIgnore]
		public bool hyperlinkSpecified
		{
			get
			{
				return hyperlinkFieldSpecified;
			}
			set
			{
				hyperlinkFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_ColorSchemeIndex followedHyperlink
		{
			get
			{
				return followedHyperlinkField;
			}
			set
			{
				followedHyperlinkField = value;
			}
		}

		[XmlIgnore]
		public bool followedHyperlinkSpecified
		{
			get
			{
				return followedHyperlinkFieldSpecified;
			}
			set
			{
				followedHyperlinkFieldSpecified = value;
			}
		}

		public static CT_ColorSchemeMapping Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ColorSchemeMapping cT_ColorSchemeMapping = new CT_ColorSchemeMapping();
			if (node.Attributes["w:bg1"] != null)
			{
				cT_ColorSchemeMapping.bg1 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:bg1"].Value);
			}
			if (node.Attributes["w:t1"] != null)
			{
				cT_ColorSchemeMapping.t1 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:t1"].Value);
			}
			if (node.Attributes["w:bg2"] != null)
			{
				cT_ColorSchemeMapping.bg2 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:bg2"].Value);
			}
			if (node.Attributes["w:t2"] != null)
			{
				cT_ColorSchemeMapping.t2 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:t2"].Value);
			}
			if (node.Attributes["w:accent1"] != null)
			{
				cT_ColorSchemeMapping.accent1 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:accent1"].Value);
			}
			if (node.Attributes["w:accent2"] != null)
			{
				cT_ColorSchemeMapping.accent2 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:accent2"].Value);
			}
			if (node.Attributes["w:accent3"] != null)
			{
				cT_ColorSchemeMapping.accent3 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:accent3"].Value);
			}
			if (node.Attributes["w:accent4"] != null)
			{
				cT_ColorSchemeMapping.accent4 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:accent4"].Value);
			}
			if (node.Attributes["w:accent5"] != null)
			{
				cT_ColorSchemeMapping.accent5 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:accent5"].Value);
			}
			if (node.Attributes["w:accent6"] != null)
			{
				cT_ColorSchemeMapping.accent6 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:accent6"].Value);
			}
			if (node.Attributes["w:hyperlink"] != null)
			{
				cT_ColorSchemeMapping.hyperlink = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:hyperlink"].Value);
			}
			if (node.Attributes["w:followedHyperlink"] != null)
			{
				cT_ColorSchemeMapping.followedHyperlink = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["w:followedHyperlink"].Value);
			}
			return cT_ColorSchemeMapping;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:bg1", bg1.ToString());
			XmlHelper.WriteAttribute(sw, "w:t1", t1.ToString());
			XmlHelper.WriteAttribute(sw, "w:bg2", bg2.ToString());
			XmlHelper.WriteAttribute(sw, "w:t2", t2.ToString());
			XmlHelper.WriteAttribute(sw, "w:accent1", accent1.ToString());
			XmlHelper.WriteAttribute(sw, "w:accent2", accent2.ToString());
			XmlHelper.WriteAttribute(sw, "w:accent3", accent3.ToString());
			XmlHelper.WriteAttribute(sw, "w:accent4", accent4.ToString());
			XmlHelper.WriteAttribute(sw, "w:accent5", accent5.ToString());
			XmlHelper.WriteAttribute(sw, "w:accent6", accent6.ToString());
			XmlHelper.WriteAttribute(sw, "w:hyperlink", hyperlink.ToString());
			XmlHelper.WriteAttribute(sw, "w:followedHyperlink", followedHyperlink.ToString());
			sw.Write("/>");
		}
	}
}
