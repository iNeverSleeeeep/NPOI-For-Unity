using System;

namespace NPOI.OpenXml4Net.OPC
{
	/// Represents the core properties of an OPC package.
	///
	/// @author Julien Chable
	/// @version 1.0
	/// @see org.apache.poi.OpenXml4Net.opc.OPCPackage
	public interface PackageProperties
	{
		/// Set the category of the content of this package.
		string GetCategoryProperty();

		/// Set the category of the content of this package.
		void SetCategoryProperty(string category);

		/// Set the status of the content.
		string GetContentStatusProperty();

		/// Get the status of the content.
		void SetContentStatusProperty(string contentStatus);

		/// Get the type of content represented, generally defined by a specific use
		/// and intended audience.
		string GetContentTypeProperty();

		/// Set the type of content represented, generally defined by a specific use
		/// and intended audience.
		void SetContentTypeProperty(string contentType);

		/// Get the date of creation of the resource.
		DateTime? GetCreatedProperty();

		/// Set the date of creation of the resource.
		void SetCreatedProperty(string created);

		/// Set the date of creation of the resource.
		void SetCreatedProperty(DateTime? created);

		/// Get the entity primarily responsible for making the content of the
		/// resource.
		string GetCreatorProperty();

		/// Set the entity primarily responsible for making the content of the
		/// resource.
		void SetCreatorProperty(string creator);

		/// Get the explanation of the content of the resource.
		string GetDescriptionProperty();

		/// Set the explanation of the content of the resource.
		void SetDescriptionProperty(string description);

		/// Get an unambiguous reference to the resource within a given context.
		string GetIdentifierProperty();

		/// Set an unambiguous reference to the resource within a given context.
		void SetIdentifierProperty(string identifier);

		/// Get a delimited Set of keywords to support searching and indexing. This
		/// is typically a list of terms that are not available elsewhere in the
		/// properties
		string GetKeywordsProperty();

		/// Set a delimited Set of keywords to support searching and indexing. This
		/// is typically a list of terms that are not available elsewhere in the
		/// properties
		void SetKeywordsProperty(string keywords);

		/// Get the language of the intellectual content of the resource.
		string GetLanguageProperty();

		/// Set the language of the intellectual content of the resource.
		void SetLanguageProperty(string language);

		/// Get the user who performed the last modification.
		string GetLastModifiedByProperty();

		/// Set the user who performed the last modification.
		void SetLastModifiedByProperty(string lastModifiedBy);

		/// Get the date and time of the last printing.
		DateTime? GetLastPrintedProperty();

		/// Set the date and time of the last printing.
		void SetLastPrintedProperty(string lastPrinted);

		/// Set the date and time of the last printing.
		void SetLastPrintedProperty(DateTime? lastPrinted);

		/// Get the date on which the resource was changed.
		DateTime? GetModifiedProperty();

		/// Set the date on which the resource was changed.
		void SetModifiedProperty(string modified);

		/// Set the date on which the resource was changed.
		void SetModifiedProperty(DateTime? modified);

		/// Get the revision number.
		string GetRevisionProperty();

		/// Set the revision number.
		void SetRevisionProperty(string revision);

		/// Get the topic of the content of the resource.
		string GetSubjectProperty();

		/// Set the topic of the content of the resource.
		void SetSubjectProperty(string subject);

		/// Get the name given to the resource.
		string GetTitleProperty();

		/// Set the name given to the resource.
		void SetTitleProperty(string title);

		/// Get the version number.
		string GetVersionProperty();

		/// Set the version number.
		void SetVersionProperty(string version);
	}
}
