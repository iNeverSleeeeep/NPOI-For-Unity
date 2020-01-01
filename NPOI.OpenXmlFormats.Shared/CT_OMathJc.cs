using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_OMathJc
	{
		private ST_Jc valField;

		private bool valFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Jc val
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

		public static CT_OMathJc Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OMathJc cT_OMathJc = new CT_OMathJc();
			if (node.Attributes["m:val"] != null)
			{
				cT_OMathJc.val = (ST_Jc)Enum.Parse(typeof(ST_Jc), node.Attributes["m:val"].Value);
			}
			return cT_OMathJc;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "m:val", val.ToString());
			sw.Write("/>");
		}
	}
}
