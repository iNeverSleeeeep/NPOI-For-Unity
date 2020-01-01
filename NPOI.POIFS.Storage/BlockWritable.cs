using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// An interface for persisting block storage of POIFS components.
	///  @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public interface BlockWritable
	{
		/// <summary>
		/// Writes the blocks.
		/// </summary>
		/// <param name="stream">The stream.</param>
		void WriteBlocks(Stream stream);
	}
}
