using NPOI.Util;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// A big block created from an InputStream, holding the raw data
	/// @author Marc Johnson (mjohnson at apache dot org
	/// </summary>
	public class RawDataBlock : ListManagedBlock
	{
		private byte[] _data;

		private bool _eof;

		private bool _hasData;

		private static POILogger log = POILogFactory.GetLogger(typeof(RawDataBlock));

		/// <summary>
		/// When we read the data, did we hit end of file?
		/// </summary>
		/// <value><c>true</c> if the EoF was hit during this block, or; otherwise, <c>false</c>if not. If you have a dodgy short last block, then
		/// it's possible to both have data, and also hit EoF...</value>
		public bool EOF => _eof;

		/// <summary>
		/// Did we actually find any data to read? It's possible,
		/// in the event of a short last block, to both have hit
		/// the EoF, but also to have data
		/// </summary>
		/// <value><c>true</c> if this instance has data; otherwise, <c>false</c>.</value>
		public bool HasData => _hasData;

		/// <summary>
		/// Get the data from the block
		/// </summary>
		/// <value>the block's data as a byte array</value>
		public byte[] Data
		{
			get
			{
				if (!HasData)
				{
					throw new IOException("Cannot return empty data");
				}
				return _data;
			}
		}

		public int BigBlockSize => _data.Length;

		/// <summary>
		/// Constructor RawDataBlock
		/// </summary>
		/// <param name="stream">the Stream from which the data will be read</param>
		public RawDataBlock(Stream stream)
			: this(stream, 512)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Storage.RawDataBlock" /> class.
		/// </summary>
		/// <param name="stream">the Stream from which the data will be read</param>
		/// <param name="blockSize">the size of the POIFS blocks, normally 512 bytes {@link POIFSConstants#BIG_BLOCK_SIZE}</param>
		public RawDataBlock(Stream stream, int blockSize)
		{
			_data = new byte[blockSize];
			int num = IOUtils.ReadFully(stream, _data);
			_hasData = (num > 0);
			if (num == -1)
			{
				_eof = true;
			}
			else if (num != blockSize)
			{
				_eof = true;
				string text = " byte" + ((num == 1) ? "" : "s");
				log.Log(7, "Unable to read entire block; " + num + text + " read before EOF; expected " + blockSize + " bytes. Your document was either written by software that ignores the spec, or has been truncated!");
			}
			else
			{
				_eof = false;
			}
		}

		public override string ToString()
		{
			return "RawDataBlock of size " + _data.Length;
		}
	}
}
