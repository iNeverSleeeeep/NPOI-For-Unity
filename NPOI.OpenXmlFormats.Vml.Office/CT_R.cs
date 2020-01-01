using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	public class CT_R
	{
		private List<CT_Proxy> proxyField;

		private string idField = string.Empty;

		private ST_RType typeField;

		private ST_How howField;

		private string idrefField;

		[XmlElement("proxy")]
		public List<CT_Proxy> proxy
		{
			get
			{
				return proxyField;
			}
			set
			{
				proxyField = value;
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
		public ST_RType type
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

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return ST_RType.NONE != typeField;
			}
		}

		[XmlAttribute]
		public ST_How how
		{
			get
			{
				return howField;
			}
			set
			{
				howField = value;
			}
		}

		[XmlIgnore]
		public bool howSpecified
		{
			get
			{
				return ST_How.NONE != howField;
			}
		}

		[XmlAttribute]
		public string idref
		{
			get
			{
				return idrefField;
			}
			set
			{
				idrefField = value;
			}
		}

		[XmlIgnore]
		public bool idrefSpecified
		{
			get
			{
				return null != idrefField;
			}
		}

		public static CT_R Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_R cT_R = new CT_R();
			cT_R.id = XmlHelper.ReadString(node.Attributes["id"]);
			if (node.Attributes["type"] != null)
			{
				cT_R.type = (ST_RType)Enum.Parse(typeof(ST_RType), node.Attributes["type"].Value);
			}
			if (node.Attributes["how"] != null)
			{
				cT_R.how = (ST_How)Enum.Parse(typeof(ST_How), node.Attributes["how"].Value);
			}
			cT_R.idref = XmlHelper.ReadString(node.Attributes["idref"]);
			cT_R.proxy = new List<CT_Proxy>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "proxy")
				{
					cT_R.proxy.Add(CT_Proxy.Parse(childNode, namespaceManager));
				}
			}
			return cT_R;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<o:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "id", id);
			if (type != 0)
			{
				XmlHelper.WriteAttribute(sw, "type", type.ToString());
			}
			if (how != 0)
			{
				XmlHelper.WriteAttribute(sw, "how", how.ToString());
			}
			XmlHelper.WriteAttribute(sw, "idref", idref);
			sw.Write(">");
			if (proxy != null)
			{
				foreach (CT_Proxy item in proxy)
				{
					item.Write(sw, "proxy");
				}
			}
			sw.Write(string.Format("</o:{0}>", nodeName));
		}
	}
}
