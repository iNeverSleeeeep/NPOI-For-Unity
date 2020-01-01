namespace NPOI.SS.UserModel
{
	/// Specifies printed page order.
	///
	/// @author Gisella Bronzetti
	public class PageOrder
	{
		/// Order pages vertically first, then move horizontally.
		public static PageOrder DOWN_THEN_OVER;

		/// Order pages horizontally first, then move vertically
		public static PageOrder OVER_THEN_DOWN;

		private int order;

		private static PageOrder[] _table;

		public int Value => order;

		static PageOrder()
		{
			_table = new PageOrder[3];
			DOWN_THEN_OVER = new PageOrder(1);
			OVER_THEN_DOWN = new PageOrder(2);
		}

		private PageOrder(int order)
		{
			this.order = order;
			_table[order] = this;
		}

		public static PageOrder ValueOf(int value)
		{
			return _table[value];
		}
	}
}
