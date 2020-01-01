namespace NPOI.DDF
{
	public class NullEscherSerializationListener : EscherSerializationListener
	{
		public void BeforeRecordSerialize(int offset, short recordId, EscherRecord record)
		{
		}

		public void AfterRecordSerialize(int offset, short recordId, int size, EscherRecord record)
		{
		}
	}
}
