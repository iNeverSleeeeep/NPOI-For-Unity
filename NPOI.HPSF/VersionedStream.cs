namespace NPOI.HPSF
{
	public class VersionedStream
	{
		private GUID _versionGuid;

		private IndirectPropertyName _streamName;

		public int Size => 16 + _streamName.Size;

		public VersionedStream(byte[] data, int offset)
		{
			_versionGuid = new GUID(data, offset);
			_streamName = new IndirectPropertyName(data, offset + 16);
		}
	}
}
