using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_FileSharing
	{
		private bool readOnlyRecommendedField;

		private string userNameField;

		private byte[] reservationPasswordField;

		[DefaultValue(false)]
		[XmlAttribute]
		public bool readOnlyRecommended
		{
			get
			{
				return readOnlyRecommendedField;
			}
			set
			{
				readOnlyRecommendedField = value;
			}
		}

		[XmlAttribute]
		public string userName
		{
			get
			{
				return userNameField;
			}
			set
			{
				userNameField = value;
			}
		}

		[XmlAttribute]
		public byte[] reservationPassword
		{
			get
			{
				return reservationPasswordField;
			}
			set
			{
				reservationPasswordField = value;
			}
		}

		public CT_FileSharing()
		{
			readOnlyRecommendedField = false;
		}

		public static CT_FileSharing Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FileSharing cT_FileSharing = new CT_FileSharing();
			cT_FileSharing.readOnlyRecommended = XmlHelper.ReadBool(node.Attributes["readOnlyRecommended"]);
			cT_FileSharing.userName = XmlHelper.ReadString(node.Attributes["userName"]);
			cT_FileSharing.reservationPassword = XmlHelper.ReadBytes(node.Attributes["reservationPassword"]);
			return cT_FileSharing;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "readOnlyRecommended", readOnlyRecommended);
			XmlHelper.WriteAttribute(sw, "userName", userName);
			XmlHelper.WriteAttribute(sw, "reservationPassword", reservationPassword);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
