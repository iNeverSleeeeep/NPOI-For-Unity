using NPOI.POIFS.Common;
using NPOI.POIFS.Properties;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// This class implements reading the small document block list from an
	/// existing file
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class SmallBlockTableReader
	{
		/// <summary>
		/// fetch the small document block list from an existing file
		/// </summary>
		/// <param name="bigBlockSize">the poifs bigBlockSize</param>
		/// <param name="blockList">the raw data from which the small block table will be extracted</param>
		/// <param name="root">the root property (which contains the start block and small block table size)</param>
		/// <param name="sbatStart">the start block of the SBAT</param>
		/// <returns>the small document block list</returns>
		public static BlockList GetSmallDocumentBlocks(POIFSBigBlockSize bigBlockSize, RawDataBlockList blockList, RootProperty root, int sbatStart)
		{
			BlockList blockList2 = new SmallDocumentBlockList(SmallDocumentBlock.Extract(bigBlockSize, blockList.FetchBlocks(root.StartBlock, -1)));
			new BlockAllocationTableReader(bigBlockSize, blockList.FetchBlocks(sbatStart, -1), blockList2);
			return blockList2;
		}
	}
}
