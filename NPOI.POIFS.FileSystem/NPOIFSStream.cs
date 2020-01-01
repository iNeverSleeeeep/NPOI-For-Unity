using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.POIFS.FileSystem
{
	/// This handles Reading and writing a stream within a
	///  {@link NPOIFSFileSystem}. It can supply an iterator
	///  to read blocks, and way to write out to existing and
	///  new blocks.
	/// Most users will want a higher level version of this, 
	///  which deals with properties to track which stream
	///  this is.
	/// This only works on big block streams, it doesn't
	///  handle small block ones.
	/// This uses the new NIO code
	///
	/// TODO Implement a streaming write method, and append
	public class NPOIFSStream : IEnumerable<ByteBuffer>, IEnumerable
	{
		private BlockStore blockStore;

		private int startBlock;

		/// Constructor for an existing stream. It's up to you
		///  to know how to Get the start block (eg from a 
		///  {@link HeaderBlock} or a {@link Property}) 
		public NPOIFSStream(BlockStore blockStore, int startBlock)
		{
			this.blockStore = blockStore;
			this.startBlock = startBlock;
		}

		/// Constructor for a new stream. A start block won't
		///  be allocated until you begin writing to it.
		public NPOIFSStream(BlockStore blockStore)
		{
			this.blockStore = blockStore;
			startBlock = -2;
		}

		/// What block does this stream start at?
		/// Will be {@link POIFSConstants#END_OF_CHAIN} for a
		///  new stream that hasn't been written to yet.
		public int GetStartBlock()
		{
			return startBlock;
		}

		public IEnumerator<ByteBuffer> GetEnumerator()
		{
			return GetBlockIterator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetBlockIterator();
		}

		/// Returns an iterator that'll supply one {@link ByteBuffer}
		///  per block in the stream.
		public IEnumerator<ByteBuffer> GetBlockIterator()
		{
			if (startBlock == -2)
			{
				throw new InvalidOperationException("Can't read from a new stream before it has been written to");
			}
			return new StreamBlockByteBufferIterator(blockStore, startBlock);
		}

		/// Updates the contents of the stream to the new
		///  Set of bytes.
		/// Note - if this is property based, you'll still
		///  need to update the size in the property yourself
		public void UpdateContents(byte[] contents)
		{
			int blockStoreBlockSize = blockStore.GetBlockStoreBlockSize();
			int num = (int)Math.Ceiling((double)contents.Length / (double)blockStoreBlockSize);
			ChainLoopDetector chainLoopDetector = blockStore.GetChainLoopDetector();
			int num2 = -2;
			int num3 = startBlock;
			for (int i = 0; i < num; i++)
			{
				int num4 = num3;
				if (num4 == -2)
				{
					num4 = blockStore.GetFreeBlock();
					chainLoopDetector.Claim(num4);
					num3 = -2;
					if (num2 != -2)
					{
						blockStore.SetNextBlock(num2, num4);
					}
					blockStore.SetNextBlock(num4, -2);
					if (startBlock == -2)
					{
						startBlock = num4;
					}
				}
				else
				{
					chainLoopDetector.Claim(num4);
					num3 = blockStore.GetNextBlock(num4);
				}
				ByteBuffer byteBuffer = blockStore.CreateBlockIfNeeded(num4);
				int num5 = i * blockStoreBlockSize;
				int length = Math.Min(contents.Length - num5, blockStoreBlockSize);
				byteBuffer.Write(contents, num5, length);
				num2 = num4;
			}
			int offset = num2;
			NPOIFSStream nPOIFSStream = new NPOIFSStream(blockStore, num3);
			nPOIFSStream.free(chainLoopDetector);
			blockStore.SetNextBlock(offset, -2);
		}

		/// Frees all blocks in the stream
		public void free()
		{
			ChainLoopDetector chainLoopDetector = blockStore.GetChainLoopDetector();
			free(chainLoopDetector);
		}

		private void free(ChainLoopDetector loopDetector)
		{
			int nextBlock = startBlock;
			while (nextBlock != -2)
			{
				int offset = nextBlock;
				loopDetector.Claim(offset);
				nextBlock = blockStore.GetNextBlock(offset);
				blockStore.SetNextBlock(offset, -1);
			}
			startBlock = -2;
		}
	}
}
