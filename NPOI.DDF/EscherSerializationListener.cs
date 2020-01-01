namespace NPOI.DDF
{
	/// Interface for listening to escher serialization events.
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	public interface EscherSerializationListener
	{
		/// Fired before a given escher record is Serialized.
		///
		/// @param offset    The position in the data array at which the record will be Serialized.
		/// @param recordId  The id of the record about to be Serialized.
		void BeforeRecordSerialize(int offset, short recordId, EscherRecord record);

		/// Fired after a record has been Serialized.
		///
		/// @param offset    The position of the end of the Serialized record + 1
		/// @param recordId  The id of the record about to be Serialized
		/// @param size      The number of bytes written for this record.  If it is a container
		///                  record then this will include the size of any included records.
		void AfterRecordSerialize(int offset, short recordId, int size, EscherRecord record);
	}
}
