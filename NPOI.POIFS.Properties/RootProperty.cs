using NPOI.POIFS.Storage;

namespace NPOI.POIFS.Properties
{
	public class RootProperty : DirectoryProperty
	{
		private const string NAME = "Root Entry";

		/// <summary>
		/// Gets or sets the size of the document associated with this Property
		/// </summary>
		/// <value>the size of the document, in bytes</value>
		public override int Size
		{
			set
			{
				base.Size = SmallDocumentBlock.CalcSize(value);
			}
		}

		public RootProperty()
			: base("Root Entry")
		{
			base.NodeColor = 1;
			base.PropertyType = 5;
			base.StartBlock = -2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Properties.RootProperty" /> class.
		/// </summary>
		/// <param name="index">index number</param>
		/// <param name="array">byte data</param>
		/// <param name="offset">offset into byte data</param>
		public RootProperty(int index, byte[] array, int offset)
			: base(index, array, offset)
		{
		}
	}
}
