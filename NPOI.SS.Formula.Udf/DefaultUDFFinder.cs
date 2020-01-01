using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;

namespace NPOI.SS.Formula.Udf
{
	/// Default UDF Finder - for Adding your own user defined functions.
	///
	/// @author PUdalau
	public class DefaultUDFFinder : UDFFinder
	{
		private Dictionary<string, FreeRefFunction> _functionsByName;

		public DefaultUDFFinder(string[] functionNames, FreeRefFunction[] functionImpls)
		{
			int num = functionNames.Length;
			if (functionImpls.Length != num)
			{
				throw new ArgumentException("Mismatch in number of function names and implementations");
			}
			Dictionary<string, FreeRefFunction> dictionary = new Dictionary<string, FreeRefFunction>(num * 3 / 2);
			for (int i = 0; i < functionImpls.Length; i++)
			{
				dictionary[functionNames[i].ToUpper()] = functionImpls[i];
			}
			_functionsByName = dictionary;
		}

		public override FreeRefFunction FindFunction(string name)
		{
			if (!_functionsByName.ContainsKey(name.ToUpper()))
			{
				return null;
			}
			return _functionsByName[name.ToUpper()];
		}
	}
}
