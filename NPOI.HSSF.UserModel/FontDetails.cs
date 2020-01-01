using NPOI.Util.Collections;
using System;
using System.Collections;
using System.Globalization;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Stores width and height details about a font.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class FontDetails
	{
		private string fontName;

		private int height;

		private Hashtable charWidths = new Hashtable();

		/// <summary>
		/// Construct the font details with the given name and height.
		/// </summary>
		/// <param name="fontName">The font name.</param>
		/// <param name="height">The height of the font.</param>
		public FontDetails(string fontName, int height)
		{
			this.fontName = fontName;
			this.height = height;
		}

		/// <summary>
		/// Gets the name of the font.
		/// </summary>
		/// <returns></returns>
		public string GetFontName()
		{
			return fontName;
		}

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <returns></returns>
		public int GetHeight()
		{
			return height;
		}

		/// <summary>
		/// Adds the char.
		/// </summary>
		/// <param name="c">The c.</param>
		/// <param name="width">The width.</param>
		public void AddChar(char c, int width)
		{
			charWidths[c] = width;
		}

		/// <summary>
		/// Retrieves the width of the specified Char.  If the metrics for
		/// a particular Char are not available it defaults to returning the
		/// width for the 'W' Char.
		/// </summary>
		/// <param name="c">The character.</param>
		/// <returns></returns>
		public int GetCharWidth(char c)
		{
			object obj = charWidths[c];
			if (obj == null && c != 'W')
			{
				return GetCharWidth('W');
			}
			return (int)obj;
		}

		/// <summary>
		/// Adds the chars.
		/// </summary>
		/// <param name="Chars">The chars.</param>
		/// <param name="widths">The widths.</param>
		public void AddChars(char[] Chars, int[] widths)
		{
			for (int i = 0; i < Chars.Length; i++)
			{
				if (Chars[i] != ' ')
				{
					charWidths[Chars[i]] = widths[i];
				}
			}
		}

		/// <summary>
		/// Builds the font height property.
		/// </summary>
		/// <param name="fontName">Name of the font.</param>
		/// <returns></returns>
		public static string BuildFontHeightProperty(string fontName)
		{
			return "font." + fontName + ".height";
		}

		/// <summary>
		/// Builds the font widths property.
		/// </summary>
		/// <param name="fontName">Name of the font.</param>
		/// <returns></returns>
		public static string BuildFontWidthsProperty(string fontName)
		{
			return "font." + fontName + ".widths";
		}

		/// <summary>
		/// Builds the font chars property.
		/// </summary>
		/// <param name="fontName">Name of the font.</param>
		/// <returns></returns>
		public static string BuildFontCharsProperty(string fontName)
		{
			return "font." + fontName + ".characters";
		}

		/// <summary>
		/// Create an instance of 
		/// <c>FontDetails</c>
		///  by loading them from the
		/// provided property object.
		/// </summary>
		/// <param name="fontName">the font name.</param>
		/// <param name="fontMetricsProps">the property object holding the details of this
		/// particular font.</param>
		/// <returns>a new FontDetails instance.</returns>
		public static FontDetails Create(string fontName, Properties fontMetricsProps)
		{
			string text = fontMetricsProps[BuildFontHeightProperty(fontName)];
			string text2 = fontMetricsProps[BuildFontWidthsProperty(fontName)];
			string text3 = fontMetricsProps[BuildFontCharsProperty(fontName)];
			if (text == null || text2 == null || text3 == null)
			{
				throw new ArgumentException("The supplied FontMetrics doesn't know about the font '" + fontName + "', so we can't use it. Please Add it to your font metrics file (see StaticFontMetrics.GetFontDetails");
			}
			int num = int.Parse(text, CultureInfo.InvariantCulture);
			FontDetails fontDetails = new FontDetails(fontName, num);
			string[] array = Split(text3, ",", -1);
			string[] array2 = Split(text2, ",", -1);
			if (array.Length != array2.Length)
			{
				throw new Exception("Number of Chars does not number of widths for font " + fontName);
			}
			for (int i = 0; i < array2.Length; i++)
			{
				if (array[i].Trim().Length != 0)
				{
					fontDetails.AddChar(array[i].Trim()[0], int.Parse(array2[i], CultureInfo.InvariantCulture));
				}
			}
			return fontDetails;
		}

		/// <summary>
		/// Gets the width of all Chars in a string.
		/// </summary>
		/// <param name="str">The string to measure.</param>
		/// <returns>The width of the string for a 10 point font.</returns>
		public int GetStringWidth(string str)
		{
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				num += GetCharWidth(str[i]);
			}
			return num;
		}

		/// <summary>
		/// Split the given string into an array of strings using the given
		/// delimiter.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="separator">The separator.</param>
		/// <param name="max">The max.</param>
		/// <returns></returns>
		private static string[] Split(string text, string separator, int max)
		{
			return text.Split(separator.ToCharArray());
		}
	}
}
