using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI
{
	/// <summary>
	/// This holds the common functionality for all POI
	/// Document classes.
	/// Currently, this relates to Document Information Properties
	/// </summary>
	/// <remarks>@author Nick Burch</remarks>
	[Serializable]
	public abstract class POIDocument
	{
		/// Holds metadata on our document 
		protected SummaryInformation sInf;

		/// Holds further metadata on our document 
		protected DocumentSummaryInformation dsInf;

		/// The directory that our document lives in 
		protected DirectoryNode directory;

		/// For our own logging use 
		protected bool initialized;

		/// <summary>
		/// Fetch the Document Summary Information of the document
		/// </summary>
		/// <value>The document summary information.</value>
		public DocumentSummaryInformation DocumentSummaryInformation
		{
			get
			{
				if (!initialized)
				{
					ReadProperties();
				}
				return dsInf;
			}
			set
			{
				dsInf = value;
			}
		}

		/// <summary>
		/// Fetch the Summary Information of the document
		/// </summary>
		/// <value>The summary information.</value>
		public SummaryInformation SummaryInformation
		{
			get
			{
				if (!initialized)
				{
					ReadProperties();
				}
				return sInf;
			}
			set
			{
				sInf = value;
			}
		}

		protected POIDocument(DirectoryNode dir)
		{
			directory = dir;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIDocument" /> class.
		/// </summary>
		/// <param name="dir">The dir.</param>
		/// <param name="fs">The fs.</param>
		[Obsolete]
		public POIDocument(DirectoryNode dir, POIFSFileSystem fs)
		{
			directory = dir;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIDocument" /> class.
		/// </summary>
		/// <param name="fs">The fs.</param>
		public POIDocument(POIFSFileSystem fs)
			: this(fs.Root)
		{
		}

		/// Will create whichever of SummaryInformation
		///  and DocumentSummaryInformation (HPSF) properties
		///  are not already part of your document.
		/// This is normally useful when creating a new
		///  document from scratch.
		/// If the information properties are already there,
		///  then nothing will happen.
		public void CreateInformationProperties()
		{
			if (!initialized)
			{
				ReadProperties();
			}
			if (sInf == null)
			{
				sInf = PropertySetFactory.CreateSummaryInformation();
			}
			if (dsInf == null)
			{
				dsInf = PropertySetFactory.CreateDocumentSummaryInformation();
			}
		}

		/// <summary>
		/// Find, and Create objects for, the standard
		/// Documment Information Properties (HPSF).
		/// If a given property Set is missing or corrupt,
		/// it will remain null;
		/// </summary>
		protected void ReadProperties()
		{
			PropertySet propertySet = GetPropertySet("\u0005DocumentSummaryInformation");
			if (propertySet != null && propertySet is DocumentSummaryInformation)
			{
				dsInf = (DocumentSummaryInformation)propertySet;
			}
			propertySet = GetPropertySet("\u0005SummaryInformation");
			if (propertySet is SummaryInformation)
			{
				sInf = (SummaryInformation)propertySet;
			}
			initialized = true;
		}

		/// <summary>
		/// For a given named property entry, either return it or null if
		/// if it wasn't found
		/// </summary>
		/// <param name="setName">Name of the set.</param>
		/// <returns></returns>
		protected PropertySet GetPropertySet(string setName)
		{
			if (directory == null || !directory.HasEntry(setName))
			{
				return null;
			}
			DocumentInputStream stream;
			try
			{
				stream = directory.CreateDocumentInputStream(setName);
			}
			catch (IOException)
			{
				return null;
			}
			try
			{
				return PropertySetFactory.Create(stream);
			}
			catch (IOException)
			{
			}
			catch (HPSFException)
			{
			}
			return null;
		}

		/// <summary>
		/// Writes out the standard Documment Information Properties (HPSF)
		/// </summary>
		/// <param name="outFS">the POIFSFileSystem to Write the properties into</param>
		protected void WriteProperties(POIFSFileSystem outFS)
		{
			WriteProperties(outFS, null);
		}

		/// <summary>
		/// Writes out the standard Documment Information Properties (HPSF)
		/// </summary>
		/// <param name="outFS">the POIFSFileSystem to Write the properties into.</param>
		/// <param name="writtenEntries">a list of POIFS entries to Add the property names too.</param>
		protected void WriteProperties(POIFSFileSystem outFS, IList writtenEntries)
		{
			if (sInf != null)
			{
				WritePropertySet("\u0005SummaryInformation", sInf, outFS);
				writtenEntries?.Add("\u0005SummaryInformation");
			}
			if (dsInf != null)
			{
				WritePropertySet("\u0005DocumentSummaryInformation", dsInf, outFS);
				writtenEntries?.Add("\u0005DocumentSummaryInformation");
			}
		}

		/// <summary>
		/// Writes out a given ProperySet
		/// </summary>
		/// <param name="name">the (POIFS Level) name of the property to Write.</param>
		/// <param name="Set">the PropertySet to Write out.</param>
		/// <param name="outFS">the POIFSFileSystem to Write the property into.</param>
		protected void WritePropertySet(string name, PropertySet Set, POIFSFileSystem outFS)
		{
			try
			{
				MutablePropertySet mutablePropertySet = new MutablePropertySet(Set);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					mutablePropertySet.Write(memoryStream);
					byte[] buffer = memoryStream.ToArray();
					using (MemoryStream stream = new MemoryStream(buffer))
					{
						outFS.CreateDocument(stream, name);
					}
				}
			}
			catch (WritingNotSupportedException)
			{
			}
		}

		/// <summary>
		/// Writes the document out to the specified output stream
		/// </summary>
		/// <param name="out1">The out1.</param>
		public abstract void Write(Stream out1);

		/// <summary>
		/// Copies nodes from one POIFS to the other minus the excepts
		/// </summary>
		/// <param name="source">the source POIFS to copy from.</param>
		/// <param name="target">the target POIFS to copy to</param>
		/// <param name="excepts">a list of Strings specifying what nodes NOT to copy</param>
		[Obsolete]
		protected void CopyNodes(POIFSFileSystem source, POIFSFileSystem target, List<string> excepts)
		{
			EntryUtils.CopyNodes(source, target, excepts);
		}

		/// <summary>
		/// Copies nodes from one POIFS to the other minus the excepts
		/// </summary>
		/// <param name="sourceRoot">the source POIFS to copy from.</param>
		/// <param name="targetRoot">the target POIFS to copy to</param>
		/// <param name="excepts">a list of Strings specifying what nodes NOT to copy</param>
		[Obsolete]
		protected void CopyNodes(DirectoryNode sourceRoot, DirectoryNode targetRoot, List<string> excepts)
		{
			EntryUtils.CopyNodes(sourceRoot, targetRoot, excepts);
		}

		/// <summary>
		/// Checks to see if the String is in the list, used when copying
		/// nodes between one POIFS and another
		/// </summary>
		/// <param name="entry">The entry.</param>
		/// <param name="list">The list.</param>
		/// <returns>
		/// 	<c>true</c> if [is in list] [the specified entry]; otherwise, <c>false</c>.
		/// </returns>
		private bool isInList(string entry, IList list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Equals(entry))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Copies an Entry into a target POIFS directory, recursively
		/// </summary>
		/// <param name="entry">The entry.</param>
		/// <param name="target">The target.</param>
		[Obsolete]
		private void CopyNodeRecursively(Entry entry, DirectoryEntry target)
		{
			EntryUtils.CopyNodeRecursively(entry, target);
		}
	}
}
