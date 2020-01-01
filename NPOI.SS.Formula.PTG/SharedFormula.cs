namespace NPOI.SS.Formula.PTG
{
	/// Encapsulates logic to convert shared formulaa into non shared equivalent
	public class SharedFormula
	{
		private int _columnWrappingMask;

		private int _rowWrappingMask;

		public SharedFormula(SpreadsheetVersion ssVersion)
		{
			_columnWrappingMask = ssVersion.LastColumnIndex;
			_rowWrappingMask = ssVersion.LastRowIndex;
		}

		/// Creates a non shared formula from the shared formula counterpart, i.e.
		/// Converts the shared formula into the equivalent {@link org.apache.poi.ss.formula.ptg.Ptg} array that it would have,
		/// were it not shared.
		///
		/// @param ptgs parsed tokens of the shared formula
		/// @param formulaRow
		/// @param formulaColumn
		public Ptg[] ConvertSharedFormulas(Ptg[] ptgs, int formulaRow, int formulaColumn)
		{
			Ptg[] array = new Ptg[ptgs.Length];
			for (int i = 0; i < ptgs.Length; i++)
			{
				Ptg ptg = ptgs[i];
				byte ptgClass = byte.MaxValue;
				if (!ptg.IsBaseToken)
				{
					ptgClass = ptg.PtgClass;
				}
				if (ptg is RefPtgBase)
				{
					RefPtgBase refPtgBase = (RefPtgBase)ptg;
					ptg = new RefPtg(FixupRelativeRow(formulaRow, refPtgBase.Row, refPtgBase.IsRowRelative), FixupRelativeColumn(formulaColumn, refPtgBase.Column, refPtgBase.IsColRelative), refPtgBase.IsRowRelative, refPtgBase.IsColRelative);
					ptg.PtgClass = ptgClass;
				}
				else if (ptg is AreaPtgBase)
				{
					AreaPtgBase areaPtgBase = (AreaPtgBase)ptg;
					ptg = new AreaPtg(FixupRelativeRow(formulaRow, areaPtgBase.FirstRow, areaPtgBase.IsFirstRowRelative), FixupRelativeRow(formulaRow, areaPtgBase.LastRow, areaPtgBase.IsLastRowRelative), FixupRelativeColumn(formulaColumn, areaPtgBase.FirstColumn, areaPtgBase.IsFirstColRelative), FixupRelativeColumn(formulaColumn, areaPtgBase.LastColumn, areaPtgBase.IsLastColRelative), areaPtgBase.IsFirstRowRelative, areaPtgBase.IsLastRowRelative, areaPtgBase.IsFirstColRelative, areaPtgBase.IsLastColRelative);
					ptg.PtgClass = ptgClass;
				}
				else if (ptg is OperandPtg)
				{
					ptg = ((OperandPtg)ptg).Copy();
				}
				array[i] = ptg;
			}
			return array;
		}

		private int FixupRelativeColumn(int currentcolumn, int column, bool relative)
		{
			if (relative)
			{
				return (column + currentcolumn) & _columnWrappingMask;
			}
			return column;
		}

		private int FixupRelativeRow(int currentrow, int row, bool relative)
		{
			if (relative)
			{
				return (row + currentrow) & _rowWrappingMask;
			}
			return row;
		}
	}
}
