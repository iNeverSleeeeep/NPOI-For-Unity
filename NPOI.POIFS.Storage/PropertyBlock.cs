using NPOI.POIFS.Common;
using NPOI.POIFS.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// A block of Property instances
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class PropertyBlock : BigBlock
	{
		private class AnonymousProperty : Property
		{
			public override bool IsDirectory => false;

			public override void PreWrite()
			{
			}
		}

		private Property[] _properties;

		/// <summary>
		/// Create a single instance initialized with default values
		/// </summary>
		/// <param name="bigBlockSize"></param>
		/// <param name="properties">the properties to be inserted</param>
		/// <param name="offset">the offset into the properties array</param>
		protected PropertyBlock(POIFSBigBlockSize bigBlockSize, Property[] properties, int offset)
			: base(bigBlockSize)
		{
			_properties = new Property[bigBlockSize.GetPropertiesPerBlock()];
			for (int i = 0; i < _properties.Length; i++)
			{
				_properties[i] = properties[i + offset];
			}
		}

		/// <summary>
		/// Create an array of PropertyBlocks from an array of Property
		/// instances, creating empty Property instances to make up any
		/// shortfall
		/// </summary>
		/// <param name="bigBlockSize"></param>
		/// <param name="properties">the Property instances to be converted into PropertyBlocks, in a java List</param>
		/// <returns>the array of newly created PropertyBlock instances</returns>
		public static BlockWritable[] CreatePropertyBlockArray(POIFSBigBlockSize bigBlockSize, List<Property> properties)
		{
			int propertiesPerBlock = bigBlockSize.GetPropertiesPerBlock();
			int num = (properties.Count + propertiesPerBlock - 1) / propertiesPerBlock;
			Property[] array = new Property[num * propertiesPerBlock];
			Array.Copy(properties.ToArray(), 0, array, 0, properties.Count);
			for (int i = properties.Count; i < array.Length; i++)
			{
				array[i] = new AnonymousProperty();
			}
			BlockWritable[] array2 = new BlockWritable[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = new PropertyBlock(bigBlockSize, array, j * propertiesPerBlock);
			}
			return array2;
		}

		/// <summary>
		/// Write the block's data to an OutputStream
		/// </summary>
		/// <param name="stream">the OutputStream to which the stored data should be written</param>
		public override void WriteData(Stream stream)
		{
			int propertiesPerBlock = bigBlockSize.GetPropertiesPerBlock();
			for (int i = 0; i < propertiesPerBlock; i++)
			{
				_properties[i].WriteData(stream);
			}
		}
	}
}
