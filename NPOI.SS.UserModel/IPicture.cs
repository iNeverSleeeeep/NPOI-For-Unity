namespace NPOI.SS.UserModel
{
	/// Repersents a picture in a SpreadsheetML document
	///
	/// @author Yegor Kozlov
	public interface IPicture
	{
		/// Return picture data for this picture
		///
		/// @return picture data for this picture
		IPictureData PictureData
		{
			get;
		}

		/// Reset the image to the original size.
		void Resize();

		/// Reset the image to the original size.
		///
		/// @param scale the amount by which image dimensions are multiplied relative to the original size.
		/// <code>resize(1.0)</code> Sets the original size, <code>resize(0.5)</code> resize to 50% of the original,
		/// <code>resize(2.0)</code> resizes to 200% of the original.
		void Resize(double scale);

		IClientAnchor GetPreferredSize();
	}
}
