using NPOI.POIFS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// Storage for documents that are too small to use regular
	/// DocumentBlocks for their data
	/// @author  Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class SmallDocumentBlock : BlockWritable, ListManagedBlock
	{
		private const int BLOCK_SHIFT = 6;

		private const byte _default_fill = byte.MaxValue;

		private const int _block_size = 64;

		private const int BLOCK_MASK = 63;

		private byte[] _data;

		private static int _blocks_per_big_block = 8;

		private POIFSBigBlockSize _bigBlockSize;

		/// <summary>
		/// Get the data from the block
		/// </summary>
		/// <value>the block's data as a byte array</value>
		public byte[] Data => _data;

		public POIFSBigBlockSize BigBlockSize => _bigBlockSize;

		public SmallDocumentBlock(POIFSBigBlockSize bigBlockSize, byte[] data, int index)
		{
			_bigBlockSize = bigBlockSize;
			_blocks_per_big_block = GetBlocksPerBigBlock(bigBlockSize);
			_data = new byte[64];
			Array.Copy(data, index * 64, _data, 0, 64);
		}

		public SmallDocumentBlock(POIFSBigBlockSize bigBlockSize)
		{
			_bigBlockSize = bigBlockSize;
			_blocks_per_big_block = GetBlocksPerBigBlock(bigBlockSize);
			_data = new byte[64];
		}

		private static int GetBlocksPerBigBlock(POIFSBigBlockSize bigBlockSize)
		{
			return bigBlockSize.GetBigBlockSize() / 64;
		}

		/// <summary>
		/// convert a single long array into an array of SmallDocumentBlock
		/// instances
		/// </summary>
		/// <param name="bigBlockSize">the poifs bigBlockSize</param>
		/// <param name="array">the byte array to be converted</param>
		/// <param name="size">the intended size of the array (which may be smaller)</param>
		/// <returns>an array of SmallDocumentBlock instances, filled from
		/// the array</returns>
		public static SmallDocumentBlock[] Convert(POIFSBigBlockSize bigBlockSize, byte[] array, int size)
		{
			SmallDocumentBlock[] array2 = new SmallDocumentBlock[(size + 64 - 1) / 64];
			int num = 0;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new SmallDocumentBlock(bigBlockSize);
				if (num < array.Length)
				{
					int num2 = Math.Min(64, array.Length - num);
					Array.Copy(array, num, array2[i]._data, 0, num2);
					if (num2 != 64)
					{
						for (int j = num2; j < 64; j++)
						{
							array2[i]._data[j] = byte.MaxValue;
						}
					}
				}
				else
				{
					for (int k = 0; k < array2[i]._data.Length; k++)
					{
						array2[i]._data[k] = byte.MaxValue;
					}
				}
				num += 64;
			}
			return array2;
		}

		/// <summary>
		/// fill out a List of SmallDocumentBlocks so that it fully occupies
		/// a Set of big blocks
		/// </summary>
		/// <param name="bigBlockSize"></param>
		/// <param name="blocks">the List to be filled out.</param>
		/// <returns>number of big blocks the list encompasses</returns>
		public static int Fill(POIFSBigBlockSize bigBlockSize, IList blocks)
		{
			int blocksPerBigBlock = GetBlocksPerBigBlock(bigBlockSize);
			int i = blocks.Count;
			int num = (i + blocksPerBigBlock - 1) / blocksPerBigBlock;
			for (int num2 = num * blocksPerBigBlock; i < num2; i++)
			{
				blocks.Add(MakeEmptySmallDocumentBlock(bigBlockSize));
			}
			return num;
		}

		/// <summary>
		/// Factory for creating SmallDocumentBlocks from DocumentBlocks
		/// </summary>
		/// <param name="bigBlocksSize"></param>
		/// <param name="store">the original DocumentBlocks</param>
		/// <param name="size">the total document size</param>
		/// <returns>an array of new SmallDocumentBlocks instances</returns>
		public static SmallDocumentBlock[] Convert(POIFSBigBlockSize bigBlocksSize, BlockWritable[] store, int size)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				for (int i = 0; i < store.Length; i++)
				{
					store[i].WriteBlocks(memoryStream);
				}
				byte[] data = memoryStream.ToArray();
				SmallDocumentBlock[] array = new SmallDocumentBlock[ConvertToBlockCount(size)];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = new SmallDocumentBlock(bigBlocksSize, data, j);
				}
				return array;
			}
		}

		/// <summary>
		/// create a list of SmallDocumentBlock's from raw data
		/// </summary>
		/// <param name="bigBlockSize"></param>
		/// <param name="blocks">the raw data containing the SmallDocumentBlock</param>
		/// <returns>a List of SmallDocumentBlock's extracted from the input</returns>
		public static List<SmallDocumentBlock> Extract(POIFSBigBlockSize bigBlockSize, ListManagedBlock[] blocks)
		{
			int blocksPerBigBlock = GetBlocksPerBigBlock(bigBlockSize);
			List<SmallDocumentBlock> list = new List<SmallDocumentBlock>();
			for (int i = 0; i < blocks.Length; i++)
			{
				byte[] data = blocks[i].Data;
				for (int j = 0; j < blocksPerBigBlock; j++)
				{
					list.Add(new SmallDocumentBlock(bigBlockSize, data, j));
				}
			}
			return list;
		}

		/// <summary>
		/// Read data from an array of SmallDocumentBlocks
		/// </summary>
		/// <param name="blocks">the blocks to Read from.</param>
		/// <param name="buffer">the buffer to Write the data into.</param>
		/// <param name="offset">the offset into the array of blocks to Read from</param>
		public static void Read(BlockWritable[] blocks, byte[] buffer, int offset)
		{
			int num = offset / 64;
			int num2 = offset % 64;
			int num3 = (offset + buffer.Length - 1) / 64;
			if (num == num3)
			{
				Array.Copy(((SmallDocumentBlock)blocks[num])._data, num2, buffer, 0, buffer.Length);
			}
			else
			{
				int num4 = 0;
				Array.Copy(((SmallDocumentBlock)blocks[num])._data, num2, buffer, num4, 64 - num2);
				num4 += 64 - num2;
				for (int i = num + 1; i < num3; i++)
				{
					Array.Copy(((SmallDocumentBlock)blocks[i])._data, 0, buffer, num4, 64);
					num4 += 64;
				}
				Array.Copy(((SmallDocumentBlock)blocks[num3])._data, 0, buffer, num4, buffer.Length - num4);
			}
		}

		public static DataInputBlock GetDataInputBlock(SmallDocumentBlock[] blocks, int offset)
		{
			int num = offset >> 6;
			int startOffset = offset & 0x3F;
			return new DataInputBlock(blocks[num]._data, startOffset);
		}

		/// <summary>
		/// Calculate the storage size of a Set of SmallDocumentBlocks
		/// </summary>
		/// <param name="size"> number of SmallDocumentBlocks</param>
		/// <returns>total size</returns>
		public static int CalcSize(int size)
		{
			return size * 64;
		}

		/// <summary>
		/// Makes the empty small document block.
		/// </summary>
		/// <returns></returns>
		private static SmallDocumentBlock MakeEmptySmallDocumentBlock(POIFSBigBlockSize bigBlockSize)
		{
			SmallDocumentBlock smallDocumentBlock = new SmallDocumentBlock(bigBlockSize);
			for (int i = 0; i < smallDocumentBlock._data.Length; i++)
			{
				smallDocumentBlock._data[i] = byte.MaxValue;
			}
			return smallDocumentBlock;
		}

		/// <summary>
		/// Converts to block count.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		private static int ConvertToBlockCount(int size)
		{
			return (size + 64 - 1) / 64;
		}

		/// <summary>
		/// Write the storage to an OutputStream
		/// </summary>
		/// <param name="stream">the OutputStream to which the stored data should
		/// be written</param>
		public void WriteBlocks(Stream stream)
		{
			stream.Write(_data, 0, _data.Length);
		}
	}
}
