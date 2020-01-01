using System;
using System.IO;
using System.Xml;

namespace NPOI.OpenXml4Net.OPC.Internal.Marshallers
{
	/// Package properties marshaller.
	///
	/// @author CDubet, Julien Chable
	public class PackagePropertiesMarshaller : PartMarshaller
	{
		private static string namespaceDC = PackagePropertiesPart.NAMESPACE_DC_URI;

		private static string namespaceCoreProperties = PackagePropertiesPart.NAMESPACE_CP_URI;

		private static string namespaceDcTerms = PackagePropertiesPart.NAMESPACE_DCTERMS_URI;

		private static string namespaceXSI = PackagePropertiesPart.NAMESPACE_XSI_URI;

		protected static string KEYWORD_CATEGORY = "category";

		protected static string KEYWORD_CONTENT_STATUS = "contentStatus";

		protected static string KEYWORD_CONTENT_TYPE = "contentType";

		protected static string KEYWORD_CREATED = "created";

		protected static string KEYWORD_CREATOR = "creator";

		protected static string KEYWORD_DESCRIPTION = "description";

		protected static string KEYWORD_IDENTIFIER = "identifier";

		protected static string KEYWORD_KEYWORDS = "keywords";

		protected static string KEYWORD_LANGUAGE = "language";

		protected static string KEYWORD_LAST_MODIFIED_BY = "lastModifiedBy";

		protected static string KEYWORD_LAST_PRINTED = "lastPrinted";

		protected static string KEYWORD_MODIFIED = "modified";

		protected static string KEYWORD_REVISION = "revision";

		protected static string KEYWORD_SUBJECT = "subject";

		protected static string KEYWORD_TITLE = "title";

		protected static string KEYWORD_VERSION = "version";

		private PackagePropertiesPart propsPart;

		protected XmlDocument xmlDoc;

		protected XmlNamespaceManager nsmgr;

		/// Marshall package core properties to an XML document. Always return
		/// <code>true</code>.
		public virtual bool Marshall(PackagePart part, Stream out1)
		{
			if (!(part is PackagePropertiesPart))
			{
				throw new ArgumentException("'part' must be a PackagePropertiesPart instance.");
			}
			propsPart = (PackagePropertiesPart)part;
			xmlDoc = new XmlDocument();
			XmlElement xmlElement = xmlDoc.CreateElement("coreProperties", namespaceCoreProperties);
			nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
			nsmgr.AddNamespace("cp", PackagePropertiesPart.NAMESPACE_CP_URI);
			nsmgr.AddNamespace("dc", PackagePropertiesPart.NAMESPACE_DC_URI);
			nsmgr.AddNamespace("dcterms", PackagePropertiesPart.NAMESPACE_DCTERMS_URI);
			nsmgr.AddNamespace("xsi", PackagePropertiesPart.NAMESPACE_XSI_URI);
			xmlElement.SetAttribute("xmlns:cp", PackagePropertiesPart.NAMESPACE_CP_URI);
			xmlElement.SetAttribute("xmlns:dc", PackagePropertiesPart.NAMESPACE_DC_URI);
			xmlElement.SetAttribute("xmlns:dcterms", PackagePropertiesPart.NAMESPACE_DCTERMS_URI);
			xmlElement.SetAttribute("xmlns:xsi", PackagePropertiesPart.NAMESPACE_XSI_URI);
			xmlDoc.AppendChild(xmlElement);
			AddCategory();
			AddContentStatus();
			AddContentType();
			AddCreated();
			AddCreator();
			AddDescription();
			AddIdentifier();
			AddKeywords();
			AddLanguage();
			AddLastModifiedBy();
			AddLastPrinted();
			AddModified();
			AddRevision();
			AddSubject();
			AddTitle();
			AddVersion();
			return true;
		}

		/// Add category property element if needed.
		private void AddCategory()
		{
			if (propsPart.GetCategoryProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_CATEGORY, namespaceCoreProperties);
				XmlNode xmlNode = null;
				if (elementsByTagName.Count == 0)
				{
					xmlNode = xmlDoc.CreateElement("cp", KEYWORD_CATEGORY, namespaceCoreProperties);
					xmlDoc.DocumentElement.AppendChild(xmlNode);
				}
				else
				{
					xmlNode = elementsByTagName[0];
					xmlNode.InnerXml = "";
				}
				xmlNode.InnerText = propsPart.GetCategoryProperty();
			}
		}

		/// Add content status property element if needed.
		private void AddContentStatus()
		{
			if (propsPart.GetContentStatusProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_CONTENT_STATUS, namespaceCoreProperties);
				XmlNode xmlNode = null;
				if (elementsByTagName.Count == 0)
				{
					xmlNode = xmlDoc.CreateElement("cp", KEYWORD_CONTENT_STATUS, namespaceCoreProperties);
					xmlDoc.DocumentElement.AppendChild(xmlNode);
				}
				else
				{
					xmlNode = elementsByTagName[0];
					xmlNode.InnerXml = "";
				}
				xmlNode.InnerText = propsPart.GetContentStatusProperty();
			}
		}

		/// Add content type property element if needed.
		private void AddContentType()
		{
			if (propsPart.GetContentTypeProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_CONTENT_TYPE, namespaceCoreProperties);
				XmlNode xmlNode = null;
				if (elementsByTagName.Count == 0)
				{
					xmlNode = xmlDoc.CreateElement("cp", KEYWORD_CONTENT_TYPE, namespaceCoreProperties);
					xmlDoc.DocumentElement.AppendChild(xmlNode);
				}
				else
				{
					xmlNode = elementsByTagName[0];
					xmlNode.InnerXml = "";
				}
				xmlNode.InnerText = propsPart.GetContentTypeProperty();
			}
		}

		/// Add created property element if needed.
		private void AddCreated()
		{
			if (propsPart.GetCreatedProperty().HasValue)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_CREATED, namespaceDcTerms);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("dcterms", KEYWORD_CREATED, namespaceDcTerms);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.SetAttribute("type", namespaceXSI, "dcterms:W3CDTF");
				xmlElement.InnerText = propsPart.GetCreatedPropertyString();
			}
		}

		/// Add creator property element if needed.
		private void AddCreator()
		{
			if (propsPart.GetCreatorProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_CREATOR, namespaceDC);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("dc", KEYWORD_CREATOR, namespaceDC);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetCreatorProperty();
			}
		}

		/// Add description property element if needed.
		private void AddDescription()
		{
			if (propsPart.GetDescriptionProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_DESCRIPTION, namespaceDC);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("dc", KEYWORD_DESCRIPTION, namespaceDC);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetDescriptionProperty();
			}
		}

		/// Add identifier property element if needed.
		private void AddIdentifier()
		{
			if (propsPart.GetIdentifierProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_IDENTIFIER, namespaceDC);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("dc", KEYWORD_IDENTIFIER, namespaceDC);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetIdentifierProperty();
			}
		}

		/// Add keywords property element if needed.
		private void AddKeywords()
		{
			if (propsPart.GetKeywordsProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_KEYWORDS, namespaceCoreProperties);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("cp", KEYWORD_KEYWORDS, namespaceCoreProperties);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetKeywordsProperty();
			}
		}

		/// Add language property element if needed.
		private void AddLanguage()
		{
			if (propsPart.GetLanguageProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_LANGUAGE, namespaceDC);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("dc", KEYWORD_LANGUAGE, namespaceDC);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetLanguageProperty();
			}
		}

		/// Add 'last modified by' property if needed.
		private void AddLastModifiedBy()
		{
			if (propsPart.GetLastModifiedByProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_LAST_MODIFIED_BY, namespaceCoreProperties);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("cp", KEYWORD_LAST_MODIFIED_BY, namespaceCoreProperties);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetLastModifiedByProperty();
			}
		}

		/// Add 'last printed' property if needed.
		private void AddLastPrinted()
		{
			if (propsPart.GetLastPrintedProperty().HasValue)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_LAST_PRINTED, namespaceCoreProperties);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("cp", KEYWORD_LAST_PRINTED, namespaceCoreProperties);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetLastPrintedPropertyString();
			}
		}

		/// Add modified property element if needed.
		private void AddModified()
		{
			if (propsPart.GetModifiedProperty().HasValue)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_MODIFIED, namespaceDcTerms);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("dcterms", KEYWORD_MODIFIED, namespaceDcTerms);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetModifiedPropertyString();
				xmlElement.SetAttribute("type", namespaceXSI, "dcterms:W3CDTF");
			}
		}

		/// Add revision property if needed.
		private void AddRevision()
		{
			if (propsPart.GetRevisionProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_REVISION, namespaceCoreProperties);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("cp", KEYWORD_REVISION, namespaceCoreProperties);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetRevisionProperty();
			}
		}

		/// Add subject property if needed.
		private void AddSubject()
		{
			if (propsPart.GetSubjectProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_SUBJECT, namespaceDC);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("dc", KEYWORD_SUBJECT, namespaceDC);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetSubjectProperty();
			}
		}

		/// Add title property if needed.
		private void AddTitle()
		{
			if (propsPart.GetTitleProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_TITLE, namespaceDC);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("dc", KEYWORD_TITLE, namespaceDC);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetTitleProperty();
			}
		}

		private void AddVersion()
		{
			if (propsPart.GetVersionProperty() != null)
			{
				XmlNodeList elementsByTagName = xmlDoc.DocumentElement.GetElementsByTagName(KEYWORD_VERSION, namespaceCoreProperties);
				XmlElement xmlElement = null;
				if (elementsByTagName.Count == 0)
				{
					xmlElement = xmlDoc.CreateElement("cp", KEYWORD_VERSION, namespaceCoreProperties);
					xmlDoc.DocumentElement.AppendChild(xmlElement);
				}
				else
				{
					xmlElement = (XmlElement)elementsByTagName[0];
					xmlElement.InnerXml = "";
				}
				xmlElement.InnerText = propsPart.GetVersionProperty();
			}
		}
	}
}
