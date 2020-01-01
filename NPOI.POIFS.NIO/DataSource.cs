using NPOI.Util;
using System.IO;

namespace NPOI.POIFS.NIO
{
	/// <summary>
	/// Common definition of how we read and write bytes
	/// </summary>
	public abstract class DataSource
	{
		public abstract long Size
		{
			get;
		}

		public abstract ByteBuffer Read(int length, long position);

		public abstract void Write(ByteBuffer src, long position);

		/// <summary>
		/// Close the underlying stream
		/// </summary>
		public abstract void Close();

		/// <summary>
		/// Copies the contents to the specified Stream
		/// </summary>
		/// <param name="stream"></param>
		public abstract void CopyTo(Stream stream);
	}
}
