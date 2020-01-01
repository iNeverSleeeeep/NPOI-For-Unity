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
	public class CT_TblGridCol
	{
		private ulong wField;

		private bool wFieldSpecified;

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

		public static CT_TblGridCol Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblGridCol cT_TblGridCol = new CT_TblGridCol();
			cT_TblGridCol.w = XmlHelper.ReadULong(node.Attributes["w:w"]);
			return cT_TblGridCol;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:w", (double)w);
			sw.Write("/>");
		}
	}
}
