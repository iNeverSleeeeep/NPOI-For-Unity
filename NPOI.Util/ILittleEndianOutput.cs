namespace NPOI.Util
{
	/// @author Josh Micich
	public interface ILittleEndianOutput
	{
		void WriteByte(int v);

		void WriteShort(int v);

		void WriteInt(int v);

		void WriteLong(long v);

		void WriteDouble(double v);

		void Write(byte[] b);

		void Write(byte[] b, int offset, int len);
	}
}
