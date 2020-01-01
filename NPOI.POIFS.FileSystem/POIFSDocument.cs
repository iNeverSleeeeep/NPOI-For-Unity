using NPOI.POIFS.Common;
using NPOI.POIFS.Dev;
using NPOI.POIFS.EventFileSystem;
using NPOI.POIFS.Properties;
using NPOI.POIFS.Storage;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// This class manages a document in the POIFS filesystem.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class POIFSDocument : BATManaged, BlockWritable, POIFSViewable
	{
		internal class SmallBlockStore
		{
			private SmallDocumentBlock[] smallBlocks;

			private POIFSDocumentPath path;

			private string name;

			private int size;

			private POIFSWriterListener writer;

			private POIFSBigBlockSize bigBlockSize;

			internal virtual SmallDocumentBlock[] Blocks
			{
				get
				{
					if (Valid && writer != null)
					{
						MemoryStream memoryStream = new MemoryStream(size);
						DocumentOutputStream stream = new DocumentOutputStream(memoryStream, size);
						writer.ProcessPOIFSWriterEvent(new POIFSWriterEvent(stream, path, name, size));
						smallBlocks = SmallDocumentBlock.Convert(bigBlockSize, memoryStream.ToArray(), size);
					}
					return smallBlocks;
				}
			}

			internal virtual bool Valid
			{
				get
				{
					if (smallBlocks.Length <= 0)
					{
						return writer != null;
					}
					return true;
				}
			}

			internal SmallBlockStore(POIFSBigBlockSize bigBlockSize, SmallDocumentBlock[] blocks)
			{
				this.bigBlockSize = bigBlockSize;
				smallBlocks = (SmallDocumentBlock[])blocks.Clone();
				path = null;
				name = null;
				size = -1;
				writer = null;
			}

			internal SmallBlockStore(POIFSBigBlockSize bigBlockSize, POIFSDocumentPath path, string name, int size, POIFSWriterListener writer)
			{
				this.bigBlockSize = bigBlockSize;
				smallBlocks = new SmallDocumentBlock[0];
				this.path = path;
				this.name = name;
				this.size = size;
				this.writer = writer;
			}
		}

		internal class BigBlockStore
		{
			private DocumentBlock[] bigBlocks;

			private POIFSDocumentPath path;

			private string name;

			private int size;

			private POIFSWriterListener writer;

			private POIFSBigBlockSize bigBlockSize;

			internal virtual bool Valid
			{
				get
				{
					if (bigBlocks.Length <= 0)
					{
						return writer != null;
					}
					return true;
				}
			}

			internal virtual DocumentBlock[] Blocks
			{
				get
				{
					if (Valid && writer != null)
					{
						MemoryStream memoryStream = new MemoryStream(size);
						DocumentOutputStream stream = new DocumentOutputStream(memoryStream, size);
						writer.ProcessPOIFSWriterEvent(new POIFSWriterEvent(stream, path, name, size));
						bigBlocks = DocumentBlock.Convert(bigBlockSize, memoryStream.ToArray(), size);
					}
					return bigBlocks;
				}
			}

			internal virtual int CountBlocks
			{
				get
				{
					int result = 0;
					if (!Valid)
					{
						return result;
					}
					if (writer != null)
					{
						return (size + 512 - 1) / 512;
					}
					return bigBlocks.Length;
				}
			}

			internal BigBlockStore(POIFSBigBlockSize bigBlockSize, DocumentBlock[] blocks)
			{
				this.bigBlockSize = bigBlockSize;
				bigBlocks = (DocumentBlock[])blocks.Clone();
				path = null;
				name = null;
				size = -1;
				writer = null;
			}

			internal BigBlockStore(POIFSBigBlockSize bigBlockSize, POIFSDocumentPath path, string name, int size, POIFSWriterListener writer)
			{
				this.bigBlockSize = bigBlockSize;
				bigBlocks = new DocumentBlock[0];
				this.path = path;
				this.name = name;
				this.size = size;
				this.writer = writer;
			}

			internal virtual void WriteBlocks(Stream stream)
			{
				if (Valid)
				{
					if (writer != null)
					{
						DocumentOutputStream documentOutputStream = new DocumentOutputStream(stream, size);
						writer.ProcessPOIFSWriterEvent(new POIFSWriterEvent(documentOutputStream, path, name, size));
						documentOutputStream.WriteFiller(CountBlocks * 512, DocumentBlock.FillByte);
					}
					else
					{
						for (int i = 0; i < bigBlocks.Length; i++)
						{
							bigBlocks[i].WriteBlocks(stream);
						}
					}
				}
			}
		}

		private static DocumentBlock[] EMPTY_BIG_BLOCK_ARRAY = new DocumentBlock[0];

		private static SmallDocumentBlock[] EMPTY_SMALL_BLOCK_ARRAY = new SmallDocumentBlock[0];

		private DocumentProperty _property;

		private int _size;

		private POIFSBigBlockSize _bigBigBlockSize;

		private SmallBlockStore _small_store;

		private BigBlockStore _big_store;

		/// <summary>
		/// Gets the number of BigBlock's this instance uses
		/// </summary>
		/// <value>count of BigBlock instances</value>
		public virtual int CountBlocks => _big_store.CountBlocks;

		/// <summary>
		/// Gets the document property.
		/// </summary>
		/// <value>The document property.</value>
		public virtual DocumentProperty DocumentProperty => _property;

		/// <summary>
		/// Provides a short description of the object to be used when a
		/// POIFSViewable object has not provided its contents.
		/// </summary>
		/// <value><c>true</c> if [prefer array]; otherwise, <c>false</c>.</value>
		public virtual bool PreferArray => true;

		/// <summary>
		/// Gets the short description.
		/// </summary>
		/// <value>The short description.</value>
		public virtual string ShortDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Document: \"").Append(_property.Name).Append("\"");
				stringBuilder.Append(" size = ").Append(Size);
				return stringBuilder.ToString();
			}
		}

		/// <summary>
		/// Gets the size.
		/// </summary>
		/// <value>The size.</value>
		public virtual int Size => _size;

		/// <summary>
		/// Gets the small blocks.
		/// </summary>
		/// <value>The small blocks.</value>
		public virtual BlockWritable[] SmallBlocks => _small_store.Blocks;

		/// <summary>
		/// Sets the start block for this instance
		/// </summary>
		/// <value>
		/// index into the array of BigBlock instances making up the the filesystem
		/// </value>
		public virtual int StartBlock
		{
			get
			{
				return _property.StartBlock;
			}
			set
			{
				_property.StartBlock = value;
			}
		}

		/// <summary>
		/// Get an array of objects, some of which may implement POIFSViewable
		/// </summary>
		/// <value>The viewable array.</value>
		public Array ViewableArray
		{
			get
			{
				object[] array = new object[1];
				string text;
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						BlockWritable[] array2 = null;
						if (_big_store.Valid)
						{
							array2 = _big_store.Blocks;
						}
						else if (_small_store.Valid)
						{
							array2 = _small_store.Blocks;
						}
						if (array2 != null)
						{
							for (int i = 0; i < array2.Length; i++)
							{
								array2[i].WriteBlocks(memoryStream);
							}
							byte[] array3 = memoryStream.ToArray();
							if (array3.Length > _property.Size)
							{
								byte[] array4 = new byte[_property.Size];
								Array.Copy(array3, 0, array4, 0, array4.Length);
								array3 = array4;
							}
							using (MemoryStream memoryStream2 = new MemoryStream())
							{
								HexDump.Dump(array3, 0L, memoryStream2, 0);
								byte[] buffer = memoryStream2.GetBuffer();
								char[] array5 = new char[(int)memoryStream2.Length];
								Array.Copy(buffer, 0, array5, 0, array5.Length);
								text = new string(array5);
							}
						}
						else
						{
							text = "<NO DATA>";
						}
					}
				}
				catch (IOException ex)
				{
					text = ex.Message;
				}
				array[0] = text;
				return array;
			}
		}

		/// <summary>
		/// Give viewers a hint as to whether to call ViewableArray or ViewableIterator
		/// </summary>
		/// <value>The viewable iterator.</value>
		public virtual IEnumerator ViewableIterator => ArrayList.ReadOnly(new ArrayList()).GetEnumerator();

		public event POIFSWriterEventHandler BeforeWriting;

		public POIFSDocument(string name, RawDataBlock[] blocks, int length)
		{
			_size = length;
			if (blocks.Length == 0)
			{
				_bigBigBlockSize = POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS;
			}
			else
			{
				_bigBigBlockSize = ((blocks[0].BigBlockSize == 512) ? POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS : POIFSConstants.LARGER_BIG_BLOCK_SIZE_DETAILS);
			}
			_big_store = new BigBlockStore(_bigBigBlockSize, ConvertRawBlocksToBigBlocks(blocks));
			_property = new DocumentProperty(name, _size);
			_small_store = new SmallBlockStore(_bigBigBlockSize, EMPTY_SMALL_BLOCK_ARRAY);
			_property.Document = this;
		}

		private static DocumentBlock[] ConvertRawBlocksToBigBlocks(ListManagedBlock[] blocks)
		{
			DocumentBlock[] array = new DocumentBlock[blocks.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DocumentBlock((RawDataBlock)blocks[i]);
			}
			return array;
		}

		private static SmallDocumentBlock[] ConvertRawBlocksToSmallBlocks(ListManagedBlock[] blocks)
		{
			if (blocks is SmallDocumentBlock[])
			{
				return (SmallDocumentBlock[])blocks;
			}
			SmallDocumentBlock[] array = new SmallDocumentBlock[blocks.Length];
			Array.Copy(blocks, 0, array, 0, blocks.Length);
			return array;
		}

		public POIFSDocument(string name, SmallDocumentBlock[] blocks, int length)
		{
			_size = length;
			if (blocks.Length == 0)
			{
				_bigBigBlockSize = POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS;
			}
			else
			{
				_bigBigBlockSize = blocks[0].BigBlockSize;
			}
			_big_store = new BigBlockStore(_bigBigBlockSize, EMPTY_BIG_BLOCK_ARRAY);
			_property = new DocumentProperty(name, _size);
			_small_store = new SmallBlockStore(_bigBigBlockSize, blocks);
			_property.Document = this;
		}

		public POIFSDocument(string name, POIFSBigBlockSize bigBlockSize, ListManagedBlock[] blocks, int length)
		{
			_size = length;
			_bigBigBlockSize = bigBlockSize;
			_property = new DocumentProperty(name, _size);
			_property.Document = this;
			if (Property.IsSmall(_size))
			{
				_big_store = new BigBlockStore(bigBlockSize, EMPTY_BIG_BLOCK_ARRAY);
				_small_store = new SmallBlockStore(bigBlockSize, ConvertRawBlocksToSmallBlocks(blocks));
			}
			else
			{
				_big_store = new BigBlockStore(bigBlockSize, ConvertRawBlocksToBigBlocks(blocks));
				_small_store = new SmallBlockStore(bigBlockSize, EMPTY_SMALL_BLOCK_ARRAY);
			}
		}

		public POIFSDocument(string name, POIFSBigBlockSize bigBlockSize, Stream stream)
		{
			List<DocumentBlock> list = new List<DocumentBlock>();
			_size = 0;
			_bigBigBlockSize = bigBlockSize;
			DocumentBlock documentBlock;
			do
			{
				documentBlock = new DocumentBlock(stream, bigBlockSize);
				int size = documentBlock.Size;
				if (size > 0)
				{
					list.Add(documentBlock);
					_size += size;
				}
			}
			while (!documentBlock.PartiallyRead);
			DocumentBlock[] array = list.ToArray();
			_big_store = new BigBlockStore(bigBlockSize, array);
			_property = new DocumentProperty(name, _size);
			_property.Document = this;
			if (_property.ShouldUseSmallBlocks)
			{
				_small_store = new SmallBlockStore(bigBlockSize, SmallDocumentBlock.Convert(bigBlockSize, array, _size));
				_big_store = new BigBlockStore(bigBlockSize, new DocumentBlock[0]);
			}
			else
			{
				_small_store = new SmallBlockStore(bigBlockSize, EMPTY_SMALL_BLOCK_ARRAY);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.FileSystem.POIFSDocument" /> class.
		/// </summary>
		/// <param name="name">the name of the POIFSDocument</param>
		/// <param name="stream">the InputStream we read data from</param>
		public POIFSDocument(string name, Stream stream)
			: this(name, POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS, stream)
		{
		}

		public POIFSDocument(string name, int size, POIFSBigBlockSize bigBlockSize, POIFSDocumentPath path, POIFSWriterListener writer)
		{
			_size = size;
			_bigBigBlockSize = bigBlockSize;
			_property = new DocumentProperty(name, _size);
			_property.Document = this;
			if (_property.ShouldUseSmallBlocks)
			{
				_small_store = new SmallBlockStore(_bigBigBlockSize, path, name, size, writer);
				_big_store = new BigBlockStore(_bigBigBlockSize, EMPTY_BIG_BLOCK_ARRAY);
			}
			else
			{
				_small_store = new SmallBlockStore(_bigBigBlockSize, EMPTY_SMALL_BLOCK_ARRAY);
				_big_store = new BigBlockStore(_bigBigBlockSize, path, name, size, writer);
			}
		}

		public POIFSDocument(string name, int size, POIFSDocumentPath path, POIFSWriterListener writer)
			: this(name, size, POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS, path, writer)
		{
		}

		/// <summary>
		/// Constructor from small blocks
		/// </summary>
		/// <param name="name">the name of the POIFSDocument</param>
		/// <param name="blocks">the small blocks making up the POIFSDocument</param>
		/// <param name="length">the actual length of the POIFSDocument</param>
		public POIFSDocument(string name, ListManagedBlock[] blocks, int length)
			: this(name, POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS, blocks, length)
		{
		}

		/// <summary>
		/// read data from the internal stores
		/// </summary>
		/// <param name="buffer">the buffer to write to</param>
		/// <param name="offset">the offset into our storage to read from</param>
		public virtual void Read(byte[] buffer, int offset)
		{
			if (_property.ShouldUseSmallBlocks)
			{
				SmallDocumentBlock.Read(_small_store.Blocks, buffer, offset);
			}
			else
			{
				DocumentBlock.Read(_big_store.Blocks, buffer, offset);
			}
		}

		/// <summary>
		/// Writes the blocks.
		/// </summary>
		/// <param name="stream">The stream.</param>
		public virtual void WriteBlocks(Stream stream)
		{
			_big_store.WriteBlocks(stream);
		}

		public DataInputBlock GetDataInputBlock(int offset)
		{
			if (offset >= _size)
			{
				if (offset > _size)
				{
					throw new Exception("Request for Offset " + offset + " doc size is " + _size);
				}
				return null;
			}
			if (_property.ShouldUseSmallBlocks)
			{
				return SmallDocumentBlock.GetDataInputBlock(_small_store.Blocks, offset);
			}
			return DocumentBlock.GetDataInputBlock(_big_store.Blocks, offset);
		}

		protected virtual void OnBeforeWriting(POIFSWriterEventArgs e)
		{
			if (this.BeforeWriting != null)
			{
				this.BeforeWriting(this, e);
			}
		}
	}
}
