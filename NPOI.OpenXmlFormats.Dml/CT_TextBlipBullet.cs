using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TextBlipBullet
	{
		private CT_Blip blipField;

		[XmlElement(Order = 0)]
		public CT_Blip blip
		{
			get
			{
				return blipField;
			}
			set
			{
				blipField = value;
			}
		}

		public static CT_TextBlipBullet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextBlipBullet cT_TextBlipBullet = new CT_TextBlipBullet();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "blip")
				{
					cT_TextBlipBullet.blip = CT_Blip.Parse(childNode, namespaceManager);
				}
			}
			return cT_TextBlipBullet;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (blip != null)
			{
				blip.Write(sw, "blip");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_TextBlipBullet()
		{
			blipField = new CT_Blip();
		}
	}
}
