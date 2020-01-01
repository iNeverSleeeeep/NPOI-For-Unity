using NPOI.POIFS.Common;
using NPOI.POIFS.FileSystem;
using NPOI.POIFS.Properties;
using System.Collections;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// This class implements reading the small document block list from an
	/// existing file
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class SmallBlockTableWriter : BlockWritable, BATManaged
	{
		private BlockAllocationTableWriter _sbat;

		private IList _small_blocks;

		private int _big_block_count;

		private RootProperty _root;

		/// <summary>
		/// Get the number of SBAT blocks
		/// </summary>
		/// <value>number of SBAT big blocks</value>
		public int SBATBlockCount => (_big_block_count + 15) / 16;

		/// <summary>
		/// Gets the SBAT.
		/// </summary>
		/// <value>the Small Block Allocation Table</value>
		public BlockAllocationTableWriter SBAT => _sbat;

		/// <summary>
		/// Return the number of BigBlock's this instance uses
		/// </summary>
		/// <value>count of BigBlock instances</value>
		public int CountBlocks => _big_block_count;

		/// <summary>
		/// Sets the start block.
		/// </summary>
		/// <value>The start block.</value>
		public int StartBlock
		{
			set
			{
				_root.StartBlock = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Storage.SmallBlockTableWriter" /> class.
		/// </summary>
		/// <param name="bigBlockSize">the poifs bigBlockSize</param>
		/// <param name="documents">a IList of POIFSDocument instances</param>
		/// <param name="root">the Filesystem's root property</param>
		public SmallBlockTableWriter(POIFSBigBlockSize bigBlockSize, IList documents, RootProperty root)
		{
			_sbat = new BlockAllocationTableWriter(bigBlockSize);
			_small_blocks = new ArrayList();
			_root = root;
			IEnumerator enumerator = documents.GetEnumerator();
			while (enumerator.MoveNext())
			{
				POIFSDocument pOIFSDocument = (POIFSDocument)enumerator.Current;
				BlockWritable[] smallBlocks = pOIFSDocument.SmallBlocks;
				if (smallBlocks.Length != 0)
				{
					pOIFSDocument.StartBlock = _sbat.AllocateSpace(smallBlocks.Length);
					for (int i = 0; i < smallBlocks.Length; i++)
					{
						_small_blocks.Add(smallBlocks[i]);
					}
				}
				else
				{
					pOIFSDocument.StartBlock = -2;
				}
			}
			_sbat.SimpleCreateBlocks();
			_root.Size = _small_blocks.Count;
			_big_block_count = SmallDocumentBlock.Fill(bigBlockSize, _small_blocks);
		}

		/// <summary>
		/// Write the storage to an OutputStream
		/// </summary>
		/// <param name="stream">the OutputStream to which the stored data should be written</param>
		public void WriteBlocks(Stream stream)
		{
			IEnumerator enumerator = _small_blocks.GetEnumerator();
			while (enumerator.MoveNext())
			{
				((BlockWritable)enumerator.Current).WriteBlocks(stream);
			}
		}
	}
}
