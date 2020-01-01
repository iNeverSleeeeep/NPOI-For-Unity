using NPOI.POIFS.Common;
using NPOI.POIFS.FileSystem;
using NPOI.Util;
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
	/// *
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class BlockAllocationTableWriter : BlockWritable, BATManaged
	{
		private List<int> _entries;

		private BATBlock[] _blocks;

		private int _start_block;

		private POIFSBigBlockSize _bigBlockSize;

		private static int _default_size = 128;

		/// <summary>
		/// Sets the start block for this instance
		/// </summary>
		/// <value>
		/// index into the array of BigBlock instances making up the the filesystem
		/// </value>
		public int StartBlock
		{
			get
			{
				return _start_block;
			}
			set
			{
				_start_block = value;
			}
		}

		/// <summary>
		/// Gets the number of BigBlock's this instance uses
		/// </summary>
		/// <value>count of BigBlock instances</value>
		public int CountBlocks => _blocks.Length;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Storage.BlockAllocationTableWriter" /> class.
		/// </summary>
		public BlockAllocationTableWriter(POIFSBigBlockSize bigBlockSize)
		{
			_start_block = -2;
			_entries = new List<int>(_default_size);
			_blocks = new BATBlock[0];
			_bigBlockSize = bigBlockSize;
		}

		/// <summary>
		/// Create the BATBlocks we need
		/// </summary>
		/// <returns>start block index of BAT blocks</returns>
		public int CreateBlocks()
		{
			int num = 0;
			int num2 = 0;
			while (true)
			{
				int num3 = BATBlock.CalculateStorageRequirements(_bigBlockSize, num2 + num + _entries.Count);
				int num4 = HeaderBlockWriter.CalculateXBATStorageRequirements(_bigBlockSize, num3);
				if (num2 == num3 && num == num4)
				{
					break;
				}
				num2 = num3;
				num = num4;
			}
			int result = AllocateSpace(num2);
			AllocateSpace(num);
			SimpleCreateBlocks();
			return result;
		}

		/// <summary>
		/// Allocate space for a block of indices
		/// </summary>
		/// <param name="blockCount">the number of blocks to allocate space for</param>
		/// <returns>the starting index of the blocks</returns>
		public int AllocateSpace(int blockCount)
		{
			int count = _entries.Count;
			if (blockCount > 0)
			{
				int num = blockCount - 1;
				int num2 = count + 1;
				for (int i = 0; i < num; i++)
				{
					_entries.Add(num2++);
				}
				_entries.Add(-2);
			}
			return count;
		}

		/// <summary>
		/// create the BATBlocks
		/// </summary>
		internal void SimpleCreateBlocks()
		{
			_blocks = BATBlock.CreateBATBlocks(_bigBlockSize, _entries.ToArray());
		}

		/// <summary>
		/// Write the storage to an OutputStream
		/// </summary>
		/// <param name="stream">the OutputStream to which the stored data should be written</param>
		public void WriteBlocks(Stream stream)
		{
			for (int i = 0; i < _blocks.Length; i++)
			{
				_blocks[i].WriteBlocks(stream);
			}
		}

		public static void WriteBlock(BATBlock bat, ByteBuffer block)
		{
			bat.WriteData(block);
		}
	}
}
