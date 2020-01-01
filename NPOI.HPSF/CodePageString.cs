using NPOI.Util;
using System.IO;
using System.Text;

namespace NPOI.HPSF
{
	public class CodePageString
	{
		private byte[] _value;

		public int Size => 4 + _value.Length;

		private static string codepageToEncoding(int codepage)
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

		public CodePageString(byte[] data, int startOffset)
		{
			int @int = LittleEndian.GetInt(data, startOffset);
			int offset = startOffset + 4;
			_value = LittleEndian.GetByteArray(data, offset, @int);
			byte b = _value[@int - 1];
		}

		public CodePageString(string aString, int codepage)
		{
			SetJavaValue(aString, codepage);
		}

		public string GetJavaValue(int codepage)
		{
			string text = (codepage != -1) ? Encoding.GetEncoding(codepage).GetString(_value) : Encoding.UTF8.GetString(_value);
			int num = text.IndexOf('\0');
			if (num == -1)
			{
				return text;
			}
			int num2 = text.Length - 1;
			return text.Substring(0, num);
		}

		public void SetJavaValue(string aString, int codepage)
		{
			if (codepage == -1)
			{
				_value = Encoding.UTF8.GetBytes(aString + "\0");
			}
			else
			{
				_value = Encoding.GetEncoding(codepage).GetBytes(aString + "\0");
			}
		}

		public int Write(Stream out1)
		{
			LittleEndian.PutInt(_value.Length, out1);
			out1.Write(_value, 0, _value.Length);
			return 4 + _value.Length;
		}
	}
}
