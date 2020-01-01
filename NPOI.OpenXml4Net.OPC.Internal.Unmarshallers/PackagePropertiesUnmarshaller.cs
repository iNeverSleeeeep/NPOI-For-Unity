using ICSharpCode.SharpZipLib.Zip;
using NPOI.OpenXml4Net.Exceptions;
using System.Collections;
using System.IO;
using System.Xml;

namespace NPOI.OpenXml4Net.OPC.Internal.Unmarshallers
{
	/// Package properties unmarshaller.
	///
	/// @author Julien Chable
	/// @version 1.0
	public class PackagePropertiesUnmarshaller : PartUnmarshaller
	{
		private static string namespaceDC = "http://purl.org/dc/elements/1.1/";

		private static string namespaceCP = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties";

		private static string namespaceDcTerms = "http://purl.org/dc/terms/";

		private static string namespaceXML = "http://www.w3.org/XML/1998/namespace";

		private static string namespaceXSI = "http://www.w3.org/2001/XMLSchema-instance";

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

		protected XmlNamespaceManager nsmgr;

		public PackagePart Unmarshall(UnmarshallContext context, Stream in1)
		{
			PackagePropertiesPart packagePropertiesPart = new PackagePropertiesPart(context.Package, context.PartName);
			if (in1 == null)
			{
				if (context.ZipEntry != null)
				{
					in1 = ((ZipPackage)context.Package).ZipArchive.GetInputStream(context.ZipEntry);
				}
				else
				{
					if (context.Package == null)
					{
						throw new IOException("Error while trying to get the part input stream.");
					}
					ZipEntry corePropertiesZipEntry;
					try
					{
						corePropertiesZipEntry = ZipHelper.GetCorePropertiesZipEntry((ZipPackage)context.Package);
					}
					catch (OpenXml4NetException)
					{
						throw new IOException("Error while trying to get the part input stream.");
					}
					in1 = ((ZipPackage)context.Package).ZipArchive.GetInputStream(corePropertiesZipEntry);
				}
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlTextReader reader = new XmlTextReader(in1);
			xmlDocument.Load(reader);
			nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
			nsmgr.AddNamespace("cp", namespaceCP);
			nsmgr.AddNamespace("dc", namespaceDC);
			nsmgr.AddNamespace("dcterms", namespaceDcTerms);
			nsmgr.AddNamespace("xsi", namespaceXSI);
			nsmgr.AddNamespace("cml", "http://schemas.openxmlformats.org/markup-compatibility/2006");
			nsmgr.AddNamespace("dcmitype", "http://purl.org/dc/dcmitype/");
			CheckElementForOPCCompliance(xmlDocument.DocumentElement);
			packagePropertiesPart.SetCategoryProperty(LoadCategory(xmlDocument));
			packagePropertiesPart.SetContentStatusProperty(LoadContentStatus(xmlDocument));
			packagePropertiesPart.SetContentTypeProperty(LoadContentType(xmlDocument));
			packagePropertiesPart.SetCreatedProperty(LoadCreated(xmlDocument));
			packagePropertiesPart.SetCreatorProperty(LoadCreator(xmlDocument));
			packagePropertiesPart.SetDescriptionProperty(LoadDescription(xmlDocument));
			packagePropertiesPart.SetIdentifierProperty(LoadIdentifier(xmlDocument));
			packagePropertiesPart.SetKeywordsProperty(LoadKeywords(xmlDocument));
			packagePropertiesPart.SetLanguageProperty(LoadLanguage(xmlDocument));
			packagePropertiesPart.SetLastModifiedByProperty(LoadLastModifiedBy(xmlDocument));
			packagePropertiesPart.SetLastPrintedProperty(LoadLastPrinted(xmlDocument));
			packagePropertiesPart.SetModifiedProperty(LoadModified(xmlDocument));
			packagePropertiesPart.SetRevisionProperty(LoadRevision(xmlDocument));
			packagePropertiesPart.SetSubjectProperty(LoadSubject(xmlDocument));
			packagePropertiesPart.SetTitleProperty(LoadTitle(xmlDocument));
			packagePropertiesPart.SetVersionProperty(LoadVersion(xmlDocument));
			return packagePropertiesPart;
		}

		private string LoadCategory(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("cp:" + KEYWORD_CATEGORY, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadContentStatus(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("cp:" + KEYWORD_CONTENT_STATUS, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadContentType(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("cp:" + KEYWORD_CONTENT_TYPE, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadCreated(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("dcterms:" + KEYWORD_CREATED, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadCreator(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("dc:" + KEYWORD_CREATOR, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadDescription(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("dc:" + KEYWORD_DESCRIPTION, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadIdentifier(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("dc:" + KEYWORD_IDENTIFIER, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadKeywords(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("cp:" + KEYWORD_KEYWORDS, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadLanguage(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("dc:" + KEYWORD_LANGUAGE, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadLastModifiedBy(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("cp:" + KEYWORD_LAST_MODIFIED_BY, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadLastPrinted(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("cp:" + KEYWORD_LAST_PRINTED, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadModified(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("dcterms:" + KEYWORD_MODIFIED, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadRevision(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("cp:" + KEYWORD_REVISION, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadSubject(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("dc:" + KEYWORD_SUBJECT, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadTitle(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("dc:" + KEYWORD_TITLE, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		private string LoadVersion(XmlDocument xmlDoc)
		{
			XmlNode xmlNode = xmlDoc.DocumentElement.SelectNodes("cp:" + KEYWORD_VERSION, nsmgr)[0];
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		public void CheckElementForOPCCompliance(XmlElement el)
		{
			foreach (XmlAttribute attribute in el.Attributes)
			{
				if (attribute.Name.StartsWith("xmlns:"))
				{
					string prefix = attribute.Name.Substring(6);
					if (nsmgr.LookupNamespace(prefix).Equals("http://schemas.openxmlformats.org/markup-compatibility/2006"))
					{
						throw new InvalidFormatException("OPC Compliance error [M4.2]: A format consumer shall consider the use of the Markup Compatibility namespace to be an error.");
					}
				}
			}
			if (el.NamespaceURI.Equals(namespaceDcTerms) && !el.LocalName.Equals(KEYWORD_CREATED) && !el.LocalName.Equals(KEYWORD_MODIFIED))
			{
				throw new InvalidFormatException("OPC Compliance error [M4.3]: Producers shall not create a document element that contains refinements to the Dublin Core elements, except for the two specified in the schema: <dcterms:created> and <dcterms:modified> Consumers shall consider a document element that violates this constraint to be an error.");
			}
			if (el.Attributes["lang", namespaceXML] != null)
			{
				throw new InvalidFormatException("OPC Compliance error [M4.4]: Producers shall not create a document element that contains the xml:lang attribute. Consumers shall consider a document element that violates this constraint to be an error.");
			}
			if (el.NamespaceURI.Equals(namespaceDcTerms))
			{
				string localName = el.LocalName;
				if (!localName.Equals(KEYWORD_CREATED) && !localName.Equals(KEYWORD_MODIFIED))
				{
					throw new InvalidFormatException("Namespace error : " + localName + " shouldn't have the following naemspace -> " + namespaceDcTerms);
				}
				XmlAttribute xmlAttribute2 = el.Attributes["xsi:type"];
				if (xmlAttribute2 == null)
				{
					throw new InvalidFormatException("The element '" + localName + "' must have the '" + nsmgr.LookupPrefix(namespaceXSI) + ":type' attribute present !");
				}
				if (!xmlAttribute2.Value.Equals("dcterms:W3CDTF"))
				{
					throw new InvalidFormatException("The element '" + localName + "' must have the '" + nsmgr.LookupPrefix(namespaceXSI) + ":type' attribute with the value 'dcterms:W3CDTF' !");
				}
			}
			IEnumerator enumerator2 = el.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current is XmlElement)
				{
					CheckElementForOPCCompliance((XmlElement)enumerator2.Current);
				}
			}
		}
	}
}
