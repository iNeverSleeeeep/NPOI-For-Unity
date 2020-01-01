using NPOI.SS.Formula.Functions;
using System.Collections.Generic;

namespace NPOI.SS.Formula.Udf
{
	/// Collects Add-in libraries and VB macro functions toGether into one UDF Finder
	///
	/// @author PUdalau
	public class AggregatingUDFFinder : UDFFinder
	{
		private List<UDFFinder> _usedToolPacks = new List<UDFFinder>();

		public AggregatingUDFFinder(params UDFFinder[] usedToolPacks)
		{
			_usedToolPacks = new List<UDFFinder>(usedToolPacks.Length);
			_usedToolPacks.AddRange(usedToolPacks);
		}

		/// <summary>
		/// Returns executor by specified name. 
		/// </summary>
		/// <param name="name">Name of function.</param>
		/// <returns>Function executor. null if not found</returns>
		public override FreeRefFunction FindFunction(string name)
		{
			foreach (UDFFinder usedToolPack in _usedToolPacks)
			{
				FreeRefFunction freeRefFunction = usedToolPack.FindFunction(name);
				if (freeRefFunction != null)
				{
					return freeRefFunction;
				}
			}
			return null;
		}

		/// <summary>
		/// Add a new toolpack
		/// </summary>
		/// <param name="toolPack"></param>
		public void Add(UDFFinder toolPack)
		{
			_usedToolPacks.Add(toolPack);
		}
	}
}
