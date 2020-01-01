using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_PivotCache
	{
		private uint cacheIdField;

		private string idField;

		[XmlAttribute]
		public uint cacheId
		{
			get
			{
				return cacheIdField;
			}
			set
			{
				cacheIdField = value;
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

		public static CT_PivotCache Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PivotCache cT_PivotCache = new CT_PivotCache();
			cT_PivotCache.cacheId = XmlHelper.ReadUInt(node.Attributes["cacheId"]);
			cT_PivotCache.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			return cT_PivotCache;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "cacheId", cacheId);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
