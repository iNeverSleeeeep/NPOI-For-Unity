using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_NumDataSource
	{
		private CT_NumData numLitField;

		private CT_NumRef numRefField;

		public CT_NumData numLit
		{
			get
			{
				return numLitField;
			}
			set
			{
				numLitField = value;
			}
		}

		public CT_NumRef numRef
		{
			get
			{
				return numRefField;
			}
			set
			{
				numRefField = value;
			}
		}

		public static CT_NumDataSource Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumDataSource cT_NumDataSource = new CT_NumDataSource();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "numLit")
				{
					cT_NumDataSource.numLit = CT_NumData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numRef")
				{
					cT_NumDataSource.numRef = CT_NumRef.Parse(childNode, namespaceManager);
				}
			}
			return cT_NumDataSource;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (numLit != null)
			{
				numLit.Write(sw, "numLit");
			}
			if (numRef != null)
			{
				numRef.Write(sw, "numRef");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_NumRef AddNewNumRef()
		{
			numRefField = new CT_NumRef();
			return numRefField;
		}

		public CT_NumData AddNewNumLit()
		{
			numLit = new CT_NumData();
			return numLit;
		}
	}
}
