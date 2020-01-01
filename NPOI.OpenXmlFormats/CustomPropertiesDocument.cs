using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	public class CustomPropertiesDocument
	{
		internal static XmlSerializer serializer = new XmlSerializer(typeof(CT_CustomProperties));

		internal static XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[2]
		{
			new XmlQualifiedName("", "http://schemas.openxmlformats.org/spreadsheetml/2006/main"),
			new XmlQualifiedName("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")
		});

		private CT_CustomProperties _props;

		public CustomPropertiesDocument(CT_CustomProperties prop)
		{
			_props = prop;
		}

		public CustomPropertiesDocument()
		{
		}

		public CT_CustomProperties GetProperties()
		{
			return _props;
		}

		public CT_CustomProperties AddNewProperties()
		{
			_props = new CT_CustomProperties();
			return _props;
		}

		public CustomPropertiesDocument Copy()
		{
			CustomPropertiesDocument customPropertiesDocument = new CustomPropertiesDocument();
			customPropertiesDocument._props = _props.Copy();
			return customPropertiesDocument;
		}

		public static CustomPropertiesDocument Parse(Stream stream)
		{
			CT_CustomProperties prop = (CT_CustomProperties)serializer.Deserialize(stream);
			return new CustomPropertiesDocument(prop);
		}

		public void Save(Stream stream)
		{
			serializer.Serialize(stream, _props, namespaces);
		}

		public override string ToString()
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				serializer.Serialize(stringWriter, _props);
				return stringWriter.ToString();
			}
		}
	}
}
