namespace NPOI.POIFS.Properties
{
	/// <summary>
	/// This interface defines methods for finding and setting sibling
	/// Property instances
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public interface Child
	{
		/// <summary>
		/// Gets or sets the previous child.
		/// </summary>
		/// <value>The previous child.</value>
		Child PreviousChild
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the next child.
		/// </summary>
		/// <value>The next child.</value>
		Child NextChild
		{
			get;
			set;
		}
	}
}
