using NPOI.POIFS.Properties;
using NPOI.POIFS.Storage;
using NPOI.Util;
using System;
using System.Collections.Generic;

namespace NPOI.POIFS.FileSystem
{
	/// This class handles the MiniStream (small block store)
	///  in the NIO case for {@link NPOIFSFileSystem}
	public class NPOIFSMiniStore : BlockStore
	{
		private NPOIFSFileSystem _filesystem;

		private NPOIFSStream _mini_stream;

		private List<BATBlock> _sbat_blocks;

		private HeaderBlock _header;

		private RootProperty _root;

		public NPOIFSMiniStore(NPOIFSFileSystem filesystem, RootProperty root, List<BATBlock> sbats, HeaderBlock header)
		{
			_filesystem = filesystem;
			_sbat_blocks = sbats;
			_header = header;
			_root = root;
			_mini_stream = new NPOIFSStream(filesystem, root.StartBlock);
		}

		/// Load the block at the given offset.
		public override ByteBuffer GetBlockAt(int offset)
		{
			int num = offset * 64;
			int num2 = num / _filesystem.GetBigBlockSize();
			int num3 = num % _filesystem.GetBigBlockSize();
			StreamBlockByteBufferIterator streamBlockByteBufferIterator = _mini_stream.GetBlockIterator() as StreamBlockByteBufferIterator;
			for (int i = 0; i < num2; i++)
			{
				streamBlockByteBufferIterator.Next();
			}
			ByteBuffer byteBuffer = streamBlockByteBufferIterator.Next();
			if (byteBuffer == null)
			{
				throw new IndexOutOfRangeException("Big block " + num2 + " outside stream");
			}
			byteBuffer.Position += num3;
			ByteBuffer byteBuffer2 = byteBuffer.Slice();
			byteBuffer2.Limit = 64;
			return byteBuffer2;
		}

		/// Load the block, extending the underlying stream if needed
		public override ByteBuffer CreateBlockIfNeeded(int offset)
		{
			try
			{
				return GetBlockAt(offset);
			}
			catch (IndexOutOfRangeException)
			{
				int freeBlock = _filesystem.GetFreeBlock();
				_filesystem.CreateBlockIfNeeded(freeBlock);
				ChainLoopDetector chainLoopDetector = _filesystem.GetChainLoopDetector();
				int offset2 = _mini_stream.GetStartBlock();
				while (true)
				{
					chainLoopDetector.Claim(offset2);
					int nextBlock = _filesystem.GetNextBlock(offset2);
					if (nextBlock == -2)
					{
						break;
					}
					offset2 = nextBlock;
				}
				_filesystem.SetNextBlock(offset2, freeBlock);
				_filesystem.SetNextBlock(freeBlock, -2);
				return CreateBlockIfNeeded(offset);
			}
		}

		/// Returns the BATBlock that handles the specified offset,
		///  and the relative index within it
		public override BATBlockAndIndex GetBATBlockAndIndex(int offset)
		{
			return BATBlock.GetSBATBlockAndIndex(offset, _header, _sbat_blocks);
		}

		/// Works out what block follows the specified one.
		public override int GetNextBlock(int offset)
		{
			BATBlockAndIndex bATBlockAndIndex = GetBATBlockAndIndex(offset);
			return bATBlockAndIndex.Block.GetValueAt(bATBlockAndIndex.Index);
		}

		/// Changes the record of what block follows the specified one.
		public override void SetNextBlock(int offset, int nextBlock)
		{
			BATBlockAndIndex bATBlockAndIndex = GetBATBlockAndIndex(offset);
			bATBlockAndIndex.Block.SetValueAt(bATBlockAndIndex.Index, nextBlock);
		}

		/// Finds a free block, and returns its offset.
		/// This method will extend the file if needed, and if doing
		///  so, allocate new FAT blocks to Address the extra space.
		public override int GetFreeBlock()
		{
			int bATEntriesPerBlock = _filesystem.GetBigBlockSizeDetails().GetBATEntriesPerBlock();
			int num = 0;
			for (int i = 0; i < _sbat_blocks.Count; i++)
			{
				BATBlock bATBlock = _sbat_blocks[i];
				if (bATBlock.HasFreeSectors)
				{
					for (int j = 0; j < bATEntriesPerBlock; j++)
					{
						int valueAt = bATBlock.GetValueAt(j);
						if (valueAt == -1)
						{
							return num + j;
						}
					}
				}
				num += bATEntriesPerBlock;
			}
			BATBlock bATBlock2 = BATBlock.CreateEmptyBATBlock(_filesystem.GetBigBlockSizeDetails(), isXBAT: false);
			int num2 = bATBlock2.OurBlockIndex = _filesystem.GetFreeBlock();
			if (_header.SBATCount == 0)
			{
				_header.SBATStart = num2;
				_header.SBATBlockCount = 1;
			}
			else
			{
				ChainLoopDetector chainLoopDetector = _filesystem.GetChainLoopDetector();
				int offset = _header.SBATStart;
				while (true)
				{
					chainLoopDetector.Claim(offset);
					int nextBlock = _filesystem.GetNextBlock(offset);
					if (nextBlock == -2)
					{
						break;
					}
					offset = nextBlock;
				}
				_filesystem.SetNextBlock(offset, num2);
				_header.SBATBlockCount = _header.SBATCount + 1;
			}
			_filesystem.SetNextBlock(num2, -2);
			_sbat_blocks.Add(bATBlock2);
			return num;
		}

		public override ChainLoopDetector GetChainLoopDetector()
		{
			return new ChainLoopDetector(_root.Size, this);
		}

		public override int GetBlockStoreBlockSize()
		{
			return 64;
		}

		/// Writes the SBATs to their backing blocks
		public void SyncWithDataSource()
		{
			foreach (BATBlock sbat_block in _sbat_blocks)
			{
				ByteBuffer blockAt = _filesystem.GetBlockAt(sbat_block.OurBlockIndex);
				BlockAllocationTableWriter.WriteBlock(sbat_block, blockAt);
			}
		}
	}
}
