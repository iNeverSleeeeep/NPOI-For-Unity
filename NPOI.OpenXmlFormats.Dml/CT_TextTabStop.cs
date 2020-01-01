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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DebuggerStepThrough]
	public class CT_TextTabStop
	{
		private int posField;

		private bool posFieldSpecified;

		private ST_TextTabAlignType algnField;

		private bool algnFieldSpecified;

		[XmlAttribute]
		public int pos
		{
			get
			{
				return posField;
			}
			set
			{
				posField = value;
			}
		}

		[XmlIgnore]
		public bool posSpecified
		{
			get
			{
				return posFieldSpecified;
			}
			set
			{
				posFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextTabAlignType algn
		{
			get
			{
				return algnField;
			}
			set
			{
				algnField = value;
			}
		}

		[XmlIgnore]
		public bool algnSpecified
		{
			get
			{
				return algnFieldSpecified;
			}
			set
			{
				algnFieldSpecified = value;
			}
		}

		public static CT_TextTabStop Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextTabStop cT_TextTabStop = new CT_TextTabStop();
			cT_TextTabStop.pos = XmlHelper.ReadInt(node.Attributes["pos"]);
			if (node.Attributes["algn"] != null)
			{
				cT_TextTabStop.algn = (ST_TextTabAlignType)Enum.Parse(typeof(ST_TextTabAlignType), node.Attributes["algn"].Value);
			}
			return cT_TextTabStop;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "pos", pos);
			XmlHelper.WriteAttribute(sw, "algn", algn.ToString());
			sw.Write("/>");
		}
	}
}
