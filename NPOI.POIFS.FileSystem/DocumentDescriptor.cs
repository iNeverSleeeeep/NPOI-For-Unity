using System;
using System.Text;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// Class DocumentDescriptor
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class DocumentDescriptor
	{
		private POIFSDocumentPath path;

		private string name;

		private int hashcode;

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <value>The path.</value>
		public string Path => path.ToString();

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name => name;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.FileSystem.DocumentDescriptor" /> class.
		/// </summary>
		/// <param name="path">the Document path</param>
		/// <param name="name">the Document name</param>
		public DocumentDescriptor(POIFSDocumentPath path, string name)
		{
			if (path == null)
			{
				throw new NullReferenceException("path must not be null");
			}
			if (name == null)
			{
				throw new NullReferenceException("name must not be null");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name cannot be empty");
			}
			this.path = path;
			this.name = name;
		}

		/// <summary>
		/// equality. Two DocumentDescriptor instances are equal if they
		/// have equal paths and names
		/// </summary>
		/// <param name="o">the object we're checking equality for</param>
		/// <returns>true if the object is equal to this object</returns>
		public override bool Equals(object o)
		{
			bool result = false;
			if (o != null && o.GetType() == GetType())
			{
				if (this == o)
				{
					result = true;
				}
				else
				{
					DocumentDescriptor documentDescriptor = (DocumentDescriptor)o;
					result = (path.Equals(documentDescriptor.path) && name.Equals(documentDescriptor.name));
				}
			}
			return result;
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// hashcode
		/// </returns>
		public override int GetHashCode()
		{
			if (hashcode == 0)
			{
				hashcode = (path.GetHashCode() ^ name.GetHashCode());
			}
			return hashcode;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(40 * (path.Length + 1));
			for (int i = 0; i < path.Length; i++)
			{
				stringBuilder.Append(path.GetComponent(i)).Append("/");
			}
			stringBuilder.Append(name);
			return stringBuilder.ToString();
		}
	}
}
