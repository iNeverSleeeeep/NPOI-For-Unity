using NPOI.POIFS.Common;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// This class manages and creates the Block Allocation Table, which is
	/// basically a set of linked lists of block indices.
	/// Each block of the filesystem has an index. The first block, the
	/// header, is skipped; the first block after the header is index 0,
	/// the next is index 1, and so on.
	/// A block's index is also its index into the Block Allocation
	/// Table. The entry that it finds in the Block Allocation Table is the
	/// index of the next block in the linked list of blocks making up a
	/// file, or it is set to -2: end of list.
	///
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class BlockAllocationTableReader
	{
		private const int MAX_BLOCK_COUNT = 65535;

		private static POILogger _logger = POILogFactory.GetLogger(typeof(BlockAllocationTableReader));

		private List<int> _entries;

		private POIFSBigBlockSize bigBlockSize;

		/// <summary>
		/// create a BlockAllocationTableReader for an existing filesystem. Side
		/// effect: when this method finishes, the BAT blocks will have
		/// been Removed from the raw block list, and any blocks labeled as
		/// 'unused' in the block allocation table will also have been
		/// Removed from the raw block list. </summary>
		/// <param name="bigBlockSizse">the poifs bigBlockSize</param>
		/// <param name="block_count">the number of BAT blocks making up the block allocation table</param>
		/// <param name="block_array">the array of BAT block indices from the
		/// filesystem's header</param>
		/// <param name="xbat_count">the number of XBAT blocks</param>
		/// <param name="xbat_index">the index of the first XBAT block</param>
		/// <param name="raw_block_list">the list of RawDataBlocks</param>
		public BlockAllocationTableReader(POIFSBigBlockSize bigBlockSizse, int block_count, int[] block_array, int xbat_count, int xbat_index, BlockList raw_block_list)
			: this(bigBlockSizse)
		{
			SanityCheckBlockCount(block_count);
			RawDataBlock[] array = new RawDataBlock[block_count];
			int num = Math.Min(block_count, block_array.Length);
			int i;
			for (i = 0; i < num; i++)
			{
				int num2 = block_array[i];
				if (num2 > raw_block_list.BlockCount())
				{
					throw new IOException("Your file contains " + raw_block_list.BlockCount() + " sectors, but the initial DIFAT array at index " + i + " referenced block # " + num2 + ". This isn't allowed and  your file is corrupt");
				}
				array[i] = (RawDataBlock)raw_block_list.Remove(num2);
			}
			if (i < block_count)
			{
				if (xbat_index < 0)
				{
					throw new IOException("BAT count exceeds limit, yet XBAT index indicates no valid entries");
				}
				int num3 = xbat_index;
				int entriesPerXBATBlock = BATBlock.EntriesPerXBATBlock;
				int xBATChainOffset = BATBlock.XBATChainOffset;
				for (int j = 0; j < xbat_count; j++)
				{
					num = Math.Min(block_count - i, entriesPerXBATBlock);
					byte[] data = raw_block_list.Remove(num3).Data;
					int num4 = 0;
					for (int k = 0; k < num; k++)
					{
						array[i++] = (RawDataBlock)raw_block_list.Remove(LittleEndian.GetInt(data, num4));
						num4 += 4;
					}
					num3 = LittleEndian.GetInt(data, xBATChainOffset);
					if (num3 == -2)
					{
						break;
					}
				}
			}
			if (i != block_count)
			{
				throw new IOException("Could not find all blocks");
			}
			SetEntries((ListManagedBlock[])array, raw_block_list);
		}

		/// <summary>
		/// create a BlockAllocationTableReader from an array of raw data blocks
		/// </summary>
		/// <param name="bigBlockSize"></param>
		/// <param name="blocks">the raw data</param>
		/// <param name="raw_block_list">the list holding the managed blocks</param>
		public BlockAllocationTableReader(POIFSBigBlockSize bigBlockSize, ListManagedBlock[] blocks, BlockList raw_block_list)
			: this(bigBlockSize)
		{
			SetEntries(blocks, raw_block_list);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Storage.BlockAllocationTableReader" /> class.
		/// </summary>
		public BlockAllocationTableReader(POIFSBigBlockSize bigBlockSize)
		{
			this.bigBlockSize = bigBlockSize;
			_entries = new List<int>();
		}

		/// <summary>
		/// walk the entries from a specified point and return the
		/// associated blocks. The associated blocks are Removed from the block list
		/// </summary>
		/// <param name="startBlock">the first block in the chain</param>
		/// <param name="headerPropertiesStartBlock"></param>
		/// <param name="blockList">the raw data block list</param>
		/// <returns>array of ListManagedBlocks, in their correct order</returns>
		public ListManagedBlock[] FetchBlocks(int startBlock, int headerPropertiesStartBlock, BlockList blockList)
		{
			List<ListManagedBlock> list = new List<ListManagedBlock>();
			int num = startBlock;
			bool flag = true;
			ListManagedBlock listManagedBlock = null;
			while (num != -2)
			{
				try
				{
					listManagedBlock = blockList.Remove(num);
					list.Add(listManagedBlock);
					num = _entries[num];
					flag = false;
				}
				catch (Exception)
				{
					if (num == headerPropertiesStartBlock)
					{
						_logger.Log(5, "Warning, header block comes after data blocks in POIFS block listing");
						num = -2;
					}
					else
					{
						if (num != 0 || !flag)
						{
							throw;
						}
						_logger.Log(5, "Warning, incorrectly terminated empty data blocks in POIFS block listing (should end at -2, ended at 0)");
						num = -2;
					}
				}
			}
			return list.ToArray();
		}

		/// <summary>
		/// determine whether the block specified by index is used or not
		/// </summary>
		/// <param name="index">determine whether the block specified by index is used or not</param>
		/// <returns>
		/// 	<c>true</c> if the specified block is used; otherwise, <c>false</c>.
		/// </returns>
		public bool IsUsed(int index)
		{
			bool result = false;
			try
			{
				result = (_entries[index] != -1);
				return result;
			}
			catch (IndexOutOfRangeException)
			{
				return result;
			}
		}

		/// <summary>
		/// return the next block index
		/// </summary>
		/// <param name="index">The index of the current block</param>
		/// <returns>index of the next block (may be
		/// POIFSConstants.END_OF_CHAIN, indicating end of chain
		/// (duh))</returns>
		public int GetNextBlockIndex(int index)
		{
			if (IsUsed(index))
			{
				return _entries[index];
			}
			throw new IOException("index " + index + " is unused");
		}

		/// <summary>
		/// Convert an array of blocks into a Set of integer indices
		/// </summary>
		/// <param name="blocks">the array of blocks containing the indices</param>
		/// <param name="raw_blocks">the list of blocks being managed. Unused
		/// blocks will be eliminated from the list</param>
		private void SetEntries(ListManagedBlock[] blocks, BlockList raw_blocks)
		{
			int bATEntriesPerBlock = bigBlockSize.GetBATEntriesPerBlock();
			for (int i = 0; i < blocks.Length; i++)
			{
				byte[] data = blocks[i].Data;
				int num = 0;
				for (int j = 0; j < bATEntriesPerBlock; j++)
				{
					int @int = LittleEndian.GetInt(data, num);
					if (@int == -1)
					{
						raw_blocks.Zap(_entries.Count);
					}
					_entries.Add(@int);
					num += 4;
				}
				blocks[i] = null;
			}
			raw_blocks.BAT = this;
		}

		public static void SanityCheckBlockCount(int block_count)
		{
			if (block_count <= 0)
			{
				throw new IOException("Illegal block count; minimum count is 1, got " + block_count + " instead");
			}
			if (block_count > 65535)
			{
				throw new IOException("Block count " + block_count + " is too high. POI maximum is " + 65535 + ".");
			}
		}
	}
}
