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
	public class CT_WebPublishObject
	{
		private uint idField;

		private string divIdField;

		private string sourceObjectField;

		private string destinationFileField;

		private string titleField;

		private bool autoRepublishField;

		[XmlAnyAttribute]
		public uint id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAnyAttribute]
		public string divId
		{
			get
			{
				return divIdField;
			}
			set
			{
				divIdField = value;
			}
		}

		[XmlAnyAttribute]
		public string sourceObject
		{
			get
			{
				return sourceObjectField;
			}
			set
			{
				sourceObjectField = value;
			}
		}

		[XmlAnyAttribute]
		public string destinationFile
		{
			get
			{
				return destinationFileField;
			}
			set
			{
				destinationFileField = value;
			}
		}

		[XmlAnyAttribute]
		public string title
		{
			get
			{
				return titleField;
			}
			set
			{
				titleField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAnyAttribute]
		public bool autoRepublish
		{
			get
			{
				return autoRepublishField;
			}
			set
			{
				autoRepublishField = value;
			}
		}

		public CT_WebPublishObject()
		{
			autoRepublishField = false;
		}

		public static CT_WebPublishObject Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_WebPublishObject cT_WebPublishObject = new CT_WebPublishObject();
			cT_WebPublishObject.id = XmlHelper.ReadUInt(node.Attributes["id"]);
			cT_WebPublishObject.divId = XmlHelper.ReadString(node.Attributes["divId"]);
			cT_WebPublishObject.sourceObject = XmlHelper.ReadString(node.Attributes["sourceObject"]);
			cT_WebPublishObject.destinationFile = XmlHelper.ReadString(node.Attributes["destinationFile"]);
			cT_WebPublishObject.title = XmlHelper.ReadString(node.Attributes["title"]);
			cT_WebPublishObject.autoRepublish = XmlHelper.ReadBool(node.Attributes["autoRepublish"]);
			return cT_WebPublishObject;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "id", id);
			XmlHelper.WriteAttribute(sw, "divId", divId);
			XmlHelper.WriteAttribute(sw, "sourceObject", sourceObject);
			XmlHelper.WriteAttribute(sw, "destinationFile", destinationFile);
			XmlHelper.WriteAttribute(sw, "title", title);
			XmlHelper.WriteAttribute(sw, "autoRepublish", autoRepublish);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
