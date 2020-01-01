namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// This interface defines behaviors for objects managed by the Block
	/// Allocation Table (BAT).
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public interface BATManaged
	{
		/// <summary>
		/// Gets the number of BigBlock's this instance uses
		/// </summary>
		/// <value>count of BigBlock instances</value>
		int CountBlocks
		{
			get;
		}

		/// <summary>
		/// Sets the start block for this instance
		/// </summary>
		/// <value>index into the array of BigBlock instances making up the the filesystem</value>
		int StartBlock
		{
			set;
		}
	}
}
