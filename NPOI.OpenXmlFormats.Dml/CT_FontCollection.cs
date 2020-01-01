using System;
using System.Collections.Generic;
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
	public class CT_FontCollection
	{
		private CT_TextFont latinField;

		private CT_TextFont eaField;

		private CT_TextFont csField;

		private List<CT_SupplementalFont> fontField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_TextFont latin
		{
			get
			{
				return latinField;
			}
			set
			{
				latinField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TextFont ea
		{
			get
			{
				return eaField;
			}
			set
			{
				eaField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TextFont cs
		{
			get
			{
				return csField;
			}
			set
			{
				csField = value;
			}
		}

		[XmlElement("font", Order = 3)]
		public List<CT_SupplementalFont> font
		{
			get
			{
				return fontField;
			}
			set
			{
				fontField = value;
			}
		}

		[XmlElement(Order = 4)]
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

		public static CT_FontCollection Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FontCollection cT_FontCollection = new CT_FontCollection();
			cT_FontCollection.font = new List<CT_SupplementalFont>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "latin")
				{
					cT_FontCollection.latin = CT_TextFont.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ea")
				{
					cT_FontCollection.ea = CT_TextFont.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cs")
				{
					cT_FontCollection.cs = CT_TextFont.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_FontCollection.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "font")
				{
					cT_FontCollection.font.Add(CT_SupplementalFont.Parse(childNode, namespaceManager));
				}
			}
			return cT_FontCollection;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (latin != null)
			{
				latin.Write(sw, "latin");
			}
			if (ea != null)
			{
				ea.Write(sw, "ea");
			}
			if (cs != null)
			{
				cs.Write(sw, "cs");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (font != null)
			{
				foreach (CT_SupplementalFont item in font)
				{
					item.Write(sw, "font");
				}
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
