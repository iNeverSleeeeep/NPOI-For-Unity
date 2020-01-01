using System.IO;

namespace NPOI.Util
{
	/// <summary>
	/// behavior of a field at a fixed location within a byte array
	/// @author Marc Johnson (mjohnson at apache dot org
	/// </summary>
	public interface FixedField
	{
		/// <summary>
		/// set the value from its offset into an array of bytes
		/// </summary>
		/// <param name="data">the byte array from which the value is to be read</param>
		void ReadFromBytes(byte[] data);

		/// <summary>
		/// set the value from an Stream
		/// </summary>
		/// <param name="stream">the Stream from which the value is to be read</param>
		void ReadFromStream(Stream stream);

		/// <summary>
		/// return the value as a String
		/// </summary>
		/// <returns></returns>
		new string ToString();

		/// <summary>
		/// write the value out to an array of bytes at the appropriate offset
		/// </summary>
		/// <param name="data">the array of bytes to which the value is to be written</param>
		void WriteToBytes(byte[] data);
	}
}
