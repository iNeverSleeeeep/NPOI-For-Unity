using NPOI.POIFS.Common;
using NPOI.POIFS.Dev;
using NPOI.POIFS.EventFileSystem;
using NPOI.POIFS.NIO;
using NPOI.POIFS.Properties;
using NPOI.POIFS.Storage;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// This is the main class of the POIFS system; it manages the entire
	/// life cycle of the filesystem.
	/// This is the new NIO version
	public class NPOIFSFileSystem : BlockStore, POIFSViewable
	{
		private static POILogger _logger = POILogFactory.GetLogger(typeof(NPOIFSFileSystem));

		private NPOIFSMiniStore _mini_store;

		private NPropertyTable _property_table;

		private List<BATBlock> _xbat_blocks;

		private List<BATBlock> _bat_blocks;

		private HeaderBlock _header;

		private DirectoryNode _root;

		private DataSource _data;

		/// What big block size the file uses. Most files
		///  use 512 bytes, but a few use 4096
		private POIFSBigBlockSize bigBlockSize = POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS;

		public DataSource Data
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
			}
		}

		/// For unit Testing only! Returns the underlying
		///  properties table
		public NPropertyTable PropertyTable => _property_table;

		/// Get the root entry
		///
		/// @return the root entry
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

		public bool PreferArray => ((POIFSViewable)Root).PreferArray;

		public string ShortDescription => GetShortDescription();

		public Array ViewableArray => GetViewableArray();

		public IEnumerator ViewableIterator => GetViewableIterator();

		/// Convenience method for clients that want to avoid the auto-close behaviour of the constructor.
		public static Stream CreateNonClosingInputStream(Stream stream)
		{
			return new CloseIgnoringInputStream(stream);
		}

		private NPOIFSFileSystem(bool newFS)
		{
			_header = new HeaderBlock(bigBlockSize);
			_property_table = new NPropertyTable(_header);
			_mini_store = new NPOIFSMiniStore(this, _property_table.Root, new List<BATBlock>(), _header);
			_xbat_blocks = new List<BATBlock>();
			_bat_blocks = new List<BATBlock>();
			_root = null;
			if (newFS)
			{
				_data = new ByteArrayBackedDataSource(new byte[bigBlockSize.GetBigBlockSize() * 3]);
			}
		}

		/// Constructor, intended for writing
		public NPOIFSFileSystem()
			: this(newFS: true)
		{
			_header.BATCount = 1;
			int[] array2 = _header.BATArray = new int[1];
			_bat_blocks.Add(BATBlock.CreateEmptyBATBlock(bigBlockSize, isXBAT: false));
			SetNextBlock(0, -3);
			_property_table.StartBlock = 1;
			SetNextBlock(1, -2);
		}

		public NPOIFSFileSystem(FileStream channel)
			: this(channel, closeChannelOnError: true)
		{
		}

		private NPOIFSFileSystem(FileStream channel, bool closeChannelOnError)
			: this(newFS: false)
		{
			try
			{
				byte[] array = new byte[512];
				IOUtils.ReadFully(channel, array);
				_header = new HeaderBlock(array);
				_data = new FileBackedDataSource(channel);
				ReadCoreContents();
				channel.Close();
			}
			catch (IOException ex)
			{
				if (closeChannelOnError)
				{
					channel.Close();
				}
				throw ex;
			}
			catch (Exception ex2)
			{
				if (closeChannelOnError)
				{
					channel.Close();
				}
				throw ex2;
			}
		}

		/// Create a POIFSFileSystem from an <tt>InputStream</tt>.  Normally the stream is read until
		/// EOF.  The stream is always closed.<p />
		///
		/// Some streams are usable After reaching EOF (typically those that return <code>true</code>
		/// for <tt>markSupported()</tt>).  In the unlikely case that the caller has such a stream
		/// <i>and</i> needs to use it After this constructor completes, a work around is to wrap the
		/// stream in order to trap the <tt>close()</tt> call.  A convenience method (
		/// <tt>CreateNonClosingInputStream()</tt>) has been provided for this purpose:
		/// <pre>
		/// InputStream wrappedStream = POIFSFileSystem.CreateNonClosingInputStream(is);
		/// HSSFWorkbook wb = new HSSFWorkbook(wrappedStream);
		/// is.Reset();
		/// doSomethingElse(is);
		/// </pre>
		/// Note also the special case of <tt>MemoryStream</tt> for which the <tt>close()</tt>
		/// method does nothing.
		/// <pre>
		/// MemoryStream bais = ...
		/// HSSFWorkbook wb = new HSSFWorkbook(bais); // calls bais.Close() !
		/// bais.Reset(); // no problem
		/// doSomethingElse(bais);
		/// </pre>
		///
		/// @param stream the InputStream from which to read the data
		///
		/// @exception IOException on errors Reading, or on invalid data
		public NPOIFSFileSystem(Stream stream)
			: this(newFS: false)
		{
			Stream stream2 = null;
			bool success = false;
			try
			{
				stream2 = stream;
				ByteBuffer byteBuffer = ByteBuffer.CreateBuffer(512);
				IOUtils.ReadFully(stream2, byteBuffer.Buffer);
				_header = new HeaderBlock(byteBuffer);
				BlockAllocationTableReader.SanityCheckBlockCount(_header.BATCount);
				int capacity = BATBlock.CalculateMaximumSize(_header);
				ByteBuffer byteBuffer2 = ByteBuffer.CreateBuffer(capacity);
				byteBuffer.Position = 0;
				byteBuffer2.Write(byteBuffer.Buffer);
				byteBuffer2.Position = byteBuffer.Length;
				byteBuffer2.Position += IOUtils.ReadFully(stream2, byteBuffer2.Buffer, byteBuffer2.Position, (int)stream2.Length);
				success = true;
				_data = new ByteArrayBackedDataSource(byteBuffer2.Buffer, byteBuffer2.Position);
			}
			finally
			{
				stream2?.Close();
				CloseInputStream(stream, success);
			}
			ReadCoreContents();
		}

		/// @param stream the stream to be closed
		/// @param success <code>false</code> if an exception is currently being thrown in the calling method
		private void CloseInputStream(Stream stream, bool success)
		{
			try
			{
				stream.Close();
			}
			catch (IOException ex)
			{
				if (success)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		/// Read and process the PropertiesTable and the
		///  FAT / XFAT blocks, so that we're Ready to
		///  work with the file
		private void ReadCoreContents()
		{
			bigBlockSize = _header.BigBlockSize;
			ChainLoopDetector chainLoopDetector = GetChainLoopDetector();
			int[] bATArray = _header.BATArray;
			foreach (int batAt in bATArray)
			{
				ReadBAT(batAt, chainLoopDetector);
			}
			int num = _header.BATCount - _header.BATArray.Length;
			int num2 = _header.XBATIndex;
			for (int j = 0; j < _header.XBATCount; j++)
			{
				chainLoopDetector.Claim(num2);
				ByteBuffer blockAt = GetBlockAt(num2);
				BATBlock bATBlock = BATBlock.CreateBATBlock(bigBlockSize, blockAt);
				bATBlock.OurBlockIndex = num2;
				num2 = bATBlock.GetValueAt(bigBlockSize.GetXBATEntriesPerBlock());
				_xbat_blocks.Add(bATBlock);
				int num3 = Math.Min(num, bigBlockSize.GetXBATEntriesPerBlock());
				for (int k = 0; k < num3; k++)
				{
					int valueAt = bATBlock.GetValueAt(k);
					if (valueAt == -1 || valueAt == -2)
					{
						break;
					}
					ReadBAT(valueAt, chainLoopDetector);
				}
				num -= num3;
			}
			_property_table = new NPropertyTable(_header, this);
			List<BATBlock> list = new List<BATBlock>();
			_mini_store = new NPOIFSMiniStore(this, _property_table.Root, list, _header);
			num2 = _header.SBATStart;
			for (int l = 0; l < _header.SBATCount; l++)
			{
				chainLoopDetector.Claim(num2);
				ByteBuffer blockAt2 = GetBlockAt(num2);
				BATBlock bATBlock2 = BATBlock.CreateBATBlock(bigBlockSize, blockAt2);
				bATBlock2.OurBlockIndex = num2;
				list.Add(bATBlock2);
				num2 = GetNextBlock(num2);
			}
		}

		private void ReadBAT(int batAt, ChainLoopDetector loopDetector)
		{
			loopDetector.Claim(batAt);
			ByteBuffer blockAt = GetBlockAt(batAt);
			BATBlock bATBlock = BATBlock.CreateBATBlock(bigBlockSize, blockAt);
			bATBlock.OurBlockIndex = batAt;
			_bat_blocks.Add(bATBlock);
		}

		private BATBlock CreateBAT(int offset, bool isBAT)
		{
			BATBlock bATBlock = BATBlock.CreateEmptyBATBlock(bigBlockSize, !isBAT);
			bATBlock.OurBlockIndex = offset;
			ByteBuffer src = ByteBuffer.CreateBuffer(bigBlockSize.GetBigBlockSize());
			int num = (1 + offset) * bigBlockSize.GetBigBlockSize();
			_data.Write(src, num);
			return bATBlock;
		}

		/// Load the block at the given offset.
		public override ByteBuffer GetBlockAt(int offset)
		{
			long position = (offset + 1) * bigBlockSize.GetBigBlockSize();
			return _data.Read(bigBlockSize.GetBigBlockSize(), position);
		}

		/// Load the block at the given offset, 
		///  extending the file if needed
		public override ByteBuffer CreateBlockIfNeeded(int offset)
		{
			try
			{
				return GetBlockAt(offset);
			}
			catch (IndexOutOfRangeException)
			{
				long position = (offset + 1) * bigBlockSize.GetBigBlockSize();
				ByteBuffer src = ByteBuffer.CreateBuffer(GetBigBlockSize());
				_data.Write(src, position);
				return GetBlockAt(offset);
			}
		}

		/// Returns the BATBlock that handles the specified offset,
		///  and the relative index within it
		public override BATBlockAndIndex GetBATBlockAndIndex(int offset)
		{
			return BATBlock.GetBATBlockAndIndex(offset, _header, _bat_blocks);
		}

		/// Works out what block follows the specified one.
		public override int GetNextBlock(int offset)
		{
			BATBlockAndIndex bATBlockAndIndex = GetBATBlockAndIndex(offset);
			return bATBlockAndIndex.Block.GetValueAt(bATBlockAndIndex.Index);
		}

		/// Changes the record of what block follows the specified one.
		public override void SetNextBlock(int offset, int nextBlock)
		{
			BATBlockAndIndex bATBlockAndIndex = GetBATBlockAndIndex(offset);
			bATBlockAndIndex.Block.SetValueAt(bATBlockAndIndex.Index, nextBlock);
		}

		/// Finds a free block, and returns its offset.
		/// This method will extend the file if needed, and if doing
		///  so, allocate new FAT blocks to Address the extra space.
		public override int GetFreeBlock()
		{
			int num = 0;
			for (int i = 0; i < _bat_blocks.Count; i++)
			{
				int bATEntriesPerBlock = bigBlockSize.GetBATEntriesPerBlock();
				BATBlock bATBlock = _bat_blocks[i];
				if (bATBlock.HasFreeSectors)
				{
					for (int j = 0; j < bATEntriesPerBlock; j++)
					{
						int valueAt = bATBlock.GetValueAt(j);
						if (valueAt == -1)
						{
							return num + j;
						}
					}
				}
				num += bATEntriesPerBlock;
			}
			BATBlock bATBlock2 = CreateBAT(num, isBAT: true);
			bATBlock2.SetValueAt(0, -3);
			_bat_blocks.Add(bATBlock2);
			if (_header.BATCount >= 109)
			{
				BATBlock bATBlock3 = null;
				foreach (BATBlock xbat_block in _xbat_blocks)
				{
					if (xbat_block.HasFreeSectors)
					{
						bATBlock3 = xbat_block;
						break;
					}
				}
				if (bATBlock3 == null)
				{
					bATBlock3 = CreateBAT(num + 1, isBAT: false);
					bATBlock3.SetValueAt(0, num);
					bATBlock2.SetValueAt(1, -4);
					num++;
					if (_xbat_blocks.Count == 0)
					{
						_header.XBATStart = num;
					}
					else
					{
						_xbat_blocks[_xbat_blocks.Count - 1].SetValueAt(bigBlockSize.GetXBATEntriesPerBlock(), num);
					}
					_xbat_blocks.Add(bATBlock3);
					_header.XBATCount = _xbat_blocks.Count;
				}
				for (int k = 0; k < bigBlockSize.GetXBATEntriesPerBlock(); k++)
				{
					if (bATBlock3.GetValueAt(k) == -1)
					{
						bATBlock3.SetValueAt(k, num);
					}
				}
			}
			else
			{
				int[] array = new int[_header.BATCount + 1];
				Array.Copy(_header.BATArray, 0, array, 0, array.Length - 1);
				array[array.Length - 1] = num;
				_header.BATArray = array;
			}
			_header.BATCount = _bat_blocks.Count;
			return num + 1;
		}

		public override ChainLoopDetector GetChainLoopDetector()
		{
			return new ChainLoopDetector(_data.Size, this);
		}

		/// Returns the MiniStore, which performs a similar low
		///  level function to this, except for the small blocks.
		public NPOIFSMiniStore GetMiniStore()
		{
			return _mini_store;
		}

		/// add a new POIFSDocument to the FileSytem 
		///
		/// @param document the POIFSDocument being Added
		public void AddDocument(NPOIFSDocument document)
		{
			_property_table.AddProperty(document.DocumentProperty);
		}

		/// add a new DirectoryProperty to the FileSystem
		///
		/// @param directory the DirectoryProperty being Added
		public void AddDirectory(DirectoryProperty directory)
		{
			_property_table.AddProperty(directory);
		}

		/// Create a new document to be Added to the root directory
		///
		/// @param stream the InputStream from which the document's data
		///               will be obtained
		/// @param name the name of the new POIFSDocument
		///
		/// @return the new DocumentEntry
		///
		/// @exception IOException on error creating the new POIFSDocument
		public DocumentEntry CreateDocument(Stream stream, string name)
		{
			return Root.CreateDocument(name, stream);
		}

		/// create a new DocumentEntry in the root entry; the data will be
		/// provided later
		///
		/// @param name the name of the new DocumentEntry
		/// @param size the size of the new DocumentEntry
		/// @param Writer the Writer of the new DocumentEntry
		///
		/// @return the new DocumentEntry
		///
		/// @exception IOException
		public DocumentEntry CreateDocument(string name, int size, POIFSWriterListener writer)
		{
			return Root.CreateDocument(name, size, writer);
		}

		/// create a new DirectoryEntry in the root directory
		///
		/// @param name the name of the new DirectoryEntry
		///
		/// @return the new DirectoryEntry
		///
		/// @exception IOException on name duplication
		public DirectoryEntry CreateDirectory(string name)
		{
			return Root.CreateDirectory(name);
		}

		/// Write the filesystem out to the open file. Will thrown an
		///  {@link ArgumentException} if opened from an 
		///  {@link InputStream}.
		///
		/// @exception IOException thrown on errors writing to the stream
		public void WriteFilesystem()
		{
			if (!(_data is FileBackedDataSource))
			{
				throw new ArgumentException("POIFS opened from an inputstream, so WriteFilesystem() may not be called. Use WriteFilesystem(OutputStream) instead");
			}
			syncWithDataSource();
		}

		/// Write the filesystem out
		///
		/// @param stream the OutputStream to which the filesystem will be
		///               written
		///
		/// @exception IOException thrown on errors writing to the stream
		public void WriteFilesystem(Stream stream)
		{
			syncWithDataSource();
			_data.CopyTo(stream);
		}

		/// Has our in-memory objects write their state
		///  to their backing blocks 
		private void syncWithDataSource()
		{
			HeaderBlockWriter headerBlockWriter = new HeaderBlockWriter(_header);
			headerBlockWriter.WriteBlock(GetBlockAt(-1));
			foreach (BATBlock bat_block in _bat_blocks)
			{
				ByteBuffer blockAt = GetBlockAt(bat_block.OurBlockIndex);
				BlockAllocationTableWriter.WriteBlock(bat_block, blockAt);
			}
			_mini_store.SyncWithDataSource();
			_property_table.Write(new NPOIFSStream(this, _header.PropertyStart));
		}

		/// Closes the FileSystem, freeing any underlying files, streams
		///  and buffers. After this, you will be unable to read or 
		///  write from the FileSystem.
		public void close()
		{
			_data.Close();
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

		/// remove an entry
		///
		/// @param entry to be Removed
		public void Remove(EntryNode entry)
		{
			_property_table.RemoveProperty(entry.Property);
		}

		/// Get an array of objects, some of which may implement
		/// POIFSViewable
		///
		/// @return an array of Object; may not be null, but may be empty
		protected object[] GetViewableArray()
		{
			if (PreferArray)
			{
				Array viewableArray = ((POIFSViewable)Root).ViewableArray;
				object[] array = new object[viewableArray.Length];
				for (int i = 0; i < viewableArray.Length; i++)
				{
					array[i] = viewableArray.GetValue(i);
				}
				return array;
			}
			return new object[0];
		}

		/// Get an Iterator of objects, some of which may implement
		/// POIFSViewable
		///
		/// @return an Iterator; may not be null, but may have an empty
		/// back end store
		protected IEnumerator GetViewableIterator()
		{
			if (!PreferArray)
			{
				return ((POIFSViewable)Root).ViewableIterator;
			}
			return null;
		}

		/// Provides a short description of the object, to be used when a
		/// POIFSViewable object has not provided its contents.
		///
		/// @return short description
		protected string GetShortDescription()
		{
			return "POIFS FileSystem";
		}

		/// @return The Big Block size, normally 512 bytes, sometimes 4096 bytes
		public int GetBigBlockSize()
		{
			return bigBlockSize.GetBigBlockSize();
		}

		/// @return The Big Block size, normally 512 bytes, sometimes 4096 bytes
		public POIFSBigBlockSize GetBigBlockSizeDetails()
		{
			return bigBlockSize;
		}

		public override int GetBlockStoreBlockSize()
		{
			return GetBigBlockSize();
		}
	}
}
