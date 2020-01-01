using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_Textbox
	{
		private string itemField;

		private string idField;

		private string styleField;

		private string insetField;

		public string ItemXml
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}

		[XmlAttribute]
		public string id
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
		public string style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlAttribute]
		public string inset
		{
			get
			{
				return insetField;
			}
			set
			{
				insetField = value;
			}
		}

		public static CT_Textbox Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Textbox cT_Textbox = new CT_Textbox();
			cT_Textbox.id = XmlHelper.ReadString(node.Attributes["id"]);
			cT_Textbox.style = XmlHelper.ReadString(node.Attributes["style"]);
			cT_Textbox.inset = XmlHelper.ReadString(node.Attributes["inset"]);
			cT_Textbox.ItemXml = node.InnerXml;
			return cT_Textbox;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "id", id);
			XmlHelper.WriteAttribute(sw, "style", style);
			XmlHelper.WriteAttribute(sw, "inset", inset);
			sw.Write(">");
			if (ItemXml != null)
			{
				sw.Write(ItemXml);
			}
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
