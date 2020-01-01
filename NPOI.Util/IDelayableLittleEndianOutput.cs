namespace NPOI.Util
{
	/// <summary>
	/// Implementors of this interface allow client code to 'delay' writing to a certain section of a 
	/// data output stream.<br />
	/// A typical application is for writing BIFF records when the size is not known until well after
	/// the header has been written.  The client code can call <see cref="M:NPOI.Util.IDelayableLittleEndianOutput.CreateDelayedOutput" />
	/// to reserve two bytes of the output for the 'ushort size' header field.  The delayed output can
	/// be written at any stage. 
	/// </summary>
	/// <remarks>@author Josh Micich</remarks>
	public interface IDelayableLittleEndianOutput : ILittleEndianOutput
	{
		/// <summary>
		/// Creates an output stream intended for outputting a sequence of <c>size</c> bytes.
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		ILittleEndianOutput CreateDelayedOutput(int size);
	}
}
