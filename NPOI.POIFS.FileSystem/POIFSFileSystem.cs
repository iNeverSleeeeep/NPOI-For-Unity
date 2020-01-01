using NPOI.POIFS.Common;
using NPOI.POIFS.Dev;
using NPOI.POIFS.EventFileSystem;
using NPOI.POIFS.Properties;
using NPOI.POIFS.Storage;
using NPOI.Util;
using System;
using System.Collections;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// This is the main class of the POIFS system; it manages the entire
	/// life cycle of the filesystem.
	/// @author Marc Johnson (mjohnson at apache dot org) 
	/// </summary>
	[Serializable]
	public class POIFSFileSystem : POIFSViewable
	{
		private PropertyTable _property_table;

		private IList _documents;

		private DirectoryNode _root;

		/// What big block size the file uses. Most files
		///  use 512 bytes, but a few use 4096
		private POIFSBigBlockSize bigBlockSize = POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS;

		/// <summary>
		/// Get the root entry
		/// </summary>
		/// <value>The root.</value>
		public DirectoryNode Root
		{
			get
			{
				if (_root == null)
				{
					_root = new DirectoryNode(_property_table.Root, this, null);
				}
				return _root;
			}
		}

		/// <summary>
		/// Get an array of objects, some of which may implement
		/// POIFSViewable        
		/// </summary>
		/// <value>an array of Object; may not be null, but may be empty</value>
		public Array ViewableArray
		{
			get
			{
				if (PreferArray)
				{
					return ((POIFSViewable)Root).ViewableArray;
				}
				return new object[0];
			}
		}

		/// <summary>
		/// Get an Iterator of objects, some of which may implement
		/// POIFSViewable
		/// </summary>
		/// <value>an Iterator; may not be null, but may have an empty
		/// back end store</value>
		public IEnumerator ViewableIterator
		{
			get
			{
				if (!PreferArray)
				{
					return ((POIFSViewable)Root).ViewableIterator;
				}
				return ArrayList.ReadOnly(new ArrayList()).GetEnumerator();
			}
		}

		/// <summary>
		/// Give viewers a hint as to whether to call GetViewableArray or
		/// GetViewableIterator
		/// </summary>
		/// <value><c>true</c> if a viewer should call GetViewableArray, <c>false</c> if
		/// a viewer should call GetViewableIterator </value>
		public bool PreferArray => ((POIFSViewable)Root).PreferArray;

		/// <summary>
		/// Provides a short description of the object, to be used when a
		/// POIFSViewable object has not provided its contents.
		/// </summary>
		/// <value>The short description.</value>
		public string ShortDescription => "POIFS FileSystem";

		/// <summary>
		/// Gets The Big Block size, normally 512 bytes, sometimes 4096 bytes
		/// </summary>
		/// <value>The size of the big block.</value>
		public int BigBlockSize => bigBlockSize.GetBigBlockSize();

		/// <summary>
		/// Convenience method for clients that want to avoid the auto-Close behaviour of the constructor.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <example>
		/// A convenience method (
		/// CreateNonClosingInputStream()) has been provided for this purpose:
		/// StreamwrappedStream = POIFSFileSystem.CreateNonClosingInputStream(is);
		/// HSSFWorkbook wb = new HSSFWorkbook(wrappedStream);
		/// is.reset();
		/// doSomethingElse(is);
		/// </example>
		/// <returns></returns>
		public static Stream CreateNonClosingInputStream(Stream stream)
		{
			return new CloseIgnoringInputStream(stream);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.FileSystem.POIFSFileSystem" /> class.  intended for writing
		/// </summary>
		public POIFSFileSystem()
		{
			HeaderBlock headerBlock = new HeaderBlock(bigBlockSize);
			_property_table = new PropertyTable(headerBlock);
			_documents = new ArrayList();
			_root = null;
		}

		/// <summary>
		/// Create a POIFSFileSystem from an Stream. Normally the stream is Read until
		/// EOF.  The stream is always Closed.  In the unlikely case that the caller has such a stream and
		/// needs to use it after this constructor completes, a work around is to wrap the
		/// stream in order to trap the Close() call.  
		/// </summary>
		/// <param name="stream">the Streamfrom which to Read the data</param>
		public POIFSFileSystem(Stream stream)
			: this()
		{
			bool success = false;
			HeaderBlock headerBlock;
			RawDataBlockList rawDataBlockList;
			try
			{
				headerBlock = new HeaderBlock(stream);
				bigBlockSize = headerBlock.BigBlockSize;
				rawDataBlockList = new RawDataBlockList(stream, bigBlockSize);
				success = true;
			}
			finally
			{
				CloseInputStream(stream, success);
			}
			new BlockAllocationTableReader(headerBlock.BigBlockSize, headerBlock.BATCount, headerBlock.BATArray, headerBlock.XBATCount, headerBlock.XBATIndex, rawDataBlockList);
			PropertyTable propertyTable = new PropertyTable(headerBlock, rawDataBlockList);
			ProcessProperties(SmallBlockTableReader.GetSmallDocumentBlocks(bigBlockSize, rawDataBlockList, propertyTable.Root, headerBlock.SBATStart), rawDataBlockList, propertyTable.Root.Children, null, headerBlock.PropertyStart);
			Root.StorageClsid = propertyTable.Root.StorageClsid;
		}

		/// @param stream the stream to be Closed
		/// @param success <c>false</c> if an exception is currently being thrown in the calling method
		private void CloseInputStream(Stream stream, bool success)
		{
			if (stream is MemoryStream)
			{
				string text = "POIFS is closing the supplied input stream of type (" + stream.GetType().Name + ") which supports mark/reset.  This will be a problem for the caller if the stream will still be used.  If that is the case the caller should wrap the input stream to avoid this Close logic.  This warning is only temporary and will not be present in future versions of POI.";
			}
			try
			{
				stream.Close();
			}
			catch (IOException)
			{
				if (success)
				{
					throw;
				}
			}
		}

		/// <summary>
		/// Checks that the supplied Stream(which MUST
		/// support mark and reset, or be a PushbackInputStream)
		/// has a POIFS (OLE2) header at the start of it.
		/// If your Streamdoes not support mark / reset,
		/// then wrap it in a PushBackInputStream, then be
		/// sure to always use that, and not the original!
		/// </summary>
		/// <param name="inp">An Streamwhich supports either mark/reset, or is a PushbackStream</param>
		/// <returns>
		/// 	<c>true</c> if [has POIFS header] [the specified inp]; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasPOIFSHeader(Stream inp)
		{
			byte[] array = new byte[8];
			IOUtils.ReadFully(inp, array);
			LongField longField = new LongField(0, array);
			return longField.Value == -2226271756974174256L;
		}

		/// <summary>
		/// Create a new document to be Added to the root directory
		/// </summary>
		/// <param name="stream"> the Streamfrom which the document's data will be obtained</param>
		/// <param name="name">the name of the new POIFSDocument</param>
		/// <returns>the new DocumentEntry</returns>
		public DocumentEntry CreateDocument(Stream stream, string name)
		{
			return Root.CreateDocument(name, stream);
		}

		/// <summary>
		/// Create a new DocumentEntry in the root entry; the data will be
		/// provided later
		/// </summary>
		/// <param name="name">the name of the new DocumentEntry</param>
		/// <param name="size">the size of the new DocumentEntry</param>
		/// <param name="writer">the Writer of the new DocumentEntry</param>
		/// <returns>the new DocumentEntry</returns>
		public DocumentEntry CreateDocument(string name, int size, POIFSWriterListener writer)
		{
			return Root.CreateDocument(name, size, writer);
		}

		/// <summary>
		/// Create a new DirectoryEntry in the root directory
		/// </summary>
		/// <param name="name">the name of the new DirectoryEntry</param>
		/// <returns>the new DirectoryEntry</returns>
		public DirectoryEntry CreateDirectory(string name)
		{
			return Root.CreateDirectory(name);
		}

		/// open a document in the root entry's list of entries
		///
		/// @param documentName the name of the document to be opened
		///
		/// @return a newly opened DocumentInputStream
		///
		/// @exception IOException if the document does not exist or the
		///            name is that of a DirectoryEntry
		public DocumentInputStream CreateDocumentInputStream(string documentName)
		{
			return Root.CreateDocumentInputStream(documentName);
		}

		/// <summary>
		/// Writes the file system.
		/// </summary>
		/// <param name="stream">the OutputStream to which the filesystem will be
		/// written</param>
		public void WriteFileSystem(Stream stream)
		{
			_property_table.PreWrite();
			SmallBlockTableWriter smallBlockTableWriter = new SmallBlockTableWriter(bigBlockSize, _documents, _property_table.Root);
			BlockAllocationTableWriter blockAllocationTableWriter = new BlockAllocationTableWriter(bigBlockSize);
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(_documents);
			arrayList.Add(_property_table);
			arrayList.Add(smallBlockTableWriter);
			arrayList.Add(smallBlockTableWriter.SBAT);
			IEnumerator enumerator = arrayList.GetEnumerator();
			while (enumerator.MoveNext())
			{
				BATManaged bATManaged = (BATManaged)enumerator.Current;
				int countBlocks = bATManaged.CountBlocks;
				if (countBlocks != 0)
				{
					bATManaged.StartBlock = blockAllocationTableWriter.AllocateSpace(countBlocks);
				}
			}
			int startBlock = blockAllocationTableWriter.CreateBlocks();
			HeaderBlockWriter headerBlockWriter = new HeaderBlockWriter(bigBlockSize);
			BATBlock[] array = headerBlockWriter.SetBATBlocks(blockAllocationTableWriter.CountBlocks, startBlock);
			headerBlockWriter.PropertyStart = _property_table.StartBlock;
			headerBlockWriter.SBATStart = smallBlockTableWriter.SBAT.StartBlock;
			headerBlockWriter.SBATBlockCount = smallBlockTableWriter.SBATBlockCount;
			ArrayList arrayList2 = new ArrayList();
			arrayList2.Add(headerBlockWriter);
			arrayList2.AddRange(_documents);
			arrayList2.Add(_property_table);
			arrayList2.Add(smallBlockTableWriter);
			arrayList2.Add(smallBlockTableWriter.SBAT);
			arrayList2.Add(blockAllocationTableWriter);
			for (int i = 0; i < array.Length; i++)
			{
				arrayList2.Add(array[i]);
			}
			enumerator = arrayList2.GetEnumerator();
			while (enumerator.MoveNext())
			{
				BlockWritable blockWritable = (BlockWritable)enumerator.Current;
				blockWritable.WriteBlocks(stream);
			}
			arrayList2 = null;
			enumerator = null;
		}

		/// <summary>
		/// Add a new POIFSDocument
		/// </summary>
		/// <param name="document">the POIFSDocument being Added</param>
		public void AddDocument(POIFSDocument document)
		{
			_documents.Add(document);
			_property_table.AddProperty(document.DocumentProperty);
		}

		/// <summary>
		/// Add a new DirectoryProperty
		/// </summary>
		/// <param name="directory">The directory.</param>
		public void AddDirectory(DirectoryProperty directory)
		{
			_property_table.AddProperty(directory);
		}

		/// <summary>
		/// Removes the specified entry.
		/// </summary>
		/// <param name="entry">The entry.</param>
		public void Remove(EntryNode entry)
		{
			_property_table.RemoveProperty(entry.Property);
			if (entry.IsDocumentEntry)
			{
				_documents.Remove(((DocumentNode)entry).Document);
			}
		}

		private void ProcessProperties(BlockList small_blocks, BlockList big_blocks, IEnumerator properties, DirectoryNode dir, int headerPropertiesStartAt)
		{
			while (properties.MoveNext())
			{
				Property property = (Property)properties.Current;
				string name = property.Name;
				DirectoryNode directoryNode = (dir == null) ? Root : dir;
				if (property.IsDirectory)
				{
					DirectoryNode directoryNode2 = (DirectoryNode)directoryNode.CreateDirectory(name);
					directoryNode2.StorageClsid = property.StorageClsid;
					ProcessProperties(small_blocks, big_blocks, ((DirectoryProperty)property).Children, directoryNode2, headerPropertiesStartAt);
				}
				else
				{
					int startBlock = property.StartBlock;
					int size = property.Size;
					POIFSDocument pOIFSDocument = null;
					pOIFSDocument = ((!property.ShouldUseSmallBlocks) ? new POIFSDocument(name, big_blocks.FetchBlocks(startBlock, headerPropertiesStartAt), size) : new POIFSDocument(name, small_blocks.FetchBlocks(startBlock, headerPropertiesStartAt), size));
					directoryNode.CreateDocument(pOIFSDocument);
				}
			}
		}
	}
}
