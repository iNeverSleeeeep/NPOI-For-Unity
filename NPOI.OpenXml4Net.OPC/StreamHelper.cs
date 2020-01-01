using System.IO;
using System.Text;
using System.Xml;

namespace NPOI.OpenXml4Net.OPC
{
	public class StreamHelper
	{
		private StreamHelper()
		{
		}

		/// Turning the DOM4j object in the specified output stream.
		///
		/// @param xmlContent
		///            The XML document.
		/// @param outStream
		///            The Stream in which the XML document will be written.
		/// @return <b>true</b> if the xml is successfully written in the stream,
		///         else <b>false</b>.
		public static void SaveXmlInStream(XmlDocument xmlContent, Stream outStream)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Encoding = Encoding.UTF8;
			xmlWriterSettings.OmitXmlDeclaration = false;
			XmlWriter xmlWriter = XmlWriter.Create(outStream, xmlWriterSettings);
			xmlContent.WriteContentTo(xmlWriter);
			xmlWriter.Flush();
		}

		/// Copy the input stream into the output stream.
		///
		/// @param inStream
		///            The source stream.
		/// @param outStream
		///            The destination stream.
		/// @return <b>true</b> if the operation succeed, else return <b>false</b>.
		public static void CopyStream(Stream inStream, Stream outStream)
		{
			byte[] array = new byte[1024];
			int num = 0;
			int num2 = 0;
			while ((num = inStream.Read(array, 0, array.Length)) > 0)
			{
				outStream.Write(array, 0, num);
				num2 += num;
			}
		}
	}
}
