namespace NPOI
{
	/// <summary>
	/// Common Parent for Text Extractors
	/// of POI Documents.
	/// You will typically find the implementation of
	/// a given format's text extractor under
	/// org.apache.poi.[format].extractor .
	/// </summary>
	/// @see org.apache.poi.hssf.extractor.ExcelExtractor
	/// @see org.apache.poi.hslf.extractor.PowerPointExtractor
	/// @see org.apache.poi.hdgf.extractor.VisioTextExtractor
	/// @see org.apache.poi.hwpf.extractor.WordExtractor
	public abstract class POITextExtractor
	{
		/// The POIDocument that's open 
		protected POIDocument document;

		/// <summary>
		/// Retrieves all the text from the document.
		/// How cells, paragraphs etc are separated in the text
		/// is implementation specific - see the javadocs for
		/// a specific project for details.
		/// </summary>
		/// <value>All the text from the document.</value>
		public abstract string Text
		{
			get;
		}

		/// <summary>
		/// Returns another text extractor, which is able to
		/// output the textual content of the document
		/// metadata / properties, such as author and title.
		/// </summary>
		/// <value>The metadata text extractor.</value>
		public abstract POITextExtractor MetadataTextExtractor
		{
			get;
		}

		/// <summary>
		/// Creates a new text extractor for the given document
		/// </summary>
		/// <param name="document">The document.</param>
		public POITextExtractor(POIDocument document)
		{
			this.document = document;
		}

		/// <summary>
		/// Creates a new text extractor, using the same
		/// document as another text extractor. Normally
		/// only used by properties extractors.
		/// </summary>
		/// <param name="otherExtractor">The other extractor.</param>
		protected POITextExtractor(POITextExtractor otherExtractor)
		{
			document = otherExtractor.document;
		}
	}
}
