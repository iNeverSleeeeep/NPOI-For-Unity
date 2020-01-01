using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_BlurEffect
	{
		private long radField;

		private bool growField;

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
		public long rad
		{
			get
			{
				return radField;
			}
			set
			{
				radField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool grow
		{
			get
			{
				return growField;
			}
			set
			{
				growField = value;
			}
		}

		public static CT_BlurEffect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BlurEffect cT_BlurEffect = new CT_BlurEffect();
			cT_BlurEffect.rad = XmlHelper.ReadLong(node.Attributes["rad"]);
			cT_BlurEffect.grow = XmlHelper.ReadBool(node.Attributes["grow"]);
			return cT_BlurEffect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "rad", (double)rad);
			XmlHelper.WriteAttribute(sw, "grow", grow);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_BlurEffect()
		{
			radField = 0L;
			growField = true;
		}
	}
}
