using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_OleObject
	{
		private string progIdField;

		private ST_DvAspect dvAspectField;

		private string linkField;

		private ST_OleUpdate oleUpdateField;

		private bool oleUpdateFieldSpecified;

		private bool autoLoadField;

		private uint shapeIdField;

		private string idField;

		public string progId
		{
			get
			{
				return progIdField;
			}
			set
			{
				progIdField = value;
			}
		}

		[DefaultValue(ST_DvAspect.DVASPECT_CONTENT)]
		public ST_DvAspect dvAspect
		{
			get
			{
				return dvAspectField;
			}
			set
			{
				dvAspectField = value;
			}
		}

		public string link
		{
			get
			{
				return linkField;
			}
			set
			{
				linkField = value;
			}
		}

		public ST_OleUpdate oleUpdate
		{
			get
			{
				return oleUpdateField;
			}
			set
			{
				oleUpdateField = value;
			}
		}

		[XmlIgnore]
		public bool oleUpdateSpecified
		{
			get
			{
				return oleUpdateFieldSpecified;
			}
			set
			{
				oleUpdateFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		public bool autoLoad
		{
			get
			{
				return autoLoadField;
			}
			set
			{
				autoLoadField = value;
			}
		}

		public uint shapeId
		{
			get
			{
				return shapeIdField;
			}
			set
			{
				shapeIdField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
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

		public CT_OleObject()
		{
			dvAspectField = ST_DvAspect.DVASPECT_CONTENT;
			autoLoadField = false;
		}

		public static CT_OleObject Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OleObject cT_OleObject = new CT_OleObject();
			cT_OleObject.progId = XmlHelper.ReadString(node.Attributes["progId"]);
			if (node.Attributes["dvAspect"] != null)
			{
				cT_OleObject.dvAspect = (ST_DvAspect)Enum.Parse(typeof(ST_DvAspect), node.Attributes["dvAspect"].Value);
			}
			cT_OleObject.link = XmlHelper.ReadString(node.Attributes["link"]);
			if (node.Attributes["oleUpdate"] != null)
			{
				cT_OleObject.oleUpdate = (ST_OleUpdate)Enum.Parse(typeof(ST_OleUpdate), node.Attributes["oleUpdate"].Value);
			}
			cT_OleObject.autoLoad = XmlHelper.ReadBool(node.Attributes["autoLoad"]);
			cT_OleObject.shapeId = XmlHelper.ReadUInt(node.Attributes["shapeId"]);
			cT_OleObject.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			return cT_OleObject;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "progId", progId);
			XmlHelper.WriteAttribute(sw, "dvAspect", dvAspect.ToString());
			XmlHelper.WriteAttribute(sw, "link", link);
			XmlHelper.WriteAttribute(sw, "oleUpdate", oleUpdate.ToString());
			XmlHelper.WriteAttribute(sw, "autoLoad", autoLoad);
			XmlHelper.WriteAttribute(sw, "shapeId", shapeId);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
