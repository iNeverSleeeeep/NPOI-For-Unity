using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TblGrid : CT_TblGridBase
	{
		private CT_TblGridChange tblGridChangeField;

		[XmlElement(Order = 0)]
		public CT_TblGridChange tblGridChange
		{
			get
			{
				return tblGridChangeField;
			}
			set
			{
				tblGridChangeField = value;
			}
		}

		public static CT_TblGrid Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblGrid cT_TblGrid = new CT_TblGrid();
			cT_TblGrid.gridCol = new List<CT_TblGridCol>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblGridChange")
				{
					cT_TblGrid.tblGridChange = CT_TblGridChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gridCol")
				{
					cT_TblGrid.gridCol.Add(CT_TblGridCol.Parse(childNode, namespaceManager));
				}
			}
			return cT_TblGrid;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (tblGridChange != null)
			{
				tblGridChange.Write(sw, "tblGridChange");
			}
			if (base.gridCol != null)
			{
				foreach (CT_TblGridCol item in base.gridCol)
				{
					item.Write(sw, "gridCol");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_TblGridCol AddNewGridCol()
		{
			if (base.gridCol == null)
			{
				base.gridCol = new List<CT_TblGridCol>();
			}
			CT_TblGridCol cT_TblGridCol = new CT_TblGridCol();
			base.gridCol.Add(cT_TblGridCol);
			return cT_TblGridCol;
		}
	}
}
