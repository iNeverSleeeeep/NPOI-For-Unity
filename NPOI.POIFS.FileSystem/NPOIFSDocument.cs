using NPOI.POIFS.Dev;
using NPOI.POIFS.Properties;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NPOI.POIFS.FileSystem
{
	/// This class manages a document in the NIO POIFS filesystem.
	/// This is the {@link NPOIFSFileSystem} version.
	public class NPOIFSDocument : POIFSViewable
	{
		private DocumentProperty _property;

		private NPOIFSFileSystem _filesystem;

		private NPOIFSStream _stream;

		private int _block_size;

		/// @return size of the document
		public int Size => _property.Size;

		/// @return the instance's DocumentProperty
		public DocumentProperty DocumentProperty => _property;

		public bool PreferArray => true;

		public string ShortDescription => GetShortDescription();

		public Array ViewableArray => GetViewableArray();

		public IEnumerator ViewableIterator => GetViewableIterator();

		/// Constructor for an existing Document 
		public NPOIFSDocument(DocumentProperty property, NPOIFSFileSystem filesystem)
		{
			_property = property;
			_filesystem = filesystem;
			if (property.Size < 4096)
			{
				_stream = new NPOIFSStream(_filesystem.GetMiniStore(), property.StartBlock);
				_block_size = _filesystem.GetMiniStore().GetBlockStoreBlockSize();
			}
			else
			{
				_stream = new NPOIFSStream(_filesystem, property.StartBlock);
				_block_size = _filesystem.GetBlockStoreBlockSize();
			}
		}

		/// Constructor for a new Document
		///
		/// @param name the name of the POIFSDocument
		/// @param stream the InputStream we read data from
		public NPOIFSDocument(string name, NPOIFSFileSystem filesystem, Stream stream)
		{
			_filesystem = filesystem;
			byte[] array;
			if (stream is MemoryStream)
			{
				MemoryStream memoryStream = (MemoryStream)stream;
				array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, array.Length);
			}
			else
			{
				MemoryStream memoryStream2 = new MemoryStream();
				IOUtils.Copy(stream, memoryStream2);
				array = memoryStream2.ToArray();
			}
			if (array.Length <= 4096)
			{
				_stream = new NPOIFSStream(filesystem.GetMiniStore());
				_block_size = _filesystem.GetMiniStore().GetBlockStoreBlockSize();
			}
			else
			{
				_stream = new NPOIFSStream(filesystem);
				_block_size = _filesystem.GetBlockStoreBlockSize();
			}
			_stream.UpdateContents(array);
			_property = new DocumentProperty(name, array.Length);
			_property.StartBlock = _stream.GetStartBlock();
		}

		public int GetDocumentBlockSize()
		{
			return _block_size;
		}

		public IEnumerator<ByteBuffer> GetBlockIterator()
		{
			if (Size > 0)
			{
				return _stream.GetBlockIterator();
			}
			List<ByteBuffer> list = new List<ByteBuffer>();
			return list.GetEnumerator();
		}

		/// Get an array of objects, some of which may implement POIFSViewable
		///
		/// @return an array of Object; may not be null, but may be empty
		protected object[] GetViewableArray()
		{
			object[] array = new object[1];
			string text;
			try
			{
				if (Size > 0)
				{
					byte[] array2 = new byte[Size];
					int num = 0;
					foreach (ByteBuffer item in _stream)
					{
						int num2 = Math.Min(_block_size, array2.Length - num);
						item.Read(array2, num, num2);
						num += num2;
					}
					MemoryStream memoryStream = new MemoryStream();
					HexDump.Dump(array2, 0L, memoryStream, 0);
					text = memoryStream.ToString();
				}
				else
				{
					text = "<NO DATA>";
				}
			}
			catch (IOException ex)
			{
				text = ex.Message;
			}
			array[0] = text;
			return array;
		}

		/// Get an Iterator of objects, some of which may implement POIFSViewable
		///
		/// @return an Iterator; may not be null, but may have an empty back end
		///             		 store
		protected IEnumerator GetViewableIterator()
		{
			return null;
		}

		/// Provides a short description of the object, to be used when a
		/// POIFSViewable object has not provided its contents.
		///
		/// @return short description
		protected string GetShortDescription()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Document: \"").Append(_property.Name).Append("\"");
			stringBuilder.Append(" size = ").Append(Size);
			return stringBuilder.ToString();
		}
	}
}
