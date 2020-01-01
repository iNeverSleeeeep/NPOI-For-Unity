namespace NPOI.SS.UserModel
{
	public interface IPictureData
	{
		/// Gets the picture data.
		///
		/// @return the picture data.
		byte[] Data
		{
			get;
		}

		/// Returns the mime type for the image
		string MimeType
		{
			get;
		}

		/// Suggests a file extension for this image.
		///
		/// @return the file extension.
		string SuggestFileExtension();
	}
}
