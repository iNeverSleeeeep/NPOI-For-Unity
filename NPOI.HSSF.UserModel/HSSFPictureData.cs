using NPOI.DDF;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Represents binary data stored in the file.  Eg. A GIF, JPEG etc...
	/// @author Daniel Noll
	/// </summary>
	public class HSSFPictureData : IPictureData
	{
		public const short MSOBI_WMF = 8544;

		public const short MSOBI_EMF = 15680;

		public const short MSOBI_PICT = 21536;

		public const short MSOBI_PNG = 28160;

		public const short MSOBI_JPEG = 18080;

		public const short MSOBI_DIB = 31360;

		public const short FORMAT_MASK = -16;

		/// Underlying escher blip record containing the bitmap data.
		private EscherBlipRecord blip;

		/// <summary>
		/// Gets the picture data.
		/// </summary>
		/// <value>the picture data.</value>
		public byte[] Data
		{
			get
			{
				byte[] array = blip.PictureData;
				if (PngUtils.MatchesPngHeader(array, 16))
				{
					byte[] array2 = new byte[array.Length - 16];
					Array.Copy(array, 16, array2, 0, array2.Length);
					array = array2;
				}
				return array;
			}
		}

		/// <summary>
		/// gets format of the picture.
		/// </summary>
		/// <value>The format.</value>
		public int Format => blip.RecordId - -4072;

		/// Returns the mime type for the image
		public string MimeType
		{
			get
			{
				switch (blip.RecordId)
				{
				case -4069:
					return "image/x-wmf";
				case -4070:
					return "image/x-emf";
				case -4068:
					return "image/x-pict";
				case -4066:
					return "image/png";
				case -4067:
					return "image/jpeg";
				case -4065:
					return "image/bmp";
				default:
					return "image/unknown";
				}
			}
		}

		/// <summary>
		/// Constructs a picture object.
		/// </summary>
		/// <param name="blip">the underlying blip record containing the bitmap data.</param>
		public HSSFPictureData(EscherBlipRecord blip)
		{
			this.blip = blip;
		}

		/// <summary>
		/// Suggests a file extension for this image.
		/// </summary>
		/// <returns>the file extension.</returns>
		public string SuggestFileExtension()
		{
			switch (blip.RecordId)
			{
			case -4069:
				return "wmf";
			case -4070:
				return "emf";
			case -4068:
				return "pict";
			case -4066:
				return "png";
			case -4067:
				return "jpeg";
			case -4065:
				return "dib";
			default:
				return "";
			}
		}
	}
}
