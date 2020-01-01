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
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	public class CT_RegroupTable
	{
		private List<CT_Entry> entryField;

		private ST_Ext extField;

		[XmlElement("entry")]
		public List<CT_Entry> entry
		{
			get
			{
				return entryField;
			}
			set
			{
				entryField = value;
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

		public static CT_RegroupTable Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RegroupTable cT_RegroupTable = new CT_RegroupTable();
			if (node.Attributes["v:ext"] != null)
			{
				cT_RegroupTable.ext = (ST_Ext)Enum.Parse(typeof(ST_Ext), node.Attributes["v:ext"].Value);
			}
			cT_RegroupTable.entry = new List<CT_Entry>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "entry")
				{
					cT_RegroupTable.entry.Add(CT_Entry.Parse(childNode, namespaceManager));
				}
			}
			return cT_RegroupTable;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<o:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "v:ext", ext.ToString());
			sw.Write(">");
			if (entry != null)
			{
				foreach (CT_Entry item in entry)
				{
					item.Write(sw, "entry");
				}
			}
			sw.Write(string.Format("</o:{0}>", nodeName));
		}
	}
}
