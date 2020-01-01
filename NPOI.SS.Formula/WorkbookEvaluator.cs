#define TRACE
using NPOI.SS.Formula.Atp;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NPOI.SS.Formula
{
	/// Evaluates formula cells.<p />
	///
	/// For performance reasons, this class keeps a cache of all previously calculated intermediate
	/// cell values.  Be sure To call {@link #ClearCache()} if any workbook cells are Changed between
	/// calls To evaluate~ methods on this class.<br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public class WorkbookEvaluator
	{
		private IEvaluationWorkbook _workbook;

		private EvaluationCache _cache;

		private int _workbookIx;

		private IEvaluationListener _evaluationListener;

		private Hashtable _sheetIndexesBySheet;

		private Dictionary<string, int> _sheetIndexesByName;

		private CollaboratingWorkbooksEnvironment _collaboratingWorkbookEnvironment;

		private IStabilityClassifier _stabilityClassifier;

		private UDFFinder _udfFinder;

		private bool _ignoreMissingWorkbooks;

		/// whether print detailed messages about the next formula evaluation
		private bool dbgEvaluationOutputForNextEval;

		private POILogger EVAL_LOG = POILogFactory.GetLogger("POI.FormulaEval");

		private int dbgEvaluationOutputIndent = -1;

		internal IEvaluationWorkbook Workbook => _workbook;

		/// Whether to ignore missing references to external workbooks and
		/// use cached formula results in the main workbook instead.
		/// <p>
		/// In some cases exetrnal workbooks referenced by formulas in the main workbook are not avaiable.
		/// With this method you can control how POI handles such missing references:
		/// <ul>
		///     <li>by default ignoreMissingWorkbooks=false and POI throws {@link WorkbookNotFoundException}
		///     if an external reference cannot be resolved</li>
		///     <li>if ignoreMissingWorkbooks=true then POI uses cached formula result
		///     that already exists in the main workbook</li>
		/// </ul>
		///             </p>
		/// @param ignore whether to ignore missing references to external workbooks
		/// @see <a href="https://issues.apache.org/bugzilla/show_bug.cgi?id=52575">Bug 52575 for details</a>
		public bool IgnoreMissingWorkbooks
		{
			get
			{
				return _ignoreMissingWorkbooks;
			}
			set
			{
				_ignoreMissingWorkbooks = value;
			}
		}

		public bool DebugEvaluationOutputForNextEval
		{
			get
			{
				return dbgEvaluationOutputForNextEval;
			}
			set
			{
				dbgEvaluationOutputForNextEval = value;
			}
		}

		public WorkbookEvaluator(IEvaluationWorkbook workbook, IStabilityClassifier stabilityClassifier, UDFFinder udfFinder)
			: this(workbook, null, stabilityClassifier, udfFinder)
		{
		}

		public WorkbookEvaluator(IEvaluationWorkbook workbook, IEvaluationListener evaluationListener, IStabilityClassifier stabilityClassifier, UDFFinder udfFinder)
		{
			_workbook = workbook;
			_evaluationListener = evaluationListener;
			_cache = new EvaluationCache(evaluationListener);
			_sheetIndexesBySheet = new Hashtable();
			_sheetIndexesByName = new Dictionary<string, int>();
			_collaboratingWorkbookEnvironment = CollaboratingWorkbooksEnvironment.EMPTY;
			_workbookIx = 0;
			_stabilityClassifier = stabilityClassifier;
			AggregatingUDFFinder aggregatingUDFFinder = (workbook == null) ? null : ((AggregatingUDFFinder)workbook.GetUDFFinder());
			if (aggregatingUDFFinder != null && udfFinder != null)
			{
				aggregatingUDFFinder.Add(udfFinder);
			}
			_udfFinder = aggregatingUDFFinder;
		}

		/// also for debug use. Used in ToString methods
		public string GetSheetName(int sheetIndex)
		{
			return _workbook.GetSheetName(sheetIndex);
		}

		public WorkbookEvaluator GetOtherWorkbookEvaluator(string workbookName)
		{
			return _collaboratingWorkbookEnvironment.GetWorkbookEvaluator(workbookName);
		}

		internal IEvaluationSheet GetSheet(int sheetIndex)
		{
			return _workbook.GetSheet(sheetIndex);
		}

		internal IEvaluationName GetName(string name, int sheetIndex)
		{
			NamePtg namePtg = _workbook.GetName(name, sheetIndex).CreatePtg();
			if (namePtg == null)
			{
				return null;
			}
			return _workbook.GetName(namePtg);
		}

		private static bool IsDebugLogEnabled()
		{
			return false;
		}

		private static bool IsInfoLogEnabled()
		{
			return true;
		}

		private static void LogDebug(string s)
		{
			IsDebugLogEnabled();
		}

		private static void LogInfo(string s)
		{
			if (IsInfoLogEnabled())
			{
				Trace.WriteLine(s);
			}
		}

		public void AttachToEnvironment(CollaboratingWorkbooksEnvironment collaboratingWorkbooksEnvironment, EvaluationCache cache, int workbookIx)
		{
			_collaboratingWorkbookEnvironment = collaboratingWorkbooksEnvironment;
			_cache = cache;
			_workbookIx = workbookIx;
		}

		public CollaboratingWorkbooksEnvironment GetEnvironment()
		{
			return _collaboratingWorkbookEnvironment;
		}

		public void DetachFromEnvironment()
		{
			_collaboratingWorkbookEnvironment = CollaboratingWorkbooksEnvironment.EMPTY;
			_cache = new EvaluationCache(_evaluationListener);
			_workbookIx = 0;
		}

		public IEvaluationListener GetEvaluationListener()
		{
			return _evaluationListener;
		}

		/// Should be called whenever there are Changes To input cells in the evaluated workbook.
		/// Failure To call this method after changing cell values will cause incorrect behaviour
		/// of the evaluate~ methods of this class
		public void ClearAllCachedResultValues()
		{
			_cache.Clear();
			_sheetIndexesBySheet.Clear();
		}

		/// Should be called To tell the cell value cache that the specified (value or formula) cell 
		/// Has Changed.
		public void NotifyUpdateCell(IEvaluationCell cell)
		{
			int sheetIndex = GetSheetIndex(cell.Sheet);
			_cache.NotifyUpdateCell(_workbookIx, sheetIndex, cell);
		}

		/// Should be called To tell the cell value cache that the specified cell Has just been
		/// deleted. 
		public void NotifyDeleteCell(IEvaluationCell cell)
		{
			int sheetIndex = GetSheetIndex(cell.Sheet);
			_cache.NotifyDeleteCell(_workbookIx, sheetIndex, cell);
		}

		public int GetSheetIndex(IEvaluationSheet sheet)
		{
			object obj = _sheetIndexesBySheet[sheet];
			if (obj == null)
			{
				int sheetIndex = _workbook.GetSheetIndex(sheet);
				if (sheetIndex < 0)
				{
					throw new Exception("Specified sheet from a different book");
				}
				obj = sheetIndex;
				_sheetIndexesBySheet[sheet] = obj;
			}
			return (int)obj;
		}

		internal int GetSheetIndexByExternIndex(int externSheetIndex)
		{
			return _workbook.ConvertFromExternSheetIndex(externSheetIndex);
		}

		/// Case-insensitive.
		/// @return -1 if sheet with specified name does not exist
		public int GetSheetIndex(string sheetName)
		{
			int num;
			if (_sheetIndexesByName.ContainsKey(sheetName))
			{
				num = _sheetIndexesByName[sheetName];
			}
			else
			{
				int sheetIndex = _workbook.GetSheetIndex(sheetName);
				if (sheetIndex < 0)
				{
					return -1;
				}
				num = sheetIndex;
				_sheetIndexesByName[sheetName] = num;
			}
			return num;
		}

		public ValueEval Evaluate(IEvaluationCell srcCell)
		{
			int sheetIndex = GetSheetIndex(srcCell.Sheet);
			return EvaluateAny(srcCell, sheetIndex, srcCell.RowIndex, srcCell.ColumnIndex, new EvaluationTracker(_cache));
		}

		/// @return never <c>null</c>, never {@link BlankEval}
		private ValueEval EvaluateAny(IEvaluationCell srcCell, int sheetIndex, int rowIndex, int columnIndex, EvaluationTracker tracker)
		{
			bool flag = _stabilityClassifier == null || !_stabilityClassifier.IsCellFinal(sheetIndex, rowIndex, columnIndex);
			ValueEval valueFromNonFormulaCell;
			if (srcCell == null || srcCell.CellType != CellType.Formula)
			{
				valueFromNonFormulaCell = GetValueFromNonFormulaCell(srcCell);
				if (flag)
				{
					tracker.AcceptPlainValueDependency(_workbookIx, sheetIndex, rowIndex, columnIndex, valueFromNonFormulaCell);
				}
				return valueFromNonFormulaCell;
			}
			FormulaCellCacheEntry orCreateFormulaCellEntry = _cache.GetOrCreateFormulaCellEntry(srcCell);
			if (flag || orCreateFormulaCellEntry.IsInputSensitive)
			{
				tracker.AcceptFormulaDependency(orCreateFormulaCellEntry);
			}
			IEvaluationListener evaluationListener = _evaluationListener;
			if (orCreateFormulaCellEntry.GetValue() != null)
			{
				evaluationListener?.OnCacheHit(sheetIndex, rowIndex, columnIndex, orCreateFormulaCellEntry.GetValue());
				return orCreateFormulaCellEntry.GetValue();
			}
			if (!tracker.StartEvaluate(orCreateFormulaCellEntry))
			{
				return ErrorEval.CIRCULAR_REF_ERROR;
			}
			OperationEvaluationContext ec = new OperationEvaluationContext(this, _workbook, sheetIndex, rowIndex, columnIndex, tracker);
			try
			{
				Ptg[] formulaTokens = _workbook.GetFormulaTokens(srcCell);
				if (evaluationListener == null)
				{
					valueFromNonFormulaCell = EvaluateFormula(ec, formulaTokens);
				}
				else
				{
					evaluationListener.OnStartEvaluate(srcCell, orCreateFormulaCellEntry);
					valueFromNonFormulaCell = EvaluateFormula(ec, formulaTokens);
					evaluationListener.OnEndEvaluate(orCreateFormulaCellEntry, valueFromNonFormulaCell);
				}
				tracker.UpdateCacheResult(valueFromNonFormulaCell);
			}
			catch (NotImplementedException inner)
			{
				throw AddExceptionInfo(inner, sheetIndex, rowIndex, columnIndex);
			}
			catch (RuntimeException ex)
			{
				if (!(ex.InnerException is WorkbookNotFoundException) || !_ignoreMissingWorkbooks)
				{
					throw ex;
				}
				LogInfo(ex.InnerException.Message + " - Continuing with cached value!");
				switch (srcCell.CachedFormulaResultType)
				{
				case CellType.Numeric:
					valueFromNonFormulaCell = new NumberEval(srcCell.NumericCellValue);
					break;
				case CellType.String:
					valueFromNonFormulaCell = new StringEval(srcCell.StringCellValue);
					break;
				case CellType.Blank:
					valueFromNonFormulaCell = BlankEval.instance;
					break;
				case CellType.Boolean:
					valueFromNonFormulaCell = BoolEval.ValueOf(srcCell.BooleanCellValue);
					break;
				case CellType.Error:
					valueFromNonFormulaCell = ErrorEval.ValueOf(srcCell.ErrorCellValue);
					break;
				default:
					throw new RuntimeException("Unexpected cell type '" + srcCell.CellType + "' found!");
				}
			}
			finally
			{
				tracker.EndEvaluate(orCreateFormulaCellEntry);
			}
			if (IsDebugLogEnabled())
			{
				string sheetName = GetSheetName(sheetIndex);
				CellReference cellReference = new CellReference(rowIndex, columnIndex);
				LogDebug("Evaluated " + sheetName + "!" + cellReference.FormatAsString() + " To " + orCreateFormulaCellEntry.GetValue());
			}
			return valueFromNonFormulaCell;
		}

		/// Adds the current cell reference to the exception for easier debugging.
		/// Would be nice to get the formula text as well, but that seems to require
		/// too much digging around and casting to get the FormulaRenderingWorkbook.
		private NotImplementedException AddExceptionInfo(NotImplementedException inner, int sheetIndex, int rowIndex, int columnIndex)
		{
			try
			{
				string sheetName = _workbook.GetSheetName(sheetIndex);
				CellReference cellReference = new CellReference(sheetName, rowIndex, columnIndex, pAbsRow: false, pAbsCol: false);
				string message = "Error evaluating cell " + cellReference.FormatAsString();
				return new NotImplementedException(message, inner);
			}
			catch (Exception)
			{
				return inner;
			}
		}

		/// Gets the value from a non-formula cell.
		/// @param cell may be <c>null</c>
		/// @return {@link BlankEval} if cell is <c>null</c> or blank, never <c>null</c>
		internal static ValueEval GetValueFromNonFormulaCell(IEvaluationCell cell)
		{
			if (cell != null)
			{
				CellType cellType = cell.CellType;
				switch (cellType)
				{
				case CellType.Numeric:
					return new NumberEval(cell.NumericCellValue);
				case CellType.String:
					return new StringEval(cell.StringCellValue);
				case CellType.Boolean:
					return BoolEval.ValueOf(cell.BooleanCellValue);
				case CellType.Blank:
					return BlankEval.instance;
				case CellType.Error:
					return ErrorEval.ValueOf(cell.ErrorCellValue);
				default:
					throw new Exception("Unexpected cell type (" + cellType + ")");
				}
			}
			return BlankEval.instance;
		}

		public ValueEval EvaluateFormula(OperationEvaluationContext ec, Ptg[] ptgs)
		{
			string text = "";
			if (dbgEvaluationOutputForNextEval)
			{
				dbgEvaluationOutputIndent = 1;
				dbgEvaluationOutputForNextEval = false;
			}
			if (dbgEvaluationOutputIndent > 0)
			{
				text = "                                                                                                    ";
				text = text.Substring(0, Math.Min(text.Length, dbgEvaluationOutputIndent * 2));
				EVAL_LOG.Log(5, text + "- evaluateFormula('" + ec.GetRefEvaluatorForCurrentSheet().SheetName + "'/" + new CellReference(ec.RowIndex, ec.ColumnIndex).FormatAsString() + "): " + Arrays.ToString(ptgs).Replace("\\Qorg.apache.poi.ss.formula.ptg.\\E", ""));
				dbgEvaluationOutputIndent++;
			}
			Stack<ValueEval> stack = new Stack<ValueEval>();
			int i = 0;
			for (int num = ptgs.Length; i < num; i++)
			{
				Ptg ptg = ptgs[i];
				if (dbgEvaluationOutputIndent > 0)
				{
					EVAL_LOG.Log(3, text + "  * ptg " + i + ": " + ptg.ToString());
				}
				if (ptg is AttrPtg)
				{
					AttrPtg attrPtg = (AttrPtg)ptg;
					if (attrPtg.IsSum)
					{
						ptg = FuncVarPtg.SUM;
					}
					if (attrPtg.IsOptimizedChoose)
					{
						ValueEval arg = stack.Pop();
						int[] jumpTable = attrPtg.JumpTable;
						int num2 = jumpTable.Length;
						int num4;
						try
						{
							int num3 = Choose.EvaluateFirstArg(arg, ec.RowIndex, ec.ColumnIndex);
							if (num3 < 1 || num3 > num2)
							{
								stack.Push(ErrorEval.VALUE_INVALID);
								num4 = attrPtg.ChooseFuncOffset + 4;
							}
							else
							{
								num4 = jumpTable[num3 - 1];
							}
						}
						catch (EvaluationException ex)
						{
							stack.Push(ex.GetErrorEval());
							num4 = attrPtg.ChooseFuncOffset + 4;
						}
						num4 -= num2 * 2 + 2;
						i += CountTokensToBeSkipped(ptgs, i, num4);
						continue;
					}
					if (attrPtg.IsOptimizedIf)
					{
						ValueEval arg2 = stack.Pop();
						bool flag;
						try
						{
							flag = If.EvaluateFirstArg(arg2, ec.RowIndex, ec.ColumnIndex);
						}
						catch (EvaluationException ex2)
						{
							stack.Push(ex2.GetErrorEval());
							int data = attrPtg.Data;
							i += CountTokensToBeSkipped(ptgs, i, data);
							attrPtg = (AttrPtg)ptgs[i];
							data = attrPtg.Data + 1;
							i += CountTokensToBeSkipped(ptgs, i, data);
							continue;
						}
						if (!flag)
						{
							int data2 = attrPtg.Data;
							i += CountTokensToBeSkipped(ptgs, i, data2);
							Ptg ptg2 = ptgs[i + 1];
							if (ptgs[i] is AttrPtg && ptg2 is FuncVarPtg)
							{
								i++;
								stack.Push(BoolEval.FALSE);
							}
						}
						continue;
					}
					if (attrPtg.IsSkip)
					{
						int distInBytes = attrPtg.Data + 1;
						i += CountTokensToBeSkipped(ptgs, i, distInBytes);
						if (stack.Peek() == MissingArgEval.instance)
						{
							stack.Pop();
							stack.Push(BlankEval.instance);
						}
						continue;
					}
				}
				if (!(ptg is ControlPtg) && !(ptg is MemFuncPtg) && !(ptg is MemAreaPtg) && !(ptg is MemErrPtg))
				{
					ValueEval valueEval2;
					if (ptg is OperationPtg)
					{
						OperationPtg operationPtg = (OperationPtg)ptg;
						if (operationPtg is UnionPtg)
						{
							continue;
						}
						int numberOfOperands = operationPtg.NumberOfOperands;
						ValueEval[] array = new ValueEval[numberOfOperands];
						for (int num5 = numberOfOperands - 1; num5 >= 0; num5--)
						{
							ValueEval valueEval = array[num5] = stack.Pop();
						}
						valueEval2 = OperationEvaluatorFactory.Evaluate(operationPtg, array, ec);
					}
					else
					{
						valueEval2 = GetEvalForPtg(ptg, ec);
					}
					if (valueEval2 == null)
					{
						throw new Exception("Evaluation result must not be null");
					}
					stack.Push(valueEval2);
					if (dbgEvaluationOutputIndent > 0)
					{
						EVAL_LOG.Log(3, text + "    = " + valueEval2.ToString());
					}
				}
			}
			ValueEval evaluationResult = stack.Pop();
			if (stack.Count != 0)
			{
				throw new InvalidOperationException("evaluation stack not empty");
			}
			ValueEval valueEval3 = DereferenceResult(evaluationResult, ec.RowIndex, ec.ColumnIndex);
			if (dbgEvaluationOutputIndent > 0)
			{
				EVAL_LOG.Log(3, text + "finshed eval of " + new CellReference(ec.RowIndex, ec.ColumnIndex).FormatAsString() + ": " + valueEval3.ToString());
				dbgEvaluationOutputIndent--;
				if (dbgEvaluationOutputIndent == 1)
				{
					dbgEvaluationOutputIndent = -1;
				}
			}
			return valueEval3;
		}

		/// Calculates the number of tokens that the evaluator should skip upon reaching a tAttrSkip.
		///
		/// @return the number of tokens (starting from <c>startIndex+1</c>) that need to be skipped
		/// to achieve the specified <c>distInBytes</c> skip distance.
		private static int CountTokensToBeSkipped(Ptg[] ptgs, int startIndex, int distInBytes)
		{
			int num = distInBytes;
			int num2 = startIndex;
			while (num != 0)
			{
				num2++;
				num -= ptgs[num2].Size;
				if (num < 0)
				{
					throw new Exception("Bad skip distance (wrong token size calculation).");
				}
				if (num2 >= ptgs.Length)
				{
					throw new Exception("Skip distance too far (ran out of formula tokens).");
				}
			}
			return num2 - startIndex;
		}

		/// Dereferences a single value from any AreaEval or RefEval evaluation result.
		/// If the supplied evaluationResult is just a plain value, it is returned as-is.
		/// @return a <c>NumberEval</c>, <c>StringEval</c>, <c>BoolEval</c>,
		///  <c>BlankEval</c> or <c>ErrorEval</c>. Never <c>null</c>.
		public static ValueEval DereferenceResult(ValueEval evaluationResult, int srcRowNum, int srcColNum)
		{
			ValueEval singleValue;
			try
			{
				singleValue = OperandResolver.GetSingleValue(evaluationResult, srcRowNum, srcColNum);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			if (singleValue == BlankEval.instance)
			{
				return NumberEval.ZERO;
			}
			return singleValue;
		}

		/// returns an appropriate Eval impl instance for the Ptg. The Ptg must be
		/// one of: Area3DPtg, AreaPtg, ReferencePtg, Ref3DPtg, IntPtg, NumberPtg,
		/// StringPtg, BoolPtg <br />special Note: OperationPtg subtypes cannot be
		/// passed here!
		private ValueEval GetEvalForPtg(Ptg ptg, OperationEvaluationContext ec)
		{
			if (ptg is NamePtg)
			{
				NamePtg namePtg = (NamePtg)ptg;
				IEvaluationName name = _workbook.GetName(namePtg);
				if (name.IsFunctionName)
				{
					return new NameEval(name.NameText);
				}
				if (name.HasFormula)
				{
					return EvaluateNameFormula(name.NameDefinition, ec);
				}
				throw new Exception("Don't now how To evalate name '" + name.NameText + "'");
			}
			if (ptg is NameXPtg)
			{
				return ec.GetNameXEval((NameXPtg)ptg);
			}
			if (ptg is IntPtg)
			{
				return new NumberEval((double)((IntPtg)ptg).Value);
			}
			if (ptg is NumberPtg)
			{
				return new NumberEval(((NumberPtg)ptg).Value);
			}
			if (ptg is StringPtg)
			{
				return new StringEval(((StringPtg)ptg).Value);
			}
			if (ptg is BoolPtg)
			{
				return BoolEval.ValueOf(((BoolPtg)ptg).Value);
			}
			if (ptg is ErrPtg)
			{
				return ErrorEval.ValueOf(((ErrPtg)ptg).ErrorCode);
			}
			if (ptg is MissingArgPtg)
			{
				return MissingArgEval.instance;
			}
			if (ptg is AreaErrPtg || ptg is RefErrorPtg || ptg is DeletedArea3DPtg || ptg is DeletedRef3DPtg)
			{
				return ErrorEval.REF_INVALID;
			}
			if (ptg is Ref3DPtg)
			{
				Ref3DPtg ref3DPtg = (Ref3DPtg)ptg;
				return ec.GetRef3DEval(ref3DPtg.Row, ref3DPtg.Column, ref3DPtg.ExternSheetIndex);
			}
			if (ptg is Area3DPtg)
			{
				Area3DPtg area3DPtg = (Area3DPtg)ptg;
				return ec.GetArea3DEval(area3DPtg.FirstRow, area3DPtg.FirstColumn, area3DPtg.LastRow, area3DPtg.LastColumn, area3DPtg.ExternSheetIndex);
			}
			if (ptg is RefPtg)
			{
				RefPtg refPtg = (RefPtg)ptg;
				return ec.GetRefEval(refPtg.Row, refPtg.Column);
			}
			if (ptg is AreaPtg)
			{
				AreaPtg areaPtg = (AreaPtg)ptg;
				return ec.GetAreaEval(areaPtg.FirstRow, areaPtg.FirstColumn, areaPtg.LastRow, areaPtg.LastColumn);
			}
			if (ptg is UnknownPtg)
			{
				throw new RuntimeException("UnknownPtg not allowed");
			}
			if (ptg is ExpPtg)
			{
				throw new RuntimeException("ExpPtg currently not supported");
			}
			throw new RuntimeException("Unexpected ptg class (" + ptg.GetType().Name + ")");
		}

		internal ValueEval EvaluateNameFormula(Ptg[] ptgs, OperationEvaluationContext ec)
		{
			if (ptgs.Length == 1)
			{
				return GetEvalForPtg(ptgs[0], ec);
			}
			return EvaluateFormula(ec, ptgs);
		}

		/// Used by the lazy ref evals whenever they need To Get the value of a contained cell.
		public ValueEval EvaluateReference(IEvaluationSheet sheet, int sheetIndex, int rowIndex, int columnIndex, EvaluationTracker tracker)
		{
			IEvaluationCell cell = sheet.GetCell(rowIndex, columnIndex);
			return EvaluateAny(cell, sheetIndex, rowIndex, columnIndex, tracker);
		}

		public FreeRefFunction FindUserDefinedFunction(string functionName)
		{
			return _udfFinder.FindFunction(functionName);
		}

		/// Return a collection of functions that POI can evaluate
		///
		/// @return names of functions supported by POI
		public static List<string> GetSupportedFunctionNames()
		{
			List<string> list = new List<string>();
			list.AddRange(FunctionEval.GetSupportedFunctionNames());
			list.AddRange(AnalysisToolPak.GetSupportedFunctionNames());
			return list;
		}

		/// Return a collection of functions that POI does not support
		///
		/// @return names of functions NOT supported by POI
		public static List<string> GetNotSupportedFunctionNames()
		{
			List<string> list = new List<string>();
			list.AddRange(FunctionEval.GetNotSupportedFunctionNames());
			list.AddRange(AnalysisToolPak.GetNotSupportedFunctionNames());
			return list;
		}

		/// Register a ATP function in runtime.
		///
		/// @param name  the function name
		/// @param func  the functoin to register
		/// @throws IllegalArgumentException if the function is unknown or already  registered.
		/// @since 3.8 beta6
		public static void RegisterFunction(string name, FreeRefFunction func)
		{
			AnalysisToolPak.RegisterFunction(name, func);
		}

		/// Register a function in runtime.
		///
		/// @param name  the function name
		/// @param func  the functoin to register
		/// @throws IllegalArgumentException if the function is unknown or already  registered.
		/// @since 3.8 beta6
		public static void RegisterFunction(string name, NPOI.SS.Formula.Functions.Function func)
		{
			FunctionEval.RegisterFunction(name, func);
		}
	}
}
