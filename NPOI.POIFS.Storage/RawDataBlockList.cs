using NPOI.POIFS.Common;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// A list of RawDataBlocks instances, and methods to manage the list
	/// @author Marc Johnson (mjohnson at apache dot org
	/// </summary>
	public class RawDataBlockList : BlockListImpl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Storage.RawDataBlockList" /> class.
		/// </summary>
		/// <param name="stream">the InputStream from which the data will be read</param>
		/// <param name="bigBlockSize">The big block size, either 512 bytes or 4096 bytes</param>
		public RawDataBlockList(Stream stream, POIFSBigBlockSize bigBlockSize)
		{
			List<RawDataBlock> list = new List<RawDataBlock>();
			RawDataBlock rawDataBlock;
			do
			{
				rawDataBlock = new RawDataBlock(stream, bigBlockSize.GetBigBlockSize());
				if (rawDataBlock.HasData)
				{
					list.Add(rawDataBlock);
				}
			}
			while (!rawDataBlock.EOF);
			SetBlocks((ListManagedBlock[])list.ToArray());
		}
	}
}
