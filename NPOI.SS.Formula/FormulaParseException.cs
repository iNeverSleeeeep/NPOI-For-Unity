using System;

namespace NPOI.SS.Formula
{
	/// <summary>
	/// Specific exception thrown when a supplied formula does not Parse properly.
	///  Primarily used by test cases when testing for specific parsing exceptions.
	/// </summary>
	[Serializable]
	public class FormulaParseException : Exception
	{
		/// <summary>
		///             This class was given package scope until it would become Clear that it is useful to general client code.
		/// </summary>
		/// <param name="msg"></param>
		public FormulaParseException(string msg)
			: base(msg)
		{
		}
	}
}
