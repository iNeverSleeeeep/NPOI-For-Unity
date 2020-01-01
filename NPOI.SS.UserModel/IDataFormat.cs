namespace NPOI.SS.UserModel
{
	public interface IDataFormat
	{
		/// get the format index that matches the given format string.
		/// Creates a new format if one is not found.  Aliases text to the proper format.
		/// @param format string matching a built in format
		/// @return index of format.
		short GetFormat(string format);

		/// get the format string that matches the given format index
		/// @param index of a format
		/// @return string represented at index of format or null if there is not a  format at that index
		string GetFormat(short index);
	}
}
