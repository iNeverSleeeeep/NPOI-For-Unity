namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// This interface defines methods specific to Document objects
	/// managed by a Filesystem instance.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public interface DocumentEntry : Entry
	{
		/// <summary>
		/// get the size of the document, in bytes
		/// </summary>
		/// <value>size in bytes</value>
		int Size
		{
			get;
		}
	}
}
