using NPOI.POIFS.Common;
using NPOI.Util;
using System;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// The block containing the archive header
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class HeaderBlockWriter : HeaderBlockConstants, BlockWritable
	{
		private HeaderBlock _header_block;

		/// <summary>
		/// Set start of Property Table
		/// </summary>
		/// <value>the index of the first block of the Property
		/// Table</value>
		public int PropertyStart
		{
			get
			{
				return _header_block.PropertyStart;
			}
			set
			{
				_header_block.PropertyStart = value;
			}
		}

		/// <summary>
		/// Set start of small block allocation table
		/// </summary>
		/// <value>the index of the first big block of the small
		/// block allocation table</value>
		public int SBAStart
		{
			get
			{
				return _header_block.SBATStart;
			}
			set
			{
				_header_block.SBATStart = value;
			}
		}

		public int SBATStart
		{
			get
			{
				return _header_block.SBATStart;
			}
			set
			{
				_header_block.SBATStart = value;
			}
		}

		/// <summary>
		/// Set count of SBAT blocks
		/// </summary>
		/// <value>the number of SBAT blocks</value>
		public int SBATBlockCount
		{
			get
			{
				return _header_block.SBATBlockCount;
			}
			set
			{
				_header_block.SBATBlockCount = value;
			}
		}

		public HeaderBlockWriter(POIFSBigBlockSize bigBlockSize)
		{
			_header_block = new HeaderBlock(bigBlockSize);
		}

		public HeaderBlockWriter(HeaderBlock headerBlock)
		{
			_header_block = headerBlock;
		}

		/// <summary>
		/// Set BAT block parameters. Assumes that all BAT blocks are
		/// contiguous. Will construct XBAT blocks if necessary and return
		/// the array of newly constructed XBAT blocks.
		/// </summary>
		/// <param name="blockCount">count of BAT blocks</param>
		/// <param name="startBlock">index of first BAT block</param>
		/// <returns>array of XBAT blocks; may be zero Length, will not be
		/// null</returns>
		public BATBlock[] SetBATBlocks(int blockCount, int startBlock)
		{
			POIFSBigBlockSize bigBlockSize = _header_block.BigBlockSize;
			_header_block.BATCount = blockCount;
			int num = Math.Min(blockCount, 109);
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = startBlock + i;
			}
			_header_block.BATArray = array;
			BATBlock[] array3;
			if (blockCount > 109)
			{
				int num2 = blockCount - 109;
				int[] array2 = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array2[j] = startBlock + j + 109;
				}
				array3 = BATBlock.CreateXBATBlocks(bigBlockSize, array2, startBlock + blockCount);
				_header_block.XBATStart = startBlock + blockCount;
			}
			else
			{
				array3 = BATBlock.CreateXBATBlocks(bigBlockSize, new int[0], 0);
				_header_block.XBATStart = -2;
			}
			_header_block.XBATCount = array3.Length;
			return array3;
		}

		/// <summary>
		/// For a given number of BAT blocks, calculate how many XBAT
		/// blocks will be needed
		/// </summary>
		/// <param name="bigBlockSize"></param>
		/// <param name="blockCount">number of BAT blocks</param>
		/// <returns>number of XBAT blocks needed</returns>
		public static int CalculateXBATStorageRequirements(POIFSBigBlockSize bigBlockSize, int blockCount)
		{
			if (blockCount <= 109)
			{
				return 0;
			}
			return BATBlock.CalculateXBATStorageRequirements(bigBlockSize, blockCount - 109);
		}

		/// <summary>
		/// Write the block's data to an Stream
		/// </summary>
		/// <param name="stream">the Stream to which the stored data should
		/// be written
		/// </param>
		public void WriteBlocks(Stream stream)
		{
			try
			{
				_header_block.WriteData(stream);
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		public void WriteBlock(ByteBuffer block)
		{
			MemoryStream memoryStream = new MemoryStream(_header_block.BigBlockSize.GetBigBlockSize());
			_header_block.WriteData(memoryStream);
			block.Write(memoryStream.ToArray());
		}

		public void WriteBlock(byte[] block)
		{
			MemoryStream memoryStream = new MemoryStream(_header_block.BigBlockSize.GetBigBlockSize());
			_header_block.WriteData(memoryStream);
			byte[] array = memoryStream.ToArray();
			Array.Copy(array, 0, block, 0, array.Length);
		}
	}
}
