using NPOI.POIFS.FileSystem;
using NPOI.POIFS.Properties;
using NPOI.POIFS.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.EventFileSystem
{
	/// <summary>
	/// An event-driven Reader for POIFS file systems. Users of this class
	/// first Create an instance of it, then use the RegisterListener
	/// methods to Register POIFSReaderListener instances for specific
	/// documents. Once all the listeners have been Registered, the Read()
	/// method is called, which results in the listeners being notified as
	/// their documents are Read.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class POIFSReader
	{
		private POIFSReaderRegistry registry;

		private bool registryClosed;

		public event POIFSReaderEventHandler StreamReaded;

		protected virtual void OnStreamReaded(POIFSReaderEventArgs e)
		{
			if (this.StreamReaded != null)
			{
				this.StreamReaded(this, e);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.EventFileSystem.POIFSReader" /> class.
		/// </summary>
		public POIFSReader()
		{
			registry = new POIFSReaderRegistry();
			registryClosed = false;
		}

		/// <summary>
		/// Read from an InputStream and Process the documents we Get
		/// </summary>
		/// <param name="stream">the InputStream from which to Read the data</param>
		/// <returns>POIFSDocument list</returns>
		public List<DocumentDescriptor> Read(Stream stream)
		{
			registryClosed = true;
			HeaderBlock headerBlock = new HeaderBlock(stream);
			RawDataBlockList rawDataBlockList = new RawDataBlockList(stream, headerBlock.BigBlockSize);
			new BlockAllocationTableReader(headerBlock.BigBlockSize, headerBlock.BATCount, headerBlock.BATArray, headerBlock.XBATCount, headerBlock.XBATIndex, rawDataBlockList);
			PropertyTable propertyTable = new PropertyTable(headerBlock, rawDataBlockList);
			return ProcessProperties(SmallBlockTableReader.GetSmallDocumentBlocks(headerBlock.BigBlockSize, rawDataBlockList, propertyTable.Root, headerBlock.SBATStart), rawDataBlockList, propertyTable.Root.Children, new POIFSDocumentPath());
		}

		/// Register a POIFSReaderListener for all documents
		///
		/// @param listener the listener to be registered
		///
		/// @exception NullPointerException if listener is null
		/// @exception IllegalStateException if read() has already been
		///                                  called
		public void RegisterListener(POIFSReaderListener listener)
		{
			if (listener == null)
			{
				throw new NullReferenceException();
			}
			if (registryClosed)
			{
				throw new InvalidOperationException();
			}
			registry.RegisterListener(listener);
		}

		/// Register a POIFSReaderListener for a document in the root
		/// directory
		///
		/// @param listener the listener to be registered
		/// @param name the document name
		///
		/// @exception NullPointerException if listener is null or name is
		///                                 null or empty
		/// @exception IllegalStateException if read() has already been
		///                                  called
		public void RegisterListener(POIFSReaderListener listener, string name)
		{
			RegisterListener(listener, null, name);
		}

		/// Register a POIFSReaderListener for a document in the specified
		/// directory
		///
		/// @param listener the listener to be registered
		/// @param path the document path; if null, the root directory is
		///             assumed
		/// @param name the document name
		///
		/// @exception NullPointerException if listener is null or name is
		///                                 null or empty
		/// @exception IllegalStateException if read() has already been
		///                                  called
		public void RegisterListener(POIFSReaderListener listener, POIFSDocumentPath path, string name)
		{
			if (listener == null || name == null || name.Length == 0)
			{
				throw new NullReferenceException();
			}
			if (registryClosed)
			{
				throw new InvalidOperationException();
			}
			registry.RegisterListener(listener, (path == null) ? new POIFSDocumentPath() : path, name);
		}

		/// <summary>
		/// Processes the properties.
		/// </summary>
		/// <param name="small_blocks">The small_blocks.</param>
		/// <param name="big_blocks">The big_blocks.</param>
		/// <param name="properties">The properties.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		private List<DocumentDescriptor> ProcessProperties(BlockList small_blocks, BlockList big_blocks, IEnumerator properties, POIFSDocumentPath path)
		{
			List<DocumentDescriptor> result = new List<DocumentDescriptor>();
			while (properties.MoveNext())
			{
				Property property = (Property)properties.Current;
				string name = property.Name;
				if (property.IsDirectory)
				{
					POIFSDocumentPath path2 = new POIFSDocumentPath(path, new string[1]
					{
						name
					});
					ProcessProperties(small_blocks, big_blocks, ((DirectoryProperty)property).Children, path2);
				}
				else
				{
					int startBlock = property.StartBlock;
					IEnumerator listeners = registry.GetListeners(path, name);
					POIFSDocument pOIFSDocument = null;
					if (listeners.MoveNext())
					{
						listeners.Reset();
						int size = property.Size;
						pOIFSDocument = ((!property.ShouldUseSmallBlocks) ? new POIFSDocument(name, big_blocks.FetchBlocks(startBlock, -1), size) : new POIFSDocument(name, small_blocks.FetchBlocks(startBlock, -1), size));
						while (listeners.MoveNext())
						{
							POIFSReaderListener pOIFSReaderListener = (POIFSReaderListener)listeners.Current;
							pOIFSReaderListener.ProcessPOIFSReaderEvent(new POIFSReaderEvent(new DocumentInputStream(pOIFSDocument), path, name));
						}
					}
					else if (property.ShouldUseSmallBlocks)
					{
						small_blocks.FetchBlocks(startBlock, -1);
					}
					else
					{
						big_blocks.FetchBlocks(startBlock, -1);
					}
				}
			}
			return result;
		}
	}
}
