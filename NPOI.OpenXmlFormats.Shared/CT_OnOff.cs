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
	public class CT_OnOff
	{
		private ST_OnOff valField;

		private bool valFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff val
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

		public static CT_OnOff Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OnOff cT_OnOff = new CT_OnOff();
			if (node.Attributes["m:val"] != null)
			{
				cT_OnOff.val = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["m:val"].Value);
			}
			return cT_OnOff;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			if (val == ST_OnOff.off || val == ST_OnOff.Value0)
			{
				XmlHelper.WriteAttribute(sw, "m:val", "0");
			}
			else
			{
				XmlHelper.WriteAttribute(sw, "m:val", "1");
			}
			sw.Write("/>");
		}
	}
}
