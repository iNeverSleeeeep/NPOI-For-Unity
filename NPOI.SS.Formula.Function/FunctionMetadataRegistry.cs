using System.Collections;

namespace NPOI.SS.Formula.Function
{
	/// Allows clients to Get <c>FunctionMetadata</c> instances for any built-in function of Excel.
	///
	/// @author Josh Micich
	public class FunctionMetadataRegistry
	{
		/// The name of the IF function (i.e. "IF").  Extracted as a constant for clarity.
		public const string FUNCTION_NAME_IF = "IF";

		public const int FUNCTION_INDEX_IF = 1;

		public const short FUNCTION_INDEX_SUM = 4;

		public const int FUNCTION_INDEX_CHOOSE = 100;

		public const short FUNCTION_INDEX_INDIRECT = 148;

		public const short FUNCTION_INDEX_EXTERNAL = 255;

		private static FunctionMetadataRegistry _instance;

		private FunctionMetadata[] _functionDataByIndex;

		private Hashtable _functionDataByName;

		private static FunctionMetadataRegistry GetInstance()
		{
			if (_instance == null)
			{
				_instance = FunctionMetadataReader.CreateRegistry();
			}
			return _instance;
		}

		public FunctionMetadataRegistry(FunctionMetadata[] functionDataByIndex, Hashtable functionDataByName)
		{
			_functionDataByIndex = functionDataByIndex;
			_functionDataByName = functionDataByName;
		}

		public ICollection GetAllFunctionNames()
		{
			return _functionDataByName.Keys;
		}

		public static FunctionMetadata GetFunctionByIndex(int index)
		{
			return GetInstance().GetFunctionByIndexInternal(index);
		}

		private FunctionMetadata GetFunctionByIndexInternal(int index)
		{
			return _functionDataByIndex[index];
		}

		/// Resolves a built-in function index. 
		/// @param name uppercase function name
		/// @return a negative value if the function name is not found.
		/// This typically occurs for external functions.
		public static short LookupIndexByName(string name)
		{
			FunctionMetadata functionByNameInternal = GetInstance().GetFunctionByNameInternal(name);
			if (functionByNameInternal == null)
			{
				return -1;
			}
			return (short)functionByNameInternal.Index;
		}

		private FunctionMetadata GetFunctionByNameInternal(string name)
		{
			return (FunctionMetadata)_functionDataByName[name];
		}

		public static FunctionMetadata GetFunctionByName(string name)
		{
			return GetInstance().GetFunctionByNameInternal(name);
		}
	}
}
