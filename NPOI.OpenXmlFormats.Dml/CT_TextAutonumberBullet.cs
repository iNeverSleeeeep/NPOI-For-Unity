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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_TextAutonumberBullet
	{
		private ST_TextAutonumberScheme typeField;

		private int startAtField;

		[XmlAttribute]
		public ST_TextAutonumberScheme type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[DefaultValue(1)]
		[XmlAttribute]
		public int startAt
		{
			get
			{
				return startAtField;
			}
			set
			{
				startAtField = value;
			}
		}

		public static CT_TextAutonumberBullet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextAutonumberBullet cT_TextAutonumberBullet = new CT_TextAutonumberBullet();
			if (node.Attributes["type"] != null)
			{
				cT_TextAutonumberBullet.type = (ST_TextAutonumberScheme)Enum.Parse(typeof(ST_TextAutonumberScheme), node.Attributes["type"].Value);
			}
			cT_TextAutonumberBullet.startAt = XmlHelper.ReadInt(node.Attributes["startAt"]);
			return cT_TextAutonumberBullet;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			XmlHelper.WriteAttribute(sw, "startAt", startAt);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_TextAutonumberBullet()
		{
			startAtField = 1;
		}
	}
}
