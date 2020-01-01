using NPOI.Util;
using System;
using System.IO;

namespace NPOI.POIFS.NIO
{
	/// <summary>
	/// A POIFS DataSource backed by a File
	/// </summary>
	public class FileBackedDataSource : DataSource
	{
		private Stream fileStream;

		public override long Size => fileStream.Length;

		public FileBackedDataSource(FileInfo file)
		{
			if (file.Exists)
			{
				throw new FileNotFoundException(file.FullName);
			}
		}

		public FileBackedDataSource(FileStream stream)
		{
			stream.Position = 0L;
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, (int)stream.Length);
			MemoryStream memoryStream = (MemoryStream)(fileStream = new MemoryStream(array, 0, array.Length));
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && fileStream != null)
			{
				fileStream.Dispose();
				fileStream = null;
			}
		}

		~FileBackedDataSource()
		{
			Dispose(disposing: false);
		}

		/// <summary>
		/// Reads a sequence of bytes from this FileStream starting at the given file position.
		/// </summary>
		/// <param name="length"></param>
		/// <param name="position">The file position at which the transfer is to begin;</param>
		/// <returns></returns>
		public override ByteBuffer Read(int length, long position)
		{
			if (position >= Size)
			{
				throw new ArgumentException("Position " + position + " past the end of the file");
			}
			fileStream.Position = position;
			ByteBuffer byteBuffer = ByteBuffer.CreateBuffer(length);
			int num = IOUtils.ReadFully(fileStream, byteBuffer.Buffer);
			if (num == -1)
			{
				throw new ArgumentException("Position " + position + " past the end of the file");
			}
			byteBuffer.Position = 0;
			return byteBuffer;
		}

		/// <summary>
		/// Writes a sequence of bytes to this FileStream from the given Stream,
		/// starting at the given file position.
		/// </summary>
		/// <param name="src">The Stream from which bytes are to be transferred</param>
		/// <param name="position">The file position at which the transfer is to begin;
		/// must be non-negative</param>
		public override void Write(ByteBuffer src, long position)
		{
			fileStream.Write(src.Buffer, (int)position, src.Length);
		}

		public override void CopyTo(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Write(array, 0, array.Length);
			fileStream.Write(array, 0, array.Length);
		}

		public override void Close()
		{
			fileStream.Close();
		}
	}
}
