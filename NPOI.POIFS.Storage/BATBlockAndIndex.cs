namespace NPOI.POIFS.Storage
{
	public class BATBlockAndIndex
	{
		private int index;

		private BATBlock block;

		public int Index => index;

		public BATBlock Block => block;

		public BATBlockAndIndex(int index, BATBlock block)
		{
			this.index = index;
			this.block = block;
		}
	}
}
