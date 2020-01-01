namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// An interface for blocks managed by a list that works with a
	/// BlockAllocationTable to keep block sequences straight
	/// @author Marc Johnson (mjohnson at apache dot org
	/// </summary>
	public interface ListManagedBlock
	{
		/// <summary>
		/// Get the data from the block
		/// </summary>
		/// <value>the block's data as a byte array</value>
		byte[] Data
		{
			get;
		}
	}
}
