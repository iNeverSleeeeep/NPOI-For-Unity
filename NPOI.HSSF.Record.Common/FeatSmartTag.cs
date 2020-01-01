using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Common
{
	/// Title: FeatSmartTag (Smart Tag Shared Feature) common record part
	///
	/// This record part specifies Smart Tag data for a sheet, stored as part
	///  of a Shared Feature. It can be found in records such as  {@link FeatRecord}.
	/// It is made up of a hash, and a Set of Factoid Data that Makes up
	///  the smart tags.
	/// For more details, see page 669 of the Excel binary file
	///  format documentation.
	public class FeatSmartTag : SharedFeature
	{
		private byte[] data;

		public int DataSize => data.Length;

		public FeatSmartTag()
		{
			data = new byte[0];
		}

		public FeatSmartTag(RecordInputStream in1)
		{
			data = in1.ReadRemainder();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" [FEATURE SMART TAGS]\n");
			stringBuilder.Append(" [/FEATURE SMART TAGS]\n");
			return stringBuilder.ToString();
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.Write(data);
		}
	}
}
