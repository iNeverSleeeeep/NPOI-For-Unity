using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Connection
	{
		private uint idField;

		private uint idxField;

		[XmlAttribute]
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

		[XmlAttribute]
		public uint idx
		{
			get
			{
				return idxField;
			}
			set
			{
				idxField = value;
			}
		}

		public static CT_Connection Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Connection cT_Connection = new CT_Connection();
			cT_Connection.id = XmlHelper.ReadUInt(node.Attributes["id"]);
			cT_Connection.idx = XmlHelper.ReadUInt(node.Attributes["idx"]);
			return cT_Connection;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "id", id);
			XmlHelper.WriteAttribute(sw, "idx", idx);
			sw.Write("/>");
		}
	}
}
