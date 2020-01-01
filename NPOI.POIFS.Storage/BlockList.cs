namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// Interface for lists of blocks that are mapped by block allocation
	/// tables
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public interface BlockList
	{
		/// <summary>
		/// set the associated BlockAllocationTable
		/// </summary>
		/// <value>the associated BlockAllocationTable</value>
		BlockAllocationTableReader BAT
		{
			set;
		}

		/// <summary>
		/// remove the specified block from the list
		/// </summary>
		/// <param name="index">the index of the specified block; if the index is
		/// out of range, that's ok</param>
		void Zap(int index);

		/// <summary>
		/// Remove and return the specified block from the list
		/// </summary>
		/// <param name="index">the index of the specified block</param>
		/// <returns>the specified block</returns>
		ListManagedBlock Remove(int index);

		/// <summary>
		/// get the blocks making up a particular stream in the list. The
		/// blocks are removed from the list.
		/// </summary>
		/// <param name="startBlock">the index of the first block in the stream</param>
		/// <param name="headerPropertiesStartBlock"></param>
		/// <returns>the stream as an array of correctly ordered blocks</returns>
		ListManagedBlock[] FetchBlocks(int startBlock, int headerPropertiesStartBlock);

		int BlockCount();
	}
}
