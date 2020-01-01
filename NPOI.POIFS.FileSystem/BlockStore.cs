using NPOI.POIFS.Storage;
using NPOI.Util;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// This abstract class describes a way to read, store, chain
	/// and free a series of blocks (be they Big or Small ones)
	/// </summary>
	public abstract class BlockStore
	{
		/// <summary>
		/// Returns the size of the blocks managed through the block store.
		/// </summary>
		/// <returns></returns>
		public abstract int GetBlockStoreBlockSize();

		/// <summary>
		/// Load the block at the given offset.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public abstract ByteBuffer GetBlockAt(int offset);

		/// <summary>
		/// Extends the file if required to hold blocks up to
		/// the specified offset, and return the block from there.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public abstract ByteBuffer CreateBlockIfNeeded(int offset);

		/// <summary>
		/// Returns the BATBlock that handles the specified offset,
		/// and the relative index within it
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public abstract BATBlockAndIndex GetBATBlockAndIndex(int offset);

		/// <summary>
		/// Works out what block follows the specified one.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public abstract int GetNextBlock(int offset);

		/// <summary>
		/// Changes the record of what block follows the specified one.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="nextBlock"></param>
		public abstract void SetNextBlock(int offset, int nextBlock);

		/// <summary>
		/// Finds a free block, and returns its offset.
		/// This method will extend the file/stream if needed, and if doing
		///  so, allocate new FAT blocks to address the extra space.
		/// </summary>
		/// <returns></returns>
		public abstract int GetFreeBlock();

		/// <summary>
		/// Creates a Detector for loops in the chain 
		/// </summary>
		/// <returns></returns>
		public abstract ChainLoopDetector GetChainLoopDetector();
	}
}
