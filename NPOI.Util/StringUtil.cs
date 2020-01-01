using System;
using System.Text;

namespace NPOI.Util
{
	/// <summary>
	/// Title: String Utility Description: Collection of string handling utilities
	/// @author     Andrew C. Oliver
	/// @author     Sergei Kozello (sergeikozello at mail.ru)
	/// @author     Toshiaki Kamoshida (kamoshida.toshiaki at future dot co dot jp)
	/// @since      May 10, 2002
	/// @version    1.0
	/// </summary>
	public class StringUtil
	{
		private const string ENCODING = "ISO-8859-1";

		/// Constructor for the StringUtil object     
		private StringUtil()
		{
		}

		/// <summary>
		/// Given a byte array of 16-bit unicode characters in Little Endian
		/// Format (most important byte last), return a Java String representation
		/// of it.
		/// { 0x16, 0x00 } -0x16
		/// </summary>
		/// <param name="str">the byte array to be converted</param>
		/// <param name="offset">the initial offset into the
		/// byte array. it is assumed that string[ offset ] and string[ offset + 1 ] contain the first 16-bit unicode character</param>
		/// <param name="len">the Length of the string</param>
		/// <returns>the converted string</returns>                              
		public static string GetFromUnicodeLE(byte[] str, int offset, int len)
		{
			if (offset < 0 || offset >= str.Length)
			{
				throw new IndexOutOfRangeException("Illegal offset");
			}
			if (len < 0 || (str.Length - offset) / 2 < len)
			{
				throw new ArgumentException("Illegal Length");
			}
			return Encoding.Unicode.GetString(str, offset, len * 2);
		}

		/// <summary>
		/// Given a byte array of 16-bit unicode characters in little endian
		/// Format (most important byte last), return a Java String representation
		/// of it.
		///             { 0x16, 0x00 } -0x16
		/// </summary>
		/// <param name="str">the byte array to be converted</param>
		/// <returns>the converted string</returns>  
		public static string GetFromUnicodeLE(byte[] str)
		{
			if (str.Length == 0)
			{
				return "";
			}
			return GetFromUnicodeLE(str, 0, str.Length / 2);
		}

		/// <summary>
		/// Given a byte array of 16-bit unicode characters in big endian
		/// Format (most important byte first), return a Java String representation
		/// of it.
		///  { 0x00, 0x16 } -0x16
		/// </summary>
		/// <param name="str">the byte array to be converted</param>
		/// <param name="offset">the initial offset into the
		/// byte array. it is assumed that string[ offset ] and string[ offset + 1 ] contain the first 16-bit unicode character</param>
		/// <param name="len">the Length of the string</param>
		/// <returns> the converted string</returns>
		public static string GetFromUnicodeBE(byte[] str, int offset, int len)
		{
			if (offset < 0 || offset >= str.Length)
			{
				throw new IndexOutOfRangeException("Illegal offset");
			}
			if (len >= 0 && (str.Length - offset) / 2 >= len)
			{
				try
				{
					return Encoding.GetEncoding("UTF-16BE").GetString(str, offset, len * 2);
				}
				catch
				{
					throw new InvalidOperationException();
				}
			}
			throw new ArgumentException("Illegal Length");
		}

		/// <summary>
		/// Given a byte array of 16-bit unicode characters in big endian
		/// Format (most important byte first), return a Java String representation
		/// of it.
		/// { 0x00, 0x16 } -0x16
		/// </summary>
		/// <param name="str">the byte array to be converted</param>
		/// <returns>the converted string</returns>      
		public static string GetFromUnicodeBE(byte[] str)
		{
			if (str.Length == 0)
			{
				return "";
			}
			return GetFromUnicodeBE(str, 0, str.Length / 2);
		}

		/// <summary>
		/// Read 8 bit data (in IsO-8859-1 codepage) into a (unicode) Java
		/// String and return.
		/// (In Excel terms, read compressed 8 bit unicode as a string)
		/// </summary>
		/// <param name="str">byte array to read</param>
		/// <param name="offset">offset to read byte array</param>
		/// <param name="len">Length to read byte array</param>
		/// <returns>generated String instance by reading byte array</returns>
		public static string GetFromCompressedUnicode(byte[] str, int offset, int len)
		{
			try
			{
				int count = Math.Min(len, str.Length - offset);
				return Encoding.GetEncoding("ISO-8859-1").GetString(str, offset, count);
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Takes a unicode (java) string, and returns it as 8 bit data (in IsO-8859-1
		/// codepage).
		/// (In Excel terms, write compressed 8 bit unicode)
		/// </summary>
		/// <param name="input">the String containing the data to be written</param>
		/// <param name="output">the byte array to which the data Is to be written</param>
		/// <param name="offset">an offset into the byte arrat at which the data Is start when written</param>
		public static void PutCompressedUnicode(string input, byte[] output, int offset)
		{
			try
			{
				byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(input);
				Array.Copy(bytes, 0, output, offset, bytes.Length);
			}
			catch
			{
				throw;
			}
		}

		public static void PutCompressedUnicode(string input, ILittleEndianOutput out1)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(input);
			}
			catch (EncoderFallbackException)
			{
				throw;
			}
			out1.Write(bytes);
		}

		/// <summary>
		/// Takes a unicode string, and returns it as little endian (most
		/// important byte last) bytes in the supplied byte array.
		/// (In Excel terms, write uncompressed unicode)
		/// </summary>
		/// <param name="input">the String containing the unicode data to be written</param>
		/// <param name="output">the byte array to hold the uncompressed unicode, should be twice the Length of the String</param>
		/// <param name="offset">the offset to start writing into the byte array</param>
		public static void PutUnicodeLE(string input, byte[] output, int offset)
		{
			byte[] bytes = Encoding.GetEncoding("UTF-16LE").GetBytes(input);
			Array.Copy(bytes, 0, output, offset, bytes.Length);
		}

		public static void PutUnicodeLE(string input, ILittleEndianOutput out1)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding("UTF-16LE").GetBytes(input);
			}
			catch (EncoderFallbackException)
			{
				throw;
			}
			out1.Write(bytes);
		}

		/// <summary>
		/// Takes a unicode string, and returns it as big endian (most
		/// important byte first) bytes in the supplied byte array.
		/// (In Excel terms, write uncompressed unicode)
		/// </summary>
		/// <param name="input">the String containing the unicode data to be written</param>
		/// <param name="output">the byte array to hold the uncompressed unicode, should be twice the Length of the String.</param>
		/// <param name="offset">the offset to start writing into the byte array</param>
		public static void PutUnicodeBE(string input, byte[] output, int offset)
		{
			try
			{
				byte[] bytes = Encoding.GetEncoding("UTF-16BE").GetBytes(input);
				Array.Copy(bytes, 0, output, offset, bytes.Length);
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Gets the preferred encoding.
		/// </summary>
		/// <returns>the encoding we want to use, currently hardcoded to IsO-8859-1</returns>
		public static string GetPreferredEncoding()
		{
			return "ISO-8859-1";
		}

		/// <summary>
		/// check the parameter Has multibyte character
		/// </summary>
		/// <param name="value"> string to check</param>
		/// <returns>
		/// 	<c>true</c> if Has at least one multibyte character; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasMultibyte(string value)
		{
			if (value == null)
			{
				return false;
			}
			foreach (char c in value)
			{
				if (c > 'ÿ')
				{
					return true;
				}
			}
			return false;
		}

		public static string ReadCompressedUnicode(ILittleEndianInput in1, int nChars)
		{
			char[] array = new char[nChars];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (char)in1.ReadUByte();
			}
			return new string(array);
		}

		public static string ReadUnicodeLE(ILittleEndianInput in1, int nChars)
		{
			char[] array = new char[nChars];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (char)in1.ReadUShort();
			}
			return new string(array);
		}

		/// InputStream <c>in</c> is expected to contain:
		/// <ol>
		/// <li>ushort nChars</li>
		/// <li>byte is16BitFlag</li>
		/// <li>byte[]/char[] characterData</li>
		/// </ol>
		/// For this encoding, the is16BitFlag is always present even if nChars==0.
		public static string ReadUnicodeString(ILittleEndianInput in1)
		{
			int nChars = in1.ReadUShort();
			byte b = (byte)in1.ReadByte();
			if ((b & 1) == 0)
			{
				return ReadCompressedUnicode(in1, nChars);
			}
			return ReadUnicodeLE(in1, nChars);
		}

		/// InputStream <c>in</c> is expected to contain:
		/// <ol>
		/// <li>byte is16BitFlag</li>
		/// <li>byte[]/char[] characterData</li>
		/// </ol>
		/// For this encoding, the is16BitFlag is always present even if nChars==0.
		/// <br />
		/// This method should be used when the nChars field is <em>not</em> stored 
		/// as a ushort immediately before the is16BitFlag. Otherwise, {@link 
		/// #readUnicodeString(LittleEndianInput)} can be used. 
		public static string ReadUnicodeString(ILittleEndianInput in1, int nChars)
		{
			byte b = (byte)in1.ReadByte();
			if ((b & 1) == 0)
			{
				return ReadCompressedUnicode(in1, nChars);
			}
			return ReadUnicodeLE(in1, nChars);
		}

		/// OutputStream <c>out</c> will get:
		/// <ol>
		/// <li>ushort nChars</li>
		/// <li>byte is16BitFlag</li>
		/// <li>byte[]/char[] characterData</li>
		/// </ol>
		/// For this encoding, the is16BitFlag is always present even if nChars==0.
		public static void WriteUnicodeString(ILittleEndianOutput out1, string value)
		{
			int length = value.Length;
			out1.WriteShort(length);
			bool flag = HasMultibyte(value);
			out1.WriteByte(flag ? 1 : 0);
			if (flag)
			{
				PutUnicodeLE(value, out1);
			}
			else
			{
				PutCompressedUnicode(value, out1);
			}
		}

		/// OutputStream <c>out</c> will get:
		/// <ol>
		/// <li>byte is16BitFlag</li>
		/// <li>byte[]/char[] characterData</li>
		/// </ol>
		/// For this encoding, the is16BitFlag is always present even if nChars==0.
		/// <br />
		/// This method should be used when the nChars field is <em>not</em> stored 
		/// as a ushort immediately before the is16BitFlag. Otherwise, {@link 
		/// #writeUnicodeString(LittleEndianOutput, String)} can be used. 
		public static void WriteUnicodeStringFlagAndData(ILittleEndianOutput out1, string value)
		{
			bool flag = HasMultibyte(value);
			out1.WriteByte(flag ? 1 : 0);
			if (flag)
			{
				PutUnicodeLE(value, out1);
			}
			else
			{
				PutCompressedUnicode(value, out1);
			}
		}

		/// <summary>
		/// Gets the number of bytes that would be written by WriteUnicodeString(LittleEndianOutput, String)
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static int GetEncodedSize(string value)
		{
			int num = 3;
			return num + value.Length * ((!HasMultibyte(value)) ? 1 : 2);
		}

		/// <summary>
		/// Checks to see if a given String needs to be represented as Unicode
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if string needs Unicode to be represented.; otherwise, <c>false</c>.
		/// </returns>
		///             <remarks>Tony Qu change the logic</remarks>
		public static bool IsUnicodeString(string value)
		{
			try
			{
				return !value.Equals(Encoding.GetEncoding("ISO-8859-1").GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(value)));
			}
			catch
			{
				return true;
			}
		}

		/// <summary> 
		/// Encodes non-US-ASCII characters in a string, good for encoding file names for download 
		/// http://www.acriticsreview.com/List.aspx?listid=42
		/// </summary> 
		/// <param name="s"></param> 
		/// <returns></returns> 
		public static string ToHexString(string s)
		{
			char[] array = s.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (NeedToEncode(array[i]))
				{
					string value = ToHexString(array[i]);
					stringBuilder.Append(value);
				}
				else
				{
					stringBuilder.Append(array[i]);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary> 
		/// Encodes a non-US-ASCII character. 
		/// </summary> 
		/// <param name="chr"></param> 
		/// <returns></returns> 
		public static string ToHexString(char chr)
		{
			return Convert.ToString(chr, 16);
		}

		/// <summary> 
		/// Encodes a non-US-ASCII character. 
		/// </summary> 
		/// <param name="chr"></param> 
		/// <returns></returns> 
		public static string ToHexString(short chr)
		{
			return ToHexString((char)chr);
		}

		/// <summary> 
		/// Encodes a non-US-ASCII character. 
		/// </summary> 
		/// <param name="chr"></param> 
		/// <returns></returns> 
		public static string ToHexString(int chr)
		{
			return ToHexString((char)chr);
		}

		/// <summary> 
		/// Encodes a non-US-ASCII character. 
		/// </summary> 
		/// <param name="chr"></param> 
		/// <returns></returns> 
		public static string ToHexString(long chr)
		{
			return ToHexString((char)chr);
		}

		/// <summary> 
		/// Determines if the character needs to be encoded. 
		/// http://www.acriticsreview.com/List.aspx?listid=42
		/// </summary> 
		/// <param name="chr"></param> 
		/// <returns></returns> 
		private static bool NeedToEncode(char chr)
		{
			string text = "$-_.+!*'(),@=&";
			if (chr > '\u007f')
			{
				return true;
			}
			if (char.IsLetterOrDigit(chr) || text.IndexOf(chr) >= 0)
			{
				return false;
			}
			return true;
		}
	}
}
