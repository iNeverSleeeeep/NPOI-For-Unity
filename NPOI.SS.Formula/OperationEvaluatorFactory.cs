using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using System;
using System.Collections;
using System.Reflection;

namespace NPOI.SS.Formula
{
	/// This class Creates <c>OperationEval</c> instances To help evaluate <c>OperationPtg</c>
	/// formula Tokens.
	///
	/// @author Josh Micich
	internal class OperationEvaluatorFactory
	{
		private static Type[] OPERATION_CONSTRUCTOR_CLASS_ARRAY = new Type[1]
		{
			typeof(Ptg)
		};

		private static Hashtable _instancesByPtgClass = InitialiseInstancesMap();

		private OperationEvaluatorFactory()
		{
		}

		private static Hashtable InitialiseInstancesMap()
		{
			Hashtable hashtable = new Hashtable(32);
			Add(hashtable, EqualPtg.instance, RelationalOperationEval.EqualEval);
			Add(hashtable, GreaterEqualPtg.instance, RelationalOperationEval.GreaterEqualEval);
			Add(hashtable, GreaterThanPtg.instance, RelationalOperationEval.GreaterThanEval);
			Add(hashtable, LessEqualPtg.instance, RelationalOperationEval.LessEqualEval);
			Add(hashtable, LessThanPtg.instance, RelationalOperationEval.LessThanEval);
			Add(hashtable, NotEqualPtg.instance, RelationalOperationEval.NotEqualEval);
			Add(hashtable, ConcatPtg.instance, ConcatEval.instance);
			Add(hashtable, AddPtg.instance, TwoOperandNumericOperation.AddEval);
			Add(hashtable, DividePtg.instance, TwoOperandNumericOperation.DivideEval);
			Add(hashtable, MultiplyPtg.instance, TwoOperandNumericOperation.MultiplyEval);
			Add(hashtable, PercentPtg.instance, PercentEval.instance);
			Add(hashtable, PowerPtg.instance, TwoOperandNumericOperation.PowerEval);
			Add(hashtable, SubtractPtg.instance, TwoOperandNumericOperation.SubtractEval);
			Add(hashtable, UnaryMinusPtg.instance, UnaryMinusEval.instance);
			Add(hashtable, UnaryPlusPtg.instance, UnaryPlusEval.instance);
			Add(hashtable, RangePtg.instance, RangeEval.instance);
			Add(hashtable, IntersectionPtg.instance, IntersectionEval.instance);
			return hashtable;
		}

		private static void Add(Hashtable m, OperationPtg ptgKey, NPOI.SS.Formula.Functions.Function instance)
		{
			ConstructorInfo[] constructors = ptgKey.GetType().GetConstructors();
			if (constructors.Length > 1 || (constructors.Length > 0 && !constructors[0].IsPrivate))
			{
				throw new Exception("Failed to verify instance (" + ptgKey.GetType().Name + ") is a singleton.");
			}
			m[ptgKey] = instance;
		}

		/// returns the OperationEval concrete impl instance corresponding
		/// to the supplied operationPtg
		public static ValueEval Evaluate(OperationPtg ptg, ValueEval[] args, OperationEvaluationContext ec)
		{
			if (ptg == null)
			{
				throw new ArgumentException("ptg must not be null");
			}
			NPOI.SS.Formula.Functions.Function function = _instancesByPtgClass[ptg] as NPOI.SS.Formula.Functions.Function;
			if (function != null)
			{
				return function.Evaluate(args, ec.RowIndex, (short)ec.ColumnIndex);
			}
			if (ptg is AbstractFunctionPtg)
			{
				AbstractFunctionPtg abstractFunctionPtg = (AbstractFunctionPtg)ptg;
				int functionIndex = abstractFunctionPtg.GetFunctionIndex();
				switch (functionIndex)
				{
				case 148:
					return Indirect.instance.Evaluate(args, ec);
				case 255:
					return UserDefinedFunction.instance.Evaluate(args, ec);
				default:
					return FunctionEval.GetBasicFunction(functionIndex).Evaluate(args, ec.RowIndex, ec.ColumnIndex);
				}
			}
			throw new Exception("Unexpected operation ptg class (" + ptg.GetType().Name + ")");
		}
	}
}
