using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_FontScheme
	{
		private CT_FontCollection majorFontField;

		private CT_FontCollection minorFontField;

		private CT_OfficeArtExtensionList extLstField;

		private string nameField;

		[XmlElement(Order = 0)]
		public CT_FontCollection majorFont
		{
			get
			{
				return majorFontField;
			}
			set
			{
				majorFontField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_FontCollection minorFont
		{
			get
			{
				return minorFontField;
			}
			set
			{
				minorFontField = value;
			}
		}

		[XmlElement(Order = 2)]
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

		public static CT_FontScheme Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FontScheme cT_FontScheme = new CT_FontScheme();
			cT_FontScheme.name = XmlHelper.ReadString(node.Attributes["name"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "majorFont")
				{
					cT_FontScheme.majorFont = CT_FontCollection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorFont")
				{
					cT_FontScheme.minorFont = CT_FontCollection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_FontScheme.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_FontScheme;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			sw.Write(">");
			if (majorFont != null)
			{
				majorFont.Write(sw, "majorFont");
			}
			if (minorFont != null)
			{
				minorFont.Write(sw, "minorFont");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
