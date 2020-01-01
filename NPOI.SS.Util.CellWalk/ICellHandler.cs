using NPOI.SS.UserModel;

namespace NPOI.SS.Util.CellWalk
{
	/// Represents callback for CellWalk traverse method.  
	/// @author Roman Kashitsyn
	public interface ICellHandler
	{
		/// @param cell current cell
		/// @param ctx information about invokation context
		void OnCell(ICell cell, ICellWalkContext ctx);
	}
}
