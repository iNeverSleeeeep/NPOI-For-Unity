using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
	public class ST_RelationshipId
	{
		private string _id;

		public static string NamespaceURI
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
			}
		}
	}
}
