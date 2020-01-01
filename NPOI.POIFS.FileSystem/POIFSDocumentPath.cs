using System;
using System.IO;
using System.Text;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// Class POIFSDocumentPath
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class POIFSDocumentPath
	{
		private string[] components;

		private int hashcode;

		/// <summary>
		/// Gets the length.
		/// </summary>
		/// <value>the number of components</value>
		public virtual int Length => components.Length;

		/// <summary>
		/// Returns the path's parent or <c>null</c> if this path
		/// is the root path.
		/// </summary>
		/// <value>path of parent, or null if this path is the root path</value>
		public virtual POIFSDocumentPath Parent
		{
			get
			{
				int num = components.Length - 1;
				if (num < 0)
				{
					return null;
				}
				POIFSDocumentPath pOIFSDocumentPath = new POIFSDocumentPath(null);
				pOIFSDocumentPath.components = new string[num];
				Array.Copy(components, 0, pOIFSDocumentPath.components, 0, num);
				return pOIFSDocumentPath;
			}
		}

		/// <summary>
		/// simple constructor for the path of a document that is in the
		/// root of the POIFSFileSystem. The constructor that takes an
		/// array of Strings can also be used to create such a
		/// POIFSDocumentPath by passing it a null or empty String array
		/// </summary>
		public POIFSDocumentPath()
		{
			components = new string[0];
		}

		/// <summary>
		/// constructor for the path of a document that is not in the root
		/// of the POIFSFileSystem
		/// </summary>
		/// <param name="components">the Strings making up the path to a document.
		/// The Strings must be ordered as they appear in
		/// the directory hierarchy of the the document
		/// -- the first string must be the name of a
		/// directory in the root of the POIFSFileSystem,
		/// and every Nth (for N &gt; 1) string thereafter
		/// must be the name of a directory in the
		/// directory identified by the (N-1)th string.
		/// If the components parameter is null or has
		/// zero length, the POIFSDocumentPath is
		/// appropriate for a document that is in the
		/// root of a POIFSFileSystem</param>
		public POIFSDocumentPath(string[] components)
		{
			if (components == null)
			{
				this.components = new string[0];
				return;
			}
			this.components = new string[components.Length];
			int num = 0;
			while (true)
			{
				if (num >= components.Length)
				{
					return;
				}
				if (components[num] == null || components[num].Length == 0)
				{
					break;
				}
				this.components[num] = components[num];
				num++;
			}
			throw new ArgumentException("components cannot contain null or empty strings");
		}

		/// <summary>
		/// constructor that adds additional subdirectories to an existing
		/// path
		/// </summary>
		/// <param name="path">the existing path</param>
		/// <param name="components">the additional subdirectory names to be added</param>
		public POIFSDocumentPath(POIFSDocumentPath path, string[] components)
		{
			if (components == null)
			{
				this.components = new string[path.components.Length];
			}
			else
			{
				this.components = new string[path.components.Length + components.Length];
			}
			for (int i = 0; i < path.components.Length; i++)
			{
				this.components[i] = path.components[i];
			}
			if (components != null)
			{
				int num = 0;
				while (true)
				{
					if (num >= components.Length)
					{
						return;
					}
					if (components[num] == null)
					{
						break;
					}
					int length = components[num].Length;
					this.components[num + path.components.Length] = components[num];
					num++;
				}
				throw new ArgumentException("components cannot contain null");
			}
		}

		/// <summary>
		/// equality. Two POIFSDocumentPath instances are equal if they
		/// have the same number of component Strings, and if each
		/// component String is equal to its coresponding component String
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
					POIFSDocumentPath pOIFSDocumentPath = (POIFSDocumentPath)o;
					if (pOIFSDocumentPath.components.Length == components.Length)
					{
						result = true;
						for (int i = 0; i < components.Length; i++)
						{
							if (!pOIFSDocumentPath.components[i].Equals(components[i]))
							{
								result = false;
								break;
							}
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// get the specified component
		/// </summary>
		/// <param name="n">which component (0 ... length() - 1)</param>
		/// <returns>the nth component;</returns>
		public virtual string GetComponent(int n)
		{
			return components[n];
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		public override int GetHashCode()
		{
			if (hashcode == 0)
			{
				for (int i = 0; i < components.Length; i++)
				{
					hashcode += components[i].GetHashCode();
				}
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
			StringBuilder stringBuilder = new StringBuilder();
			int length = Length;
			stringBuilder.Append(Path.DirectorySeparatorChar);
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(GetComponent(i));
				if (i < length - 1)
				{
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
