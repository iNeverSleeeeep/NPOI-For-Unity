namespace NPOI.SS.Formula
{
	public class ExternalName
	{
		private string _nameName;

		private int _nameNumber;

		private int _ix;

		public string Name => _nameName;

		public int Number => _nameNumber;

		public int Ix => _ix;

		public ExternalName(string nameName, int nameNumber, int ix)
		{
			_nameName = nameName;
			_nameNumber = nameNumber;
			_ix = ix;
		}
	}
}
