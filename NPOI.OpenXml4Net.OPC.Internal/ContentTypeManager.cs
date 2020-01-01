using NPOI.OpenXml4Net.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace NPOI.OpenXml4Net.OPC.Internal
{
	/// Manage package content types ([Content_Types].xml part).
	///
	/// @author Julien Chable
	/// @version 1.0
	public abstract class ContentTypeManager
	{
		/// Content type part name.
		public const string CONTENT_TYPES_PART_NAME = "[Content_Types].xml";

		/// Content type namespace
		public const string TYPES_NAMESPACE_URI = "http://schemas.openxmlformats.org/package/2006/content-types";

		private const string TYPES_TAG_NAME = "Types";

		private const string DEFAULT_TAG_NAME = "Default";

		private const string EXTENSION_ATTRIBUTE_NAME = "Extension";

		private const string CONTENT_TYPE_ATTRIBUTE_NAME = "ContentType";

		private const string OVERRIDE_TAG_NAME = "Override";

		private const string PART_NAME_ATTRIBUTE_NAME = "PartName";

		/// Reference to the package using this content type manager.
		protected OPCPackage container;

		private SortedList<string, string> defaultContentType;

		/// Override content type tree.
		private SortedList<PackagePartName, string> overrideContentType;

		/// Constructor. Parses the content of the specified input stream.
		///
		/// @param in
		///            If different of <i>null</i> then the content types part is
		///            retrieve and parse.
		/// @throws InvalidFormatException
		///             If the content types part content is not valid.
		public ContentTypeManager(Stream in1, OPCPackage pkg)
		{
			container = pkg;
			defaultContentType = new SortedList<string, string>();
			if (in1 != null)
			{
				try
				{
					ParseContentTypesFile(in1);
				}
				catch (InvalidFormatException)
				{
					throw new InvalidFormatException("Can't read content types part !");
				}
			}
		}

		/// Build association extention-&gt; content type (will be stored in
		/// [Content_Types].xml) for example ContentType="image/png" Extension="png"
		/// <p>
		/// [M2.8]: When adding a new part to a package, the package implementer
		/// shall ensure that a content type for that part is specified in the
		/// Content Types stream; the package implementer shall perform the steps
		/// described in §9.1.2.3:
		/// </p><p>
		/// 1. Get the extension from the part name by taking the substring to the
		/// right of the rightmost occurrence of the dot character (.) from the
		/// rightmost segment.
		/// </p><p>
		/// 2. If a part name has no extension, a corresponding Override element
		/// shall be added to the Content Types stream.
		/// </p><p>
		/// 3. Compare the resulting extension with the values specified for the
		/// Extension attributes of the Default elements in the Content Types stream.
		/// The comparison shall be case-insensitive ASCII.
		/// </p><p>
		/// 4. If there is a Default element with a matching Extension attribute,
		/// then the content type of the new part shall be compared with the value of
		/// the ContentType attribute. The comparison might be case-sensitive and
		/// include every character regardless of the role it plays in the
		/// content-type grammar of RFC 2616, or it might follow the grammar of RFC
		/// 2616.
		/// </p><p>
		/// a. If the content types match, no further action is required.
		/// </p><p>
		/// b. If the content types do not match, a new Override element shall be
		/// added to the Content Types stream. .
		/// </p><p>
		/// 5. If there is no Default element with a matching Extension attribute, a
		/// new Default element or Override element shall be added to the Content
		/// Types stream.
		/// </p>
		public void AddContentType(PackagePartName partName, string contentType)
		{
			bool flag = false;
			string text = partName.Extension.ToLower();
			if (text.Length == 0 || (defaultContentType.ContainsKey(text) && !(flag = defaultContentType.ContainsValue(contentType))))
			{
				AddOverrideContentType(partName, contentType);
			}
			else if (!flag)
			{
				AddDefaultContentType(text, contentType);
			}
		}

		/// Add an override content type for a specific part.
		///
		/// @param partName
		///            Name of the part.
		/// @param contentType
		///            Content type of the part.
		private void AddOverrideContentType(PackagePartName partName, string contentType)
		{
			if (overrideContentType == null)
			{
				overrideContentType = new SortedList<PackagePartName, string>();
			}
			if (!overrideContentType.ContainsKey(partName))
			{
				overrideContentType.Add(partName, contentType);
			}
			else
			{
				overrideContentType[partName] = contentType;
			}
		}

		/// Add a content type associated with the specified extension.
		///
		/// @param extension
		///            The part name extension to bind to a content type.
		/// @param contentType
		///            The content type associated with the specified extension.
		private void AddDefaultContentType(string extension, string contentType)
		{
			defaultContentType.Add(extension.ToLower(), contentType);
		}

		/// <p>
		/// Delete a content type based on the specified part name. If the specified
		/// part name is register with an override content type, then this content
		/// type is remove, else the content type is remove in the default content
		/// type list if it exists and if no part is associated with it yet.
		/// </p><p>
		/// Check rule M2.4: The package implementer shall require that the Content
		/// Types stream contain one of the following for every part in the package:
		/// One matching Default element One matching Override element Both a
		/// matching Default element and a matching Override element, in which case
		/// the Override element takes precedence.
		/// </p>
		/// @param partName
		///            The part URI associated with the override content type to
		///            delete.
		/// @exception InvalidOperationException
		///                Throws if
		public void RemoveContentType(PackagePartName partName)
		{
			if (partName == null)
			{
				throw new ArgumentException("partName");
			}
			if (overrideContentType != null && overrideContentType.ContainsKey(partName))
			{
				overrideContentType.Remove(partName);
			}
			else
			{
				string extension = partName.Extension;
				bool flag = true;
				if (container != null)
				{
					try
					{
						foreach (PackagePart part in container.GetParts())
						{
							if (!part.PartName.Equals(partName) && part.PartName.Extension.Equals(extension, StringComparison.InvariantCultureIgnoreCase))
							{
								flag = false;
								break;
							}
						}
					}
					catch (InvalidFormatException ex)
					{
						throw new InvalidOperationException(ex.Message);
					}
				}
				if (flag)
				{
					defaultContentType.Remove(extension);
				}
				if (container != null)
				{
					try
					{
						foreach (PackagePart part2 in container.GetParts())
						{
							if (!part2.PartName.Equals(partName) && GetContentType(part2.PartName) == null)
							{
								throw new InvalidOperationException("Rule M2.4 is not respected: Nor a default element or override element is associated with the part: " + part2.PartName.Name);
							}
						}
					}
					catch (InvalidFormatException ex2)
					{
						throw new InvalidOperationException(ex2.Message);
					}
				}
			}
		}

		/// Check if the specified content type is already register.
		///
		/// @param contentType
		///            The content type to check.
		/// @return <code>true</code> if the specified content type is already
		///         register, then <code>false</code>.
		public bool IsContentTypeRegister(string contentType)
		{
			if (contentType == null)
			{
				throw new ArgumentException("contentType");
			}
			if (!defaultContentType.Values.Contains(contentType))
			{
				if (overrideContentType != null)
				{
					return overrideContentType.Values.Contains(contentType);
				}
				return false;
			}
			return true;
		}

		/// Get the content type for the specified part, if any.
		/// <p>
		/// Rule [M2.9]: To get the content type of a part, the package implementer
		/// shall perform the steps described in §9.1.2.4:
		/// </p><p>
		/// 1. Compare the part name with the values specified for the PartName
		/// attribute of the Override elements. The comparison shall be
		/// case-insensitive ASCII.
		/// </p><p>
		/// 2. If there is an Override element with a matching PartName attribute,
		/// return the value of its ContentType attribute. No further action is
		/// required.
		/// </p><p>
		/// 3. If there is no Override element with a matching PartName attribute,
		/// then a. Get the extension from the part name by taking the substring to
		/// the right of the rightmost occurrence of the dot character (.) from the
		/// rightmost segment. b. Check the Default elements of the Content Types
		/// stream, comparing the extension with the value of the Extension
		/// attribute. The comparison shall be case-insensitive ASCII.
		/// </p><p>
		/// 4. If there is a Default element with a matching Extension attribute,
		/// return the value of its ContentType attribute. No further action is
		/// required.
		/// </p><p>
		/// 5. If neither Override nor Default elements with matching attributes are
		/// found for the specified part name, the implementation shall not map this
		/// part name to a part.
		/// </p>
		/// @param partName
		///            The URI part to check.
		/// @return The content type associated with the URI (in case of an override
		///         content type) or the extension (in case of default content type),
		///         else <code>null</code>.
		///
		/// @exception OpenXml4NetRuntimeException
		///                Throws if the content type manager is not able to find the
		///                content from an existing part.
		public string GetContentType(PackagePartName partName)
		{
			if (partName == null)
			{
				throw new ArgumentException("partName");
			}
			if (overrideContentType != null && overrideContentType.ContainsKey(partName))
			{
				return overrideContentType[partName];
			}
			string key = partName.Extension.ToLower();
			if (defaultContentType.ContainsKey(key))
			{
				return defaultContentType[key];
			}
			if (container != null && container.GetPart(partName) != null)
			{
				throw new OpenXml4NetException("Rule M2.4 exception : this error should NEVER happen, if so please send a mail to the developers team, thanks !");
			}
			return null;
		}

		/// Clear all content types.
		public void ClearAll()
		{
			defaultContentType.Clear();
			if (overrideContentType != null)
			{
				overrideContentType.Clear();
			}
		}

		/// Clear all override content types.
		public void ClearOverrideContentTypes()
		{
			if (overrideContentType != null)
			{
				overrideContentType.Clear();
			}
		}

		/// Parse the content types part.
		///
		/// @throws InvalidFormatException
		///             Throws if the content type doesn't exist or the XML format is
		///             invalid.
		private void ParseContentTypesFile(Stream in1)
		{
			try
			{
				XPathDocument xPathDocument = new XPathDocument(in1);
				XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xPathNavigator.NameTable);
				xmlNamespaceManager.AddNamespace("x", "http://schemas.openxmlformats.org/package/2006/content-types");
				XPathNodeIterator xPathNodeIterator = xPathNavigator.Select("//x:Default", xmlNamespaceManager);
				while (xPathNodeIterator.MoveNext())
				{
					string attribute = xPathNodeIterator.Current.GetAttribute("Extension", xPathNavigator.NamespaceURI);
					string attribute2 = xPathNodeIterator.Current.GetAttribute("ContentType", xPathNavigator.NamespaceURI);
					AddDefaultContentType(attribute, attribute2);
				}
				xPathNodeIterator = xPathNavigator.Select("//x:Override", xmlNamespaceManager);
				while (xPathNodeIterator.MoveNext())
				{
					Uri partUri = PackagingUriHelper.ParseUri(xPathNodeIterator.Current.GetAttribute("PartName", xPathNavigator.NamespaceURI), UriKind.RelativeOrAbsolute);
					PackagePartName partName = PackagingUriHelper.CreatePartName(partUri);
					string attribute3 = xPathNodeIterator.Current.GetAttribute("ContentType", xPathNavigator.NamespaceURI);
					AddOverrideContentType(partName, attribute3);
				}
			}
			catch (UriFormatException ex)
			{
				throw new InvalidFormatException(ex.Message);
			}
		}

		/// Save the contents type part.
		///
		/// @param outStream
		///            The output stream use to save the XML content of the content
		///            types part.
		/// @return <b>true</b> if the operation success, else <b>false</b>.
		public bool Save(Stream outStream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("x", "http://schemas.openxmlformats.org/package/2006/content-types");
			XmlElement xmlElement = xmlDocument.CreateElement("Types", "http://schemas.openxmlformats.org/package/2006/content-types");
			xmlDocument.AppendChild(xmlElement);
			IEnumerator<KeyValuePair<string, string>> enumerator = defaultContentType.GetEnumerator();
			while (enumerator.MoveNext())
			{
				AppendDefaultType(xmlDocument, xmlElement, enumerator.Current);
			}
			if (overrideContentType != null)
			{
				IEnumerator<KeyValuePair<PackagePartName, string>> enumerator2 = overrideContentType.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					AppendSpecificTypes(xmlDocument, xmlElement, enumerator2.Current);
				}
			}
			xmlDocument.Normalize();
			return SaveImpl(xmlDocument, outStream);
		}

		/// Use to Append specific type XML elements, use by the save() method.
		///
		/// @param root
		///            XML parent element use to Append this override type element.
		/// @param entry
		///            The values to Append.
		/// @see #save(java.io.OutputStream)
		private void AppendSpecificTypes(XmlDocument xmldoc, XmlElement root, KeyValuePair<PackagePartName, string> entry)
		{
			XmlElement xmlElement = xmldoc.CreateElement("Override", "http://schemas.openxmlformats.org/package/2006/content-types");
			root.AppendChild(xmlElement);
			xmlElement.SetAttribute("PartName", entry.Key.Name);
			xmlElement.SetAttribute("ContentType", entry.Value);
		}

		/// Use to Append default types XML elements, use by the save() metid.
		///
		/// @param root
		///            XML parent element use to Append this default type element.
		/// @param entry
		///            The values to Append.
		/// @see #save(java.io.OutputStream)
		private void AppendDefaultType(XmlDocument xmldoc, XmlElement root, KeyValuePair<string, string> entry)
		{
			XmlElement xmlElement = xmldoc.CreateElement("Default", "http://schemas.openxmlformats.org/package/2006/content-types");
			root.AppendChild(xmlElement);
			xmlElement.SetAttribute("Extension", entry.Key);
			xmlElement.SetAttribute("ContentType", entry.Value);
		}

		/// Specific implementation of the save method. Call by the save() method,
		/// call before exiting.
		///
		/// @param out
		///            The output stream use to write the content type XML.
		public abstract bool SaveImpl(XmlDocument content, Stream out1);
	}
}
