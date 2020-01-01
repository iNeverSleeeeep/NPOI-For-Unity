using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	public class CT_Rules
	{
		private List<CT_R> rField;

		private ST_Ext extField;

		[XmlElement("r")]
		public List<CT_R> r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:vml")]
		public ST_Ext ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}

		[XmlIgnore]
		public bool extSpecified
		{
			get
			{
				return ST_Ext.NONE != extField;
			}
		}

		public static CT_Rules Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Rules cT_Rules = new CT_Rules();
			if (node.Attributes["v:ext"] != null)
			{
				cT_Rules.ext = (ST_Ext)Enum.Parse(typeof(ST_Ext), node.Attributes["v:ext"].Value);
			}
			cT_Rules.r = new List<CT_R>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "r")
				{
					cT_Rules.r.Add(CT_R.Parse(childNode, namespaceManager));
				}
			}
			return cT_Rules;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<o:{0}", nodeName));
			if (ext != 0)
			{
				XmlHelper.WriteAttribute(sw, "v:ext", ext.ToString());
			}
			sw.Write(">");
			if (r != null)
			{
				foreach (CT_R item in r)
				{
					item.Write(sw, "r");
				}
			}
			sw.Write(string.Format("</o:{0}>", nodeName));
		}
	}
}
