using System;

namespace NPOI.HSSF.UserModel
{
	/// This class makes an <c>EvaluationCycleDetector</c> instance available to
	/// each thRead via a <c>ThReadLocal</c> in order to avoid Adding a parameter
	/// to a few protected methods within <c>HSSFFormulaEvaluator</c>.
	///
	/// @author Josh Micich
	internal class EvaluationCycleDetectorManager
	{
		[ThreadStatic]
		private static EvaluationCycleDetector ecd = new EvaluationCycleDetector();

		/// @return
		public static EvaluationCycleDetector GetTracker()
		{
			return ecd;
		}

		private EvaluationCycleDetectorManager()
		{
		}
	}
}
