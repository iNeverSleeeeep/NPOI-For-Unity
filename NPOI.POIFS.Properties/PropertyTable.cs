using NPOI.POIFS.Common;
using NPOI.POIFS.Storage;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Properties
{
	public class PropertyTable : PropertyTableBase, BlockWritable
	{
		private POIFSBigBlockSize _bigBigBlockSize;

		private BlockWritable[] _blocks;

		/// Return the number of BigBlock's this instance uses
		///
		/// @return count of BigBlock instances
		public override int CountBlocks
		{
			get
			{
				if (_blocks != null)
				{
					return _blocks.Length;
				}
				return 0;
			}
		}

		/// Default constructor
		public PropertyTable(HeaderBlock headerBlock)
			: base(headerBlock)
		{
			_bigBigBlockSize = headerBlock.BigBlockSize;
			_blocks = null;
		}

		/// reading constructor (used when we've read in a file and we want
		/// to extract the property table from it). Populates the
		/// properties thoroughly
		///
		/// @param startBlock the first block of the property table
		/// @param blockList the list of blocks
		///
		/// @exception IOException if anything goes wrong (which should be
		///            a result of the input being NFG)
		public PropertyTable(HeaderBlock headerBlock, RawDataBlockList blockList)
			: base(headerBlock, PropertyFactory.ConvertToProperties(blockList.FetchBlocks(headerBlock.PropertyStart, -1)))
		{
			_bigBigBlockSize = headerBlock.BigBlockSize;
			_blocks = null;
		}

		/// Prepare to be written Leon
		public void PreWrite()
		{
			List<Property> list = new List<Property>(_properties.Count);
			for (int i = 0; i < _properties.Count; i++)
			{
				list.Add(_properties[i]);
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].Index = j;
			}
			_blocks = PropertyBlock.CreatePropertyBlockArray(_bigBigBlockSize, list);
			for (int k = 0; k < list.Count; k++)
			{
				list[k].PreWrite();
			}
		}

		/// Write the storage to an Stream
		///
		/// @param stream the Stream to which the stored data should
		///               be written
		///
		/// @exception IOException on problems writing to the specified
		///            stream
		public void WriteBlocks(Stream stream)
		{
			if (_blocks != null)
			{
				for (int i = 0; i < _blocks.Length; i++)
				{
					_blocks[i].WriteBlocks(stream);
				}
			}
		}
	}
}
