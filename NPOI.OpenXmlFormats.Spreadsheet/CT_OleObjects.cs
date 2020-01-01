using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_OleObjects
	{
		private List<CT_OleObject> oleObjectField;

		public List<CT_OleObject> oleObject
		{
			get
			{
				return oleObjectField;
			}
			set
			{
				oleObjectField = value;
			}
		}

		public static CT_OleObjects Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OleObjects cT_OleObjects = new CT_OleObjects();
			cT_OleObjects.oleObject = new List<CT_OleObject>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "oleObject")
				{
					cT_OleObjects.oleObject.Add(CT_OleObject.Parse(childNode, namespaceManager));
				}
			}
			return cT_OleObjects;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (oleObject != null)
			{
				foreach (CT_OleObject item in oleObject)
				{
					item.Write(sw, "oleObject");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
