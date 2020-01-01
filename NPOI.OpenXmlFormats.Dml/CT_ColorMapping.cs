using NPOI.OpenXml4Net.Util;
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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_ColorMapping
	{
		private CT_OfficeArtExtensionList extLstField;

		private ST_ColorSchemeIndex bg1Field;

		private ST_ColorSchemeIndex tx1Field;

		private ST_ColorSchemeIndex bg2Field;

		private ST_ColorSchemeIndex tx2Field;

		private ST_ColorSchemeIndex accent1Field;

		private ST_ColorSchemeIndex accent2Field;

		private ST_ColorSchemeIndex accent3Field;

		private ST_ColorSchemeIndex accent4Field;

		private ST_ColorSchemeIndex accent5Field;

		private ST_ColorSchemeIndex accent6Field;

		private ST_ColorSchemeIndex hlinkField;

		private ST_ColorSchemeIndex folHlinkField;

		[XmlElement(Order = 0)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex bg1
		{
			get
			{
				return bg1Field;
			}
			set
			{
				bg1Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex tx1
		{
			get
			{
				return tx1Field;
			}
			set
			{
				tx1Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex bg2
		{
			get
			{
				return bg2Field;
			}
			set
			{
				bg2Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex tx2
		{
			get
			{
				return tx2Field;
			}
			set
			{
				tx2Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex accent1
		{
			get
			{
				return accent1Field;
			}
			set
			{
				accent1Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex accent2
		{
			get
			{
				return accent2Field;
			}
			set
			{
				accent2Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex accent3
		{
			get
			{
				return accent3Field;
			}
			set
			{
				accent3Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex accent4
		{
			get
			{
				return accent4Field;
			}
			set
			{
				accent4Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex accent5
		{
			get
			{
				return accent5Field;
			}
			set
			{
				accent5Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex accent6
		{
			get
			{
				return accent6Field;
			}
			set
			{
				accent6Field = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex hlink
		{
			get
			{
				return hlinkField;
			}
			set
			{
				hlinkField = value;
			}
		}

		[XmlAttribute]
		public ST_ColorSchemeIndex folHlink
		{
			get
			{
				return folHlinkField;
			}
			set
			{
				folHlinkField = value;
			}
		}

		public static CT_ColorMapping Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ColorMapping cT_ColorMapping = new CT_ColorMapping();
			if (node.Attributes["bg1"] != null)
			{
				cT_ColorMapping.bg1 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["bg1"].Value);
			}
			if (node.Attributes["tx1"] != null)
			{
				cT_ColorMapping.tx1 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["tx1"].Value);
			}
			if (node.Attributes["bg2"] != null)
			{
				cT_ColorMapping.bg2 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["bg2"].Value);
			}
			if (node.Attributes["tx2"] != null)
			{
				cT_ColorMapping.tx2 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["tx2"].Value);
			}
			if (node.Attributes["accent1"] != null)
			{
				cT_ColorMapping.accent1 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["accent1"].Value);
			}
			if (node.Attributes["accent2"] != null)
			{
				cT_ColorMapping.accent2 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["accent2"].Value);
			}
			if (node.Attributes["accent3"] != null)
			{
				cT_ColorMapping.accent3 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["accent3"].Value);
			}
			if (node.Attributes["accent4"] != null)
			{
				cT_ColorMapping.accent4 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["accent4"].Value);
			}
			if (node.Attributes["accent5"] != null)
			{
				cT_ColorMapping.accent5 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["accent5"].Value);
			}
			if (node.Attributes["accent6"] != null)
			{
				cT_ColorMapping.accent6 = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["accent6"].Value);
			}
			if (node.Attributes["hlink"] != null)
			{
				cT_ColorMapping.hlink = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["hlink"].Value);
			}
			if (node.Attributes["folHlink"] != null)
			{
				cT_ColorMapping.folHlink = (ST_ColorSchemeIndex)Enum.Parse(typeof(ST_ColorSchemeIndex), node.Attributes["folHlink"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_ColorMapping.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_ColorMapping;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "bg1", bg1.ToString());
			XmlHelper.WriteAttribute(sw, "tx1", tx1.ToString());
			XmlHelper.WriteAttribute(sw, "bg2", bg2.ToString());
			XmlHelper.WriteAttribute(sw, "tx2", tx2.ToString());
			XmlHelper.WriteAttribute(sw, "accent1", accent1.ToString());
			XmlHelper.WriteAttribute(sw, "accent2", accent2.ToString());
			XmlHelper.WriteAttribute(sw, "accent3", accent3.ToString());
			XmlHelper.WriteAttribute(sw, "accent4", accent4.ToString());
			XmlHelper.WriteAttribute(sw, "accent5", accent5.ToString());
			XmlHelper.WriteAttribute(sw, "accent6", accent6.ToString());
			XmlHelper.WriteAttribute(sw, "hlink", hlink.ToString());
			XmlHelper.WriteAttribute(sw, "folHlink", folHlink.ToString());
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
