using System;
using System.IO;

namespace NPOI.POIFS.Storage
{
	public class BlockListImpl : BlockList
	{
		private ListManagedBlock[] _blocks;

		private BlockAllocationTableReader _bat;

		/// <summary>
		/// set the associated BlockAllocationTable
		/// </summary>
		/// <value>the associated BlockAllocationTable</value>
		public virtual BlockAllocationTableReader BAT
		{
			set
			{
				if (_bat != null)
				{
					throw new IOException("Attempt to replace existing BlockAllocationTable");
				}
				_bat = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Storage.BlockListImpl" /> class.
		/// </summary>
		public BlockListImpl()
		{
			_blocks = new ListManagedBlock[0];
			_bat = null;
		}

		/// <summary>
		/// provide blocks to manage
		/// </summary>
		/// <param name="blocks">blocks to be managed</param> 
		public virtual void SetBlocks(ListManagedBlock[] blocks)
		{
			_blocks = blocks;
		}

		/// <summary>
		/// remove the specified block from the list
		/// </summary>
		/// <param name="index">the index of the specified block; if the index is
		/// out of range, that's ok</param>
		public virtual void Zap(int index)
		{
			if (index >= 0 && index < _blocks.Length)
			{
				_blocks[index] = null;
			}
		}

		protected ListManagedBlock Get(int index)
		{
			return _blocks[index];
		}

		/// <summary>
		/// Remove and return the specified block from the list
		/// </summary>
		/// <param name="index">the index of the specified block</param>
		/// <returns>the specified block</returns>
		public virtual ListManagedBlock Remove(int index)
		{
			ListManagedBlock listManagedBlock = null;
			try
			{
				listManagedBlock = _blocks[index];
				if (listManagedBlock == null)
				{
					throw new IOException("block[ " + index + " ] already removed");
				}
				_blocks[index] = null;
				return listManagedBlock;
			}
			catch (IndexOutOfRangeException)
			{
				throw new IOException("Cannot remove block[ " + index + " ]; out of range[ 0 - " + (_blocks.Length - 1) + " ]");
			}
		}

		/// <summary>
		/// get the blocks making up a particular stream in the list. The
		/// blocks are removed from the list.
		/// </summary>
		/// <param name="startBlock">the index of the first block in the stream</param>
		/// <param name="headerPropertiesStartBlock"></param>
		/// <returns>
		/// the stream as an array of correctly ordered blocks
		/// </returns>
		public virtual ListManagedBlock[] FetchBlocks(int startBlock, int headerPropertiesStartBlock)
		{
			if (_bat == null)
			{
				throw new IOException("Improperly initialized list: no block allocation table provided");
			}
			return _bat.FetchBlocks(startBlock, headerPropertiesStartBlock, this);
		}

		public virtual int BlockCount()
		{
			return _blocks.Length;
		}

		protected int RemainingBlocks()
		{
			int num = 0;
			for (int i = 0; i < _blocks.Length; i++)
			{
				if (_blocks[i] != null)
				{
					num++;
				}
			}
			return num;
		}
	}
}
