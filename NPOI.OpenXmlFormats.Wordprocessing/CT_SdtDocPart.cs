using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SdtDocPart
	{
		private CT_String docPartGalleryField;

		private CT_String docPartCategoryField;

		private CT_OnOff docPartUniqueField;

		[XmlElement(Order = 0)]
		public CT_String docPartGallery
		{
			get
			{
				return docPartGalleryField;
			}
			set
			{
				docPartGalleryField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_String docPartCategory
		{
			get
			{
				return docPartCategoryField;
			}
			set
			{
				docPartCategoryField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff docPartUnique
		{
			get
			{
				return docPartUniqueField;
			}
			set
			{
				docPartUniqueField = value;
			}
		}

		public static CT_SdtDocPart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtDocPart cT_SdtDocPart = new CT_SdtDocPart();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "docPartGallery")
				{
					cT_SdtDocPart.docPartGallery = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "docPartCategory")
				{
					cT_SdtDocPart.docPartCategory = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "docPartUnique")
				{
					cT_SdtDocPart.docPartUnique = CT_OnOff.Parse(childNode, namespaceManager);
				}
			}
			return cT_SdtDocPart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (docPartGallery != null)
			{
				docPartGallery.Write(sw, "docPartGallery");
			}
			if (docPartCategory != null)
			{
				docPartCategory.Write(sw, "docPartCategory");
			}
			if (docPartUnique != null)
			{
				docPartUnique.Write(sw, "docPartUnique");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_String AddNewDocPartGallery()
		{
			if (docPartGalleryField == null)
			{
				docPartGalleryField = new CT_String();
			}
			return docPartGalleryField;
		}
	}
}
