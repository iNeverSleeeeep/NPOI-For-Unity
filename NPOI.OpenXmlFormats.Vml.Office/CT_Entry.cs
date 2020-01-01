using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	public class CT_Entry
	{
		private int? newField = null;

		private int? oldField = null;

		[XmlAttribute]
		public int @new
		{
			get
			{
				return newField.Value;
			}
			set
			{
				newField = value;
			}
		}

		[XmlIgnore]
		public bool newSpecified
		{
			get
			{
				return newField.HasValue;
			}
		}

		[XmlAttribute]
		public int old
		{
			get
			{
				return oldField.Value;
			}
			set
			{
				oldField = value;
			}
		}

		[XmlIgnore]
		public bool oldSpecified
		{
			get
			{
				return oldField.HasValue;
			}
		}

		public static CT_Entry Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Entry cT_Entry = new CT_Entry();
			if (node.Attributes["new"] != null)
			{
				cT_Entry.@new = XmlHelper.ReadInt(node.Attributes["new"]);
			}
			if (node.Attributes["old"] != null)
			{
				cT_Entry.old = XmlHelper.ReadInt(node.Attributes["old"]);
			}
			return cT_Entry;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<o:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "new", @new);
			XmlHelper.WriteAttribute(sw, "old", old);
			sw.Write(">");
			sw.Write(string.Format("</o:{0}>", nodeName));
		}
	}
}
