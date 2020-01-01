using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot("Properties", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties", IsNullable = true)]
	public class CT_CustomProperties
	{
		private List<CT_Property> propertyField;

		[XmlElement("property")]
		public List<CT_Property> property
		{
			get
			{
				return propertyField;
			}
			set
			{
				propertyField = value;
			}
		}

		public CT_CustomProperties()
		{
			propertyField = new List<CT_Property>();
		}

		public int sizeOfPropertyArray()
		{
			return propertyField.Count;
		}

		public CT_Property AddNewProperty()
		{
			CT_Property cT_Property = new CT_Property();
			propertyField.Add(cT_Property);
			return cT_Property;
		}

		public CT_Property GetPropertyArray(int index)
		{
			return propertyField[index];
		}

		public List<CT_Property> GetPropertyList()
		{
			return propertyField;
		}

		public CT_Property GetProperty(string name)
		{
			for (int i = 0; i < propertyField.Count; i++)
			{
				if (propertyField[i].name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					return propertyField[i];
				}
			}
			return null;
		}

		public CT_CustomProperties Copy()
		{
			CT_CustomProperties cT_CustomProperties = new CT_CustomProperties();
			cT_CustomProperties.propertyField = new List<CT_Property>();
			foreach (CT_Property item in propertyField)
			{
				cT_CustomProperties.propertyField.Add(item);
			}
			return cT_CustomProperties;
		}
	}
}
