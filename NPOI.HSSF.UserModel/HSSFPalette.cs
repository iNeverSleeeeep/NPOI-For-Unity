using NPOI.HSSF.Record;
using NPOI.HSSF.Util;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Represents a workbook color palette.
	/// Internally, the XLS format refers to colors using an offset into the palette
	/// record.  Thus, the first color in the palette has the index 0x8, the second
	/// has the index 0x9, etc. through 0x40
	/// @author Brian Sanders (bsanders at risklabs dot com)
	/// </summary>
	public class HSSFPalette
	{
		/// <summary>
		/// user custom color
		/// </summary>
		private class CustomColor : HSSFColor
		{
			private short byteOffset;

			private byte red;

			private byte green;

			private byte blue;

			/// <summary>
			/// Gets index to the standard palette
			/// </summary>
			/// <value></value>
			public override short Indexed => byteOffset;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFPalette.CustomColor" /> class.
			/// </summary>
			/// <param name="byteOffset">The byte offset.</param>
			/// <param name="colors">The colors.</param>
			public CustomColor(short byteOffset, byte[] colors)
				: this(byteOffset, colors[0], colors[1], colors[2])
			{
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFPalette.CustomColor" /> class.
			/// </summary>
			/// <param name="byteOffset">The byte offset.</param>
			/// <param name="red">The red.</param>
			/// <param name="green">The green.</param>
			/// <param name="blue">The blue.</param>
			public CustomColor(short byteOffset, byte red, byte green, byte blue)
			{
				this.byteOffset = byteOffset;
				this.red = red;
				this.green = green;
				this.blue = blue;
			}

			/// <summary>
			/// Gets triplet representation like that in Excel
			/// </summary>
			/// <value></value>
			public override byte[] GetTriplet()
			{
				return new byte[3]
				{
					(byte)(red & 0xFF),
					(byte)(green & 0xFF),
					(byte)(blue & 0xFF)
				};
			}

			/// <summary>
			/// Gets a hex string exactly like a gnumeric triplet
			/// </summary>
			/// <value></value>
			public override string GetHexString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(GetGnumericPart(red));
				stringBuilder.Append(':');
				stringBuilder.Append(GetGnumericPart(green));
				stringBuilder.Append(':');
				stringBuilder.Append(GetGnumericPart(blue));
				return stringBuilder.ToString();
			}

			/// <summary>
			/// Gets the gnumeric part.
			/// </summary>
			/// <param name="color">The color.</param>
			/// <returns></returns>
			private string GetGnumericPart(byte color)
			{
				string text;
				if (color == 0)
				{
					text = "0";
				}
				else
				{
					int num = color & 0xFF;
					num = ((num << 8) | num);
					text = StringUtil.ToHexString(num).ToUpper();
					while (text.Length < 4)
					{
						text = "0" + text;
					}
				}
				return text;
			}
		}

		private PaletteRecord palette;

		public HSSFPalette(PaletteRecord palette)
		{
			this.palette = palette;
		}

		/// <summary>
		/// Retrieves the color at a given index
		/// </summary>
		/// <param name="index">the palette index, between 0x8 to 0x40 inclusive.</param>
		/// <returns>the color, or null if the index Is not populated</returns>
		public HSSFColor GetColor(short index)
		{
			if (index == 64)
			{
				return HSSFColor.Automatic.GetInstance();
			}
			byte[] color = palette.GetColor(index);
			if (color != null)
			{
				return new CustomColor(index, color);
			}
			return null;
		}

		/// <summary>
		/// Finds the first occurance of a given color
		/// </summary>
		/// <param name="red">the RGB red component, between 0 and 255 inclusive</param>
		/// <param name="green">the RGB green component, between 0 and 255 inclusive</param>
		/// <param name="blue">the RGB blue component, between 0 and 255 inclusive</param>
		/// <returns>the color, or null if the color does not exist in this palette</returns>
		public HSSFColor FindColor(byte red, byte green, byte blue)
		{
			byte[] color = palette.GetColor(8);
			short num = 8;
			while (color != null)
			{
				if (color[0] == red && color[1] == green && color[2] == blue)
				{
					return new CustomColor(num, color);
				}
				color = palette.GetColor(num = (short)(num + 1));
			}
			return null;
		}

		/// <summary>
		/// Finds the closest matching color in the custom palette.  The
		/// method for Finding the distance between the colors Is fairly
		/// primative.
		/// </summary>
		/// <param name="red">The red component of the color to match.</param>
		/// <param name="green">The green component of the color to match.</param>
		/// <param name="blue">The blue component of the color to match.</param>
		/// <returns>The closest color or null if there are no custom
		/// colors currently defined.</returns>
		public HSSFColor FindSimilarColor(byte red, byte green, byte blue)
		{
			HSSFColor result = null;
			int num = 2147483647;
			byte[] color = palette.GetColor(8);
			short num2 = 8;
			while (color != null)
			{
				int num3 = Math.Abs(red - color[0]) + Math.Abs(green - color[1]) + Math.Abs(blue - color[2]);
				if (num3 < num)
				{
					num = num3;
					result = GetColor(num2);
				}
				color = palette.GetColor(num2 = (short)(num2 + 1));
			}
			return result;
		}

		/// <summary>
		/// Sets the color at the given offset
		/// </summary>
		/// <param name="index">the palette index, between 0x8 to 0x40 inclusive</param>
		/// <param name="red">the RGB red component, between 0 and 255 inclusive</param>
		/// <param name="green">the RGB green component, between 0 and 255 inclusive</param>
		/// <param name="blue">the RGB blue component, between 0 and 255 inclusive</param>
		public void SetColorAtIndex(short index, byte red, byte green, byte blue)
		{
			palette.SetColor(index, red, green, blue);
		}

		/// <summary>
		/// Adds a new color into an empty color slot.
		/// </summary>
		/// <param name="red">The red component</param>
		/// <param name="green">The green component</param>
		/// <param name="blue">The blue component</param>
		/// <returns>The new custom color.</returns>
		public HSSFColor AddColor(byte red, byte green, byte blue)
		{
			byte[] color = palette.GetColor(8);
			short num = 8;
			while (num < 64)
			{
				if (color == null)
				{
					SetColorAtIndex(num, red, green, blue);
					return GetColor(num);
				}
				color = palette.GetColor(num = (short)(num + 1));
			}
			throw new Exception("Could not Find free color index");
		}
	}
}
