using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NPOI.HPSF
{
	/// <summary>
	/// Supports Reading and writing of variant data.
	/// <strong>FIXME (3):</strong>
	///  Reading and writing should be made more
	/// uniform than it is now. The following items should be resolved:
	/// Reading requires a Length parameter that is 4 byte greater than the
	/// actual data, because the variant type field is included.
	/// Reading Reads from a byte array while writing Writes To an byte array
	/// output stream.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2003-08-08
	/// </summary>
	public class VariantSupport : Variant
	{
		private static bool logUnsupportedTypes = false;

		/// Keeps a list of the variant types an "unsupported" message has alReady
		/// been issued for.
		protected static List<long> unsupportedMessage;

		/// HPSF is able To Read these {@link Variant} types.
		public static int[] SUPPORTED_TYPES = new int[10]
		{
			0,
			2,
			3,
			20,
			5,
			64,
			30,
			31,
			71,
			11
		};

		/// <summary>
		/// Checks whether logging of unsupported variant types warning is turned
		/// on or off.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if logging is turned on; otherwise, <c>false</c>.
		/// </value>
		public static bool IsLogUnsupportedTypes
		{
			get
			{
				return logUnsupportedTypes;
			}
			set
			{
				logUnsupportedTypes = value;
			}
		}

		/// <summary>
		/// Writes a warning To System.err that a variant type Is
		/// unsupported by HPSF. Such a warning is written only once for each variant
		/// type. Log messages can be turned on or off by
		/// </summary>
		/// <param name="ex">The exception To log</param>
		public static void WriteUnsupportedTypeMessage(UnsupportedVariantTypeException ex)
		{
			if (IsLogUnsupportedTypes)
			{
				if (unsupportedMessage == null)
				{
					unsupportedMessage = new List<long>();
				}
				long variantType = ex.VariantType;
				if (!unsupportedMessage.Contains(variantType))
				{
					Console.Error.WriteLine(ex.Message);
					unsupportedMessage.Add(variantType);
				}
			}
		}

		/// <summary>
		/// Checks whether HPSF supports the specified variant type. Unsupported
		/// types should be implemented included in the {@link #SUPPORTED_TYPES}
		/// array.
		/// </summary>
		/// <param name="variantType">the variant type To check</param>
		/// <returns>
		/// 	<c>true</c> if HPFS supports this type,otherwise, <c>false</c>.
		/// </returns>
		public bool IsSupportedType(int variantType)
		{
			for (int i = 0; i < SUPPORTED_TYPES.Length; i++)
			{
				if (variantType == SUPPORTED_TYPES[i])
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Reads a variant type from a byte array
		/// </summary>
		/// <param name="src">The byte array</param>
		/// <param name="offset">The offset in the byte array where the variant starts</param>
		/// <param name="length">The Length of the variant including the variant type field</param>
		/// <param name="type">The variant type To Read</param>
		/// <param name="codepage">The codepage To use for non-wide strings</param>
		/// <returns>A Java object that corresponds best To the variant field. For
		/// example, a VT_I4 is returned as a {@link long}, a VT_LPSTR as a
		/// {@link String}.</returns>
		public static object Read(byte[] src, int offset, int length, long type, int codepage)
		{
			TypedPropertyValue typedPropertyValue = new TypedPropertyValue((int)type, null);
			int num;
			try
			{
				num = typedPropertyValue.ReadValue(src, offset);
			}
			catch (InvalidOperationException)
			{
				int num2 = Math.Min(length, src.Length - offset);
				byte[] array = new byte[num2];
				System.Array.Copy(src, offset, array, 0, num2);
				throw new ReadingNotSupportedException(type, array);
			}
			switch (type)
			{
			case 0L:
			case 3L:
			case 5L:
			case 20L:
				return typedPropertyValue.Value;
			case 2L:
				return (short)typedPropertyValue.Value;
			case 64L:
			{
				Filetime filetime = (Filetime)typedPropertyValue.Value;
				return Util.FiletimeToDate((int)filetime.High, (int)filetime.Low);
			}
			case 30L:
			{
				CodePageString codePageString = (CodePageString)typedPropertyValue.Value;
				return codePageString.GetJavaValue(codepage);
			}
			case 31L:
			{
				UnicodeString unicodeString = (UnicodeString)typedPropertyValue.Value;
				return unicodeString.ToJavaString();
			}
			case 71L:
			{
				ClipboardData clipboardData = (ClipboardData)typedPropertyValue.Value;
				return clipboardData.ToByteArray();
			}
			case 11L:
			{
				VariantBool variantBool = (VariantBool)typedPropertyValue.Value;
				return variantBool.Value;
			}
			default:
			{
				byte[] array2 = new byte[num];
				System.Array.Copy(src, offset, array2, 0, num);
				throw new ReadingNotSupportedException(type, array2);
			}
			}
		}

		/// <p>Turns a codepage number into the equivalent character encoding's
		/// name.</p>
		///
		/// @param codepage The codepage number
		///
		/// @return The character encoding's name. If the codepage number is 65001,
		/// the encoding name is "UTF-8". All other positive numbers are mapped to
		/// "cp" followed by the number, e.g. if the codepage number is 1252 the
		/// returned character encoding name will be "cp1252".
		///
		/// @exception UnsupportedEncodingException if the specified codepage is
		/// less than zero.
		public static string CodepageToEncoding(int codepage)
		{
			if (codepage > 0)
			{
				switch (codepage)
				{
				case 1200:
					return "UTF-16";
				case 1201:
					return "UTF-16BE";
				case 65001:
					return "UTF-8";
				case 37:
					return "cp037";
				case 936:
					return "GBK";
				case 949:
					return "ms949";
				case 1250:
					return "windows-1250";
				case 1251:
					return "windows-1251";
				case 1252:
					return "windows-1252";
				case 1253:
					return "windows-1253";
				case 1254:
					return "windows-1254";
				case 1255:
					return "windows-1255";
				case 1256:
					return "windows-1256";
				case 1257:
					return "windows-1257";
				case 1258:
					return "windows-1258";
				case 1361:
					return "johab";
				case 10000:
					return "MacRoman";
				case 10001:
					return "SJIS";
				case 10002:
					return "Big5";
				case 10003:
					return "EUC-KR";
				case 10004:
					return "MacArabic";
				case 10005:
					return "MacHebrew";
				case 10006:
					return "MacGreek";
				case 10007:
					return "MacCyrillic";
				case 10008:
					return "EUC_CN";
				case 10010:
					return "MacRomania";
				case 10017:
					return "MacUkraine";
				case 10021:
					return "MacThai";
				case 10029:
					return "MacCentralEurope";
				case 10079:
					return "MacIceland";
				case 10081:
					return "MacTurkish";
				case 10082:
					return "MacCroatian";
				case 20127:
				case 65000:
					return "US-ASCII";
				case 20866:
					return "KOI8-R";
				case 28591:
					return "ISO-8859-1";
				case 28592:
					return "ISO-8859-2";
				case 28593:
					return "ISO-8859-3";
				case 28594:
					return "ISO-8859-4";
				case 28595:
					return "ISO-8859-5";
				case 28596:
					return "ISO-8859-6";
				case 28597:
					return "ISO-8859-7";
				case 28598:
					return "ISO-8859-8";
				case 28599:
					return "ISO-8859-9";
				case 50220:
				case 50221:
				case 50222:
					return "ISO-2022-JP";
				case 50225:
					return "ISO-2022-KR";
				case 51932:
					return "EUC-JP";
				case 51949:
					return "EUC-KR";
				case 52936:
					return "GB2312";
				case 54936:
					return "GB18030";
				case 932:
					return "SJIS";
				default:
					return "cp" + codepage;
				}
			}
			throw new UnsupportedEncodingException("Codepage number may not be " + codepage);
		}

		/// <summary>
		/// Writes a variant value To an output stream. This method ensures that
		/// always a multiple of 4 bytes is written.
		/// If the codepage is UTF-16, which is encouraged, strings
		/// <strong>must</strong> always be written as {@link Variant#VT_LPWSTR}
		/// strings, not as {@link Variant#VT_LPSTR} strings. This method ensure this
		/// by Converting strings appropriately, if needed.
		/// </summary>
		/// <param name="out1">The stream To Write the value To.</param>
		/// <param name="type">The variant's type.</param>
		/// <param name="value">The variant's value.</param>
		/// <param name="codepage">The codepage To use To Write non-wide strings</param>
		/// <returns>The number of entities that have been written. In many cases an
		/// "entity" is a byte but this is not always the case.</returns>
		public static int Write(Stream out1, long type, object value, int codepage)
		{
			int num = 0;
			switch (type)
			{
			case 11L:
				if ((bool)value)
				{
					out1.WriteByte(byte.MaxValue);
					out1.WriteByte(byte.MaxValue);
				}
				else
				{
					out1.WriteByte(0);
					out1.WriteByte(0);
				}
				num += 2;
				break;
			case 30L:
			{
				CodePageString codePageString = new CodePageString((string)value, codepage);
				num += codePageString.Write(out1);
				break;
			}
			case 31L:
			{
				int n2 = ((string)value).Length + 1;
				num += TypeWriter.WriteUIntToStream(out1, (uint)n2);
				char[] array3 = ((string)value).ToCharArray();
				for (int i = 0; i < array3.Length; i++)
				{
					int num3 = (array3[i] & 0xFF00) >> 8;
					int num4 = array3[i] & 0xFF;
					byte value2 = (byte)num3;
					byte value3 = (byte)num4;
					out1.WriteByte(value3);
					out1.WriteByte(value2);
					num += 2;
				}
				out1.WriteByte(0);
				out1.WriteByte(0);
				num += 2;
				break;
			}
			case 71L:
			{
				byte[] array2 = (byte[])value;
				out1.Write(array2, 0, array2.Length);
				num = array2.Length;
				break;
			}
			case 0L:
				num += TypeWriter.WriteUIntToStream(out1, 0u);
				break;
			case 2L:
			{
				short n;
				try
				{
					n = Convert.ToInt16(value, CultureInfo.InvariantCulture);
				}
				catch (OverflowException)
				{
					n = (short)(int)value;
				}
				num += TypeWriter.WriteToStream(out1, n);
				break;
			}
			case 3L:
				if (!(value is int))
				{
					throw new Exception("Could not cast an object To int: " + value.GetType().Name + ", " + value.ToString());
				}
				num += TypeWriter.WriteToStream(out1, (int)value);
				break;
			case 20L:
				num += TypeWriter.WriteToStream(out1, Convert.ToInt64(value, CultureInfo.CurrentCulture));
				break;
			case 5L:
				num += TypeWriter.WriteToStream(out1, (double)value);
				break;
			case 64L:
			{
				long num2 = (value == null) ? 0 : Util.DateToFileTime((DateTime)value);
				int high = (int)((num2 >> 32) & uint.MaxValue);
				int low = (int)(num2 & uint.MaxValue);
				Filetime filetime = new Filetime(low, high);
				num += filetime.Write(out1);
				break;
			}
			default:
			{
				if (!(value is byte[]))
				{
					throw new WritingNotSupportedException(type, value);
				}
				byte[] array = (byte[])value;
				out1.Write(array, 0, array.Length);
				num = array.Length;
				WriteUnsupportedTypeMessage(new WritingNotSupportedException(type, value));
				break;
			}
			}
			for (; (num & 3) != 0; num++)
			{
				out1.WriteByte(0);
			}
			return num;
		}
	}
}
