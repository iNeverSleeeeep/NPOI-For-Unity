using NPOI.OpenXmlFormats;
using System;

namespace NPOI
{
	/// Custom document properties
	public class CustomProperties
	{
		/// Each custom property element Contains an fmtid attribute
		/// with the same GUID value ({D5CDD505-2E9C-101B-9397-08002B2CF9AE}).
		public static string FORMAT_ID = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}";

		public CustomPropertiesDocument props;

		internal CustomProperties(CustomPropertiesDocument props)
		{
			this.props = props;
		}

		public CT_CustomProperties GetUnderlyingProperties()
		{
			return props.GetProperties();
		}

		/// Add a new property
		///
		/// @param name the property name
		/// @throws IllegalArgumentException if a property with this name already exists
		private CT_Property Add(string name)
		{
			if (Contains(name))
			{
				throw new ArgumentException("A property with this name already exists in the custom properties");
			}
			CT_Property cT_Property = props.GetProperties().AddNewProperty();
			int num2 = cT_Property.pid = NextPid();
			cT_Property.fmtid = FORMAT_ID;
			cT_Property.name = name;
			return cT_Property;
		}

		/// Add a new string property
		///
		/// @throws IllegalArgumentException if a property with this name already exists
		public void AddProperty(string name, string value)
		{
			CT_Property cT_Property = Add(name);
			cT_Property.ItemElementName = ItemChoiceType.lpwstr;
			cT_Property.Item = value;
		}

		/// Add a new double property
		///
		/// @throws IllegalArgumentException if a property with this name already exists
		public void AddProperty(string name, double value)
		{
			CT_Property cT_Property = Add(name);
			cT_Property.ItemElementName = ItemChoiceType.r8;
			cT_Property.Item = value;
		}

		/// Add a new integer property
		///
		/// @throws IllegalArgumentException if a property with this name already exists
		public void AddProperty(string name, int value)
		{
			CT_Property cT_Property = Add(name);
			cT_Property.ItemElementName = ItemChoiceType.i4;
			cT_Property.Item = value;
		}

		/// Add a new bool property
		///
		/// @throws IllegalArgumentException if a property with this name already exists
		public void AddProperty(string name, bool value)
		{
			CT_Property cT_Property = Add(name);
			cT_Property.ItemElementName = ItemChoiceType.@bool;
			cT_Property.Item = value;
		}

		/// Generate next id that uniquely relates a custom property
		///
		/// @return next property id starting with 2
		protected int NextPid()
		{
			int num = 1;
			foreach (CT_Property property in props.GetProperties().GetPropertyList())
			{
				if (property.pid > num)
				{
					num = property.pid;
				}
			}
			return num + 1;
		}

		/// Check if a property with this name already exists in the collection of custom properties
		///
		/// @param name the name to check
		/// @return whether a property with the given name exists in the custom properties
		public bool Contains(string name)
		{
			foreach (CT_Property property in props.GetProperties().GetPropertyList())
			{
				if (property.name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}
	}
}
