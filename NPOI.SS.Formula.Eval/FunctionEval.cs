using NPOI.SS.Formula.Atp;
using NPOI.SS.Formula.Function;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public abstract class FunctionEval
	{
		/// Some function IDs that require special treatment
		private class FunctionID
		{
			/// 1 
			public const int IF = 1;

			public const int SUM = 4;

			/// 78 
			public const int OFFSET = 78;

			/// 100 
			public const int CHOOSE = 100;

			/// 148 
			public const int INDIRECT = 148;

			/// 255 
			public const int EXTERNAL_FUNC = 255;
		}

		protected static NPOI.SS.Formula.Functions.Function[] functions = ProduceFunctions();

		private static FunctionMetadataRegistry _instance;

		public abstract Eval Evaluate(Eval[] evals, int srcCellRow, short srcCellCol);

		private static FunctionMetadataRegistry GetInstance()
		{
			if (_instance == null)
			{
				_instance = FunctionMetadataReader.CreateRegistry();
			}
			return _instance;
		}

		public static NPOI.SS.Formula.Functions.Function GetBasicFunction(int functionIndex)
		{
			if (functionIndex == 148 || functionIndex == 255)
			{
				return null;
			}
			NPOI.SS.Formula.Functions.Function function = functions[functionIndex];
			if (function == null)
			{
				throw new NotImplementedException("FuncIx=" + functionIndex);
			}
			return function;
		}

		private static NPOI.SS.Formula.Functions.Function[] ProduceFunctions()
		{
			return new NPOI.SS.Formula.Functions.Function[368]
			{
				new Count(),
				new If(),
				LogicalFunction.ISNA,
				LogicalFunction.ISERROR,
				AggregateFunction.SUM,
				AggregateFunction.AVERAGE,
				AggregateFunction.MIN,
				AggregateFunction.MAX,
				new Row(),
				new Column(),
				new Na(),
				new Npv(),
				AggregateFunction.STDEV,
				NumericFunction.DOLLAR,
				new NotImplementedFunction("FIXED"),
				NumericFunction.SIN,
				NumericFunction.COS,
				NumericFunction.TAN,
				NumericFunction.ATAN,
				new Pi(),
				NumericFunction.SQRT,
				NumericFunction.EXP,
				NumericFunction.LN,
				NumericFunction.LOG10,
				NumericFunction.ABS,
				NumericFunction.INT,
				NumericFunction.SIGN,
				NumericFunction.ROUND,
				new Lookup(),
				new Index(),
				new Rept(),
				TextFunction.MID,
				TextFunction.LEN,
				new Value(),
				new True(),
				new False(),
				new And(),
				new Or(),
				new Not(),
				NumericFunction.MOD,
				new NotImplementedFunction("DCOUNT"),
				new NotImplementedFunction("DSUM"),
				new NotImplementedFunction("DAVERAGE"),
				new NotImplementedFunction("DMIN"),
				new NotImplementedFunction("DMAX"),
				new NotImplementedFunction("DSTDEV"),
				AggregateFunction.VAR,
				new NotImplementedFunction("DVAR"),
				TextFunction.TEXT,
				new NotImplementedFunction("LINEST"),
				new NotImplementedFunction("TREND"),
				new NotImplementedFunction("LOGEST"),
				new NotImplementedFunction("GROWTH"),
				new NotImplementedFunction("GOTO"),
				new NotImplementedFunction("HALT"),
				null,
				FinanceFunction.PV,
				FinanceFunction.FV,
				FinanceFunction.NPER,
				FinanceFunction.PMT,
				new Rate(),
				new NotImplementedFunction("MIRR"),
				new Irr(),
				new Rand(),
				new Match(),
				DateFunc.instance,
				new TimeFunc(),
				CalendarFieldFunction.DAY,
				CalendarFieldFunction.MONTH,
				CalendarFieldFunction.YEAR,
				WeekdayFunc.instance,
				CalendarFieldFunction.HOUR,
				CalendarFieldFunction.MINUTE,
				CalendarFieldFunction.SECOND,
				new Now(),
				new NotImplementedFunction("AREAS"),
				new Rows(),
				new Columns(),
				new Offset(),
				new NotImplementedFunction("ABSREF"),
				new NotImplementedFunction("RELREF"),
				new NotImplementedFunction("ARGUMENT"),
				TextFunction.SEARCH,
				new NotImplementedFunction("TRANSPOSE"),
				new NotImplementedFunction("ERROR"),
				new NotImplementedFunction("STEP"),
				new NotImplementedFunction("TYPE"),
				new NotImplementedFunction("ECHO"),
				new NotImplementedFunction("SetNAME"),
				new NotImplementedFunction("CALLER"),
				new NotImplementedFunction("DEREF"),
				new NotImplementedFunction("WINDOWS"),
				new NotImplementedFunction("SERIES"),
				new NotImplementedFunction("DOCUMENTS"),
				new NotImplementedFunction("ACTIVECELL"),
				new NotImplementedFunction("SELECTION"),
				new NotImplementedFunction("RESULT"),
				NumericFunction.ATAN2,
				NumericFunction.ASIN,
				NumericFunction.ACOS,
				new Choose(),
				new Hlookup(),
				new Vlookup(),
				new NotImplementedFunction("LINKS"),
				new NotImplementedFunction("INPUT"),
				LogicalFunction.ISREF,
				new NotImplementedFunction("GetFORMULA"),
				new NotImplementedFunction("GetNAME"),
				new NotImplementedFunction("SetVALUE"),
				NumericFunction.LOG,
				new NotImplementedFunction("EXEC"),
				TextFunction.CHAR,
				TextFunction.LOWER,
				TextFunction.UPPER,
				new NotImplementedFunction("PROPER"),
				TextFunction.LEFT,
				TextFunction.RIGHT,
				TextFunction.EXACT,
				TextFunction.TRIM,
				new Replace(),
				new Substitute(),
				new Code(),
				new NotImplementedFunction("NAMES"),
				new NotImplementedFunction("DIRECTORY"),
				TextFunction.FIND,
				new NotImplementedFunction("CELL"),
				LogicalFunction.ISERR,
				LogicalFunction.ISTEXT,
				LogicalFunction.ISNUMBER,
				LogicalFunction.ISBLANK,
				new T(),
				new NotImplementedFunction("N"),
				new NotImplementedFunction("FOPEN"),
				new NotImplementedFunction("FCLOSE"),
				new NotImplementedFunction("FSIZE"),
				new NotImplementedFunction("FReadLN"),
				new NotImplementedFunction("FRead"),
				new NotImplementedFunction("FWriteLN"),
				new NotImplementedFunction("FWrite"),
				new NotImplementedFunction("FPOS"),
				new NotImplementedFunction("DATEVALUE"),
				new NotImplementedFunction("TIMEVALUE"),
				new NotImplementedFunction("SLN"),
				new NotImplementedFunction("SYD"),
				new NotImplementedFunction("DDB"),
				new NotImplementedFunction("GetDEF"),
				new NotImplementedFunction("REFTEXT"),
				new NotImplementedFunction("TEXTREF"),
				null,
				new NotImplementedFunction("REGISTER"),
				new NotImplementedFunction("CALL"),
				new NotImplementedFunction("AddBAR"),
				new NotImplementedFunction("AddMENU"),
				new NotImplementedFunction("AddCOMMAND"),
				new NotImplementedFunction("ENABLECOMMAND"),
				new NotImplementedFunction("CHECKCOMMAND"),
				new NotImplementedFunction("RenameCOMMAND"),
				new NotImplementedFunction("SHOWBAR"),
				new NotImplementedFunction("DELETEMENU"),
				new NotImplementedFunction("DELETECOMMAND"),
				new NotImplementedFunction("GetCHARTITEM"),
				new NotImplementedFunction("DIALOGBOX"),
				TextFunction.CLEAN,
				new NotImplementedFunction("MDETERM"),
				new NotImplementedFunction("MINVERSE"),
				new NotImplementedFunction("MMULT"),
				new NotImplementedFunction("FILES"),
				new NotImplementedFunction("IPMT"),
				new NotImplementedFunction("PPMT"),
				new Counta(),
				new NotImplementedFunction("CANCELKEY"),
				null,
				null,
				null,
				null,
				new NotImplementedFunction("INITIATE"),
				new NotImplementedFunction("REQUEST"),
				new NotImplementedFunction("POKE"),
				new NotImplementedFunction("EXECUTE"),
				new NotImplementedFunction("TERMINATE"),
				new NotImplementedFunction("RESTART"),
				new NotImplementedFunction("HELP"),
				new NotImplementedFunction("GetBAR"),
				AggregateFunction.PRODUCT,
				NumericFunction.FACT,
				new NotImplementedFunction("GetCELL"),
				new NotImplementedFunction("GetWORKSPACE"),
				new NotImplementedFunction("GetWINDOW"),
				new NotImplementedFunction("GetDOCUMENT"),
				new NotImplementedFunction("DPRODUCT"),
				LogicalFunction.ISNONTEXT,
				new NotImplementedFunction("GetNOTE"),
				new NotImplementedFunction("NOTE"),
				new NotImplementedFunction("STDEVP"),
				AggregateFunction.VARP,
				new NotImplementedFunction("DSTDEVP"),
				new NotImplementedFunction("DVARP"),
				NumericFunction.TRUNC,
				LogicalFunction.ISLOGICAL,
				new NotImplementedFunction("DCOUNTA"),
				new NotImplementedFunction("DELETEBAR"),
				new NotImplementedFunction("UNREGISTER"),
				null,
				null,
				new NotImplementedFunction("USDOLLAR"),
				new NotImplementedFunction("FindB"),
				new NotImplementedFunction("SEARCHB"),
				new NotImplementedFunction("ReplaceB"),
				new NotImplementedFunction("LEFTB"),
				new NotImplementedFunction("RIGHTB"),
				new NotImplementedFunction("MIDB"),
				new NotImplementedFunction("LENB"),
				NumericFunction.ROUNDUP,
				NumericFunction.ROUNDDOWN,
				new NotImplementedFunction("ASC"),
				new NotImplementedFunction("DBCS"),
				new Rank(),
				null,
				null,
				new Address(),
				new Days360(),
				new Today(),
				new NotImplementedFunction("VDB"),
				null,
				null,
				null,
				null,
				AggregateFunction.MEDIAN,
				new Sumproduct(),
				NumericFunction.SINH,
				NumericFunction.COSH,
				NumericFunction.TANH,
				NumericFunction.ASINH,
				NumericFunction.ACOSH,
				NumericFunction.ATANH,
				new NotImplementedFunction("DGet"),
				new NotImplementedFunction("CreateOBJECT"),
				new NotImplementedFunction("VOLATILE"),
				new NotImplementedFunction("LASTERROR"),
				new NotImplementedFunction("CUSTOMUNDO"),
				new NotImplementedFunction("CUSTOMREPEAT"),
				new NotImplementedFunction("FORMULAConvert"),
				new NotImplementedFunction("GetLINKINFO"),
				new NotImplementedFunction("TEXTBOX"),
				new NotImplementedFunction("INFO"),
				new NotImplementedFunction("GROUP"),
				new NotImplementedFunction("GetOBJECT"),
				new NotImplementedFunction("DB"),
				new NotImplementedFunction("PAUSE"),
				null,
				new NotImplementedFunction("RESUME"),
				null,
				new NotImplementedFunction("FREQUENCY"),
				new NotImplementedFunction("AddTOOLBAR"),
				new NotImplementedFunction("DELETETOOLBAR"),
				null,
				new NotImplementedFunction("RESetTOOLBAR"),
				new NotImplementedFunction("EVALUATE"),
				new NotImplementedFunction("GetTOOLBAR"),
				new NotImplementedFunction("GetTOOL"),
				new NotImplementedFunction("SPELLINGCHECK"),
				new Errortype(),
				new NotImplementedFunction("APPTITLE"),
				new NotImplementedFunction("WINDOWTITLE"),
				new NotImplementedFunction("SAVETOOLBAR"),
				new NotImplementedFunction("ENABLETOOL"),
				new NotImplementedFunction("PRESSTOOL"),
				new NotImplementedFunction("REGISTERID"),
				new NotImplementedFunction("GetWORKBOOK"),
				AggregateFunction.AVEDEV,
				new NotImplementedFunction("BETADIST"),
				new NotImplementedFunction("GAMMALN"),
				new NotImplementedFunction("BETAINV"),
				new NotImplementedFunction("BINOMDIST"),
				new NotImplementedFunction("CHIDIST"),
				new NotImplementedFunction("CHIINV"),
				NumericFunction.COMBIN,
				new NotImplementedFunction("CONFIDENCE"),
				new NotImplementedFunction("CRITBINOM"),
				new Even(),
				new NotImplementedFunction("EXPONDIST"),
				new NotImplementedFunction("FDIST"),
				new NotImplementedFunction("FINV"),
				new NotImplementedFunction("FISHER"),
				new NotImplementedFunction("FISHERINV"),
				NumericFunction.FLOOR,
				new NotImplementedFunction("GAMMADIST"),
				new NotImplementedFunction("GAMMAINV"),
				NumericFunction.CEILING,
				new NotImplementedFunction("HYPGEOMDIST"),
				new NotImplementedFunction("LOGNORMDIST"),
				new NotImplementedFunction("LOGINV"),
				new NotImplementedFunction("NEGBINOMDIST"),
				new NotImplementedFunction("NORMDIST"),
				new NotImplementedFunction("NORMSDIST"),
				new NotImplementedFunction("NORMINV"),
				new NotImplementedFunction("NORMSINV"),
				new NotImplementedFunction("STANDARDIZE"),
				new Odd(),
				new NotImplementedFunction("PERMUT"),
				NumericFunction.POISSON,
				new NotImplementedFunction("TDIST"),
				new NotImplementedFunction("WEIBULL"),
				new Sumxmy2(),
				new Sumx2my2(),
				new Sumx2py2(),
				new NotImplementedFunction("CHITEST"),
				new NotImplementedFunction("CORREL"),
				new NotImplementedFunction("COVAR"),
				new NotImplementedFunction("FORECAST"),
				new NotImplementedFunction("FTEST"),
				new NotImplementedFunction("INTERCEPT"),
				new NotImplementedFunction("PEARSON"),
				new NotImplementedFunction("RSQ"),
				new NotImplementedFunction("STEYX"),
				new NotImplementedFunction("SLOPE"),
				new NotImplementedFunction("TTEST"),
				new NotImplementedFunction("PROB"),
				AggregateFunction.DEVSQ,
				new NotImplementedFunction("GEOMEAN"),
				new NotImplementedFunction("HARMEAN"),
				AggregateFunction.SUMSQ,
				new NotImplementedFunction("KURT"),
				new NotImplementedFunction("SKEW"),
				new NotImplementedFunction("ZTEST"),
				AggregateFunction.LARGE,
				AggregateFunction.SMALL,
				new NotImplementedFunction("QUARTILE"),
				new NotImplementedFunction("PERCENTILE"),
				new NotImplementedFunction("PERCENTRANK"),
				new Mode(),
				new NotImplementedFunction("TRIMMEAN"),
				new NotImplementedFunction("TINV"),
				null,
				new NotImplementedFunction("MOVIECOMMAND"),
				new NotImplementedFunction("GetMOVIE"),
				TextFunction.CONCATENATE,
				NumericFunction.POWER,
				new NotImplementedFunction("PIVOTAddDATA"),
				new NotImplementedFunction("GetPIVOTTABLE"),
				new NotImplementedFunction("GetPIVOTFIELD"),
				new NotImplementedFunction("GetPIVOTITEM"),
				NumericFunction.RADIANS,
				NumericFunction.DEGREES,
				new Subtotal(),
				new Sumif(),
				new Countif(),
				new Countblank(),
				new NotImplementedFunction("SCENARIOGet"),
				new NotImplementedFunction("OPTIONSLISTSGet"),
				new NotImplementedFunction("IsPMT"),
				new NotImplementedFunction("DATEDIF"),
				new NotImplementedFunction("DATESTRING"),
				new NotImplementedFunction("NUMBERSTRING"),
				new NotImplementedFunction("ROMAN"),
				new NotImplementedFunction("OPENDIALOG"),
				new NotImplementedFunction("SAVEDIALOG"),
				new NotImplementedFunction("VIEWGet"),
				new NotImplementedFunction("GetPIVOTDATA"),
				new Hyperlink(),
				new NotImplementedFunction("PHONETIC"),
				new NotImplementedFunction("AVERAGEA"),
				new Maxa(),
				new Mina(),
				new NotImplementedFunction("STDEVPA"),
				new NotImplementedFunction("VARPA"),
				new NotImplementedFunction("STDEVA"),
				new NotImplementedFunction("VARA")
			};
		}

		/// Register a new function in runtime.
		///
		/// @param name  the function name
		/// @param func  the functoin to register
		/// @throws ArgumentException if the function is unknown or already  registered.
		/// @since 3.8 beta6
		public static void RegisterFunction(string name, NPOI.SS.Formula.Functions.Function func)
		{
			FunctionMetadata functionByName = FunctionMetadataRegistry.GetFunctionByName(name);
			if (functionByName == null)
			{
				if (AnalysisToolPak.IsATPFunction(name))
				{
					throw new ArgumentException(name + " is a function from the Excel Analysis Toolpack. Use AnalysisToolpack.RegisterFunction(String name, FreeRefFunction func) instead.");
				}
				throw new ArgumentException("Unknown function: " + name);
			}
			int index = functionByName.Index;
			if (functions[index] is NotImplementedFunction)
			{
				functions[index] = func;
				return;
			}
			throw new ArgumentException("POI already implememts " + name + ". You cannot override POI's implementations of Excel functions");
		}

		/// Returns a collection of function names implemented by POI.
		///
		/// @return an array of supported functions
		/// @since 3.8 beta6
		public static ReadOnlyCollection<string> GetSupportedFunctionNames()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < functions.Length; i++)
			{
				NPOI.SS.Formula.Functions.Function function = functions[i];
				FunctionMetadata functionByIndex = FunctionMetadataRegistry.GetFunctionByIndex(i);
				if (function != null && !(function is NotImplementedFunction))
				{
					list.Add(functionByIndex.Name);
				}
			}
			list.Add("INDIRECT");
			return list.AsReadOnly();
		}

		/// Returns an array of function names NOT implemented by POI.
		///
		/// @return an array of not supported functions
		/// @since 3.8 beta6
		public static ReadOnlyCollection<string> GetNotSupportedFunctionNames()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < functions.Length; i++)
			{
				NPOI.SS.Formula.Functions.Function function = functions[i];
				if (function != null && function is NotImplementedFunction)
				{
					FunctionMetadata functionByIndex = FunctionMetadataRegistry.GetFunctionByIndex(i);
					list.Add(functionByIndex.Name);
				}
			}
			list.Remove("INDIRECT");
			return list.AsReadOnly();
		}
	}
}
