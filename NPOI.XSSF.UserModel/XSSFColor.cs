using NPOI.HSSF.Util;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Drawing;
using System.Text;

namespace NPOI.XSSF.UserModel
{
	/// Represents a color in SpreadsheetML
	public class XSSFColor : IColor
	{
		private CT_Color ctColor;

		/// <summary>
		///             A bool value indicating the ctColor is automatic and system ctColor dependent.
		/// </summary>
		public bool IsAuto
		{
			get
			{
				return ctColor.auto;
			}
			set
			{
				ctColor.auto = value;
				ctColor.autoSpecified = true;
			}
		}

		/// Indexed ctColor value. Only used for backwards compatibility. References a ctColor in indexedColors.
		public short Indexed
		{
			get
			{
				if (!ctColor.indexedSpecified)
				{
					return 0;
				}
				return (short)ctColor.indexed;
			}
			set
			{
				ctColor.indexed = (uint)value;
				ctColor.indexedSpecified = true;
			}
		}

		/// Standard Red Green Blue ctColor value (RGB).
		/// If there was an A (Alpha) value, it will be stripped.
		public byte[] RGB
		{
			get
			{
				byte[] rGBOrARGB = GetRGBOrARGB();
				if (rGBOrARGB == null)
				{
					return null;
				}
				if (rGBOrARGB.Length == 4)
				{
					byte[] array = new byte[3];
					Array.Copy(rGBOrARGB, 1, array, 0, 3);
					return array;
				}
				return rGBOrARGB;
			}
		}

		public int Theme
		{
			get
			{
				if (!ctColor.themeSpecified)
				{
					return 0;
				}
				return (int)ctColor.theme;
			}
			set
			{
				ctColor.theme = (uint)value;
			}
		}

		/// Specifies the tint value applied to the ctColor.
		///
		/// <p>
		/// If tint is supplied, then it is applied to the RGB value of the ctColor to determine the final
		/// ctColor applied.
		/// </p>
		/// <p>
		/// The tint value is stored as a double from -1.0 .. 1.0, where -1.0 means 100% darken and
		/// 1.0 means 100% lighten. Also, 0.0 means no Change.
		/// </p>
		/// <p>
		/// In loading the RGB value, it is Converted to HLS where HLS values are (0..HLSMAX), where
		/// HLSMAX is currently 255.
		/// </p>
		/// Here are some examples of how to apply tint to ctColor:
		/// <blockquote>
		/// <pre>
		/// If (tint &lt; 0)
		/// Lum' = Lum * (1.0 + tint)
		///
		/// For example: Lum = 200; tint = -0.5; Darken 50%
		/// Lum' = 200 * (0.5) =&gt; 100
		/// For example: Lum = 200; tint = -1.0; Darken 100% (make black)
		/// Lum' = 200 * (1.0-1.0) =&gt; 0
		/// If (tint &gt; 0)
		/// Lum' = Lum * (1.0-tint) + (HLSMAX - HLSMAX * (1.0-tint))
		/// For example: Lum = 100; tint = 0.75; Lighten 75%
		///
		/// Lum' = 100 * (1-.75) + (HLSMAX - HLSMAX*(1-.75))
		/// = 100 * .25 + (255 - 255 * .25)
		/// = 25 + (255 - 63) = 25 + 192 = 217
		/// For example: Lum = 100; tint = 1.0; Lighten 100% (make white)
		/// Lum' = 100 * (1-1) + (HLSMAX - HLSMAX*(1-1))
		/// = 100 * 0 + (255 - 255 * 0)
		/// = 0 + (255 - 0) = 255
		/// </pre>
		/// </blockquote>
		///
		/// @return the tint value
		public double Tint
		{
			get
			{
				return ctColor.tint;
			}
			set
			{
				ctColor.tint = value;
				ctColor.tintSpecified = true;
			}
		}

		/// Create an instance of XSSFColor from the supplied XML bean
		public XSSFColor(CT_Color color)
		{
			ctColor = color;
		}

		/// Create an new instance of XSSFColor
		public XSSFColor()
		{
			ctColor = new CT_Color();
		}

		public XSSFColor(Color clr)
			: this()
		{
			ctColor.SetRgb(clr.R, clr.G, clr.B);
		}

		public XSSFColor(byte[] rgb)
			: this()
		{
			ctColor.SetRgb(rgb);
		}

		/// For RGB colours, but not ARGB (we think...)
		/// Excel Gets black and white the wrong way around, so switch them 
		private byte[] CorrectRGB(byte[] rgb)
		{
			if (rgb.Length == 4)
			{
				return rgb;
			}
			if (rgb[0] == 0 && rgb[1] == 0 && rgb[2] == 0)
			{
				rgb = new byte[3]
				{
					byte.MaxValue,
					byte.MaxValue,
					byte.MaxValue
				};
			}
			else if (rgb[0] == 255 && rgb[1] == 255 && rgb[2] == 255)
			{
				byte[] array = new byte[3];
				rgb = array;
			}
			return rgb;
		}

		public byte[] GetRgb()
		{
			return RGB;
		}

		/// Standard Alpha Red Green Blue ctColor value (ARGB).
		public byte[] GetARgb()
		{
			byte[] rGBOrARGB = GetRGBOrARGB();
			if (rGBOrARGB == null)
			{
				return null;
			}
			if (rGBOrARGB.Length == 3)
			{
				byte[] array = new byte[4]
				{
					byte.MaxValue,
					0,
					0,
					0
				};
				Array.Copy(rGBOrARGB, 0, array, 1, 3);
				return array;
			}
			return rGBOrARGB;
		}

		private byte[] GetRGBOrARGB()
		{
			byte[] array = null;
			if (ctColor.indexedSpecified && ctColor.indexed != 0)
			{
				HSSFColor hSSFColor = (HSSFColor)HSSFColor.GetIndexHash()[(int)ctColor.indexed];
				if (hSSFColor != null)
				{
					return new byte[3]
					{
						hSSFColor.GetTriplet()[0],
						hSSFColor.GetTriplet()[1],
						hSSFColor.GetTriplet()[2]
					};
				}
			}
			if (!ctColor.IsSetRgb())
			{
				return null;
			}
			array = ctColor.GetRgb();
			return CorrectRGB(array);
		}

		/// Standard Red Green Blue ctColor value (RGB) with applied tint.
		/// Alpha values are ignored.
		public byte[] GetRgbWithTint()
		{
			byte[] array = ctColor.GetRgb();
			if (array != null)
			{
				if (array.Length == 4)
				{
					byte[] array2 = new byte[3];
					Array.Copy(array, 1, array2, 0, 3);
					array = array2;
				}
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ApplyTint(array[i] & 0xFF, ctColor.tint);
				}
			}
			return array;
		}

		/// Return the ARGB value in hex format, eg FF00FF00.
		/// Works for both regular and indexed colours. 
		public string GetARGBHex()
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] aRgb = GetARgb();
			if (aRgb == null)
			{
				return null;
			}
			byte[] array = aRgb;
			foreach (byte b in array)
			{
				int num = b;
				if (num < 0)
				{
					num += 256;
				}
				string text = StringUtil.ToHexString(num);
				if (text.Length == 1)
				{
					stringBuilder.Append('0');
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString().ToUpper();
		}

		private static byte ApplyTint(int lum, double tint)
		{
			if (tint > 0.0)
			{
				return (byte)((double)lum * (1.0 - tint) + (255.0 - 255.0 * (1.0 - tint)));
			}
			if (tint < 0.0)
			{
				return (byte)((double)lum * (1.0 + tint));
			}
			return (byte)lum;
		}

		/// Standard Alpha Red Green Blue ctColor value (ARGB).
		public void SetRgb(byte[] rgb)
		{
			ctColor.SetRgb(CorrectRGB(rgb));
		}

		/// Returns the underlying XML bean
		///
		/// @return the underlying XML bean
		internal CT_Color GetCTColor()
		{
			return ctColor;
		}

		public override int GetHashCode()
		{
			return ctColor.ToString().GetHashCode();
		}

		public override bool Equals(object o)
		{
			if (o == null || !(o is XSSFColor))
			{
				return false;
			}
			XSSFColor xSSFColor = (XSSFColor)o;
			return ctColor.ToString().Equals(xSSFColor.GetCTColor().ToString());
		}
	}
}
