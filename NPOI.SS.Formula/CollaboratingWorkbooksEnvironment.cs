using System;
using System.Collections;
using System.Text;

namespace NPOI.SS.Formula
{
	/// Manages a collection of {@link WorkbookEvaluator}s, in order To support evaluation of formulas
	/// across spreadsheets.<p />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public class CollaboratingWorkbooksEnvironment
	{
		public static readonly CollaboratingWorkbooksEnvironment EMPTY = new CollaboratingWorkbooksEnvironment();

		private Hashtable _evaluatorsByName;

		private WorkbookEvaluator[] _evaluators;

		private bool _unhooked;

		private CollaboratingWorkbooksEnvironment()
		{
			_evaluatorsByName = new Hashtable();
			_evaluators = new WorkbookEvaluator[0];
		}

		public static void Setup(string[] workbookNames, WorkbookEvaluator[] evaluators)
		{
			int num = workbookNames.Length;
			if (evaluators.Length != num)
			{
				throw new ArgumentException("Number of workbook names is " + num + " but number of evaluators is " + evaluators.Length);
			}
			if (num < 1)
			{
				throw new ArgumentException("Must provide at least one collaborating worbook");
			}
			CollaboratingWorkbooksEnvironment env = new CollaboratingWorkbooksEnvironment(workbookNames, evaluators, num);
			HookNewEnvironment(evaluators, env);
		}

		private CollaboratingWorkbooksEnvironment(string[] workbookNames, WorkbookEvaluator[] evaluators, int nItems)
		{
			Hashtable hashtable = new Hashtable(nItems * 3 / 2);
			Hashtable hashtable2 = new Hashtable(nItems * 3 / 2);
			for (int i = 0; i < nItems; i++)
			{
				string text = workbookNames[i];
				WorkbookEvaluator workbookEvaluator = evaluators[i];
				if (hashtable.ContainsKey(text))
				{
					throw new ArgumentException("Duplicate workbook name '" + text + "'");
				}
				if (hashtable2.ContainsKey(workbookEvaluator))
				{
					string message = "Attempted To register same workbook under names '" + hashtable2[workbookEvaluator + "' and '" + text + "'"];
					throw new ArgumentException(message);
				}
				hashtable2[workbookEvaluator] = text;
				hashtable[text] = workbookEvaluator;
			}
			UnhookOldEnvironments(evaluators);
			_unhooked = false;
			_evaluators = evaluators;
			_evaluatorsByName = hashtable;
		}

		private static void HookNewEnvironment(WorkbookEvaluator[] evaluators, CollaboratingWorkbooksEnvironment env)
		{
			int num = evaluators.Length;
			IEvaluationListener evaluationListener = evaluators[0].GetEvaluationListener();
			for (int i = 0; i < num; i++)
			{
				if (evaluationListener != evaluators[i].GetEvaluationListener())
				{
					throw new Exception("Workbook evaluators must all have the same evaluation listener");
				}
			}
			EvaluationCache cache = new EvaluationCache(evaluationListener);
			for (int j = 0; j < num; j++)
			{
				evaluators[j].AttachToEnvironment(env, cache, j);
			}
		}

		private void UnhookOldEnvironments(WorkbookEvaluator[] evaluators)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < evaluators.Length; i++)
			{
				arrayList.Add(evaluators[i].GetEnvironment());
			}
			CollaboratingWorkbooksEnvironment[] array = new CollaboratingWorkbooksEnvironment[arrayList.Count];
			array = (CollaboratingWorkbooksEnvironment[])arrayList.ToArray(typeof(CollaboratingWorkbooksEnvironment));
			for (int j = 0; j < array.Length; j++)
			{
				array[j].Unhook();
			}
		}

		private void Unhook()
		{
			if (_evaluators.Length >= 1)
			{
				for (int i = 0; i < _evaluators.Length; i++)
				{
					_evaluators[i].DetachFromEnvironment();
				}
				_unhooked = true;
			}
		}

		public WorkbookEvaluator GetWorkbookEvaluator(string workbookName)
		{
			if (_unhooked)
			{
				throw new InvalidOperationException("This environment Has been unhooked");
			}
			if (!_evaluatorsByName.ContainsKey(workbookName))
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.Append("Could not resolve external workbook name '").Append(workbookName).Append("'.");
				if (_evaluators.Length < 1)
				{
					stringBuilder.Append(" Workbook environment has not been set up.");
				}
				else
				{
					stringBuilder.Append(" The following workbook names are valid: (");
					IEnumerator enumerator = _evaluatorsByName.Keys.GetEnumerator();
					int num = 0;
					while (enumerator.MoveNext())
					{
						if (num++ > 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append("'").Append(enumerator.Current).Append("'");
					}
					stringBuilder.Append(")");
				}
				throw new WorkbookNotFoundException(stringBuilder.ToString());
			}
			return (WorkbookEvaluator)_evaluatorsByName[workbookName];
		}
	}
}
