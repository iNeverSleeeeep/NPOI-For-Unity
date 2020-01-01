using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// The base abstract record from which all escher records are defined.  Subclasses will need
	/// to define methods for serialization/deserialization and for determining the record size.
	/// @author Glen Stampoultzis
	/// </summary>
	public abstract class EscherRecord : ICloneable
	{
		/// <summary>
		/// This class Reads the standard escher header.
		/// </summary>
		internal class DeleteEscherRecordHeader
		{
			private short options;

			private short recordId;

			private int remainingBytes;

			/// <summary>
			/// Gets the options.
			/// </summary>
			/// <value>The options.</value>
			public short Options => options;

			/// <summary>
			/// Gets the record id.
			/// </summary>
			/// <value>The record id.</value>
			public virtual short RecordId => recordId;

			/// <summary>
			/// Gets the remaining bytes.
			/// </summary>
			/// <value>The remaining bytes.</value>
			public int RemainingBytes => remainingBytes;

			private DeleteEscherRecordHeader()
			{
			}

			/// <summary>
			/// Reads the header.
			/// </summary>
			/// <param name="data">The data.</param>
			/// <param name="offset">The off set.</param>
			/// <returns></returns>
			public static DeleteEscherRecordHeader ReadHeader(byte[] data, int offset)
			{
				DeleteEscherRecordHeader deleteEscherRecordHeader = new DeleteEscherRecordHeader();
				deleteEscherRecordHeader.options = LittleEndian.GetShort(data, offset);
				deleteEscherRecordHeader.recordId = LittleEndian.GetShort(data, offset + 2);
				deleteEscherRecordHeader.remainingBytes = LittleEndian.GetInt(data, offset + 4);
				return deleteEscherRecordHeader;
			}

			/// <summary>
			/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
			/// </summary>
			/// <returns>
			/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
			/// </returns>
			public override string ToString()
			{
				return "EscherRecordHeader{options=" + options + ", recordId=" + recordId + ", remainingBytes=" + remainingBytes + "}";
			}
		}

		private static BitField fInstance = BitFieldFactory.GetInstance(65520);

		private static BitField fVersion = BitFieldFactory.GetInstance(15);

		private short _options;

		private short _recordId;

		/// <summary>
		/// Determine whether this is a container record by inspecting the option
		/// field.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is container record; otherwise, <c>false</c>.
		/// </value>
		public bool IsContainerRecord => Version == 15;

		/// <summary>
		/// Gets or sets the options field for this record.  All records have one
		/// </summary>
		/// <value>The options.</value>
		internal virtual short Options
		{
			get
			{
				return _options;
			}
			set
			{
				Version = fVersion.GetShortValue(value);
				Instance = fInstance.GetShortValue(value);
				_options = value;
			}
		}

		/// <summary>
		/// Subclasses should effeciently return the number of bytes required to
		/// Serialize the record.
		/// </summary>
		/// <value>number of bytes</value>
		public abstract int RecordSize
		{
			get;
		}

		/// <summary>
		/// Return the current record id.
		/// </summary>
		/// <value>The 16 bit record id.</value>
		public virtual short RecordId
		{
			get
			{
				return _recordId;
			}
			set
			{
				_recordId = value;
			}
		}

		/// <summary>
		/// Gets or sets the child records.
		/// </summary>
		/// <value>Returns the children of this record.  By default this will
		/// be an empty list.  EscherCotainerRecord is the only record that may contain children.</value>
		public virtual List<EscherRecord> ChildRecords
		{
			get
			{
				return new List<EscherRecord>();
			}
			set
			{
				throw new ArgumentException("This record does not support child records.");
			}
		}

		/// <summary>
		/// Gets the name of the record.
		/// </summary>
		/// <value>The name of the record.</value>
		public abstract string RecordName
		{
			get;
		}

		/// <summary>
		/// Get or set the instance part of the option record.
		/// </summary>
		public virtual short Instance
		{
			get
			{
				return fInstance.GetShortValue(_options);
			}
			set
			{
				_options = fInstance.SetShortValue(_options, value);
			}
		}

		/// <summary>
		/// Get or set the version part of the option record.
		/// </summary>
		public virtual short Version
		{
			get
			{
				return fVersion.GetShortValue(_options);
			}
			set
			{
				_options = fVersion.SetShortValue(_options, value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.DDF.EscherRecord" /> class.
		/// </summary>
		public EscherRecord()
		{
		}

		/// <summary>
		/// Delegates to FillFields(byte[], int, EscherRecordFactory)
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="f">The f.</param>
		/// <returns></returns>
		public int FillFields(byte[] data, IEscherRecordFactory f)
		{
			return FillFields(data, 0, f);
		}

		/// <summary>
		/// The contract of this method is to deSerialize an escher record including
		/// it's children.
		/// </summary>
		/// <param name="data">The byte array containing the Serialized escher
		/// records.</param>
		/// <param name="offset">The offset into the byte array.</param>
		/// <param name="recordFactory">A factory for creating new escher records.</param>
		/// <returns>The number of bytes written.</returns>       
		public abstract int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory);

		/// <summary>
		/// Reads the 8 byte header information and populates the 
		/// <c>options</c>
		/// and 
		/// <c>recordId</c>
		///  records.
		/// </summary>
		/// <param name="data">the byte array to Read from</param>
		/// <param name="offset">the offset to start Reading from</param>
		/// <returns>the number of bytes remaining in this record.  This</returns>
		protected int ReadHeader(byte[] data, int offset)
		{
			_options = LittleEndian.GetShort(data, offset);
			_recordId = LittleEndian.GetShort(data, offset + 2);
			return LittleEndian.GetInt(data, offset + 4);
		}

		/// <summary>
		/// Read the options field from header and return instance part of it.
		/// </summary>
		/// <param name="data">the byte array to read from</param>
		/// <param name="offset">the offset to start reading from</param>
		/// <returns>value of instance part of options field</returns>
		protected static short ReadInstance(byte[] data, int offset)
		{
			short @short = LittleEndian.GetShort(data, offset);
			return fInstance.GetShortValue(@short);
		}

		/// <summary>
		/// Serializes to a new byte array.  This is done by delegating to
		/// Serialize(int, byte[]);
		/// </summary>
		/// <returns>the Serialized record.</returns>
		public byte[] Serialize()
		{
			byte[] array = new byte[RecordSize];
			Serialize(0, array);
			return array;
		}

		/// <summary>
		/// Serializes to an existing byte array without serialization listener.
		/// This is done by delegating to Serialize(int, byte[], EscherSerializationListener).
		/// </summary>
		/// <param name="offset">the offset within the data byte array.</param>
		/// <param name="data">the data array to Serialize to.</param>
		/// <returns>The number of bytes written.</returns>
		public int Serialize(int offset, byte[] data)
		{
			return Serialize(offset, data, new NullEscherSerializationListener());
		}

		/// <summary>
		/// Serializes the record to an existing byte array.
		/// </summary>
		/// <param name="offset">the offset within the byte array.</param>
		/// <param name="data">the offset within the byte array</param>
		/// <param name="listener">a listener for begin and end serialization events.  This.
		/// is useful because the serialization is
		/// hierarchical/recursive and sometimes you need to be able
		/// break into that.
		/// </param>
		/// <returns></returns>
		public abstract int Serialize(int offset, byte[] data, EscherSerializationListener listener);

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public object Clone()
		{
			throw new Exception("The class " + GetType().Name + " needs to define a clone method");
		}

		/// <summary>
		/// Returns the indexed child record.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public EscherRecord GetChild(int index)
		{
			return ChildRecords[index];
		}

		/// <summary>
		/// The display methods allows escher variables to print the record names
		/// according to their hierarchy.
		/// </summary>
		/// <param name="indent">The current indent level.</param>  
		public virtual void Display(int indent)
		{
			for (int i = 0; i < indent * 4; i++)
			{
				Console.Write(' ');
			}
			Console.WriteLine(RecordName);
		}

		/// @param tab - each children must be a right of his parent
		/// @return xml representation of this record
		public virtual string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append("<").Append(GetType().Name)
				.Append(">\n")
				.Append(tab)
				.Append("\t")
				.Append("<RecordId>0x")
				.Append(HexDump.ToHex(_recordId))
				.Append("</RecordId>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Options>")
				.Append(_options)
				.Append("</Options>\n")
				.Append(tab)
				.Append("</")
				.Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		protected string FormatXmlRecordHeader(string className, string recordId, string version, string instance)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<").Append(className).Append(" recordId=\"0x")
				.Append(recordId)
				.Append("\" version=\"0x")
				.Append(version)
				.Append("\" instance=\"0x")
				.Append(instance)
				.Append("\" size=\"")
				.Append(RecordSize)
				.Append("\">\n");
			return stringBuilder.ToString();
		}

		public string ToXml()
		{
			return ToXml("");
		}
	}
}
