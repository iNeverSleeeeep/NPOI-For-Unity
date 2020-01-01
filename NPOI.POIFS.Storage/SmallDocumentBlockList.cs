using System.Collections.Generic;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// A list of SmallDocumentBlocks instances, and methods to manage the list
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class SmallDocumentBlockList : BlockListImpl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Storage.SmallDocumentBlockList" /> class.
		/// </summary>
		/// <param name="blocks">a list of SmallDocumentBlock instances</param>
		public SmallDocumentBlockList(List<SmallDocumentBlock> blocks)
		{
			SetBlocks((ListManagedBlock[])blocks.ToArray());
		}
	}
}
