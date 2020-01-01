using NPOI.HPSF.Wellknown;
using System.IO;

namespace NPOI.HPSF
{
	/// <summary>
	/// Factory class To Create instances of {@link SummaryInformation},
	/// {@link DocumentSummaryInformation} and {@link PropertySet}.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2002-02-09
	/// </summary>
	public class PropertySetFactory
	{
		/// <summary>
		/// Creates the most specific {@link PropertySet} from an {@link
		/// InputStream}. This is preferrably a {@link
		/// DocumentSummaryInformation} or a {@link SummaryInformation}. If
		/// the specified {@link InputStream} does not contain a property
		/// Set stream, an exception is thrown and the {@link InputStream}
		/// is repositioned at its beginning.
		/// </summary>
		/// <param name="stream">Contains the property set stream's data.</param>
		/// <returns>The Created {@link PropertySet}.</returns>
		public static PropertySet Create(Stream stream)
		{
			PropertySet propertySet = new PropertySet(stream);
			try
			{
				if (propertySet.IsSummaryInformation)
				{
					return new SummaryInformation(propertySet);
				}
				if (propertySet.IsDocumentSummaryInformation)
				{
					return new DocumentSummaryInformation(propertySet);
				}
				return propertySet;
			}
			catch (UnexpectedPropertySetTypeException)
			{
				throw;
			}
		}

		/// <summary>
		/// Creates a new summary information
		/// </summary>
		/// <returns>the new summary information.</returns>
		public static SummaryInformation CreateSummaryInformation()
		{
			MutablePropertySet mutablePropertySet = new MutablePropertySet();
			MutableSection mutableSection = (MutableSection)mutablePropertySet.FirstSection;
			mutableSection.SetFormatID(SectionIDMap.SUMMARY_INFORMATION_ID);
			try
			{
				return new SummaryInformation(mutablePropertySet);
			}
			catch (UnexpectedPropertySetTypeException reason)
			{
				throw new HPSFRuntimeException(reason);
			}
		}

		/// <summary>
		/// Creates a new document summary information.
		/// </summary>
		/// <returns>the new document summary information.</returns>
		public static DocumentSummaryInformation CreateDocumentSummaryInformation()
		{
			MutablePropertySet mutablePropertySet = new MutablePropertySet();
			MutableSection mutableSection = (MutableSection)mutablePropertySet.FirstSection;
			mutableSection.SetFormatID(SectionIDMap.DOCUMENT_SUMMARY_INFORMATION_ID1);
			try
			{
				return new DocumentSummaryInformation(mutablePropertySet);
			}
			catch (UnexpectedPropertySetTypeException reason)
			{
				throw new HPSFRuntimeException(reason);
			}
		}
	}
}
