using NPOI.OpenXmlFormats;

namespace NPOI
{
	/// Extended document properties
	public class ExtendedProperties
	{
		public ExtendedPropertiesDocument props;

		internal ExtendedProperties(ExtendedPropertiesDocument props)
		{
			this.props = props;
		}

		public CT_ExtendedProperties GetUnderlyingProperties()
		{
			return props.GetProperties();
		}
	}
}
