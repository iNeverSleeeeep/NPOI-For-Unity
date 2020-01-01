using NPOI.OpenXml4Net.Util;
using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class SstDocument
	{
		private CT_Sst sst;

		public SstDocument()
		{
		}

		public SstDocument(CT_Sst sst)
		{
			this.sst = sst;
		}

		public void AddNewSst()
		{
			sst = new CT_Sst();
		}

		public CT_Sst GetSst()
		{
			return sst;
		}

		public static SstDocument Parse(XmlDocument xml, XmlNamespaceManager namespaceManager)
		{
			try
			{
				SstDocument sstDocument = new SstDocument();
				sstDocument.AddNewSst();
				CT_Sst cT_Sst = sstDocument.GetSst();
				cT_Sst.count = XmlHelper.ReadInt(xml.DocumentElement.Attributes["count"]);
				cT_Sst.uniqueCount = XmlHelper.ReadInt(xml.DocumentElement.Attributes["uniqueCount"]);
				XmlNodeList xmlNodeList = xml.SelectNodes("//d:sst/d:si", namespaceManager);
				if (xmlNodeList != null)
				{
					foreach (XmlNode item2 in xmlNodeList)
					{
						CT_Rst item = CT_Rst.Parse(item2, namespaceManager);
						sstDocument.sst.si.Add(item);
					}
				}
				return sstDocument;
			}
			catch (XmlException ex)
			{
				throw new IOException(ex.Message);
			}
		}

		public void Save(Stream stream)
		{
			StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?><sst xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" count=\"{0}\" uniqueCount=\"{1}\">", GetSst().count, GetSst().uniqueCount);
			foreach (CT_Rst item in GetSst().si)
			{
				item.Write(streamWriter, "si");
			}
			streamWriter.Write("</sst>");
			streamWriter.Flush();
		}
	}
}
