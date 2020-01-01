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
	public class CT_PageSz
	{
		private ulong wField;

		private bool wFieldSpecified;

		private ulong hField;

		private bool hFieldSpecified;

		private ST_PageOrientation orientField;

		private bool orientFieldSpecified;

		private string codeField;

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

		[XmlIgnore]
		public bool hSpecified
		{
			get
			{
				return hFieldSpecified;
			}
			set
			{
				hFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_PageOrientation orient
		{
			get
			{
				return orientField;
			}
			set
			{
				orientField = value;
			}
		}

		[XmlIgnore]
		public bool orientSpecified
		{
			get
			{
				return orientFieldSpecified;
			}
			set
			{
				orientFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string code
		{
			get
			{
				return codeField;
			}
			set
			{
				codeField = value;
			}
		}

		public static CT_PageSz Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageSz cT_PageSz = new CT_PageSz();
			cT_PageSz.w = XmlHelper.ReadULong(node.Attributes["w:w"]);
			cT_PageSz.h = XmlHelper.ReadULong(node.Attributes["w:h"]);
			if (node.Attributes["w:orient"] != null)
			{
				cT_PageSz.orient = (ST_PageOrientation)Enum.Parse(typeof(ST_PageOrientation), node.Attributes["w:orient"].Value);
			}
			cT_PageSz.code = XmlHelper.ReadString(node.Attributes["w:code"]);
			return cT_PageSz;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:w", (double)w);
			XmlHelper.WriteAttribute(sw, "w:h", (double)h);
			if (orientField != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:orient", orient.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:code", code);
			sw.Write("/>");
		}
	}
}
