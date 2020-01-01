using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_NumPicBullet
	{
		private CT_Picture pictField;

		private string numPicBulletIdField;

		[XmlElement(Order = 0)]
		public CT_Picture pict
		{
			get
			{
				return pictField;
			}
			set
			{
				pictField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string numPicBulletId
		{
			get
			{
				return numPicBulletIdField;
			}
			set
			{
				numPicBulletIdField = value;
			}
		}

		public static CT_NumPicBullet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumPicBullet cT_NumPicBullet = new CT_NumPicBullet();
			cT_NumPicBullet.numPicBulletId = XmlHelper.ReadString(node.Attributes["w:numPicBulletId"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pict")
				{
					cT_NumPicBullet.pict = CT_Picture.Parse(childNode, namespaceManager);
				}
			}
			return cT_NumPicBullet;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:numPicBulletId", numPicBulletId);
			sw.Write(">");
			if (pict != null)
			{
				pict.Write(sw, "pict");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
