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
	public class CT_VMerge
	{
		private ST_Merge valField;

		private bool valFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Merge val
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

		public CT_VMerge()
		{
			valField = ST_Merge.@continue;
		}

		public static CT_VMerge Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_VMerge cT_VMerge = new CT_VMerge();
			if (node.Attributes["w:val"] != null)
			{
				cT_VMerge.val = (ST_Merge)Enum.Parse(typeof(ST_Merge), node.Attributes["w:val"].Value);
			}
			return cT_VMerge;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			if (valField != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			}
			sw.Write("/>");
		}
	}
}
