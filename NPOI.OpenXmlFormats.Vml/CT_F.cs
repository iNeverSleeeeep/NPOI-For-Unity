using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_F
	{
		private string eqnField;

		[XmlAttribute]
		public string eqn
		{
			get
			{
				return eqnField;
			}
			set
			{
				eqnField = value;
			}
		}

		[XmlIgnore]
		public bool eqnSpecified
		{
			get
			{
				return null != eqnField;
			}
		}

		public static CT_F Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_F cT_F = new CT_F();
			cT_F.eqn = XmlHelper.ReadString(node.Attributes["eqn"]);
			return cT_F;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "eqn", eqn);
			sw.Write(">");
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
