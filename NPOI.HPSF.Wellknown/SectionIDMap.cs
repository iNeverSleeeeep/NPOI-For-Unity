using System.Collections;
using System.Text;

namespace NPOI.HPSF.Wellknown
{
	/// <summary>
	/// Maps section format IDs To {@link PropertyIDMap}s. It Is
	/// initialized with two well-known section format IDs: those of the
	/// <c>\005SummaryInformation</c> stream and the
	/// <c>\005DocumentSummaryInformation</c> stream.
	/// If you have a section format ID you can use it as a key To query
	/// this map.  If you Get a {@link PropertyIDMap} returned your section
	/// is well-known and you can query the {@link PropertyIDMap} for PID
	/// strings. If you Get back <c>null</c> you are on your own.
	/// This {@link java.util.Map} expects the byte arrays of section format IDs
	/// as keys. A key maps To a {@link PropertyIDMap} describing the
	/// property IDs in sections with the specified section format ID.
	/// @author Rainer Klute (klute@rainer-klute.de)
	/// @since 2002-02-09
	/// </summary>
	public class SectionIDMap : Hashtable
	{
		/// A property without a known name is described by this string. 
		public const string UNDEFINED = "[undefined]";

		/// The SummaryInformation's section's format ID.
		public static readonly byte[] SUMMARY_INFORMATION_ID = new byte[16]
		{
			242,
			159,
			133,
			224,
			79,
			249,
			16,
			104,
			171,
			145,
			8,
			0,
			43,
			39,
			179,
			217
		};

		/// The DocumentSummaryInformation's first and second sections' format
		/// ID.
		public static readonly byte[] DOCUMENT_SUMMARY_INFORMATION_ID1 = new byte[16]
		{
			213,
			205,
			213,
			2,
			46,
			156,
			16,
			27,
			147,
			151,
			8,
			0,
			43,
			44,
			249,
			174
		};

		public static readonly byte[] DOCUMENT_SUMMARY_INFORMATION_ID2 = new byte[16]
		{
			213,
			205,
			213,
			5,
			46,
			156,
			16,
			27,
			147,
			151,
			8,
			0,
			43,
			44,
			249,
			174
		};

		/// The default section ID map. It maps section format IDs To
		/// {@link PropertyIDMap}s.
		private static SectionIDMap defaultMap;

		/// <summary>
		/// Returns the singleton instance of the default {@link
		/// SectionIDMap}.
		/// </summary>
		/// <returns>The instance value</returns>
		public static SectionIDMap GetInstance()
		{
			if (defaultMap == null)
			{
				SectionIDMap sectionIDMap = new SectionIDMap();
				sectionIDMap.Put(SUMMARY_INFORMATION_ID, PropertyIDMap.SummaryInformationProperties);
				sectionIDMap.Put(DOCUMENT_SUMMARY_INFORMATION_ID1, PropertyIDMap.DocumentSummaryInformationProperties);
				defaultMap = sectionIDMap;
			}
			return defaultMap;
		}

		/// <summary>
		/// Returns the property ID string that is associated with a
		/// given property ID in a section format ID's namespace.
		/// </summary>
		/// <param name="sectionFormatID">Each section format ID has its own name
		/// space of property ID strings and thus must be specified.</param>
		/// <param name="pid">The property ID</param>
		/// <returns>The well-known property ID string associated with the
		/// property ID pid in the name space spanned by sectionFormatID If the pid
		/// sectionFormatID combination is not well-known, the
		/// string "[undefined]" is returned.
		/// </returns>
		public static string GetPIDString(byte[] sectionFormatID, long pid)
		{
			PropertyIDMap propertyIDMap = GetInstance().Get(sectionFormatID);
			if (propertyIDMap == null)
			{
				return "[undefined]";
			}
			string text = (string)propertyIDMap.Get(pid);
			if (text == null)
			{
				return "[undefined]";
			}
			return text;
		}

		/// <summary>
		/// Returns the {@link PropertyIDMap} for a given section format
		/// ID.
		/// </summary>
		/// <param name="sectionFormatID">The section format ID.</param>
		/// <returns>the property ID map</returns>
		public PropertyIDMap Get(byte[] sectionFormatID)
		{
			return (PropertyIDMap)this[Encoding.UTF8.GetString(sectionFormatID)];
		}

		/// <summary>
		/// Returns the {@link PropertyIDMap} for a given section format
		/// ID.
		/// </summary>
		/// <param name="sectionFormatID">A section format ID as a 
		/// <c>byte[]</c></param>
		/// <returns>the property ID map</returns>
		public object Get(object sectionFormatID)
		{
			return Get((byte[])sectionFormatID);
		}

		/// <summary>
		/// Associates a section format ID with a {@link
		/// PropertyIDMap}.
		/// </summary>
		/// <param name="sectionFormatID">the section format ID</param>
		/// <param name="propertyIDMap">The property ID map.</param>
		/// <returns></returns>
		public object Put(byte[] sectionFormatID, PropertyIDMap propertyIDMap)
		{
			return this[sectionFormatID] = propertyIDMap;
		}

		/// <summary>
		/// Puts the specified key.
		/// </summary>
		/// <param name="key">This parameter remains undocumented since the method Is
		/// deprecated.</param>
		/// <param name="value">This parameter remains undocumented since the method Is
		/// deprecated.</param>
		/// <returns>The return value remains undocumented since the method Is
		/// deprecated.</returns>
		public object Put(object key, object value)
		{
			return Put((byte[])key, (PropertyIDMap)value);
		}
	}
}
