using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	public class StreamBlockByteBufferIterator : IEnumerator<ByteBuffer>, IDisposable, IEnumerator
	{
		private ChainLoopDetector loopDetector;

		private int nextBlock;

		private BlockStore blockStore;

		private ByteBuffer current;

		public ByteBuffer Current => current;

		object IEnumerator.Current
		{
			get
			{
				return current;
			}
		}

		public StreamBlockByteBufferIterator(BlockStore blockStore, int firstBlock)
		{
			this.blockStore = blockStore;
			nextBlock = firstBlock;
			try
			{
				loopDetector = blockStore.GetChainLoopDetector();
			}
			catch (IOException ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool HasNext()
		{
			if (nextBlock == -2)
			{
				return false;
			}
			return true;
		}

		public ByteBuffer Next()
		{
			if (nextBlock != -2)
			{
				try
				{
					loopDetector.Claim(nextBlock);
					ByteBuffer blockAt = blockStore.GetBlockAt(nextBlock);
					nextBlock = blockStore.GetNextBlock(nextBlock);
					return blockAt;
				}
				catch (IOException ex)
				{
					throw new RuntimeException(ex.Message);
				}
			}
			throw new IndexOutOfRangeException("Can't read past the end of the stream");
		}

		public void Remove()
		{
			throw new NotImplementedException("Unsupported Operations!");
		}

		void IEnumerator.Reset()
		{
			throw new NotImplementedException();
		}

		bool IEnumerator.MoveNext()
		{
			if (nextBlock != -2)
			{
				try
				{
					loopDetector.Claim(nextBlock);
					current = blockStore.GetBlockAt(nextBlock);
					nextBlock = blockStore.GetNextBlock(nextBlock);
					return true;
				}
				catch (IOException)
				{
					return false;
				}
			}
			return false;
		}

		public void Dispose()
		{
		}
	}
}
