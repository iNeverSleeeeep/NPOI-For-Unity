using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_AltChunk
	{
		private CT_AltChunkPr altChunkPrField;

		private string idField;

		[XmlElement(Order = 0)]
		public CT_AltChunkPr altChunkPr
		{
			get
			{
				return altChunkPrField;
			}
			set
			{
				altChunkPrField = value;
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

		public static CT_AltChunk Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AltChunk cT_AltChunk = new CT_AltChunk();
			cT_AltChunk.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "altChunkPr")
				{
					cT_AltChunk.altChunkPr = CT_AltChunkPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_AltChunk;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			if (altChunkPr != null)
			{
				altChunkPr.Write(sw, "altChunkPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
