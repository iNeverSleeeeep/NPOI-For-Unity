using System.Text;

namespace NPOI.SS.Formula.Function
{
	/// Holds information about Excel built-in functions.
	///
	/// @author Josh Micich
	public class FunctionMetadata
	{
		private const short FUNCTION_MAX_PARAMS = 30;

		private int _index;

		private string _name;

		private int _minParams;

		private int _maxParams;

		private byte _returnClassCode;

		private byte[] _parameterClassCodes;

		public int Index => _index;

		public string Name => _name;

		public int MinParams => _minParams;

		public int MaxParams => _maxParams;

		public bool HasFixedArgsLength => _minParams == _maxParams;

		public byte ReturnClassCode => _returnClassCode;

		public byte[] ParameterClassCodes => (byte[])_parameterClassCodes.Clone();

		public bool HasUnlimitedVarags => 30 == _maxParams;

		internal FunctionMetadata(int index, string name, int minParams, int maxParams, byte returnClassCode, byte[] parameterClassCodes)
		{
			_index = index;
			_name = name;
			_minParams = minParams;
			_maxParams = maxParams;
			_returnClassCode = returnClassCode;
			_parameterClassCodes = parameterClassCodes;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(_index).Append(" ").Append(_name);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
