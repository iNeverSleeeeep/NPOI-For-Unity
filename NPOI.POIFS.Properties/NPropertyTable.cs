using NPOI.POIFS.Common;
using NPOI.POIFS.FileSystem;
using NPOI.POIFS.Storage;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Properties
{
	public class NPropertyTable : PropertyTableBase
	{
		private POIFSBigBlockSize _bigBigBlockSize;

		public override int CountBlocks
		{
			get
			{
				int num = _properties.Count * 128;
				return (int)Math.Ceiling(1.0 * (double)num / (double)_bigBigBlockSize.GetBigBlockSize());
			}
		}

		public NPropertyTable(HeaderBlock headerBlock)
			: base(headerBlock)
		{
			_bigBigBlockSize = headerBlock.BigBlockSize;
		}

		public NPropertyTable(HeaderBlock headerBlock, NPOIFSFileSystem fileSystem)
			: base(headerBlock, BuildProperties(new NPOIFSStream(fileSystem, headerBlock.PropertyStart).GetEnumerator(), headerBlock.BigBlockSize))
		{
			_bigBigBlockSize = headerBlock.BigBlockSize;
		}

		private static List<Property> BuildProperties(IEnumerator<ByteBuffer> dataSource, POIFSBigBlockSize bigBlockSize)
		{
			try
			{
				List<Property> list = new List<Property>();
				while (dataSource.MoveNext())
				{
					ByteBuffer current = dataSource.Current;
					byte[] array;
					if (current.HasBuffer && current.Offset == 0 && current.Buffer.Length == bigBlockSize.GetBigBlockSize())
					{
						array = current.Buffer;
					}
					else
					{
						array = new byte[bigBlockSize.GetBigBlockSize()];
						int length = array.Length;
						if (current.Remaining() < bigBlockSize.GetBigBlockSize())
						{
							length = current.Remaining();
						}
						current.Read(array, 0, length);
					}
					PropertyFactory.ConvertToProperties(array, list);
				}
				return list;
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		public void Write(NPOIFSStream stream)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				foreach (Property property in _properties)
				{
					property?.WriteData(memoryStream);
				}
				stream.UpdateContents(memoryStream.ToArray());
				if (StartBlock != stream.GetStartBlock())
				{
					StartBlock = stream.GetStartBlock();
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}
	}
}
