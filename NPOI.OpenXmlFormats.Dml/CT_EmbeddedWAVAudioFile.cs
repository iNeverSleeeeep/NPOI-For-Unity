using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_EmbeddedWAVAudioFile
	{
		private string embedField;

		private string nameField;

		private bool builtInField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string embed
		{
			get
			{
				return embedField;
			}
			set
			{
				embedField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool builtIn
		{
			get
			{
				return builtInField;
			}
			set
			{
				builtInField = value;
			}
		}

		public CT_EmbeddedWAVAudioFile()
		{
			nameField = "";
			builtInField = false;
		}

		public static CT_EmbeddedWAVAudioFile Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EmbeddedWAVAudioFile cT_EmbeddedWAVAudioFile = new CT_EmbeddedWAVAudioFile();
			cT_EmbeddedWAVAudioFile.embed = XmlHelper.ReadString(node.Attributes["embed"]);
			cT_EmbeddedWAVAudioFile.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_EmbeddedWAVAudioFile.builtIn = XmlHelper.ReadBool(node.Attributes["builtIn"]);
			return cT_EmbeddedWAVAudioFile;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "embed", embed);
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "builtIn", builtIn);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
