using System;

namespace NPOI.SS.Formula
{
	[Serializable]
	public class WorkbookNotFoundException : Exception
	{
		public WorkbookNotFoundException(string msg)
			: base(msg)
		{
		}
	}
}
