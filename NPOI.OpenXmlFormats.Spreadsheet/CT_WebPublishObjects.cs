using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_WebPublishObjects
	{
		private List<CT_WebPublishObject> webPublishObjectField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_WebPublishObject> webPublishObject
		{
			get
			{
				return webPublishObjectField;
			}
			set
			{
				webPublishObjectField = value;
			}
		}

		[XmlAttribute]
		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		[XmlIgnore]
		public bool countSpecified
		{
			get
			{
				return countFieldSpecified;
			}
			set
			{
				countFieldSpecified = value;
			}
		}

		public static CT_WebPublishObjects Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_WebPublishObjects cT_WebPublishObjects = new CT_WebPublishObjects();
			cT_WebPublishObjects.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_WebPublishObjects.countSpecified = (node.Attributes["count"] != null);
			cT_WebPublishObjects.webPublishObject = new List<CT_WebPublishObject>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "webPublishObject")
				{
					cT_WebPublishObjects.webPublishObject.Add(CT_WebPublishObject.Parse(childNode, namespaceManager));
				}
			}
			return cT_WebPublishObjects;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (webPublishObject != null)
			{
				foreach (CT_WebPublishObject item in webPublishObject)
				{
					item.Write(sw, "webPublishObject");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
