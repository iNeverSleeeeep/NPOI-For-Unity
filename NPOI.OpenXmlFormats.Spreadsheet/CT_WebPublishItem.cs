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
	public class CT_WebPublishItem
	{
		private uint idField;

		private string divIdField;

		private ST_WebSourceType sourceTypeField;

		private string sourceRefField;

		private string sourceObjectField;

		private string destinationFileField;

		private string titleField;

		private bool autoRepublishField;

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

		public ST_WebSourceType sourceType
		{
			get
			{
				return sourceTypeField;
			}
			set
			{
				sourceTypeField = value;
			}
		}

		public string sourceRef
		{
			get
			{
				return sourceRefField;
			}
			set
			{
				sourceRefField = value;
			}
		}

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

		public CT_WebPublishItem()
		{
			autoRepublishField = false;
		}

		public static CT_WebPublishItem Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_WebPublishItem cT_WebPublishItem = new CT_WebPublishItem();
			if (node.Attributes["id"] != null)
			{
				cT_WebPublishItem.id = XmlHelper.ReadUInt(node.Attributes["id"]);
			}
			cT_WebPublishItem.divId = XmlHelper.ReadString(node.Attributes["divId"]);
			if (node.Attributes["sourceType"] != null)
			{
				cT_WebPublishItem.sourceType = (ST_WebSourceType)Enum.Parse(typeof(ST_WebSourceType), node.Attributes["sourceType"].Value);
			}
			cT_WebPublishItem.sourceRef = XmlHelper.ReadString(node.Attributes["sourceRef"]);
			cT_WebPublishItem.sourceObject = XmlHelper.ReadString(node.Attributes["sourceObject"]);
			cT_WebPublishItem.destinationFile = XmlHelper.ReadString(node.Attributes["destinationFile"]);
			cT_WebPublishItem.title = XmlHelper.ReadString(node.Attributes["title"]);
			if (node.Attributes["autoRepublish"] != null)
			{
				cT_WebPublishItem.autoRepublish = XmlHelper.ReadBool(node.Attributes["autoRepublish"]);
			}
			return cT_WebPublishItem;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "id", id);
			XmlHelper.WriteAttribute(sw, "divId", divId);
			XmlHelper.WriteAttribute(sw, "sourceType", sourceType.ToString());
			XmlHelper.WriteAttribute(sw, "sourceRef", sourceRef);
			XmlHelper.WriteAttribute(sw, "sourceObject", sourceObject);
			XmlHelper.WriteAttribute(sw, "destinationFile", destinationFile);
			XmlHelper.WriteAttribute(sw, "title", title);
			if (autoRepublish)
			{
				XmlHelper.WriteAttribute(sw, "autoRepublish", autoRepublish);
			}
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
