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
	public class CT_ChartsheetProtection
	{
		private byte[] passwordField;

		private bool contentField;

		private bool objectsField;

		public byte[] password
		{
			get
			{
				return passwordField;
			}
			set
			{
				passwordField = value;
			}
		}

		[DefaultValue(false)]
		public bool content
		{
			get
			{
				return contentField;
			}
			set
			{
				contentField = value;
			}
		}

		[DefaultValue(false)]
		public bool objects
		{
			get
			{
				return objectsField;
			}
			set
			{
				objectsField = value;
			}
		}

		public CT_ChartsheetProtection()
		{
			contentField = false;
			objectsField = false;
		}

		public static CT_ChartsheetProtection Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ChartsheetProtection cT_ChartsheetProtection = new CT_ChartsheetProtection();
			if (node.Attributes["password"] != null)
			{
				cT_ChartsheetProtection.password = XmlHelper.ReadBytes(node.Attributes["password"]);
			}
			if (node.Attributes["content"] != null)
			{
				cT_ChartsheetProtection.content = XmlHelper.ReadBool(node.Attributes["content"]);
			}
			if (node.Attributes["objects"] != null)
			{
				cT_ChartsheetProtection.objects = XmlHelper.ReadBool(node.Attributes["objects"]);
			}
			return cT_ChartsheetProtection;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "password", password);
			if (content)
			{
				XmlHelper.WriteAttribute(sw, "content", content);
			}
			if (objects)
			{
				XmlHelper.WriteAttribute(sw, "objects", objects);
			}
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
