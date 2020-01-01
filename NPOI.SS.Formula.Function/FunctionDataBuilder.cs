using System;
using System.Collections;

namespace NPOI.SS.Formula.Function
{
	/// Temporarily collects <c>FunctionMetadata</c> instances for creation of a
	/// <c>FunctionMetadataRegistry</c>.
	///
	/// @author Josh Micich
	internal class FunctionDataBuilder
	{
		private int _maxFunctionIndex;

		private Hashtable _functionDataByName;

		private Hashtable _functionDataByIndex;

		/// stores indexes of all functions with footnotes (i.e. whose definitions might Change) 
		private ArrayList _mutatingFunctionIndexes;

		public FunctionDataBuilder(int sizeEstimate)
		{
			_maxFunctionIndex = -1;
			_functionDataByName = new Hashtable(sizeEstimate * 3 / 2);
			_functionDataByIndex = new Hashtable(sizeEstimate * 3 / 2);
			_mutatingFunctionIndexes = new ArrayList();
		}

		public void Add(int functionIndex, string functionName, int minParams, int maxParams, byte returnClassCode, byte[] parameterClassCodes, bool hasFootnote)
		{
			FunctionMetadata value = new FunctionMetadata(functionIndex, functionName, minParams, maxParams, returnClassCode, parameterClassCodes);
			if (functionIndex > _maxFunctionIndex)
			{
				_maxFunctionIndex = functionIndex;
			}
			FunctionMetadata functionMetadata = (FunctionMetadata)_functionDataByName[functionName];
			if (functionMetadata != null)
			{
				if (!hasFootnote || !_mutatingFunctionIndexes.Contains(functionIndex))
				{
					throw new Exception("Multiple entries for function name '" + functionName + "'");
				}
				_functionDataByIndex.Remove(functionMetadata.Index);
			}
			functionMetadata = (FunctionMetadata)_functionDataByIndex[functionIndex];
			if (functionMetadata != null)
			{
				if (!hasFootnote || !_mutatingFunctionIndexes.Contains(functionIndex))
				{
					throw new Exception("Multiple entries for function index (" + functionIndex + ")");
				}
				_functionDataByName.Remove(functionMetadata.Name);
			}
			if (hasFootnote)
			{
				_mutatingFunctionIndexes.Add(functionIndex);
			}
			_functionDataByIndex[functionIndex] = value;
			_functionDataByName[functionName] = value;
		}

		public FunctionMetadataRegistry Build()
		{
			int count = _functionDataByName.Count;
			IEnumerator enumerator = _functionDataByName.Values.GetEnumerator();
			FunctionMetadata[] array = new FunctionMetadata[_maxFunctionIndex + 1];
			while (enumerator.MoveNext())
			{
				FunctionMetadata functionMetadata = (FunctionMetadata)enumerator.Current;
				array[functionMetadata.Index] = functionMetadata;
			}
			return new FunctionMetadataRegistry(array, _functionDataByName);
		}
	}
}
