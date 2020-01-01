namespace NPOI.POIFS.Common
{
	/// <summary>
	/// A repository for constants shared by POI classes.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class POIFSConstants
	{
		/// Most files use 512 bytes as their big block size 
		public const int SMALLER_BIG_BLOCK_SIZE = 512;

		/// Some use 4096 bytes 
		public const int LARGER_BIG_BLOCK_SIZE = 4096;

		/// Most files use 512 bytes as their big block size 
		public const int BIG_BLOCK_SIZE = 512;

		/// Most files use 512 bytes as their big block size 
		public const int MINI_BLOCK_SIZE = 64;

		/// How big a block in the small block stream is. Fixed size 
		public const int SMALL_BLOCK_SIZE = 64;

		/// How big a single property is 
		public const int PROPERTY_SIZE = 128;

		/// The minimum size of a document before it's stored using 
		///  Big Blocks (normal streams). Smaller documents go in the 
		///  Mini Stream (SBAT / Small Blocks)
		public const int BIG_BLOCK_MINIMUM_DOCUMENT_SIZE = 4096;

		/// The highest sector number you're allowed, 0xFFFFFFFA 
		public const int LARGEST_REGULAR_SECTOR_NUMBER = -5;

		/// Indicates the sector holds a FAT block (0xFFFFFFFD) 
		public const int FAT_SECTOR_BLOCK = -3;

		/// Indicates the sector holds a DIFAT block (0xFFFFFFFC) 
		public const int DIFAT_SECTOR_BLOCK = -4;

		/// Indicates the sector is the end of a chain (0xFFFFFFFE) 
		public const int END_OF_CHAIN = -2;

		/// Indicates the sector is not used (0xFFFFFFFF) 
		public const int UNUSED_BLOCK = -1;

		public static readonly POIFSBigBlockSize SMALLER_BIG_BLOCK_SIZE_DETAILS = new POIFSBigBlockSize(512, 9);

		public static readonly POIFSBigBlockSize LARGER_BIG_BLOCK_SIZE_DETAILS = new POIFSBigBlockSize(4096, 12);

		/// The first 4 bytes of an OOXML file, used in detection 
		public static readonly byte[] OOXML_FILE_HEADER = new byte[4]
		{
			80,
			75,
			3,
			4
		};
	}
}
