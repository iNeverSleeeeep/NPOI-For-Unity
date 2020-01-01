using NPOI.SS.Formula.Function;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.Udf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NPOI.SS.Formula.Atp
{
	public class AnalysisToolPak : UDFFinder
	{
		public static UDFFinder instance = new AnalysisToolPak();

		private static Hashtable _functionsByName = CreateFunctionsMap();

		private AnalysisToolPak()
		{
		}

		public override FreeRefFunction FindFunction(string name)
		{
			if (name.StartsWith("_xlfn."))
			{
				name = name.Substring(6);
			}
			return (FreeRefFunction)_functionsByName[name.ToUpper()];
		}

		private static Hashtable CreateFunctionsMap()
		{
			Hashtable hashtable = new Hashtable(100);
			r(hashtable, "ACCRINT", null);
			r(hashtable, "ACCRINTM", null);
			r(hashtable, "AMORDEGRC", null);
			r(hashtable, "AMORLINC", null);
			r(hashtable, "AVERAGEIF", null);
			r(hashtable, "AVERAGEIFS", null);
			r(hashtable, "BAHTTEXT", null);
			r(hashtable, "BESSELI", null);
			r(hashtable, "BESSELJ", null);
			r(hashtable, "BESSELK", null);
			r(hashtable, "BESSELY", null);
			r(hashtable, "BIN2DEC", null);
			r(hashtable, "BIN2HEX", null);
			r(hashtable, "BIN2OCT", null);
			r(hashtable, "COMPLEX", Complex.Instance);
			r(hashtable, "CONVERT", null);
			r(hashtable, "COUNTIFS", null);
			r(hashtable, "COUPDAYBS", null);
			r(hashtable, "COUPDAYS", null);
			r(hashtable, "COUPDAYSNC", null);
			r(hashtable, "COUPNCD", null);
			r(hashtable, "COUPNUM", null);
			r(hashtable, "COUPPCD", null);
			r(hashtable, "CUBEKPIMEMBER", null);
			r(hashtable, "CUBEMEMBER", null);
			r(hashtable, "CUBEMEMBERPROPERTY", null);
			r(hashtable, "CUBERANKEDMEMBER", null);
			r(hashtable, "CUBESET", null);
			r(hashtable, "CUBESETCOUNT", null);
			r(hashtable, "CUBEVALUE", null);
			r(hashtable, "CUMIPMT", null);
			r(hashtable, "CUMPRINC", null);
			r(hashtable, "DEC2BIN", null);
			r(hashtable, "DEC2HEX", null);
			r(hashtable, "DEC2OCT", null);
			r(hashtable, "DELTA", null);
			r(hashtable, "DISC", null);
			r(hashtable, "DOLLARDE", null);
			r(hashtable, "DOLLARFR", null);
			r(hashtable, "DURATION", null);
			r(hashtable, "EDATE", EDate.Instance);
			r(hashtable, "EFFECT", null);
			r(hashtable, "EOMONTH", null);
			r(hashtable, "ERF", null);
			r(hashtable, "ERFC", null);
			r(hashtable, "FACTDOUBLE", null);
			r(hashtable, "FVSCHEDULE", null);
			r(hashtable, "GCD", null);
			r(hashtable, "GESTEP", null);
			r(hashtable, "HEX2BIN", null);
			r(hashtable, "HEX2DEC", null);
			r(hashtable, "HEX2OCT", null);
			r(hashtable, "IFERROR", IfError.Instance);
			r(hashtable, "IMABS", null);
			r(hashtable, "IMAGINARY", null);
			r(hashtable, "IMARGUMENT", null);
			r(hashtable, "IMCONJUGATE", null);
			r(hashtable, "IMCOS", null);
			r(hashtable, "IMDIV", null);
			r(hashtable, "IMEXP", null);
			r(hashtable, "IMLN", null);
			r(hashtable, "IMLOG10", null);
			r(hashtable, "IMLOG2", null);
			r(hashtable, "IMPOWER", null);
			r(hashtable, "IMPRODUCT", null);
			r(hashtable, "IMREAL", null);
			r(hashtable, "IMSIN", null);
			r(hashtable, "IMSQRT", null);
			r(hashtable, "IMSUB", null);
			r(hashtable, "IMSUM", null);
			r(hashtable, "INTRATE", null);
			r(hashtable, "ISEVEN", ParityFunction.IS_EVEN);
			r(hashtable, "ISODD", ParityFunction.IS_ODD);
			r(hashtable, "JIS", null);
			r(hashtable, "LCM", null);
			r(hashtable, "MDURATION", null);
			r(hashtable, "MROUND", MRound.Instance);
			r(hashtable, "MULTINOMIAL", null);
			r(hashtable, "NETWORKDAYS", NetworkdaysFunction.instance);
			r(hashtable, "NOMINAL", null);
			r(hashtable, "OCT2BIN", null);
			r(hashtable, "OCT2DEC", null);
			r(hashtable, "OCT2HEX", null);
			r(hashtable, "ODDFPRICE", null);
			r(hashtable, "ODDFYIELD", null);
			r(hashtable, "ODDLPRICE", null);
			r(hashtable, "ODDLYIELD", null);
			r(hashtable, "PRICE", null);
			r(hashtable, "PRICEDISC", null);
			r(hashtable, "PRICEMAT", null);
			r(hashtable, "QUOTIENT", null);
			r(hashtable, "RANDBETWEEN", RandBetween.Instance);
			r(hashtable, "RECEIVED", null);
			r(hashtable, "RTD", null);
			r(hashtable, "SERIESSUM", null);
			r(hashtable, "SQRTPI", null);
			r(hashtable, "SUMIFS", Sumifs.instance);
			r(hashtable, "TBILLEQ", null);
			r(hashtable, "TBILLPRICE", null);
			r(hashtable, "TBILLYIELD", null);
			r(hashtable, "WEEKNUM", null);
			r(hashtable, "WORKDAY", WorkdayFunction.instance);
			r(hashtable, "XIRR", null);
			r(hashtable, "XNPV", null);
			r(hashtable, "YEARFRAC", YearFrac.instance);
			r(hashtable, "YIELD", null);
			r(hashtable, "YIELDDISC", null);
			r(hashtable, "YIELDMAT", null);
			return hashtable;
		}

		private static void r(Hashtable m, string functionName, FreeRefFunction pFunc)
		{
			FreeRefFunction freeRefFunction = (FreeRefFunction)(m[functionName] = ((pFunc == null) ? new NotImplemented(functionName) : pFunc));
		}

		public static bool IsATPFunction(string name)
		{
			return _functionsByName.ContainsKey(name);
		}

		/// Returns a collection of ATP function names implemented by POI.
		///
		/// @return an array of supported functions
		/// @since 3.8 beta6
		public static ReadOnlyCollection<string> GetSupportedFunctionNames()
		{
			AnalysisToolPak analysisToolPak = (AnalysisToolPak)instance;
			List<string> list = new List<string>();
			foreach (string key in _functionsByName.Keys)
			{
				FreeRefFunction freeRefFunction = (FreeRefFunction)_functionsByName[key];
				if (freeRefFunction != null && !(freeRefFunction is NotImplemented))
				{
					list.Add(key);
				}
			}
			return list.AsReadOnly();
		}

		/// Returns a collection of ATP function names NOT implemented by POI.
		///
		/// @return an array of not supported functions
		/// @since 3.8 beta6
		public static ReadOnlyCollection<string> GetNotSupportedFunctionNames()
		{
			AnalysisToolPak analysisToolPak = (AnalysisToolPak)instance;
			List<string> list = new List<string>();
			foreach (string key in _functionsByName.Keys)
			{
				FreeRefFunction freeRefFunction = (FreeRefFunction)_functionsByName[key];
				if (freeRefFunction != null && freeRefFunction is NotImplemented)
				{
					list.Add(key);
				}
			}
			return list.AsReadOnly();
		}

		/// Register a ATP function in runtime.
		///
		/// @param name  the function name
		/// @param func  the functoin to register
		/// @throws ArgumentException if the function is unknown or already  registered.
		/// @since 3.8 beta6
		public static void RegisterFunction(string name, FreeRefFunction func)
		{
			AnalysisToolPak analysisToolPak = (AnalysisToolPak)instance;
			if (!IsATPFunction(name))
			{
				FunctionMetadata functionByName = FunctionMetadataRegistry.GetFunctionByName(name);
				if (functionByName != null)
				{
					throw new ArgumentException(name + " is a built-in Excel function. Use FunctoinEval.RegisterFunction(String name, Function func) instead.");
				}
				throw new ArgumentException(name + " is not a function from the Excel Analysis Toolpack.");
			}
			FreeRefFunction freeRefFunction = analysisToolPak.FindFunction(name);
			if (freeRefFunction != null && !(freeRefFunction is NotImplemented))
			{
				throw new ArgumentException("POI already implememts " + name + ". You cannot override POI's implementations of Excel functions");
			}
			if (_functionsByName.ContainsKey(name))
			{
				_functionsByName[name] = func;
			}
			else
			{
				_functionsByName.Add(name, func);
			}
		}
	}
}
