using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.PTG;
using System;
using System.Collections;
using System.Reflection;

namespace NPOI.HSSF.UserModel
{
	/// This class Creates <c>OperationEval</c> instances to help evaluate <c>OperationPtg</c>
	/// formula tokens.
	///
	/// @author Josh Micich
	internal class OperationEvaluatorFactory
	{
		private static Type[] OPERATION_CONSTRUCTOR_CLASS_ARRAY = new Type[1]
		{
			typeof(Ptg)
		};

		private static Hashtable _constructorsByPtgClass = InitialiseConstructorsMap();

		private OperationEvaluatorFactory()
		{
		}

		private static Hashtable InitialiseConstructorsMap()
		{
			Hashtable hashtable = new Hashtable(32);
			Add(hashtable, typeof(AddPtg), typeof(AddEval));
			Add(hashtable, typeof(DividePtg), typeof(DivideEval));
			Add(hashtable, typeof(EqualPtg), typeof(EqualEval));
			Add(hashtable, typeof(MultiplyPtg), typeof(MultiplyEval));
			Add(hashtable, typeof(ConcatPtg), typeof(ConcatEval));
			Add(hashtable, typeof(GreaterEqualPtg), typeof(GreaterEqualEval));
			Add(hashtable, typeof(GreaterThanPtg), typeof(GreaterThanEval));
			Add(hashtable, typeof(LessEqualPtg), typeof(LessEqualEval));
			Add(hashtable, typeof(LessThanPtg), typeof(LessThanEval));
			Add(hashtable, typeof(NotEqualPtg), typeof(NotEqualEval));
			Add(hashtable, typeof(PercentPtg), typeof(PercentEval));
			Add(hashtable, typeof(PowerPtg), typeof(PowerEval));
			Add(hashtable, typeof(SubtractPtg), typeof(SubtractEval));
			Add(hashtable, typeof(UnaryMinusPtg), typeof(UnaryMinusEval));
			Add(hashtable, typeof(UnaryPlusPtg), typeof(UnaryPlusEval));
			return hashtable;
		}

		private static void Add(Hashtable m, Type ptgClass, Type evalClass)
		{
			if (!typeof(Ptg).IsAssignableFrom(ptgClass))
			{
				throw new ArgumentException("Expected Ptg subclass");
			}
			if (!typeof(OperationEval).IsAssignableFrom(evalClass))
			{
				throw new ArgumentException("Expected OperationEval subclass");
			}
			if (!evalClass.IsPublic)
			{
				throw new Exception("Eval class must be public");
			}
			if (evalClass.IsAbstract)
			{
				throw new Exception("Eval class must not be abstract");
			}
			ConstructorInfo constructor;
			try
			{
				constructor = evalClass.GetConstructor(OPERATION_CONSTRUCTOR_CLASS_ARRAY);
			}
			catch (Exception)
			{
				throw;
			}
			if (!constructor.IsPublic)
			{
				throw new Exception("Eval constructor must be public");
			}
			m[ptgClass] = constructor;
		}

		/// returns the OperationEval concrete impl instance corresponding
		/// to the supplied operationPtg
		public static OperationEval Create(OperationPtg ptg)
		{
			if (ptg == null)
			{
				throw new ArgumentException("ptg must not be null");
			}
			Type type = ptg.GetType();
			ConstructorInfo constructorInfo = (ConstructorInfo)_constructorsByPtgClass[type];
			if (constructorInfo == null)
			{
				if (type == typeof(ExpPtg))
				{
					throw new Exception("ExpPtg currently not supported");
				}
				throw new Exception("Unexpected operation ptg class (" + type.Name + ")");
			}
			object[] parameters = new object[1]
			{
				ptg
			};
			object obj;
			try
			{
				obj = constructorInfo.Invoke(parameters);
			}
			catch (Exception)
			{
				throw;
			}
			return (OperationEval)obj;
		}
	}
}
