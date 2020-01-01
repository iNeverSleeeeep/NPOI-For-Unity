using NPOI.POIFS.Common;
using NPOI.Util;
using System;
using System.IO;

namespace NPOI.POIFS.Storage
{
	public class DocumentBlock : BigBlock
	{
		private static byte _default_value = byte.MaxValue;

		private byte[] _data;

		private int _bytes_Read;

		/// <summary>
		/// Get the number of bytes Read for this block.
		/// </summary>
		/// <value>bytes Read into the block</value>
		public int Size => _bytes_Read;

		/// <summary>
		/// Was this a partially Read block?
		/// </summary>
		/// <value><c>true</c> if the block was only partially filled with data</value>
		public bool PartiallyRead => _bytes_Read != 512;

		/// <summary>
		/// Gets the fill byte used
		/// </summary>
		/// <value>The fill byte.</value>
		public static byte FillByte => _default_value;

		/// <summary>
		/// create a document block from a raw data block
		/// </summary>
		/// <param name="block">The block.</param>
		public DocumentBlock(RawDataBlock block)
			: base((block.BigBlockSize == 512) ? POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS : POIFSConstants.LARGER_BIG_BLOCK_SIZE_DETAILS)
		{
			_data = block.Data;
			_bytes_Read = _data.Length;
		}

		/// <summary>
		/// Create a single instance initialized with data.
		/// </summary>
		/// <param name="stream">the InputStream delivering the data.</param>
		/// <param name="bigBlockSize">the poifs bigBlockSize</param>
		public DocumentBlock(Stream stream, POIFSBigBlockSize bigBlockSize)
			: this(bigBlockSize)
		{
			int num = IOUtils.ReadFully(stream, _data);
			_bytes_Read = ((num != -1) ? num : 0);
		}

		public DocumentBlock(POIFSBigBlockSize bigBlockSize)
			: base(bigBlockSize)
		{
			_data = new byte[512];
			Arrays.Fill(_data, _default_value);
		}

		/// <summary>
		/// convert a single long array into an array of DocumentBlock
		/// instances
		/// </summary>
		/// <param name="bigBlockSize">the poifs bigBlockSize</param>
		/// <param name="array">the byte array to be converted</param>
		/// <param name="size">the intended size of the array (which may be smaller)</param>
		/// <returns>an array of DocumentBlock instances, filled from the
		/// input array</returns>
		public static DocumentBlock[] Convert(POIFSBigBlockSize bigBlockSize, byte[] array, int size)
		{
			DocumentBlock[] array2 = new DocumentBlock[(size + 512 - 1) / 512];
			int num = 0;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new DocumentBlock(bigBlockSize);
				if (num < array.Length)
				{
					int num2 = Math.Min(512, array.Length - num);
					Array.Copy(array, num, array2[i]._data, 0, num2);
					if (num2 != 512)
					{
						for (int j = (num2 > 0) ? (num2 - 1) : num2; j < 512; j++)
						{
							array2[i]._data[j] = _default_value;
						}
					}
				}
				else
				{
					for (int k = 0; k < array2[i]._data.Length; k++)
					{
						array2[i]._data[k] = _default_value;
					}
				}
				num += 512;
			}
			return array2;
		}

		/// <summary>
		/// Read data from an array of DocumentBlocks
		/// </summary>
		/// <param name="blocks">the blocks to Read from</param>
		/// <param name="buffer">the buffer to Write the data into</param>
		/// <param name="offset">the offset into the array of blocks to Read from</param>
		public static void Read(DocumentBlock[] blocks, byte[] buffer, int offset)
		{
			int num = offset / 512;
			int num2 = offset % 512;
			int num3 = (offset + buffer.Length - 1) / 512;
			if (num == num3)
			{
				Array.Copy(blocks[num]._data, num2, buffer, 0, buffer.Length);
			}
			else
			{
				int num4 = 0;
				Array.Copy(blocks[num]._data, num2, buffer, num4, 512 - num2);
				num4 += 512 - num2;
				for (int i = num + 1; i < num3; i++)
				{
					Array.Copy(blocks[i]._data, 0, buffer, num4, 512);
					num4 += 512;
				}
				Array.Copy(blocks[num3]._data, 0, buffer, num4, buffer.Length - num4);
			}
		}

		public static DataInputBlock GetDataInputBlock(DocumentBlock[] blocks, int offset)
		{
			if (blocks == null || blocks.Length == 0)
			{
				return null;
			}
			POIFSBigBlockSize bigBlockSize = blocks[0].bigBlockSize;
			int headerValue = bigBlockSize.GetHeaderValue();
			int bigBlockSize2 = bigBlockSize.GetBigBlockSize();
			int num = bigBlockSize2 - 1;
			int num2 = offset >> headerValue;
			int startOffset = offset & num;
			return new DataInputBlock(blocks[num2]._data, startOffset);
		}

		/// <summary>
		/// Write the storage to an OutputStream
		/// </summary>
		/// <param name="stream">the OutputStream to which the stored data should
		/// be written</param>
		public override void WriteData(Stream stream)
		{
			WriteData(stream, _data);
		}
	}
}
