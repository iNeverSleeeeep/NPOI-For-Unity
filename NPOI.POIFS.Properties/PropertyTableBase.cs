using NPOI.POIFS.FileSystem;
using NPOI.POIFS.Storage;
using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Properties
{
	public abstract class PropertyTableBase : BATManaged
	{
		protected HeaderBlock _header_block;

		protected List<Property> _properties;

		public RootProperty Root => (RootProperty)_properties[0];

		public virtual int StartBlock
		{
			get
			{
				return _header_block.PropertyStart;
			}
			set
			{
				_header_block.PropertyStart = value;
			}
		}

		public virtual int CountBlocks
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public PropertyTableBase(HeaderBlock header_block)
		{
			_header_block = header_block;
			_properties = new List<Property>();
			AddProperty(new RootProperty());
		}

		public PropertyTableBase(HeaderBlock header_block, List<Property> properties)
		{
			_header_block = header_block;
			_properties = properties;
			PopulatePropertyTree((DirectoryProperty)_properties[0]);
		}

		public void AddProperty(Property property)
		{
			_properties.Add(property);
		}

		public void RemoveProperty(Property property)
		{
			_properties.Remove(property);
		}

		protected void PopulatePropertyTree(DirectoryProperty root)
		{
			try
			{
				int childIndex = root.ChildIndex;
				if (Property.IsValidIndex(childIndex))
				{
					Stack<Property> stack = new Stack<Property>();
					stack.Push(_properties[childIndex]);
					while (stack.Count != 0)
					{
						Property property = stack.Pop();
						root.AddChild(property);
						if (property.IsDirectory)
						{
							PopulatePropertyTree((DirectoryProperty)property);
						}
						childIndex = property.PreviousChildIndex;
						if (Property.IsValidIndex(childIndex))
						{
							stack.Push(_properties[childIndex]);
						}
						childIndex = property.NextChildIndex;
						if (Property.IsValidIndex(childIndex))
						{
							stack.Push(_properties[childIndex]);
						}
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}
	}
}
