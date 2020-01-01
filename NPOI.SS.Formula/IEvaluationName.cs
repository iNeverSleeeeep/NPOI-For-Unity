using NPOI.SS.Formula.PTG;

namespace NPOI.SS.Formula
{
	/// Abstracts a name record for formula evaluation.<br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public interface IEvaluationName
	{
		string NameText
		{
			get;
		}

		bool IsFunctionName
		{
			get;
		}

		bool HasFormula
		{
			get;
		}

		Ptg[] NameDefinition
		{
			get;
		}

		bool IsRange
		{
			get;
		}

		NamePtg CreatePtg();
	}
}
