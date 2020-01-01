using NPOI.OpenXml4Net.OPC;

namespace NPOI
{
	public abstract class POIXMLTextExtractor : POITextExtractor
	{
		/// The POIXMLDocument that's open 
		private POIXMLDocument _document;

		/// Returns opened document
		public POIXMLDocument Document
		{
			get
			{
				return _document;
			}
		}

		/// Returns the opened OPCPackage that Contains the document
		public OPCPackage Package
		{
			get
			{
				return _document.Package;
			}
		}

		/// Returns an OOXML properties text extractor for the
		///  document properties metadata, such as title and author.
		public override POITextExtractor MetadataTextExtractor
		{
			get
			{
				return new POIXMLPropertiesTextExtractor(_document);
			}
		}

		/// Creates a new text extractor for the given document
		public POIXMLTextExtractor(POIXMLDocument document)
			: base((POIDocument)null)
		{
			_document = document;
		}

		/// Returns the core document properties
		public CoreProperties GetCoreProperties()
		{
			return _document.GetProperties().CoreProperties;
		}

		/// Returns the extended document properties
		public ExtendedProperties GetExtendedProperties()
		{
			return _document.GetProperties().ExtendedProperties;
		}

		/// Returns the custom document properties
		public CustomProperties GetCustomProperties()
		{
			return _document.GetProperties().CustomProperties;
		}
	}
}
