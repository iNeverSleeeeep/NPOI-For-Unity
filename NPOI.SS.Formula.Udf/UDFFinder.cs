using NPOI.SS.Formula.Atp;
using NPOI.SS.Formula.Functions;

namespace NPOI.SS.Formula.Udf
{
	/// Common interface for "Add-in" libraries and user defined function libraries.
	///
	/// @author PUdalau
	public abstract class UDFFinder
	{
		public static readonly UDFFinder DEFAULT = new AggregatingUDFFinder(AnalysisToolPak.instance);

		/// Returns executor by specified name. Returns <code>null</code> if the function name is unknown.
		///
		/// @param name Name of function.
		/// @return Function executor.
		public abstract FreeRefFunction FindFunction(string name);
	}
}
