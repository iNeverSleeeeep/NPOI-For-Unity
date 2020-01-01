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
	public class CT_TrackChangeNumbering : CT_TrackChange
	{
		private string originalField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string original
		{
			get
			{
				return originalField;
			}
			set
			{
				originalField = value;
			}
		}

		public new static CT_TrackChangeNumbering Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TrackChangeNumbering cT_TrackChangeNumbering = new CT_TrackChangeNumbering();
			cT_TrackChangeNumbering.original = XmlHelper.ReadString(node.Attributes["original"]);
			cT_TrackChangeNumbering.author = XmlHelper.ReadString(node.Attributes["author"]);
			cT_TrackChangeNumbering.date = XmlHelper.ReadString(node.Attributes["date"]);
			cT_TrackChangeNumbering.id = XmlHelper.ReadString(node.Attributes["id"]);
			return cT_TrackChangeNumbering;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "original", original);
			XmlHelper.WriteAttribute(sw, "author", base.author);
			XmlHelper.WriteAttribute(sw, "date", base.date);
			XmlHelper.WriteAttribute(sw, "id", base.id);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
