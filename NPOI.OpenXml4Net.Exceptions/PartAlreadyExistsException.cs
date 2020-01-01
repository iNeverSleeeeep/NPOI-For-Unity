using System;

namespace NPOI.OpenXml4Net.Exceptions
{
	public class PartAlreadyExistsException : InvalidOperationException
	{
		public PartAlreadyExistsException(string message)
			: base(message)
		{
		}
	}
}
