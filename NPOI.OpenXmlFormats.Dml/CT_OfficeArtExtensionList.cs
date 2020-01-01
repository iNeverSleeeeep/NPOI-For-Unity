using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_OfficeArtExtensionList
	{
		private List<CT_OfficeArtExtension> extField;

		[XmlElement("ext", Order = 0)]
		public List<CT_OfficeArtExtension> ext
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

		public CT_OfficeArtExtensionList()
		{
			extField = new List<CT_OfficeArtExtension>();
		}

		public static CT_OfficeArtExtensionList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OfficeArtExtensionList cT_OfficeArtExtensionList = new CT_OfficeArtExtensionList();
			cT_OfficeArtExtensionList.ext = new List<CT_OfficeArtExtension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ext")
				{
					cT_OfficeArtExtensionList.ext.Add(CT_OfficeArtExtension.Parse(childNode, namespaceManager));
				}
			}
			return cT_OfficeArtExtensionList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (ext != null)
			{
				foreach (CT_OfficeArtExtension item in ext)
				{
					item.Write(sw, "ext");
				}
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
