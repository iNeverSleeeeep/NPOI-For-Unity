using System;
using System.Collections;

namespace NPOI.HPSF
{
	/// <summary>
	/// The <em>Variant</em> types as defined by Microsoft's COM. I
	/// found this information in <a href="http://www.marin.clara.net/COM/variant_type_definitions.htm">
	/// http://www.marin.clara.net/COM/variant_type_definitions.htm</a>.
	/// In the variant types descriptions the following shortcuts are
	/// used: <strong> [V]</strong> - may appear in a VARIANT,
	/// <strong>[T]</strong> - may appear in a TYPEDESC,
	/// <strong>[P]</strong> - may appear in an OLE property Set,
	/// <strong>[S]</strong> - may appear in a Safe Array.
	/// @author Rainer Klute (klute@rainer-klute.de)
	/// @since 2002-02-09
	/// </summary>
	public class Variant
	{
		/// [V][P] Nothing, i.e. not a single byte of data.
		public const int VT_EMPTY = 0;

		/// [V][P] SQL style Null.
		public const int VT_NULL = 1;

		/// [V][T][P][S] 2 byte signed int.
		public const int VT_I2 = 2;

		/// [V][T][P][S] 4 byte signed int.
		public const int VT_I4 = 3;

		/// [V][T][P][S] 4 byte real.
		public const int VT_R4 = 4;

		/// [V][T][P][S] 8 byte real.
		public const int VT_R8 = 5;

		/// [V][T][P][S] currency. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_CY = 6;

		/// [V][T][P][S] DateTime. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_DATE = 7;

		/// [V][T][P][S] OLE Automation string. <span style="background-color: #ffff00">How long is this? How is it
		/// To be interpreted?</span>
		public const int VT_BSTR = 8;

		/// [V][T][P][S] IDispatch *. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_DISPATCH = 9;

		/// [V][T][S] SCODE. <span style="background-color: #ffff00">How
		/// long is this? How is it To be interpreted?</span>
		public const int VT_ERROR = 10;

		/// [V][T][P][S] True=-1, False=0.
		public const int VT_BOOL = 11;

		/// [V][T][P][S] VARIANT *. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_VARIANT = 12;

		/// [V][T][S] IUnknown *. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_UNKNOWN = 13;

		/// [V][T][S] 16 byte fixed point.
		public const int VT_DECIMAL = 14;

		/// [T] signed char.
		public const int VT_I1 = 16;

		/// [V][T][P][S] unsigned char.
		public const int VT_UI1 = 17;

		/// [T][P] unsigned short.
		public const int VT_UI2 = 18;

		/// [T][P] unsigned int.
		public const int VT_UI4 = 19;

		/// [T][P] signed 64-bit int.
		public const int VT_I8 = 20;

		/// [T][P] unsigned 64-bit int.
		public const int VT_UI8 = 21;

		/// [T] signed machine int.
		public const int VT_INT = 22;

		/// [T] unsigned machine int.
		public const int VT_UINT = 23;

		/// [T] C style void.
		public const int VT_VOID = 24;

		/// [T] Standard return type. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_HRESULT = 25;

		/// [T] pointer type. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_PTR = 26;

		/// [T] (use VT_ARRAY in VARIANT).
		public const int VT_SAFEARRAY = 27;

		/// [T] C style array. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_CARRAY = 28;

		/// [T] user defined type. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_USERDEFINED = 29;

		/// [T][P] null terminated string.
		public const int VT_LPSTR = 30;

		/// [T][P] wide (Unicode) null terminated string.
		public const int VT_LPWSTR = 31;

		/// [P] FILETIME. The FILETIME structure holds a DateTime and time
		/// associated with a file. The structure identifies a 64-bit
		/// integer specifying the number of 100-nanosecond intervals which
		/// have passed since January 1, 1601. This 64-bit value is split
		/// into the two dwords stored in the structure.
		public const int VT_FILETIME = 64;

		/// [P] Length prefixed bytes.
		public const int VT_BLOB = 65;

		/// [P] Name of the stream follows.
		public const int VT_STREAM = 66;

		/// [P] Name of the storage follows.
		public const int VT_STORAGE = 67;

		/// [P] Stream Contains an object. <span style="background-color: #ffff00"> How long is this? How is it
		/// To be interpreted?</span>
		public const int VT_STREAMED_OBJECT = 68;

		/// [P] Storage Contains an object. <span style="background-color: #ffff00"> How long is this? How is it
		/// To be interpreted?</span>
		public const int VT_STORED_OBJECT = 69;

		/// [P] Blob Contains an object. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_BLOB_OBJECT = 70;

		/// [P] Clipboard format. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_CF = 71;

		/// [P] A Class ID.
		///
		/// It consists of a 32 bit unsigned integer indicating the size
		/// of the structure, a 32 bit signed integer indicating (Clipboard
		/// Format Tag) indicating the type of data that it Contains, and
		/// then a byte array containing the data.
		///
		/// The valid Clipboard Format Tags are:
		///
		/// <ul>
		///  <li>{@link Thumbnail#CFTAG_WINDOWS}</li>
		///  <li>{@link Thumbnail#CFTAG_MACINTOSH}</li>
		///  <li>{@link Thumbnail#CFTAG_NODATA}</li>
		///  <li>{@link Thumbnail#CFTAG_FMTID}</li>
		/// </ul>
		///
		/// <pre>typedef struct tagCLIPDATA {
		/// // cbSize is the size of the buffer pointed To
		/// // by pClipData, plus sizeof(ulClipFmt)
		/// ULONG              cbSize;
		/// long               ulClipFmt;
		/// BYTE*              pClipData;
		/// } CLIPDATA;</pre>
		///
		/// See <a href="msdn.microsoft.com/library/en-us/com/stgrstrc_0uwk.asp" tarGet="_blank">
		/// msdn.microsoft.com/library/en-us/com/stgrstrc_0uwk.asp</a>.
		public const int VT_CLSID = 72;

		/// "MUST be a VersionedStream. The storage representing the (non-simple)
		/// property set MUST have a stream element with the name in the StreamName
		/// field." -- [MS-OLEPS] -- v20110920; Object Linking and Embedding (OLE)
		/// Property Set Data Structures; page 24 / 63
		public const int VT_VERSIONED_STREAM = 73;

		/// [P] simple counted array. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_VECTOR = 4096;

		/// [V] SAFEARRAY*. <span style="background-color: #ffff00">How
		/// long is this? How is it To be interpreted?</span>
		public const int VT_ARRAY = 8192;

		/// [V] void* for local use. <span style="background-color:&#xD;&#xA;            #ffff00">How long is this? How is it To be
		/// interpreted?</span>
		public const int VT_BYREF = 16384;

		/// FIXME (3): Document this!
		public const int VT_RESERVED = 32768;

		/// FIXME (3): Document this!
		public const int VT_ILLEGAL = 65535;

		/// FIXME (3): Document this!
		public const int VT_ILLEGALMASKED = 4095;

		/// FIXME (3): Document this!
		public const int VT_TYPEMASK = 4095;

		/// Denotes a variant type with a Length that is unknown To HPSF yet.
		public const int Length_UNKNOWN = -2;

		/// Denotes a variant type with a variable Length.
		public const int Length_VARIABLE = -1;

		/// Denotes a variant type with a Length of 0 bytes.
		public const int Length_0 = 0;

		/// Denotes a variant type with a Length of 2 bytes.
		public const int Length_2 = 2;

		/// Denotes a variant type with a Length of 4 bytes.
		public const int Length_4 = 4;

		/// Denotes a variant type with a Length of 8 bytes.
		public const int Length_8 = 8;

		/// Maps the numbers denoting the variant types To their corresponding
		/// variant type names.
		private static IDictionary numberToName;

		private static IDictionary numberToLength;

		static Variant()
		{
			numberToName = new Hashtable
			{
				[0] = "VT_EMPTY",
				[1] = "VT_NULL",
				[2] = "VT_I2",
				[3] = "VT_I4",
				[4] = "VT_R4",
				[5] = "VT_R8",
				[6] = "VT_CY",
				[7] = "VT_DATE",
				[8] = "VT_BSTR",
				[9] = "VT_DISPATCH",
				[10] = "VT_ERROR",
				[11] = "VT_BOOL",
				[12] = "VT_VARIANT",
				[13] = "VT_UNKNOWN",
				[14] = "VT_DECIMAL",
				[16] = "VT_I1",
				[17] = "VT_UI1",
				[18] = "VT_UI2",
				[19] = "VT_UI4",
				[20] = "VT_I8",
				[21] = "VT_UI8",
				[22] = "VT_INT",
				[23] = "VT_UINT",
				[24] = "VT_VOID",
				[25] = "VT_HRESULT",
				[26] = "VT_PTR",
				[27] = "VT_SAFEARRAY",
				[28] = "VT_CARRAY",
				[29] = "VT_USERDEFINED",
				[30] = "VT_LPSTR",
				[31] = "VT_LPWSTR",
				[64] = "VT_FILETIME",
				[65] = "VT_BLOB",
				[66] = "VT_STREAM",
				[67] = "VT_STORAGE",
				[68] = "VT_STREAMED_OBJECT",
				[69] = "VT_STORED_OBJECT",
				[70] = "VT_BLOB_OBJECT",
				[71] = "VT_CF",
				[72] = "VT_CLSID"
			};
			numberToLength = new Hashtable
			{
				[0] = 0,
				[1] = -2,
				[2] = 2,
				[3] = 4,
				[4] = 4,
				[5] = 8,
				[6] = -2,
				[7] = -2,
				[8] = -2,
				[9] = -2,
				[10] = -2,
				[11] = -2,
				[12] = -2,
				[13] = -2,
				[14] = -2,
				[16] = -2,
				[17] = -2,
				[18] = -2,
				[19] = -2,
				[20] = -2,
				[21] = -2,
				[22] = -2,
				[23] = -2,
				[24] = -2,
				[25] = -2,
				[26] = -2,
				[27] = -2,
				[28] = -2,
				[29] = -2,
				[30] = -1,
				[31] = -2,
				[64] = 8,
				[65] = -2,
				[66] = -2,
				[67] = -2,
				[68] = -2,
				[69] = -2,
				[70] = -2,
				[71] = -2,
				[72] = -2
			};
		}

		/// <summary>
		/// Returns the variant type name associated with a variant type
		/// number.
		/// </summary>
		/// <param name="variantType">The variant type number.</param>
		/// <returns>The variant type name or the string "unknown variant type"</returns>
		public static string GetVariantName(long variantType)
		{
			string text = (string)numberToName[variantType];
			if (text == null)
			{
				return "unknown variant type";
			}
			return text;
		}

		/// <summary>
		/// Returns a variant type's Length.
		/// </summary>
		/// <param name="variantType">The variant type number.</param>
		/// <returns>The Length of the variant type's data in bytes. If the Length Is
		/// variable, i.e. the Length of a string, -1 is returned. If HPSF does not
		/// know the Length, -2 is returned. The latter usually indicates an
		/// unsupported variant type.</returns>
		public static int GetVariantLength(long variantType)
		{
			long num = (int)variantType;
			if (numberToLength.Contains(num))
			{
				return -2;
			}
			long value = (long)numberToLength[num];
			return Convert.ToInt32(value);
		}
	}
}
