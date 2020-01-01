using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.DDF
{
	/// Common abstract class for {@link EscherOptRecord} and
	/// {@link EscherTertiaryOptRecord}
	///
	/// @author Sergey Vladimirov (vlsergey {at} gmail {dot} com)
	/// @author Glen Stampoultzis
	public abstract class AbstractEscherOptRecord : EscherRecord
	{
		internal class EscherPropertyComparer : IComparer<EscherProperty>
		{
			public int Compare(EscherProperty p1, EscherProperty p2)
			{
				short propertyNumber = p1.PropertyNumber;
				short propertyNumber2 = p2.PropertyNumber;
				if (propertyNumber >= propertyNumber2)
				{
					if (propertyNumber != propertyNumber2)
					{
						return 1;
					}
					return 0;
				}
				return -1;
			}
		}

		protected List<EscherProperty> properties = new List<EscherProperty>();

		/// The list of properties stored by this record.
		public List<EscherProperty> EscherProperties => properties;

		private int PropertiesSize
		{
			get
			{
				int num = 0;
				foreach (EscherProperty property in properties)
				{
					num += property.PropertySize;
				}
				return num;
			}
		}

		public override int RecordSize => 8 + PropertiesSize;

		/// Add a property to this record.
		public void AddEscherProperty(EscherProperty prop)
		{
			properties.Add(prop);
		}

		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			short numProperties = EscherRecord.ReadInstance(data, offset);
			int offset2 = offset + 8;
			EscherPropertyFactory escherPropertyFactory = new EscherPropertyFactory();
			properties = escherPropertyFactory.CreateProperties(data, offset2, numProperties);
			return num + 8;
		}

		/// The list of properties stored by this record.
		public EscherProperty GetEscherProperty(int index)
		{
			return properties[index];
		}

		public EscherProperty Lookup(int propId)
		{
			foreach (EscherProperty property in properties)
			{
				if (property.PropertyNumber == propId)
				{
					return property;
				}
			}
			return null;
		}

		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			LittleEndian.PutInt(data, offset + 4, PropertiesSize);
			int num = offset + 8;
			foreach (EscherProperty property in properties)
			{
				num += property.SerializeSimplePart(data, num);
			}
			foreach (EscherProperty property2 in properties)
			{
				num += property2.SerializeComplexPart(data, num);
			}
			listener.AfterRecordSerialize(num, RecordId, num - offset, this);
			return num - offset;
		}

		/// Records should be sorted by property number before being stored.
		public void SortProperties()
		{
			properties.Sort(new EscherPropertyComparer());
		}

		/// * Set an escher property. If a property with given propId already
		/// exists it is replaced.
		/// *
		/// * @param value the property to set.
		public void SetEscherProperty(EscherProperty value)
		{
			List<EscherProperty> list = new List<EscherProperty>();
			IEnumerator<EscherProperty> enumerator = properties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherProperty current = enumerator.Current;
				if (current.Id == value.Id)
				{
					list.Add(current);
				}
			}
			foreach (EscherProperty item in list)
			{
				EscherProperties.Remove(item);
			}
			properties.Add(value);
			SortProperties();
		}

		public void RemoveEscherProperty(int num)
		{
			List<EscherProperty> list = new List<EscherProperty>();
			IEnumerator<EscherProperty> enumerator = EscherProperties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherProperty current = enumerator.Current;
				if (current.PropertyNumber == num)
				{
					list.Add(current);
				}
			}
			foreach (EscherProperty item in list)
			{
				EscherProperties.Remove(item);
			}
		}

		/// Retrieve the string representation of this record.
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append(":");
			stringBuilder.Append(newLine);
			stringBuilder.Append("  isContainer: ");
			stringBuilder.Append(base.IsContainerRecord);
			stringBuilder.Append(newLine);
			stringBuilder.Append("  version: 0x");
			stringBuilder.Append(HexDump.ToHex(Version));
			stringBuilder.Append(newLine);
			stringBuilder.Append("  instance: 0x");
			stringBuilder.Append(HexDump.ToHex(Instance));
			stringBuilder.Append(newLine);
			stringBuilder.Append("  recordId: 0x");
			stringBuilder.Append(HexDump.ToHex(RecordId));
			stringBuilder.Append(newLine);
			stringBuilder.Append("  numchildren: ");
			stringBuilder.Append(ChildRecords.Count);
			stringBuilder.Append(newLine);
			stringBuilder.Append("  properties:");
			stringBuilder.Append(newLine);
			foreach (EscherProperty property in properties)
			{
				stringBuilder.Append("    " + property.ToString() + newLine);
			}
			return stringBuilder.ToString();
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance)));
			foreach (EscherProperty escherProperty in EscherProperties)
			{
				stringBuilder.Append(escherProperty.ToXml(tab + "\t"));
			}
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}
	}
}
