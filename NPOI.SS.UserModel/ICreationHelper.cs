namespace NPOI.SS.UserModel
{
	/// An object that handles instantiating concrete
	///  classes of the various instances one needs for
	///  HSSF and XSSF.
	/// Works around a major shortcoming in Java, where we
	///  can't have static methods on interfaces or abstract
	///  classes.
	/// This allows you to get the appropriate class for
	///  a given interface, without you having to worry
	///  about if you're dealing with HSSF or XSSF, despite
	///  Java being quite rubbish.
	public interface ICreationHelper
	{
		/// Creates a new RichTextString instance
		/// @param text The text to Initialise the RichTextString with
		IRichTextString CreateRichTextString(string text);

		/// Creates a new DataFormat instance
		IDataFormat CreateDataFormat();

		/// Creates a new Hyperlink, of the given type
		IHyperlink CreateHyperlink(HyperlinkType type);

		/// Creates FormulaEvaluator - an object that Evaluates formula cells.
		///
		/// @return a FormulaEvaluator instance
		IFormulaEvaluator CreateFormulaEvaluator();

		IClientAnchor CreateClientAnchor();
	}
}
