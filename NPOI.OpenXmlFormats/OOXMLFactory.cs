using System;
using System.IO;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	public class OOXMLFactory<T>
	{
		private XmlSerializer serializerObj;

		public OOXMLFactory()
		{
			serializerObj = new XmlSerializer(typeof(T));
		}

		public T Parse(Stream stream)
		{
			T result = (T)serializerObj.Deserialize(stream);
			stream.Close();
			return result;
		}

		public T Create()
		{
			return (T)Activator.CreateInstance(typeof(T));
		}
	}
}
