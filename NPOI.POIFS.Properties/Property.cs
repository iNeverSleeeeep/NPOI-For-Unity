using NPOI.POIFS.Dev;
using NPOI.Util;
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace NPOI.POIFS.Properties
{
	/// <summary>
	/// This abstract base class is the ancestor of all classes
	/// implementing POIFS Property behavior.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public abstract class Property : Child, POIFSViewable
	{
		private const byte _default_fill = 0;

		private const int _name_size_offset = 64;

		private const int _max_name_length = 31;

		protected const int _NO_INDEX = -1;

		private const int _node_color_offset = 67;

		private const int _previous_property_offset = 68;

		private const int _next_property_offset = 72;

		private const int _child_property_offset = 76;

		private const int _storage_clsid_offset = 80;

		private const int _user_flags_offset = 96;

		private const int _seconds_1_offset = 100;

		private const int _days_1_offset = 104;

		private const int _seconds_2_offset = 108;

		private const int _days_2_offset = 112;

		private const int _start_block_offset = 116;

		private const int _size_offset = 120;

		protected const byte _NODE_BLACK = 1;

		protected const byte _NODE_RED = 0;

		private const int _big_block_minimum_bytes = 4096;

		private string _name;

		private ShortField _name_size;

		private ByteField _property_type;

		private ByteField _node_color;

		private IntegerField _previous_property;

		private IntegerField _next_property;

		private IntegerField _child_property;

		private ClassID _storage_clsid;

		private IntegerField _user_flags;

		private IntegerField _seconds_1;

		private IntegerField _days_1;

		private IntegerField _seconds_2;

		private IntegerField _days_2;

		private IntegerField _start_block;

		private IntegerField _size;

		private byte[] _raw_data;

		private int _index;

		private Child _next_child;

		private Child _previous_child;

		/// <summary>
		/// Gets or sets the start block for the document referred to by this
		/// Property.
		/// </summary>
		/// <value>the start block index</value>
		public int StartBlock
		{
			get
			{
				return _start_block.Value;
			}
			set
			{
				_start_block.Set(value, _raw_data);
			}
		}

		/// <summary>
		/// Based on the currently defined size, Should this property use
		/// small blocks?
		/// </summary>
		/// <returns>true if the size Is less than _big_block_minimum_bytes</returns>
		public bool ShouldUseSmallBlocks => IsSmall(_size.Value);

		/// <summary>
		/// Gets or sets the name of this property
		/// </summary>
		/// <value>property name</value>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				char[] array = value.ToCharArray();
				int num = Math.Min(array.Length, 31);
				_name = new string(array, 0, num);
				short num2 = 0;
				int i;
				for (i = 0; i < num; i++)
				{
					ShortField.Write(num2, (short)array[i], ref _raw_data);
					num2 = (short)(num2 + 2);
				}
				for (; i < 32; i++)
				{
					ShortField.Write(num2, 0, ref _raw_data);
					num2 = (short)(num2 + 2);
				}
				_name_size.Set((short)((num + 1) * 2), ref _raw_data);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is directory.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if a directory type Property; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsDirectory => false;

		/// <summary>
		/// Gets or sets the storage class ID for this property stream. ThIs Is the Class ID
		/// of the COM object which can read and write this property stream </summary>
		/// <value>Storage Class ID</value>
		public ClassID StorageClsid
		{
			get
			{
				return _storage_clsid;
			}
			set
			{
				_storage_clsid = value;
				if (value == null)
				{
					for (int i = 80; i < 96; i++)
					{
						_raw_data[i] = 0;
					}
				}
				else
				{
					value.Write(_raw_data, 80);
				}
			}
		}

		/// <summary>
		/// Set the property type. Makes no attempt to validate the value.
		/// </summary>
		/// <value>the property type (root, file, directory)</value>
		public byte PropertyType
		{
			set
			{
				_property_type.Set(value, _raw_data);
			}
		}

		/// <summary>
		/// Sets the color of the node.
		/// </summary>
		/// <value>the node color (red or black)</value>
		public byte NodeColor
		{
			set
			{
				_node_color.Set(value, _raw_data);
			}
		}

		/// <summary>
		/// Sets the child property.
		/// </summary>
		/// <value>the child property's index in the Property Table</value>
		public int ChildProperty
		{
			set
			{
				_child_property.Set(value, _raw_data);
			}
		}

		/// <summary>
		/// Get the child property (its index in the Property Table)
		/// </summary>
		/// <value>The index of the child.</value>
		public int ChildIndex => _child_property.Value;

		/// <summary>
		/// Gets or sets the size of the document associated with this Property
		/// </summary>
		/// <value>the size of the document, in bytes</value>
		public virtual int Size
		{
			get
			{
				return _size.Value;
			}
			set
			{
				_size.Set(value, _raw_data);
			}
		}

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		/// Get the index for this Property
		/// @return the index of this Property within its Property Table
		public int Index
		{
			get
			{
				return _index;
			}
			set
			{
				_index = value;
			}
		}

		/// <summary>
		/// Gets the index of the next child.
		/// </summary>
		/// <value>The index of the next child.</value>
		public int NextChildIndex => _next_property.Value;

		/// <summary>
		/// Gets the index of the previous child.
		/// </summary>
		/// <value>The index of the previous child.</value>
		public int PreviousChildIndex => _previous_property.Value;

		/// <summary>
		/// Gets or sets the previous child.
		/// </summary>
		/// <value>the new 'previous' child; may be null, which has
		/// the effect of saying there Is no 'previous' child</value>
		public Child PreviousChild
		{
			get
			{
				return _previous_child;
			}
			set
			{
				_previous_child = value;
				_previous_property.Set((value == null) ? (-1) : ((Property)value).Index, _raw_data);
			}
		}

		/// <summary>
		/// Gets or sets the next Child
		/// </summary>
		/// <value> the new 'next' child; may be null, which has the
		/// effect of saying there Is no 'next' child</value>
		public Child NextChild
		{
			get
			{
				return _next_child;
			}
			set
			{
				_next_child = value;
				_next_property.Set((value == null) ? (-1) : ((Property)value).Index, _raw_data);
			}
		}

		/// <summary>
		/// Get an array of objects, some of which may implement
		/// POIFSViewable
		/// </summary>
		/// <value>an array of Object; may not be null, but may be empty</value>
		public Array ViewableArray
		{
			get
			{
				Array array = new string[5];
				array.SetValue("Name          = \"" + Name + "\"", 0);
				array.SetValue("Property Type = " + _property_type.Value, 1);
				array.SetValue("Node Color    = " + _node_color.Value, 2);
				long num = _days_1.Value;
				num <<= 32;
				num += ((long)_seconds_1.Value & 65535L);
				array.SetValue("Time 1        = " + num, 3);
				num = _days_2.Value;
				num <<= 32;
				num += ((long)_seconds_2.Value & 65535L);
				array.SetValue("Time 2        = " + num, 4);
				return array;
			}
		}

		/// <summary>
		/// Get an Iterator of objects, some of which may implement POIFSViewable
		/// </summary>
		/// <value> may not be null, but may have an empty
		/// back end store</value>
		public IEnumerator ViewableIterator => ArrayList.ReadOnly(new ArrayList()).GetEnumerator();

		/// <summary>
		/// Give viewers a hint as to whether to call GetViewableArray or
		/// GetViewableIterator
		/// </summary>
		/// <value><c>true</c> if a viewer Should call GetViewableArray; otherwise, <c>false</c>
		/// if a viewer Should call GetViewableIterator
		/// </value>
		public bool PreferArray => true;

		/// <summary>
		/// Provides a short description of the object, to be used when a
		/// POIFSViewable object has not provided its contents.
		/// </summary>
		/// <value>The short description.</value>
		public string ShortDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Property: \"").Append(Name).Append("\"");
				return stringBuilder.ToString();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Properties.Property" /> class.
		/// </summary>
		protected Property()
		{
			_raw_data = new byte[128];
			for (int i = 0; i < _raw_data.Length; i++)
			{
				_raw_data[i] = 0;
			}
			_name_size = new ShortField(64);
			_property_type = new ByteField(66);
			_node_color = new ByteField(67);
			_previous_property = new IntegerField(68, -1, _raw_data);
			_next_property = new IntegerField(72, -1, _raw_data);
			_child_property = new IntegerField(76, -1, _raw_data);
			_storage_clsid = new ClassID(_raw_data, 80);
			_user_flags = new IntegerField(96, 0, _raw_data);
			_seconds_1 = new IntegerField(100, 0, _raw_data);
			_days_1 = new IntegerField(104, 0, _raw_data);
			_seconds_2 = new IntegerField(108, 0, _raw_data);
			_days_2 = new IntegerField(112, 0, _raw_data);
			_start_block = new IntegerField(116);
			_size = new IntegerField(120, 0, _raw_data);
			_index = -1;
			Name = "";
			NextChild = null;
			PreviousChild = null;
		}

		/// <summary>
		/// Constructor from byte data
		/// </summary>
		/// <param name="index">index number</param>
		/// <param name="array">byte data</param>
		/// <param name="offset">offset into byte data</param>
		protected Property(int index, byte[] array, int offset)
		{
			_raw_data = new byte[128];
			Array.Copy(array, offset, _raw_data, 0, 128);
			_name_size = new ShortField(64, _raw_data);
			_property_type = new ByteField(66, _raw_data);
			_node_color = new ByteField(67, _raw_data);
			_previous_property = new IntegerField(68, _raw_data);
			_next_property = new IntegerField(72, _raw_data);
			_child_property = new IntegerField(76, _raw_data);
			_storage_clsid = new ClassID(_raw_data, 80);
			_user_flags = new IntegerField(96, 0, _raw_data);
			_seconds_1 = new IntegerField(100, _raw_data);
			_days_1 = new IntegerField(104, _raw_data);
			_seconds_2 = new IntegerField(108, _raw_data);
			_days_2 = new IntegerField(112, _raw_data);
			_start_block = new IntegerField(116, _raw_data);
			_size = new IntegerField(120, _raw_data);
			_index = index;
			int num = _name_size.Value / 2 - 1;
			if (num < 1)
			{
				_name = "";
			}
			else
			{
				char[] array2 = new char[num];
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					array2[i] = (char)new ShortField(num2, _raw_data).Value;
					num2 += 2;
				}
				_name = new string(array2, 0, num);
			}
			_next_child = null;
			_previous_child = null;
		}

		/// <summary>
		/// Write the raw data to an OutputStream.
		/// </summary>
		/// <param name="stream">the OutputStream to which the data Should be
		/// written.</param>
		public void WriteData(Stream stream)
		{
			stream.Write(_raw_data, 0, _raw_data.Length);
		}

		/// <summary>
		/// does the length indicate a small document?
		/// </summary>
		/// <param name="length">length in bytes</param>
		/// <returns>
		/// 	<c>true</c> if the length Is less than
		/// _big_block_minimum_bytes; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsSmall(int length)
		{
			return length < 4096;
		}

		/// <summary>
		/// Perform whatever activities need to be performed prior to
		/// writing
		/// </summary>
		public abstract void PreWrite();

		/// <summary>
		/// Determines whether the specified index Is valid
		/// </summary>
		/// <param name="index">value to be checked</param>
		/// <returns>
		/// 	<c>true</c> if the index Is valid; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidIndex(int index)
		{
			return index != -1;
		}
	}
}
