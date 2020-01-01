using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.OpenXmlFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI
{
	/// A {@link POITextExtractor} for returning the textual
	///  content of the OOXML file properties, eg author
	///  and title.
	public class POIXMLPropertiesTextExtractor : POIXMLTextExtractor
	{
		public override string Text
		{
			get
			{
				return GetCorePropertiesText() + GetExtendedPropertiesText() + GetCustomPropertiesText();
			}
		}

		public override POITextExtractor MetadataTextExtractor
		{
			get
			{
				throw new InvalidOperationException("You already have the Metadata Text Extractor, not recursing!");
			}
		}

		/// Creates a new POIXMLPropertiesTextExtractor for the
		///  given open document.
		public POIXMLPropertiesTextExtractor(POIXMLDocument doc)
			: base(doc)
		{
		}

		/// Creates a new POIXMLPropertiesTextExtractor, for the
		///  same file that another TextExtractor is already
		///  working on.
		public POIXMLPropertiesTextExtractor(POIXMLTextExtractor otherExtractor)
			: base(otherExtractor.Document)
		{
		}

		private void AppendIfPresent(StringBuilder text, string thing, bool value)
		{
			AppendIfPresent(text, thing, value.ToString());
		}

		private void AppendIfPresent(StringBuilder text, string thing, int value)
		{
			AppendIfPresent(text, thing, value.ToString());
		}

		private void AppendIfPresent(StringBuilder text, string thing, DateTime? value)
		{
			if (value.HasValue)
			{
				AppendIfPresent(text, thing, value.ToString());
			}
		}

		private void AppendIfPresent(StringBuilder text, string thing, string value)
		{
			if (value != null)
			{
				text.Append(thing);
				text.Append(" = ");
				text.Append(value);
				text.Append("\n");
			}
		}

		/// Returns the core document properties, eg author
		public string GetCorePropertiesText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			PackagePropertiesPart underlyingProperties = base.Document.GetProperties().CoreProperties.GetUnderlyingProperties();
			AppendIfPresent(stringBuilder, "Category", underlyingProperties.GetCategoryProperty());
			AppendIfPresent(stringBuilder, "Category", underlyingProperties.GetCategoryProperty());
			AppendIfPresent(stringBuilder, "ContentStatus", underlyingProperties.GetContentStatusProperty());
			AppendIfPresent(stringBuilder, "ContentType", underlyingProperties.GetContentTypeProperty());
			AppendIfPresent(stringBuilder, "Created", underlyingProperties.GetCreatedProperty().Value);
			AppendIfPresent(stringBuilder, "CreatedString", underlyingProperties.GetCreatedPropertyString());
			AppendIfPresent(stringBuilder, "Creator", underlyingProperties.GetCreatorProperty());
			AppendIfPresent(stringBuilder, "Description", underlyingProperties.GetDescriptionProperty());
			AppendIfPresent(stringBuilder, "Identifier", underlyingProperties.GetIdentifierProperty());
			AppendIfPresent(stringBuilder, "Keywords", underlyingProperties.GetKeywordsProperty());
			AppendIfPresent(stringBuilder, "Language", underlyingProperties.GetLanguageProperty());
			AppendIfPresent(stringBuilder, "LastModifiedBy", underlyingProperties.GetLastModifiedByProperty());
			AppendIfPresent(stringBuilder, "LastPrinted", underlyingProperties.GetLastPrintedProperty());
			AppendIfPresent(stringBuilder, "LastPrintedString", underlyingProperties.GetLastPrintedPropertyString());
			AppendIfPresent(stringBuilder, "Modified", underlyingProperties.GetModifiedProperty());
			AppendIfPresent(stringBuilder, "ModifiedString", underlyingProperties.GetModifiedPropertyString());
			AppendIfPresent(stringBuilder, "Revision", underlyingProperties.GetRevisionProperty());
			AppendIfPresent(stringBuilder, "Subject", underlyingProperties.GetSubjectProperty());
			AppendIfPresent(stringBuilder, "Title", underlyingProperties.GetTitleProperty());
			AppendIfPresent(stringBuilder, "Version", underlyingProperties.GetVersionProperty());
			return stringBuilder.ToString();
		}

		/// Returns the extended document properties, eg
		///  application
		public string GetExtendedPropertiesText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			CT_ExtendedProperties underlyingProperties = base.Document.GetProperties().ExtendedProperties.GetUnderlyingProperties();
			AppendIfPresent(stringBuilder, "Application", underlyingProperties.Application);
			AppendIfPresent(stringBuilder, "AppVersion", underlyingProperties.AppVersion);
			AppendIfPresent(stringBuilder, "Characters", underlyingProperties.Characters);
			AppendIfPresent(stringBuilder, "CharactersWithSpaces", underlyingProperties.CharactersWithSpaces);
			AppendIfPresent(stringBuilder, "Company", underlyingProperties.Company);
			AppendIfPresent(stringBuilder, "HyperlinkBase", underlyingProperties.HyperlinkBase);
			AppendIfPresent(stringBuilder, "HyperlinksChanged", underlyingProperties.HyperlinksChanged);
			AppendIfPresent(stringBuilder, "Lines", underlyingProperties.Lines);
			AppendIfPresent(stringBuilder, "LinksUpToDate", underlyingProperties.LinksUpToDate);
			AppendIfPresent(stringBuilder, "Manager", underlyingProperties.Manager);
			AppendIfPresent(stringBuilder, "Pages", underlyingProperties.Pages);
			AppendIfPresent(stringBuilder, "Paragraphs", underlyingProperties.Paragraphs);
			AppendIfPresent(stringBuilder, "PresentationFormat", underlyingProperties.PresentationFormat);
			AppendIfPresent(stringBuilder, "Template", underlyingProperties.Template);
			AppendIfPresent(stringBuilder, "TotalTime", underlyingProperties.TotalTime);
			return stringBuilder.ToString();
		}

		/// Returns the custom document properties, if
		///  there are any
		public string GetCustomPropertiesText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			CT_CustomProperties underlyingProperties = base.Document.GetProperties().CustomProperties.GetUnderlyingProperties();
			List<CT_Property> propertyList = underlyingProperties.GetPropertyList();
			foreach (CT_Property item in propertyList)
			{
				string str = "(not implemented!)";
				stringBuilder.Append(item.name + " = " + str + "\n");
			}
			return stringBuilder.ToString();
		}
	}
}
