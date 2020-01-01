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
	public class CT_ReadingModeInkLockDown
	{
		private ST_OnOff actualPgField;

		private ulong wField;

		private ulong hField;

		private string fontSzField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff actualPg
		{
			get
			{
				return actualPgField;
			}
			set
			{
				actualPgField = value;
			}
		}

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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong h
		{
			get
			{
				return hField;
			}
			set
			{
				hField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string fontSz
		{
			get
			{
				return fontSzField;
			}
			set
			{
				fontSzField = value;
			}
		}

		public static CT_ReadingModeInkLockDown Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ReadingModeInkLockDown cT_ReadingModeInkLockDown = new CT_ReadingModeInkLockDown();
			if (node.Attributes["w:actualPg"] != null)
			{
				cT_ReadingModeInkLockDown.actualPg = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:actualPg"].Value);
			}
			cT_ReadingModeInkLockDown.w = XmlHelper.ReadULong(node.Attributes["w:w"]);
			cT_ReadingModeInkLockDown.h = XmlHelper.ReadULong(node.Attributes["w:h"]);
			cT_ReadingModeInkLockDown.fontSz = XmlHelper.ReadString(node.Attributes["w:fontSz"]);
			return cT_ReadingModeInkLockDown;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:actualPg", actualPg.ToString());
			XmlHelper.WriteAttribute(sw, "w:w", (double)w);
			XmlHelper.WriteAttribute(sw, "w:h", (double)h);
			XmlHelper.WriteAttribute(sw, "w:fontSz", fontSz);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
