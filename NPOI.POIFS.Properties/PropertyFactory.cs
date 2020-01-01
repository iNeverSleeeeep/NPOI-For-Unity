using NPOI.POIFS.Storage;
using System.Collections.Generic;

namespace NPOI.POIFS.Properties
{
	public class PropertyFactory
	{
		private PropertyFactory()
		{
		}

		/// <summary>
		/// Convert raw data blocks to an array of Property's
		/// </summary>
		/// <param name="blocks">The blocks to be converted</param>
		/// <returns>the converted List of Property objects. May contain
		/// nulls, but will not be null</returns>
		public static List<Property> ConvertToProperties(ListManagedBlock[] blocks)
		{
			List<Property> list = new List<Property>();
			for (int i = 0; i < blocks.Length; i++)
			{
				byte[] data = blocks[i].Data;
				ConvertToProperties(data, list);
			}
			return list;
		}

		public static void ConvertToProperties(byte[] data, List<Property> properties)
		{
			int num = data.Length / 128;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				switch (data[num2 + 66])
				{
				case 1:
					properties.Add(new DirectoryProperty(properties.Count, data, num2));
					break;
				case 2:
					properties.Add(new DocumentProperty(properties.Count, data, num2));
					break;
				case 5:
					properties.Add(new RootProperty(properties.Count, data, num2));
					break;
				default:
					properties.Add(null);
					break;
				}
				num2 += 128;
			}
		}
	}
}
