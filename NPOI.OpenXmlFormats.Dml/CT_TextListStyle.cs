using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TextListStyle
	{
		private CT_TextParagraphProperties defPPrField;

		private CT_TextParagraphProperties lvl1pPrField;

		private CT_TextParagraphProperties lvl2pPrField;

		private CT_TextParagraphProperties lvl3pPrField;

		private CT_TextParagraphProperties lvl4pPrField;

		private CT_TextParagraphProperties lvl5pPrField;

		private CT_TextParagraphProperties lvl6pPrField;

		private CT_TextParagraphProperties lvl7pPrField;

		private CT_TextParagraphProperties lvl8pPrField;

		private CT_TextParagraphProperties lvl9pPrField;

		private CT_OfficeArtExtensionList extLstField;

		public CT_TextParagraphProperties defPPr
		{
			get
			{
				return defPPrField;
			}
			set
			{
				defPPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl1pPr
		{
			get
			{
				return lvl1pPrField;
			}
			set
			{
				lvl1pPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl2pPr
		{
			get
			{
				return lvl2pPrField;
			}
			set
			{
				lvl2pPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl3pPr
		{
			get
			{
				return lvl3pPrField;
			}
			set
			{
				lvl3pPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl4pPr
		{
			get
			{
				return lvl4pPrField;
			}
			set
			{
				lvl4pPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl5pPr
		{
			get
			{
				return lvl5pPrField;
			}
			set
			{
				lvl5pPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl6pPr
		{
			get
			{
				return lvl6pPrField;
			}
			set
			{
				lvl6pPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl7pPr
		{
			get
			{
				return lvl7pPrField;
			}
			set
			{
				lvl7pPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl8pPr
		{
			get
			{
				return lvl8pPrField;
			}
			set
			{
				lvl8pPrField = value;
			}
		}

		public CT_TextParagraphProperties lvl9pPr
		{
			get
			{
				return lvl9pPrField;
			}
			set
			{
				lvl9pPrField = value;
			}
		}

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

		public static CT_TextListStyle Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextListStyle cT_TextListStyle = new CT_TextListStyle();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "defPPr")
				{
					cT_TextListStyle.defPPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl1pPr")
				{
					cT_TextListStyle.lvl1pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl2pPr")
				{
					cT_TextListStyle.lvl2pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl3pPr")
				{
					cT_TextListStyle.lvl3pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl4pPr")
				{
					cT_TextListStyle.lvl4pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl5pPr")
				{
					cT_TextListStyle.lvl5pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl6pPr")
				{
					cT_TextListStyle.lvl6pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl7pPr")
				{
					cT_TextListStyle.lvl7pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl8pPr")
				{
					cT_TextListStyle.lvl8pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl9pPr")
				{
					cT_TextListStyle.lvl9pPr = CT_TextParagraphProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_TextListStyle.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_TextListStyle;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (defPPr != null)
			{
				defPPr.Write(sw, "defPPr");
			}
			if (lvl1pPr != null)
			{
				lvl1pPr.Write(sw, "lvl1pPr");
			}
			if (lvl2pPr != null)
			{
				lvl2pPr.Write(sw, "lvl2pPr");
			}
			if (lvl3pPr != null)
			{
				lvl3pPr.Write(sw, "lvl3pPr");
			}
			if (lvl4pPr != null)
			{
				lvl4pPr.Write(sw, "lvl4pPr");
			}
			if (lvl5pPr != null)
			{
				lvl5pPr.Write(sw, "lvl5pPr");
			}
			if (lvl6pPr != null)
			{
				lvl6pPr.Write(sw, "lvl6pPr");
			}
			if (lvl7pPr != null)
			{
				lvl7pPr.Write(sw, "lvl7pPr");
			}
			if (lvl8pPr != null)
			{
				lvl8pPr.Write(sw, "lvl8pPr");
			}
			if (lvl9pPr != null)
			{
				lvl9pPr.Write(sw, "lvl9pPr");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
