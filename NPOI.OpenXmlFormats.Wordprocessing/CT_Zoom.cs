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
	public class CT_Zoom
	{
		private ST_Zoom valField;

		private bool valFieldSpecified;

		private string percentField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Zoom val
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

		[XmlIgnore]
		public bool valSpecified
		{
			get
			{
				return valFieldSpecified;
			}
			set
			{
				valFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string percent
		{
			get
			{
				return percentField;
			}
			set
			{
				percentField = value;
			}
		}

		public CT_Zoom()
		{
			valField = ST_Zoom.none;
			percent = "100";
		}

		public static CT_Zoom Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Zoom cT_Zoom = new CT_Zoom();
			if (node.Attributes["w:val"] != null)
			{
				cT_Zoom.val = (ST_Zoom)Enum.Parse(typeof(ST_Zoom), node.Attributes["w:val"].Value);
			}
			cT_Zoom.percent = XmlHelper.ReadString(node.Attributes["w:percent"]);
			return cT_Zoom;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			if (val != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:percent", percent);
			sw.Write("/>");
		}
	}
}
