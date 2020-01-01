using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NPOI.DDF
{
	/// <summary>
	/// Generates escher records when provided the byte array containing those records.
	/// @author Glen Stampoultzis
	/// @author Nick Burch   (nick at torchbox . com)
	/// </summary>
	public class DefaultEscherRecordFactory : IEscherRecordFactory
	{
		private static Type[] escherRecordClasses = new Type[12]
		{
			typeof(EscherBSERecord),
			typeof(EscherOptRecord),
			typeof(EscherTertiaryOptRecord),
			typeof(EscherClientAnchorRecord),
			typeof(EscherDgRecord),
			typeof(EscherSpgrRecord),
			typeof(EscherSpRecord),
			typeof(EscherClientDataRecord),
			typeof(EscherDggRecord),
			typeof(EscherSplitMenuColorsRecord),
			typeof(EscherChildAnchorRecord),
			typeof(EscherTextboxRecord)
		};

		private static Dictionary<short, ConstructorInfo> recordsMap = RecordsToMap(escherRecordClasses);

		/// <summary>
		/// Generates an escher record including the any children contained under that record.
		/// An exception is thrown if the record could not be generated.
		/// </summary>
		/// <param name="data">The byte array containing the records</param>
		/// <param name="offset">The starting offset into the byte array</param>
		/// <returns>The generated escher record</returns>
		public virtual EscherRecord CreateRecord(byte[] data, int offset)
		{
			short @short = LittleEndian.GetShort(data, offset);
			short short2 = LittleEndian.GetShort(data, offset + 2);
			if (IsContainer(@short, short2))
			{
				EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
				escherContainerRecord.RecordId = short2;
				escherContainerRecord.Options = @short;
				return escherContainerRecord;
			}
			if (short2 >= -4072 && short2 <= -3817)
			{
				EscherBlipRecord escherBlipRecord;
				switch (short2)
				{
				case -4067:
				case -4066:
				case -4065:
					escherBlipRecord = new EscherBitmapBlip();
					break;
				case -4070:
				case -4069:
				case -4068:
					escherBlipRecord = new EscherMetafileBlip();
					break;
				default:
					escherBlipRecord = new EscherBlipRecord();
					break;
				}
				escherBlipRecord.RecordId = short2;
				escherBlipRecord.Options = @short;
				return escherBlipRecord;
			}
			ConstructorInfo constructorInfo = null;
			if (recordsMap.ContainsKey(short2))
			{
				constructorInfo = recordsMap[short2];
			}
			EscherRecord escherRecord = null;
			if (constructorInfo == null)
			{
				return new UnknownEscherRecord();
			}
			try
			{
				escherRecord = (EscherRecord)constructorInfo.Invoke(new object[0]);
			}
			catch (Exception)
			{
				return new UnknownEscherRecord();
			}
			escherRecord.RecordId = short2;
			escherRecord.Options = @short;
			return escherRecord;
		}

		/// <summary>
		/// Converts from a list of classes into a map that Contains the record id as the key and
		/// the Constructor in the value part of the map.  It does this by using reflection to look up
		/// the RECORD_ID field then using reflection again to find a reference to the constructor.
		/// </summary>
		/// <param name="records">The records to convert</param>
		/// <returns>The map containing the id/constructor pairs.</returns>
		private static Dictionary<short, ConstructorInfo> RecordsToMap(Type[] records)
		{
			Dictionary<short, ConstructorInfo> dictionary = new Dictionary<short, ConstructorInfo>();
			Type[] types = new Type[0];
			foreach (Type type in records)
			{
				short num = 0;
				try
				{
					num = (short)type.GetField("RECORD_ID").GetValue(null);
				}
				catch (Exception)
				{
					throw new RecordFormatException("Unable to determine record types");
				}
				ConstructorInfo constructor;
				try
				{
					constructor = type.GetConstructor(types);
				}
				catch (Exception e)
				{
					throw new RuntimeException(e);
				}
				dictionary.Add(num, constructor);
			}
			return dictionary;
		}

		public static bool IsContainer(short options, short recordId)
		{
			if (recordId >= -4096 && recordId <= -4091)
			{
				return true;
			}
			if (recordId == -4083)
			{
				return false;
			}
			return (options & 0xF) == 15;
		}
	}
}
