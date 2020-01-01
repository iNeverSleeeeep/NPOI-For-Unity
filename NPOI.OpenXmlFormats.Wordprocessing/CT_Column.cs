using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Column
	{
		private ulong wField;

		private bool wFieldSpecified;

		private ulong spaceField;

		private bool spaceFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		[XmlIgnore]
		public bool wSpecified
		{
			get
			{
				return wFieldSpecified;
			}
			set
			{
				wFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong space
		{
			get
			{
				return spaceField;
			}
			set
			{
				spaceField = value;
			}
		}

		[XmlIgnore]
		public bool spaceSpecified
		{
			get
			{
				return spaceFieldSpecified;
			}
			set
			{
				spaceFieldSpecified = value;
			}
		}

		public static CT_Column Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Column cT_Column = new CT_Column();
			cT_Column.w = XmlHelper.ReadULong(node.Attributes["w:w"]);
			cT_Column.space = XmlHelper.ReadULong(node.Attributes["w:space"]);
			return cT_Column;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:w", (double)w);
			XmlHelper.WriteAttribute(sw, "w:space", (double)space);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
