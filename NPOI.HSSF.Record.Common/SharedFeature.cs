using NPOI.Util;

namespace NPOI.HSSF.Record.Common
{
	/// Common Interface for all Shared Features
	public interface SharedFeature
	{
		int DataSize
		{
			get;
		}

		new string ToString();

		void Serialize(ILittleEndianOutput out1);
	}
}
