using NPOI.POIFS.Common;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// Abstract base class of all POIFS block storage classes. All
	/// extensions of BigBlock should write 512 bytes of data when
	/// requested to write their data.
	/// This class has package scope, as there is no reason at this time to
	/// make the class public.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public abstract class BigBlock : BlockWritable
	{
		protected POIFSBigBlockSize bigBlockSize;

		protected BigBlock()
		{
		}

		protected BigBlock(POIFSBigBlockSize bigBlockSize)
		{
			this.bigBlockSize = bigBlockSize;
		}

		/// <summary>
		/// Default implementation of write for extending classes that
		/// contain their data in a simple array of bytes.
		/// </summary>
		/// <param name="stream">the OutputStream to which the data should be written.</param>
		/// <param name="data">the byte array of to be written.</param>
		protected void WriteData(Stream stream, byte[] data)
		{
			stream.Write(data, 0, data.Length);
		}

		/// <summary>
		/// Write the block's data to an OutputStream
		/// </summary>
		/// <param name="stream">the OutputStream to which the stored data should be written</param>
		public void WriteBlocks(Stream stream)
		{
			WriteData(stream);
		}

		/// <summary>
		/// Write the storage to an OutputStream
		/// </summary>
		/// <param name="stream">the OutputStream to which the stored data should be written </param>
		public abstract void WriteData(Stream stream);
	}
}
