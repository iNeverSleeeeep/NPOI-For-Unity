namespace NPOI.SS.Extractor
{
	/// Common interface for Excel text extractors, covering
	///  HSSF and XSSF
	public interface IExcelExtractor
	{
		bool IncludeCellComments
		{
			get;
			set;
		}

		bool IncludeSheetNames
		{
			get;
			set;
		}

		bool FormulasNotResults
		{
			get;
			set;
		}

		bool IncludeHeaderFooter
		{
			get;
			set;
		}

		/// Retreives the text contents of the file
		string Text
		{
			get;
		}
	}
}
