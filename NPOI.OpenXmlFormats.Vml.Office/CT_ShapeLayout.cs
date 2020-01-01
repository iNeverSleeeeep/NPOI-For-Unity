using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot("shapelayout", Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	public class CT_ShapeLayout
	{
		private CT_IdMap idmapField;

		private CT_RegroupTable regrouptableField;

		private CT_Rules rulesField;

		private ST_Ext extField;

		[XmlElement]
		public CT_IdMap idmap
		{
			get
			{
				return idmapField;
			}
			set
			{
				idmapField = value;
			}
		}

		[XmlElement]
		public CT_RegroupTable regrouptable
		{
			get
			{
				return regrouptableField;
			}
			set
			{
				regrouptableField = value;
			}
		}

		[XmlElement]
		public CT_Rules rules
		{
			get
			{
				return rulesField;
			}
			set
			{
				rulesField = value;
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

		public CT_IdMap AddNewIdmap()
		{
			idmapField = new CT_IdMap();
			return idmapField;
		}

		public static CT_ShapeLayout Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ShapeLayout cT_ShapeLayout = new CT_ShapeLayout();
			if (node.Attributes["v:ext"] != null)
			{
				cT_ShapeLayout.ext = (ST_Ext)Enum.Parse(typeof(ST_Ext), node.Attributes["v:ext"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idmap")
				{
					cT_ShapeLayout.idmap = CT_IdMap.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "regrouptable")
				{
					cT_ShapeLayout.regrouptable = CT_RegroupTable.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rules")
				{
					cT_ShapeLayout.rules = CT_Rules.Parse(childNode, namespaceManager);
				}
			}
			return cT_ShapeLayout;
		}

		public void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<o:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "v:ext", ext.ToString());
			sw.Write(">");
			if (idmap != null)
			{
				idmap.Write(sw, "idmap");
			}
			if (regrouptable != null)
			{
				regrouptable.Write(sw, "regrouptable");
			}
			if (rules != null)
			{
				rules.Write(sw, "rules");
			}
			sw.Write(string.Format("</o:{0}>", nodeName));
		}
	}
}
