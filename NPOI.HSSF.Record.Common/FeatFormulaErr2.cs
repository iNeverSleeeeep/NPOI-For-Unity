using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Common
{
	/// FeatFormulaErr2 (Formula Evaluation Shared Feature) common record part
	///
	/// This record part specifies Formula Evaluation &amp; Error Ignoring data 
	///  for a sheet, stored as part of a Shared Feature. It can be found in 
	///  records such as {@link FeatRecord}.
	/// For the full meanings of the flags, see pages 669 and 670
	///  of the Excel binary file format documentation.
	public class FeatFormulaErr2 : SharedFeature
	{
		private static BitField checkCalculationErrors = BitFieldFactory.GetInstance(1);

		private static BitField checkEmptyCellRef = BitFieldFactory.GetInstance(2);

		private static BitField checkNumbersAsText = BitFieldFactory.GetInstance(4);

		private static BitField checkInconsistentRanges = BitFieldFactory.GetInstance(8);

		private static BitField checkInconsistentFormulas = BitFieldFactory.GetInstance(16);

		private static BitField checkDateTimeFormats = BitFieldFactory.GetInstance(32);

		private static BitField checkUnprotectedFormulas = BitFieldFactory.GetInstance(64);

		private static BitField performDataValidation = BitFieldFactory.GetInstance(128);

		/// What errors we should ignore
		private int errorCheck;

		public int DataSize => 4;

		public int RawErrorCheckValue => errorCheck;

		public bool CheckCalculationErrors
		{
			get
			{
				return checkCalculationErrors.IsSet(errorCheck);
			}
			set
			{
				checkCalculationErrors.SetBoolean(errorCheck, value);
			}
		}

		public bool CheckEmptyCellRef
		{
			get
			{
				return checkEmptyCellRef.IsSet(errorCheck);
			}
			set
			{
				checkEmptyCellRef.SetBoolean(errorCheck, value);
			}
		}

		public bool CheckNumbersAsText
		{
			get
			{
				return checkNumbersAsText.IsSet(errorCheck);
			}
			set
			{
				checkNumbersAsText.SetBoolean(errorCheck, value);
			}
		}

		public bool CheckInconsistentRanges
		{
			get
			{
				return checkInconsistentRanges.IsSet(errorCheck);
			}
			set
			{
				checkInconsistentRanges.SetBoolean(errorCheck, value);
			}
		}

		public bool CheckInconsistentFormulas
		{
			get
			{
				return checkInconsistentFormulas.IsSet(errorCheck);
			}
			set
			{
				checkInconsistentFormulas.SetBoolean(errorCheck, value);
			}
		}

		public bool CheckDateTimeFormats
		{
			get
			{
				return checkDateTimeFormats.IsSet(errorCheck);
			}
			set
			{
				checkDateTimeFormats.SetBoolean(errorCheck, value);
			}
		}

		public bool CheckUnprotectedFormulas
		{
			get
			{
				return checkUnprotectedFormulas.IsSet(errorCheck);
			}
			set
			{
				checkUnprotectedFormulas.SetBoolean(errorCheck, value);
			}
		}

		public bool PerformDataValidation
		{
			get
			{
				return performDataValidation.IsSet(errorCheck);
			}
			set
			{
				performDataValidation.SetBoolean(errorCheck, value);
			}
		}

		public FeatFormulaErr2()
		{
		}

		public FeatFormulaErr2(RecordInputStream in1)
		{
			errorCheck = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" [FEATURE FORMULA ERRORS]\n");
			stringBuilder.Append("  checkCalculationErrors    = ");
			stringBuilder.Append("  checkEmptyCellRef         = ");
			stringBuilder.Append("  checkNumbersAsText        = ");
			stringBuilder.Append("  checkInconsistentRanges   = ");
			stringBuilder.Append("  checkInconsistentFormulas = ");
			stringBuilder.Append("  checkDateTimeFormats      = ");
			stringBuilder.Append("  checkUnprotectedFormulas  = ");
			stringBuilder.Append("  performDataValidation     = ");
			stringBuilder.Append(" [/FEATURE FORMULA ERRORS]\n");
			return stringBuilder.ToString();
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(errorCheck);
		}
	}
}
