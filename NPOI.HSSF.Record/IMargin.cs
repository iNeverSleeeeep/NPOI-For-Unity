namespace NPOI.HSSF.Record
{
	/// The margin interface Is a parent used to define left, right, top and bottom margins.
	/// This allows much of the code to be generic when it comes to handling margins.
	/// NOTE: This source wass automatically generated.
	///
	/// @author Shawn Laubach (slaubach at apache dot org)
	public interface IMargin
	{
		/// Get the margin field for the Margin.
		double Margin
		{
			get;
			set;
		}
	}
}
