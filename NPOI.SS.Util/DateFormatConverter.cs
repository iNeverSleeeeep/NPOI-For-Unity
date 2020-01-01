using NPOI.Util;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NPOI.SS.Util
{
	/// Convert DateFormat patterns into Excel custom number formats.
	/// For example, to format a date in excel using the "dd MMMM, yyyy" pattern and Japanese
	/// locale, use the following code:
	///
	/// <pre><code>
	///     // returns "[$-0411]dd MMMM, yyyy;@" where the [$-0411] prefix tells Excel to use the Japanese locale
	///     String excelFormatPattern = DateFormatConverter.convert(Locale.JAPANESE, "dd MMMM, yyyy");
	///
	///     CellStyle cellStyle = workbook.createCellStyle();
	///
	///     DataFormat poiFormat = workbook.createDataFormat();
	///     cellStyle.setDataFormat(poiFormat.getFormat(excelFormatPattern));
	///     cell.setCellValue(new Date());
	///     cell.setCellStyle(cellStyle);  // formats date as '2012\u5e743\u670817\u65e5'
	///
	/// </code></pre>
	public class DateFormatConverter
	{
		public class DateFormatTokenizer
		{
			private string format;

			private int pos;

			public DateFormatTokenizer(string format)
			{
				this.format = format;
			}

			public string GetNextToken()
			{
				if (pos >= format.Length)
				{
					return null;
				}
				int num = pos;
				char c = format[pos];
				pos++;
				if (c == '\'')
				{
					while (pos < format.Length && (c = format[pos]) != '\'')
					{
						pos++;
					}
					if (pos < format.Length)
					{
						pos++;
					}
				}
				else
				{
					char c2 = c;
					while (pos < format.Length && (c = format[pos]) == c2)
					{
						pos++;
					}
				}
				return format.Substring(num, pos - num);
			}

			public static string[] Tokenize(string format)
			{
				List<string> list = new List<string>();
				DateFormatTokenizer dateFormatTokenizer = new DateFormatTokenizer(format);
				string nextToken;
				while ((nextToken = dateFormatTokenizer.GetNextToken()) != null)
				{
					list.Add(nextToken);
				}
				return list.ToArray();
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				DateFormatTokenizer dateFormatTokenizer = new DateFormatTokenizer(format);
				string nextToken;
				while ((nextToken = dateFormatTokenizer.GetNextToken()) != null)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("[").Append(nextToken).Append("]");
				}
				return stringBuilder.ToString();
			}
		}

		private static POILogger logger = POILogFactory.GetLogger(typeof(DateFormatConverter));

		private static Dictionary<string, string> tokenConversions = PrepareTokenConversions();

		private static Dictionary<string, string> localePrefixes = PrepareLocalePrefixes();

		private static Dictionary<string, string> PrepareTokenConversions()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("EEEE", "dddd");
			dictionary.Add("EEE", "ddd");
			dictionary.Add("EE", "ddd");
			dictionary.Add("E", "d");
			dictionary.Add("Z", "");
			dictionary.Add("z", "");
			dictionary.Add("a", "am/pm");
			dictionary.Add("A", "AM/PM");
			dictionary.Add("K", "H");
			dictionary.Add("KK", "HH");
			dictionary.Add("k", "h");
			dictionary.Add("kk", "hh");
			dictionary.Add("S", "0");
			dictionary.Add("SS", "00");
			dictionary.Add("SSS", "000");
			return dictionary;
		}

		private static Dictionary<string, string> PrepareLocalePrefixes()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("af", "[$-0436]");
			dictionary.Add("am", "[$-45E]");
			dictionary.Add("ar-ae", "[$-3801]");
			dictionary.Add("ar-bh", "[$-3C01]");
			dictionary.Add("ar-dz", "[$-1401]");
			dictionary.Add("ar-eg", "[$-C01]");
			dictionary.Add("ar-iq", "[$-0801]");
			dictionary.Add("ar-jo", "[$-2C01]");
			dictionary.Add("ar-kw", "[$-3401]");
			dictionary.Add("ar-lb", "[$-3001]");
			dictionary.Add("ar-ly", "[$-1001]");
			dictionary.Add("ar-ma", "[$-1801]");
			dictionary.Add("ar-om", "[$-2001]");
			dictionary.Add("ar-qa", "[$-4001]");
			dictionary.Add("ar-sa", "[$-0401]");
			dictionary.Add("ar-sy", "[$-2801]");
			dictionary.Add("ar-tn", "[$-1C01]");
			dictionary.Add("ar-ye", "[$-2401]");
			dictionary.Add("as", "[$-44D]");
			dictionary.Add("az-az", "[$-82C]");
			dictionary.Add("be", "[$-0423]");
			dictionary.Add("bg", "[$-0402]");
			dictionary.Add("bn", "[$-0845]");
			dictionary.Add("bo", "[$-0451]");
			dictionary.Add("bs", "[$-141A]");
			dictionary.Add("ca", "[$-0403]");
			dictionary.Add("cs", "[$-0405]");
			dictionary.Add("cy", "[$-0452]");
			dictionary.Add("da", "[$-0406]");
			dictionary.Add("de-at", "[$-C07]");
			dictionary.Add("de-ch", "[$-0807]");
			dictionary.Add("de-de", "[$-0407]");
			dictionary.Add("de-li", "[$-1407]");
			dictionary.Add("de-lu", "[$-1007]");
			dictionary.Add("dv", "[$-0465]");
			dictionary.Add("el", "[$-0408]");
			dictionary.Add("en-au", "[$-C09]");
			dictionary.Add("en-bz", "[$-2809]");
			dictionary.Add("en-ca", "[$-1009]");
			dictionary.Add("en-cb", "[$-2409]");
			dictionary.Add("en-gb", "[$-0809]");
			dictionary.Add("en-ie", "[$-1809]");
			dictionary.Add("en-in", "[$-4009]");
			dictionary.Add("en-jm", "[$-2009]");
			dictionary.Add("en-nz", "[$-1409]");
			dictionary.Add("en-ph", "[$-3409]");
			dictionary.Add("en-tt", "[$-2C09]");
			dictionary.Add("en-us", "[$-0409]");
			dictionary.Add("en-za", "[$-1C09]");
			dictionary.Add("es-ar", "[$-2C0A]");
			dictionary.Add("es-bo", "[$-400A]");
			dictionary.Add("es-cl", "[$-340A]");
			dictionary.Add("es-co", "[$-240A]");
			dictionary.Add("es-cr", "[$-140A]");
			dictionary.Add("es-do", "[$-1C0A]");
			dictionary.Add("es-ec", "[$-300A]");
			dictionary.Add("es-es", "[$-40A]");
			dictionary.Add("es-gt", "[$-100A]");
			dictionary.Add("es-hn", "[$-480A]");
			dictionary.Add("es-mx", "[$-80A]");
			dictionary.Add("es-ni", "[$-4C0A]");
			dictionary.Add("es-pa", "[$-180A]");
			dictionary.Add("es-pe", "[$-280A]");
			dictionary.Add("es-pr", "[$-500A]");
			dictionary.Add("es-py", "[$-3C0A]");
			dictionary.Add("es-sv", "[$-440A]");
			dictionary.Add("es-uy", "[$-380A]");
			dictionary.Add("es-ve", "[$-200A]");
			dictionary.Add("et", "[$-0425]");
			dictionary.Add("eu", "[$-42D]");
			dictionary.Add("fa", "[$-0429]");
			dictionary.Add("fi", "[$-40B]");
			dictionary.Add("fo", "[$-0438]");
			dictionary.Add("fr-be", "[$-80C]");
			dictionary.Add("fr-ca", "[$-C0C]");
			dictionary.Add("fr-ch", "[$-100C]");
			dictionary.Add("fr-fr", "[$-40C]");
			dictionary.Add("fr-lu", "[$-140C]");
			dictionary.Add("gd", "[$-43C]");
			dictionary.Add("gd-ie", "[$-83C]");
			dictionary.Add("gn", "[$-0474]");
			dictionary.Add("gu", "[$-0447]");
			dictionary.Add("he", "[$-40D]");
			dictionary.Add("hi", "[$-0439]");
			dictionary.Add("hr", "[$-41A]");
			dictionary.Add("hu", "[$-40E]");
			dictionary.Add("hy", "[$-42B]");
			dictionary.Add("id", "[$-0421]");
			dictionary.Add("is", "[$-40F]");
			dictionary.Add("it-ch", "[$-0810]");
			dictionary.Add("it-it", "[$-0410]");
			dictionary.Add("ja", "[$-0411]");
			dictionary.Add("kk", "[$-43F]");
			dictionary.Add("km", "[$-0453]");
			dictionary.Add("kn", "[$-44B]");
			dictionary.Add("ko", "[$-0412]");
			dictionary.Add("ks", "[$-0460]");
			dictionary.Add("la", "[$-0476]");
			dictionary.Add("lo", "[$-0454]");
			dictionary.Add("lt", "[$-0427]");
			dictionary.Add("lv", "[$-0426]");
			dictionary.Add("mi", "[$-0481]");
			dictionary.Add("mk", "[$-42F]");
			dictionary.Add("ml", "[$-44C]");
			dictionary.Add("mn", "[$-0850]");
			dictionary.Add("mr", "[$-44E]");
			dictionary.Add("ms-bn", "[$-83E]");
			dictionary.Add("ms-my", "[$-43E]");
			dictionary.Add("mt", "[$-43A]");
			dictionary.Add("my", "[$-0455]");
			dictionary.Add("ne", "[$-0461]");
			dictionary.Add("nl-be", "[$-0813]");
			dictionary.Add("nl-nl", "[$-0413]");
			dictionary.Add("no-no", "[$-0814]");
			dictionary.Add("or", "[$-0448]");
			dictionary.Add("pa", "[$-0446]");
			dictionary.Add("pl", "[$-0415]");
			dictionary.Add("pt-br", "[$-0416]");
			dictionary.Add("pt-pt", "[$-0816]");
			dictionary.Add("rm", "[$-0417]");
			dictionary.Add("ro", "[$-0418]");
			dictionary.Add("ro-mo", "[$-0818]");
			dictionary.Add("ru", "[$-0419]");
			dictionary.Add("ru-mo", "[$-0819]");
			dictionary.Add("sa", "[$-44F]");
			dictionary.Add("sb", "[$-42E]");
			dictionary.Add("sd", "[$-0459]");
			dictionary.Add("si", "[$-45B]");
			dictionary.Add("sk", "[$-41B]");
			dictionary.Add("sl", "[$-0424]");
			dictionary.Add("so", "[$-0477]");
			dictionary.Add("sq", "[$-41C]");
			dictionary.Add("sr-sp", "[$-C1A]");
			dictionary.Add("sv-fi", "[$-81D]");
			dictionary.Add("sv-se", "[$-41D]");
			dictionary.Add("sw", "[$-0441]");
			dictionary.Add("ta", "[$-0449]");
			dictionary.Add("te", "[$-44A]");
			dictionary.Add("tg", "[$-0428]");
			dictionary.Add("th", "[$-41E]");
			dictionary.Add("tk", "[$-0442]");
			dictionary.Add("tn", "[$-0432]");
			dictionary.Add("tr", "[$-41F]");
			dictionary.Add("ts", "[$-0431]");
			dictionary.Add("tt", "[$-0444]");
			dictionary.Add("uk", "[$-0422]");
			dictionary.Add("ur", "[$-0420]");
			dictionary.Add("UTF-8", "[$-0000]");
			dictionary.Add("uz-uz", "[$-0843]");
			dictionary.Add("vi", "[$-42A]");
			dictionary.Add("xh", "[$-0434]");
			dictionary.Add("yi", "[$-43D]");
			dictionary.Add("zh-cn", "[$-0804]");
			dictionary.Add("zh-hk", "[$-C04]");
			dictionary.Add("zh-mo", "[$-1404]");
			dictionary.Add("zh-sg", "[$-1004]");
			dictionary.Add("zh-tw", "[$-0404]");
			dictionary.Add("zu", "[$-0435]");
			dictionary.Add("ar", "[$-0401]");
			dictionary.Add("de", "[$-0407]");
			dictionary.Add("en", "[$-0409]");
			dictionary.Add("es", "[$-40A]");
			dictionary.Add("fr", "[$-40C]");
			dictionary.Add("it", "[$-0410]");
			dictionary.Add("ms", "[$-43E]");
			dictionary.Add("nl", "[$-0413]");
			dictionary.Add("nn", "[$-0814]");
			dictionary.Add("no", "[$-0414]");
			dictionary.Add("pt", "[$-0816]");
			dictionary.Add("sr", "[$-C1A]");
			dictionary.Add("sv", "[$-41D]");
			dictionary.Add("uz", "[$-0843]");
			dictionary.Add("zh", "[$-0804]");
			dictionary.Add("ga", "[$-43C]");
			dictionary.Add("ga-ie", "[$-83C]");
			dictionary.Add("in", "[$-0421]");
			dictionary.Add("iw", "[$-40D]");
			return dictionary;
		}

		public static string GetPrefixForLocale(CultureInfo locale)
		{
			string text = locale.ToString().ToLower();
			string text2 = localePrefixes[text];
			if (text2 == null)
			{
				text2 = localePrefixes[text.Substring(0, 2)];
				if (text2 == null)
				{
					CultureInfo cultureInfo = CultureInfo.GetCultureInfo(text.Substring(0, 2));
					logger.Log(7, "Unable to find prefix for " + locale + "(" + locale.DisplayName + ") or " + text.Substring(0, 2) + "(" + cultureInfo.DisplayName + ")");
					return "";
				}
			}
			return text2;
		}

		public static string Convert(CultureInfo locale, string format)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetPrefixForLocale(locale));
			DateFormatTokenizer dateFormatTokenizer = new DateFormatTokenizer(format);
			string nextToken;
			while ((nextToken = dateFormatTokenizer.GetNextToken()) != null)
			{
				if (nextToken.StartsWith("'"))
				{
					stringBuilder.Append(nextToken.Replace("'", "\""));
				}
				else if (!char.IsLetter(nextToken[0]))
				{
					stringBuilder.Append(nextToken);
				}
				else
				{
					string text = tokenConversions[nextToken];
					stringBuilder.Append((text == null) ? nextToken : text);
				}
			}
			stringBuilder.Append(";@");
			return stringBuilder.ToString().Trim();
		}

		public static string GetDatePattern(int style, CultureInfo locale)
		{
			return DateFormat.GetDatePattern(style, locale);
		}

		public static string GetTimePattern(int style, CultureInfo locale)
		{
			return DateFormat.GetTimePattern(style, locale);
		}

		public static string GetDateTimePattern(int style, CultureInfo locale)
		{
			return DateFormat.GetDateTimePattern(style, style, locale);
		}
	}
}
