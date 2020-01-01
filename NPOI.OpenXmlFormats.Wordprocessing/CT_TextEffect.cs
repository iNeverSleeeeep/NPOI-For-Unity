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
	public class CT_TextEffect
	{
		private ST_TextEffect valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TextEffect val
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

		public static CT_TextEffect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextEffect cT_TextEffect = new CT_TextEffect();
			if (node.Attributes["w:val"] != null)
			{
				cT_TextEffect.val = (ST_TextEffect)Enum.Parse(typeof(ST_TextEffect), node.Attributes["w:val"].Value);
			}
			return cT_TextEffect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write("/>");
		}
	}
}
