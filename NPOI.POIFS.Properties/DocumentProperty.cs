using NPOI.POIFS.FileSystem;

namespace NPOI.POIFS.Properties
{
	/// <summary>
	/// Trivial extension of Property for POIFSDocuments
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class DocumentProperty : Property
	{
		private POIFSDocument _document;

		/// <summary>
		/// Gets or sets the document.
		/// </summary>
		/// <value>the associated POIFSDocument</value>
		public POIFSDocument Document
		{
			get
			{
				return _document;
			}
			set
			{
				_document = value;
			}
		}

		/// <summary>
		/// Determines whether this instance is directory.
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if this instance is directory; otherwise, <c>false</c>.
		/// </returns>
		public override bool IsDirectory => false;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Properties.DocumentProperty" /> class.
		/// </summary>
		/// <param name="name">POIFSDocument name</param>
		/// <param name="size">POIFSDocument size</param>
		public DocumentProperty(string name, int size)
		{
			_document = null;
			base.Name = name;
			Size = size;
			base.NodeColor = 1;
			base.PropertyType = 2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Properties.DocumentProperty" /> class.
		/// </summary>
		/// <param name="index">index number</param>
		/// <param name="array">byte data</param>
		/// <param name="offset">offset into byte data</param> 
		public DocumentProperty(int index, byte[] array, int offset)
			: base(index, array, offset)
		{
			_document = null;
		}

		/// <summary>
		/// Perform whatever activities need to be performed prior to
		/// writing
		/// </summary>
		public override void PreWrite()
		{
		}
	}
}
