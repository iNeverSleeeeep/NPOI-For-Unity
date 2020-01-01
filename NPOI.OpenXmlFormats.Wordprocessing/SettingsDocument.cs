using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	public class SettingsDocument
	{
		private CT_Settings settings;

		public CT_Settings Settings
		{
			get
			{
				return settings;
			}
		}

		public SettingsDocument()
		{
			settings = new CT_Settings();
		}

		public static SettingsDocument Parse(XmlDocument doc, XmlNamespaceManager NameSpaceManager)
		{
			CT_Settings cT_Settings = CT_Settings.Parse(doc.DocumentElement, NameSpaceManager);
			return new SettingsDocument(cT_Settings);
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				settings.Write(sw);
			}
		}

		public SettingsDocument(CT_Settings settings)
		{
			this.settings = settings;
		}
	}
}
