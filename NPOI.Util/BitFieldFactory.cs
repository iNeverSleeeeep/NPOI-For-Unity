using System.Collections;

namespace NPOI.Util
{
	/// <summary>
	/// Returns immutable Btfield instances.
	/// @author Jason Height (jheight at apache dot org)
	/// </summary>
	public class BitFieldFactory
	{
		private static Hashtable instances = new Hashtable();

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <param name="mask">The mask.</param>
		/// <returns></returns>
		public static BitField GetInstance(int mask)
		{
			BitField bitField = (BitField)instances[mask];
			if (bitField == null)
			{
				bitField = new BitField(mask);
				instances[mask] = bitField;
			}
			return bitField;
		}
	}
}
