namespace NPOI.DDF
{
	/// <summary>
	/// The escher record factory interface allows for the creation of escher
	/// records from a pointer into a data array.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public interface IEscherRecordFactory
	{
		/// <summary>
		/// Create a new escher record from the data provided.  Does not attempt
		/// to Fill the contents of the record however.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="offset">The off set.</param>
		/// <returns></returns>
		EscherRecord CreateRecord(byte[] data, int offset);
	}
}
