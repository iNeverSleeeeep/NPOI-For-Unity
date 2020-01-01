using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_FtnEdnSepRef
	{
		private string idField;

		[XmlAttribute(DataType = "integer")]
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

		public static CT_FtnEdnSepRef Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FtnEdnSepRef cT_FtnEdnSepRef = new CT_FtnEdnSepRef();
			cT_FtnEdnSepRef.id = XmlHelper.ReadString(node.Attributes["w:id"]);
			return cT_FtnEdnSepRef;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:id", id);
			sw.Write("/>");
		}
	}
}
