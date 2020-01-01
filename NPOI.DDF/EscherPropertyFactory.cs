using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.DDF
{
	/// <summary>
	/// Generates a property given a reference into the byte array storing that property.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherPropertyFactory
	{
		/// <summary>
		/// Create new properties from a byte array.
		/// </summary>
		/// <param name="data">The byte array containing the property</param>
		/// <param name="offset">The starting offset into the byte array</param>
		/// <param name="numProperties">The new properties</param>
		/// <returns></returns>        
		public List<EscherProperty> CreateProperties(byte[] data, int offset, short numProperties)
		{
			List<EscherProperty> list = new List<EscherProperty>();
			int num = offset;
			for (int i = 0; i < numProperties; i++)
			{
				short @short = LittleEndian.GetShort(data, num);
				int @int = LittleEndian.GetInt(data, num + 2);
				short propertyId = (short)(@short & 0x3FFF);
				bool flag = (@short & -32768) != 0;
				byte propertyType = EscherProperties.GetPropertyType(propertyId);
				switch (propertyType)
				{
				case 1:
					list.Add(new EscherBoolProperty(@short, @int));
					break;
				case 2:
					list.Add(new EscherRGBProperty(@short, @int));
					break;
				case 3:
					list.Add(new EscherShapePathProperty(@short, @int));
					break;
				default:
					if (!flag)
					{
						list.Add(new EscherSimpleProperty(@short, @int));
					}
					else if (propertyType == 5)
					{
						list.Add(new EscherArrayProperty(@short, new byte[@int]));
					}
					else
					{
						list.Add(new EscherComplexProperty(@short, new byte[@int]));
					}
					break;
				}
				num += 6;
			}
			IEnumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherProperty escherProperty = (EscherProperty)enumerator.Current;
				if (escherProperty is EscherComplexProperty)
				{
					if (escherProperty is EscherArrayProperty)
					{
						num += ((EscherArrayProperty)escherProperty).SetArrayData(data, num);
					}
					else
					{
						byte[] complexData = ((EscherComplexProperty)escherProperty).ComplexData;
						Array.Copy(data, num, complexData, 0, complexData.Length);
						num += complexData.Length;
					}
				}
			}
			return list;
		}
	}
}
