using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Path2DList
	{
		private List<CT_Path2D> pathField;

		public List<CT_Path2D> path
		{
			get
			{
				return pathField;
			}
			set
			{
				pathField = value;
			}
		}

		internal static CT_Path2DList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_Path2DList cT_Path2DList = new CT_Path2DList();
			cT_Path2DList.path = new List<CT_Path2D>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "path")
				{
					cT_Path2DList.pathField.Add(CT_Path2D.Parse(childNode, namespaceManager));
				}
			}
			return cT_Path2DList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write("<a:{0}>", nodeName);
			if (pathField != null)
			{
				foreach (CT_Path2D item in pathField)
				{
					item.Write(sw, "path");
				}
			}
			sw.Write("</a:{0}>", nodeName);
		}
	}
}
