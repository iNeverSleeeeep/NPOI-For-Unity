using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_UnderlineProperty
	{
		private ST_UnderlineValues? valField = null;

		[XmlAttribute]
		[DefaultValue(ST_UnderlineValues.single)]
		public ST_UnderlineValues val
		{
			get
			{
				if (valField.HasValue)
				{
					return valField.Value;
				}
				return ST_UnderlineValues.single;
			}
			set
			{
				valField = value;
			}
		}

		[XmlIgnore]
		public bool valbSpecified
		{
			get
			{
				return valField.HasValue;
			}
		}

		public CT_UnderlineProperty()
		{
			valField = ST_UnderlineValues.single;
		}

		public static CT_UnderlineProperty Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_UnderlineProperty cT_UnderlineProperty = new CT_UnderlineProperty();
			if (node.Attributes["val"] != null)
			{
				cT_UnderlineProperty.val = (ST_UnderlineValues)Enum.Parse(typeof(ST_UnderlineValues), node.Attributes["val"].Value);
			}
			return cT_UnderlineProperty;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write("/>");
		}
	}
}
