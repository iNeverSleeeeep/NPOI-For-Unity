using System;

namespace NPOI
{
	[Serializable]
	public class EncryptedDocumentException : InvalidOperationException
	{
		public EncryptedDocumentException(string s)
			: base(s)
		{
		}
	}
}
