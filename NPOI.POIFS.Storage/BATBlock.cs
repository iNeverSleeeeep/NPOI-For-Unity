using NPOI.POIFS.Common;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// A block of block allocation table entries. BATBlocks are created
	/// only through a static factory method: createBATBlocks.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class BATBlock : BigBlock
	{
		private static int _entries_per_block = 128;

		private static int _entries_per_xbat_block = _entries_per_block - 1;

		private static int _xbat_chain_offset = _entries_per_xbat_block * 4;

		private static byte _default_value = byte.MaxValue;

		private IntegerField[] _fields;

		private byte[] _data;

		/// For a regular fat block, these are 128 / 1024 
		///  next sector values.
		/// For a XFat (DIFat) block, these are 127 / 1023
		///  next sector values, then a chaining value.
		private int[] _values;

		/// Does this BATBlock have any free sectors in it?
		private bool _has_free_sectors;

		/// Where in the file are we?
		private int ourBlockIndex;

		/// <summary>
		/// Gets the entries per block.
		/// </summary>
		/// <value>The number of entries per block</value>
		public static int EntriesPerBlock => _entries_per_block;

		/// <summary>
		/// Gets the entries per XBAT block.
		/// </summary>
		/// <value>number of entries per XBAT block</value>
		public static int EntriesPerXBATBlock => _entries_per_xbat_block;

		/// <summary>
		/// Gets the XBAT chain offset.
		/// </summary>
		/// <value>offset of chain index of XBAT block</value>
		public static int XBATChainOffset => _xbat_chain_offset;

		/// Does this BATBlock have any free sectors in it, or
		///  is it full?
		public bool HasFreeSectors => _has_free_sectors;

		/// Retrieve where in the file we live 
		public int OurBlockIndex
		{
			get
			{
				return ourBlockIndex;
			}
			set
			{
				ourBlockIndex = value;
			}
		}

		/// <summary>
		/// Create a single instance initialized with default values
		/// </summary>
		protected BATBlock()
		{
			_data = new byte[512];
			for (int i = 0; i < _data.Length; i++)
			{
				_data[i] = _default_value;
			}
			_fields = new IntegerField[_entries_per_block];
			int num = 0;
			for (int j = 0; j < _entries_per_block; j++)
			{
				_fields[j] = new IntegerField(num);
				num += 4;
			}
		}

		protected BATBlock(POIFSBigBlockSize bigBlockSize)
			: base(bigBlockSize)
		{
			int bATEntriesPerBlock = bigBlockSize.GetBATEntriesPerBlock();
			_values = new int[bATEntriesPerBlock];
			_has_free_sectors = true;
			for (int i = 0; i < _values.Length; i++)
			{
				_values[i] = -1;
			}
		}

		/// Create a single instance initialized (perhaps partially) with entries
		///
		/// @param entries the array of block allocation table entries
		/// @param start_index the index of the first entry to be written
		///                    to the block
		/// @param end_index the index, plus one, of the last entry to be
		///                  written to the block (writing is for all index
		///                  k, start_index &lt;= k &lt; end_index)
		protected BATBlock(POIFSBigBlockSize bigBlockSize, int[] entries, int start_index, int end_index)
			: this(bigBlockSize)
		{
			for (int i = start_index; i < end_index; i++)
			{
				_values[i - start_index] = entries[i];
			}
			if (end_index - start_index == _values.Length)
			{
				RecomputeFree();
			}
		}

		private void RecomputeFree()
		{
			bool has_free_sectors = false;
			for (int i = 0; i < _values.Length; i++)
			{
				if (_values[i] == -1)
				{
					has_free_sectors = true;
					break;
				}
			}
			_has_free_sectors = has_free_sectors;
		}

		/// Create a single BATBlock from the byte buffer, which must hold at least
		///  one big block of data to be read.
		public static BATBlock CreateBATBlock(POIFSBigBlockSize bigBlockSize, BinaryReader data)
		{
			BATBlock bATBlock = new BATBlock(bigBlockSize);
			byte[] array = new byte[4];
			for (int i = 0; i < bATBlock._values.Length; i++)
			{
				data.Read(array, 0, array.Length);
				bATBlock._values[i] = LittleEndian.GetInt(array);
			}
			bATBlock.RecomputeFree();
			return bATBlock;
		}

		public static BATBlock CreateBATBlock(POIFSBigBlockSize bigBlockSize, ByteBuffer data)
		{
			BATBlock bATBlock = new BATBlock(bigBlockSize);
			byte[] array = new byte[4];
			for (int i = 0; i < bATBlock._values.Length; i++)
			{
				data.Read(array);
				bATBlock._values[i] = LittleEndian.GetInt(array);
			}
			bATBlock.RecomputeFree();
			return bATBlock;
		}

		/// **
		public static BATBlock CreateEmptyBATBlock(POIFSBigBlockSize bigBlockSize, bool isXBAT)
		{
			BATBlock bATBlock = new BATBlock(bigBlockSize);
			if (isXBAT)
			{
				bATBlock.SetXBATChain(bigBlockSize, -2);
			}
			return bATBlock;
		}

		/// <summary>
		/// Create an array of BATBlocks from an array of int block
		/// allocation table entries
		/// </summary>
		/// <param name="bigBlockSize">the poifs bigBlockSize</param>
		/// <param name="entries">the array of int entries</param>
		/// <returns>the newly created array of BATBlocks</returns>
		public static BATBlock[] CreateBATBlocks(POIFSBigBlockSize bigBlockSize, int[] entries)
		{
			int num = CalculateStorageRequirements(entries.Length);
			BATBlock[] array = new BATBlock[num];
			int num2 = 0;
			int num3 = entries.Length;
			for (int i = 0; i < entries.Length; i += _entries_per_block)
			{
				array[num2++] = new BATBlock(bigBlockSize, entries, i, (num3 > _entries_per_block) ? (i + _entries_per_block) : entries.Length);
				num3 -= _entries_per_block;
			}
			return array;
		}

		/// <summary>
		/// Create an array of XBATBlocks from an array of int block
		/// allocation table entries
		/// </summary>
		/// <param name="bigBlockSize"></param>
		/// <param name="entries">the array of int entries</param>
		/// <param name="startBlock">the start block of the array of XBAT blocks</param>
		/// <returns>the newly created array of BATBlocks</returns>
		public static BATBlock[] CreateXBATBlocks(POIFSBigBlockSize bigBlockSize, int[] entries, int startBlock)
		{
			int num = CalculateXBATStorageRequirements(entries.Length);
			BATBlock[] array = new BATBlock[num];
			int num2 = 0;
			int num3 = entries.Length;
			if (num != 0)
			{
				for (int i = 0; i < entries.Length; i += _entries_per_xbat_block)
				{
					array[num2++] = new BATBlock(bigBlockSize, entries, i, (num3 > _entries_per_xbat_block) ? (i + _entries_per_xbat_block) : entries.Length);
					num3 -= _entries_per_xbat_block;
				}
				for (num2 = 0; num2 < array.Length - 1; num2++)
				{
					array[num2].SetXBATChain(bigBlockSize, startBlock + num2 + 1);
				}
				array[num2].SetXBATChain(bigBlockSize, -2);
			}
			return array;
		}

		/// <summary>
		/// Calculate how many BATBlocks are needed to hold a specified
		/// number of BAT entries.
		/// </summary>
		/// <param name="entryCount">the number of entries</param>
		/// <returns>the number of BATBlocks needed</returns>
		public static int CalculateStorageRequirements(int entryCount)
		{
			return (entryCount + _entries_per_block - 1) / _entries_per_block;
		}

		public static int CalculateStorageRequirements(POIFSBigBlockSize bigBlockSize, int entryCount)
		{
			int bATEntriesPerBlock = bigBlockSize.GetBATEntriesPerBlock();
			return (entryCount + bATEntriesPerBlock - 1) / bATEntriesPerBlock;
		}

		/// <summary>
		/// Calculate how many XBATBlocks are needed to hold a specified
		/// number of BAT entries.
		/// </summary>
		/// <param name="entryCount">the number of entries</param>
		/// <returns>the number of XBATBlocks needed</returns>
		public static int CalculateXBATStorageRequirements(int entryCount)
		{
			return (entryCount + _entries_per_xbat_block - 1) / _entries_per_xbat_block;
		}

		public static int CalculateXBATStorageRequirements(POIFSBigBlockSize bigBlockSize, int entryCount)
		{
			int xBATEntriesPerBlock = bigBlockSize.GetXBATEntriesPerBlock();
			return (entryCount + xBATEntriesPerBlock - 1) / xBATEntriesPerBlock;
		}

		/// Calculates the maximum size of a file which is addressable given the
		///  number of FAT (BAT) sectors specified. (We don't care if those BAT
		///  blocks come from the 109 in the header, or from header + XBATS, it
		///  won't affect the calculation)
		///
		/// The actual file size will be between [size of fatCount-1 blocks] and
		///   [size of fatCount blocks].
		///  For 512 byte block sizes, this means we may over-estimate by up to 65kb.
		///  For 4096 byte block sizes, this means we may over-estimate by up to 4mb
		public static int CalculateMaximumSize(POIFSBigBlockSize bigBlockSize, int numBATs)
		{
			int num = 1;
			num += numBATs * bigBlockSize.GetBATEntriesPerBlock();
			return num * bigBlockSize.GetBigBlockSize();
		}

		public static int CalculateMaximumSize(HeaderBlock header)
		{
			return CalculateMaximumSize(header.BigBlockSize, header.BATCount);
		}

		public static BATBlockAndIndex GetBATBlockAndIndex(int offset, HeaderBlock header, List<BATBlock> bats)
		{
			POIFSBigBlockSize bigBlockSize = header.BigBlockSize;
			int index = (int)Math.Floor(1.0 * (double)offset / (double)bigBlockSize.GetBATEntriesPerBlock());
			int index2 = offset % bigBlockSize.GetBATEntriesPerBlock();
			return new BATBlockAndIndex(index2, bats[index]);
		}

		public static BATBlockAndIndex GetSBATBlockAndIndex(int offset, HeaderBlock header, List<BATBlock> sbats)
		{
			POIFSBigBlockSize bigBlockSize = header.BigBlockSize;
			int index = (int)Math.Floor(1.0 * (double)offset / (double)bigBlockSize.GetBATEntriesPerBlock());
			int index2 = offset % bigBlockSize.GetBATEntriesPerBlock();
			return new BATBlockAndIndex(index2, sbats[index]);
		}

		private void SetXBATChain(int chainIndex)
		{
			_fields[_entries_per_xbat_block].Set(chainIndex, _data);
		}

		private void SetXBATChain(POIFSBigBlockSize bigBlockSize, int chainIndex)
		{
			int xBATEntriesPerBlock = bigBlockSize.GetXBATEntriesPerBlock();
			_values[xBATEntriesPerBlock] = chainIndex;
		}

		public int GetValueAt(int relativeOffset)
		{
			if (relativeOffset >= _values.Length)
			{
				throw new IndexOutOfRangeException("Unable to fetch offset " + relativeOffset + " as the BAT only contains " + _values.Length + " entries");
			}
			return _values[relativeOffset];
		}

		public void SetValueAt(int relativeOffset, int value)
		{
			int num = _values[relativeOffset];
			_values[relativeOffset] = value;
			if (value == -1)
			{
				_has_free_sectors = true;
			}
			else if (num == -1)
			{
				RecomputeFree();
			}
		}

		/// <summary>
		/// Create a single instance initialized (perhaps partially) with entries
		/// </summary>
		/// <param name="entries">the array of block allocation table entries</param>
		/// <param name="start_index">the index of the first entry to be written
		/// to the block</param>
		/// <param name="end_index">the index, plus one, of the last entry to be
		/// written to the block (writing is for all index
		/// k, start_index less than k less than end_index)
		/// </param>
		private BATBlock(int[] entries, int start_index, int end_index)
			: this()
		{
			for (int i = start_index; i < end_index; i++)
			{
				_fields[i - start_index].Set(entries[i], _data);
			}
		}

		public void WriteData(ByteBuffer block)
		{
			block.Write(Serialize());
		}

		/// <summary>
		/// Write the block's data to an Stream
		/// </summary>
		/// <param name="stream">the Stream to which the stored data should
		/// be written</param>
		public override void WriteData(Stream stream)
		{
			byte[] array = Serialize();
			stream.Write(array, 0, array.Length);
		}

		public void WriteData(byte[] block)
		{
			byte[] array = Serialize();
			for (int i = 0; i < array.Length; i++)
			{
				block[i] = array[i];
			}
		}

		private byte[] Serialize()
		{
			byte[] array = new byte[bigBlockSize.GetBigBlockSize()];
			int num = 0;
			for (int i = 0; i < _values.Length; i++)
			{
				LittleEndian.PutInt(array, num, _values[i]);
				num += 4;
			}
			return array;
		}
	}
}
