using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	public class ExtendedPropertiesDocument
	{
		internal static XmlSerializer serializer = new XmlSerializer(typeof(CT_ExtendedProperties));

		internal static XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[2]
		{
			new XmlQualifiedName("", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties"),
			new XmlQualifiedName("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")
		});

		private CT_ExtendedProperties _props;

		public ExtendedPropertiesDocument()
		{
		}

		public ExtendedPropertiesDocument(CT_ExtendedProperties prop)
		{
			_props = prop;
		}

		public CT_ExtendedProperties GetProperties()
		{
			return _props;
		}

		public CT_ExtendedProperties AddNewProperties()
		{
			_props = new CT_ExtendedProperties();
			return _props;
		}

		public ExtendedPropertiesDocument Copy()
		{
			ExtendedPropertiesDocument extendedPropertiesDocument = new ExtendedPropertiesDocument();
			extendedPropertiesDocument._props = _props.Copy();
			return extendedPropertiesDocument;
		}

		public static ExtendedPropertiesDocument Parse(Stream stream)
		{
			CT_ExtendedProperties prop = (CT_ExtendedProperties)serializer.Deserialize(stream);
			return new ExtendedPropertiesDocument(prop);
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
