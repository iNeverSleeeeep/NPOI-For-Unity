using System.Collections;

namespace NPOI.HPSF.Wellknown
{
	/// <summary>
	/// This is a dictionary which maps property ID values To property
	/// ID strings.
	/// The methods {@link #GetSummaryInformationProperties} and {@link
	/// #GetDocumentSummaryInformationProperties} return singleton {@link
	/// PropertyIDMap}s. An application that wants To extend these maps
	/// should treat them as unmodifiable, copy them and modifiy the
	/// copies.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2002-02-09
	/// </summary>
	public class PropertyIDMap : Hashtable
	{
		/// ID of the property that denotes the document's title 
		public const int PID_TITLE = 2;

		/// ID of the property that denotes the document's subject 
		public const int PID_SUBJECT = 3;

		/// ID of the property that denotes the document's author 
		public const int PID_AUTHOR = 4;

		/// ID of the property that denotes the document's keywords 
		public const int PID_KEYWORDS = 5;

		/// ID of the property that denotes the document's comments 
		public const int PID_COMMENTS = 6;

		/// ID of the property that denotes the document's template 
		public const int PID_TEMPLATE = 7;

		/// ID of the property that denotes the document's last author 
		public const int PID_LASTAUTHOR = 8;

		/// ID of the property that denotes the document's revision number 
		public const int PID_REVNUMBER = 9;

		/// ID of the property that denotes the document's edit time 
		public const int PID_EDITTIME = 10;

		/// ID of the property that denotes the DateTime and time the document was
		/// last printed 
		public const int PID_LASTPRINTED = 11;

		/// ID of the property that denotes the DateTime and time the document was
		/// Created. 
		public const int PID_Create_DTM = 12;

		/// ID of the property that denotes the DateTime and time the document was
		/// saved 
		public const int PID_LASTSAVE_DTM = 13;

		/// ID of the property that denotes the number of pages in the
		/// document 
		public const int PID_PAGECOUNT = 14;

		/// ID of the property that denotes the number of words in the
		/// document 
		public const int PID_WORDCOUNT = 15;

		/// ID of the property that denotes the number of characters in the
		/// document 
		public const int PID_CHARCOUNT = 16;

		/// ID of the property that denotes the document's thumbnail 
		public const int PID_THUMBNAIL = 17;

		/// ID of the property that denotes the application that Created the
		/// document 
		public const int PID_APPNAME = 18;

		/// ID of the property that denotes whether Read/Write access To the
		/// document is allowed or whether is should be opened as Read-only. It can
		/// have the following values:
		///
		/// <table>
		///  <tbody>
		///   <tr>
		///    <th>Value</th>
		///    <th>Description</th>
		///   </tr>
		///   <tr>
		///    <th>0</th>
		///    <th>No restriction</th>
		///   </tr>
		///   <tr>
		///    <th>2</th>
		///    <th>Read-only recommended</th>
		///   </tr>
		///   <tr>
		///    <th>4</th>
		///    <th>Read-only enforced</th>
		///   </tr>
		///  </tbody>
		/// </table>
		public const int PID_SECURITY = 19;

		/// The entry is a dictionary.
		public const int PID_DICTIONARY = 0;

		/// The entry denotes a code page.
		public const int PID_CODEPAGE = 1;

		/// The entry is a string denoting the category the file belongs
		/// To, e.g. review, memo, etc. This is useful To Find documents of
		/// same type.
		public const int PID_CATEGORY = 2;

		/// TarGet format for power point presentation, e.g. 35mm,
		/// printer, video etc.
		public const int PID_PRESFORMAT = 3;

		/// Number of bytes.
		public const int PID_BYTECOUNT = 4;

		/// Number of lines.
		public const int PID_LINECOUNT = 5;

		/// Number of paragraphs.
		public const int PID_PARCOUNT = 6;

		/// Number of slides in a power point presentation.
		public const int PID_SLIDECOUNT = 7;

		/// Number of slides with notes.
		public const int PID_NOTECOUNT = 8;

		/// Number of hidden slides.
		public const int PID_HIDDENCOUNT = 9;

		/// Number of multimedia clips, e.g. sound or video.
		public const int PID_MMCLIPCOUNT = 10;

		/// This entry is Set To -1 when scaling of the thumbnail Is
		/// desired. Otherwise the thumbnail should be cropped.
		public const int PID_SCALE = 11;

		/// This entry denotes an internally used property. It is a
		/// vector of variants consisting of pairs of a string (VT_LPSTR)
		/// and a number (VT_I4). The string is a heading name, and the
		/// number tells how many document parts are under that
		/// heading.
		public const int PID_HEADINGPAIR = 12;

		/// This entry Contains the names of document parts (word: names
		/// of the documents in the master document, excel: sheet names,
		/// power point: slide titles, binder: document names).
		public const int PID_DOCPARTS = 13;

		/// This entry Contains the name of the project manager.
		public const int PID_MANAGER = 14;

		/// This entry Contains the company name.
		public const int PID_COMPANY = 15;

		/// If this entry is -1 the links are dirty and should be
		/// re-evaluated.
		public const int PID_LINKSDIRTY = 16;

		/// The highest well-known property ID. Applications are free To use higher values for custom purposes.
		public const int PID_MAX = 16;

		/// Contains the summary information property ID values and
		/// associated strings. See the overall HPSF documentation for
		/// details!
		private static PropertyIDMap summaryInformationProperties;

		/// Contains the summary information property ID values and
		/// associated strings. See the overall HPSF documentation for
		/// details!
		private static PropertyIDMap documentSummaryInformationProperties;

		/// <summary>
		/// Gets the Summary Information properties singleton
		/// </summary>
		/// <returns></returns>
		public static PropertyIDMap SummaryInformationProperties
		{
			get
			{
				if (summaryInformationProperties == null)
				{
					PropertyIDMap propertyIDMap = new PropertyIDMap(18, 1f);
					propertyIDMap.Put(2L, "PID_TITLE");
					propertyIDMap.Put(3L, "PID_SUBJECT");
					propertyIDMap.Put(4L, "PID_AUTHOR");
					propertyIDMap.Put(5L, "PID_KEYWORDS");
					propertyIDMap.Put(6L, "PID_COMMENTS");
					propertyIDMap.Put(7L, "PID_TEMPLATE");
					propertyIDMap.Put(8L, "PID_LASTAUTHOR");
					propertyIDMap.Put(9L, "PID_REVNUMBER");
					propertyIDMap.Put(10L, "PID_EDITTIME");
					propertyIDMap.Put(11L, "PID_LASTPRINTED");
					propertyIDMap.Put(12L, "PID_Create_DTM");
					propertyIDMap.Put(13L, "PID_LASTSAVE_DTM");
					propertyIDMap.Put(14L, "PID_PAGECOUNT");
					propertyIDMap.Put(15L, "PID_WORDCOUNT");
					propertyIDMap.Put(16L, "PID_CHARCOUNT");
					propertyIDMap.Put(17L, "PID_THUMBNAIL");
					propertyIDMap.Put(18L, "PID_APPNAME");
					propertyIDMap.Put(19L, "PID_SECURITY");
					summaryInformationProperties = propertyIDMap;
				}
				return summaryInformationProperties;
			}
		}

		/// <summary>
		/// Gets the Document Summary Information properties
		/// singleton.
		/// </summary>
		/// <returns>The Document Summary Information properties singleton.</returns>
		public static PropertyIDMap DocumentSummaryInformationProperties
		{
			get
			{
				if (documentSummaryInformationProperties == null)
				{
					PropertyIDMap propertyIDMap = new PropertyIDMap(17, 1f);
					propertyIDMap.Put(0L, "PID_DICTIONARY");
					propertyIDMap.Put(1L, "PID_CODEPAGE");
					propertyIDMap.Put(2L, "PID_CATEGORY");
					propertyIDMap.Put(3L, "PID_PRESFORMAT");
					propertyIDMap.Put(4L, "PID_BYTECOUNT");
					propertyIDMap.Put(5L, "PID_LINECOUNT");
					propertyIDMap.Put(6L, "PID_PARCOUNT");
					propertyIDMap.Put(7L, "PID_SLIDECOUNT");
					propertyIDMap.Put(8L, "PID_NOTECOUNT");
					propertyIDMap.Put(9L, "PID_HIDDENCOUNT");
					propertyIDMap.Put(10L, "PID_MMCLIPCOUNT");
					propertyIDMap.Put(11L, "PID_SCALE");
					propertyIDMap.Put(12L, "PID_HEADINGPAIR");
					propertyIDMap.Put(13L, "PID_DOCPARTS");
					propertyIDMap.Put(14L, "PID_MANAGER");
					propertyIDMap.Put(15L, "PID_COMPANY");
					propertyIDMap.Put(16L, "PID_LINKSDIRTY");
					documentSummaryInformationProperties = propertyIDMap;
				}
				return documentSummaryInformationProperties;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.Wellknown.PropertyIDMap" /> class.
		/// </summary>
		/// <param name="initialCapacity">initialCapacity The initial capacity as defined for
		/// {@link HashMap}</param>
		/// <param name="loadFactor">The load factor as defined for {@link HashMap}</param>
		public PropertyIDMap(int initialCapacity, float loadFactor)
			: base(initialCapacity, loadFactor)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.Wellknown.PropertyIDMap" /> class.
		/// </summary>
		/// <param name="map">The instance To be Created is backed by this map.</param>
		public PropertyIDMap(IDictionary map)
			: base(map)
		{
		}

		/// <summary>
		/// Puts a ID string for an ID into the {@link
		/// PropertyIDMap}.
		/// </summary>
		/// <param name="id">The ID string.</param>
		/// <param name="idString">The id string.</param>
		/// <returns>As specified by the {@link java.util.Map} interface, this method
		/// returns the previous value associated with the specified id</returns>
		public object Put(long id, string idString)
		{
			return this[id] = idString;
		}

		/// <summary>
		/// Gets the ID string for an ID from the {@link
		/// PropertyIDMap}.
		/// </summary>
		/// <param name="id">The ID.</param>
		/// <returns>The ID string associated with id</returns>
		public object Get(long id)
		{
			return this[id];
		}
	}
}
