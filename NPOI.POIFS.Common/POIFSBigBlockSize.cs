namespace NPOI.POIFS.Common
{
	/// <p>A class describing attributes of the Big Block Size</p>
	public class POIFSBigBlockSize
	{
		private int bigBlockSize;

		private short headerValue;

		internal POIFSBigBlockSize(int bigBlockSize, short headerValue)
		{
			this.bigBlockSize = bigBlockSize;
			this.headerValue = headerValue;
		}

		public int GetBigBlockSize()
		{
			return bigBlockSize;
		}

		/// Returns the value that Gets written into the 
		///  header.
		/// Is the power of two that corresponds to the
		///  size of the block, eg 512 =&gt; 9
		public short GetHeaderValue()
		{
			return headerValue;
		}

		public int GetPropertiesPerBlock()
		{
			return bigBlockSize / 128;
		}

		public int GetBATEntriesPerBlock()
		{
			return bigBlockSize / 4;
		}

		public int GetXBATEntriesPerBlock()
		{
			return GetBATEntriesPerBlock() - 1;
		}

		public int GetNextXBATChainOffset()
		{
			return GetXBATEntriesPerBlock() * 4;
		}
	}
}
