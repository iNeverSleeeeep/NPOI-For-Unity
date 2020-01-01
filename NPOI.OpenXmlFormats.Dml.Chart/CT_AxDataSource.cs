using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_AxDataSource
	{
		private CT_MultiLvlStrRef multiLvlStrRefField;

		private CT_NumData numLitField;

		private CT_NumRef numRefField;

		private CT_StrData strLitField;

		private CT_StrRef strRefField;

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

		public CT_StrData strLit
		{
			get
			{
				return strLitField;
			}
			set
			{
				strLitField = value;
			}
		}

		public CT_StrRef strRef
		{
			get
			{
				return strRefField;
			}
			set
			{
				strRefField = value;
			}
		}

		public CT_MultiLvlStrRef multiLvlStrRef
		{
			get
			{
				return multiLvlStrRefField;
			}
			set
			{
				multiLvlStrRefField = value;
			}
		}

		public static CT_AxDataSource Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AxDataSource cT_AxDataSource = new CT_AxDataSource();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "numLit")
				{
					cT_AxDataSource.numLit = CT_NumData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numRef")
				{
					cT_AxDataSource.numRef = CT_NumRef.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strLit")
				{
					cT_AxDataSource.strLit = CT_StrData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strRef")
				{
					cT_AxDataSource.strRef = CT_StrRef.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "multiLvlStrRef")
				{
					cT_AxDataSource.multiLvlStrRef = CT_MultiLvlStrRef.Parse(childNode, namespaceManager);
				}
			}
			return cT_AxDataSource;
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
			if (strLit != null)
			{
				strLit.Write(sw, "strLit");
			}
			if (strRef != null)
			{
				strRef.Write(sw, "strRef");
			}
			if (multiLvlStrRef != null)
			{
				multiLvlStrRef.Write(sw, "multiLvlStrRef");
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

		public CT_StrRef AddNewStrRef()
		{
			strRef = new CT_StrRef();
			return strRef;
		}

		public CT_StrData AddNewStrLit()
		{
			strLit = new CT_StrData();
			return strLit;
		}
	}
}
