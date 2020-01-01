using System;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// Used to detect if a chain has a loop in it, so
	///  we can bail out with an error rather than
	///  spinning away for ever... 
	/// </summary>
	public class ChainLoopDetector
	{
		private bool[] used_blocks;

		private BlockStore blockStore;

		public ChainLoopDetector(long rawSize, BlockStore blockStore)
		{
			this.blockStore = blockStore;
			int num = (int)Math.Ceiling(1.0 * (double)(rawSize / blockStore.GetBlockStoreBlockSize()));
			used_blocks = new bool[num];
		}

		public void Claim(int offset)
		{
			if (offset < used_blocks.Length)
			{
				if (used_blocks[offset])
				{
					throw new InvalidOperationException("Potential loop detected - Block " + offset + " was already claimed but was just requested again");
				}
				used_blocks[offset] = true;
			}
		}
	}
}
