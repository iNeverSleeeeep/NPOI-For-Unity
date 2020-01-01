using System;
using System.Collections.Generic;
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
	public class CT_Scaling
	{
		private CT_LogBase logBaseField;

		private CT_Orientation orientationField;

		private CT_Double maxField;

		private CT_Double minField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_LogBase logBase
		{
			get
			{
				return logBaseField;
			}
			set
			{
				logBaseField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Orientation orientation
		{
			get
			{
				return orientationField;
			}
			set
			{
				orientationField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Double max
		{
			get
			{
				return maxField;
			}
			set
			{
				maxField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Double min
		{
			get
			{
				return minField;
			}
			set
			{
				minField = value;
			}
		}

		[XmlElement(Order = 4)]
		public List<CT_Extension> extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public static CT_Scaling Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Scaling cT_Scaling = new CT_Scaling();
			cT_Scaling.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "logBase")
				{
					cT_Scaling.logBase = CT_LogBase.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "orientation")
				{
					cT_Scaling.orientation = CT_Orientation.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "max")
				{
					cT_Scaling.max = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "min")
				{
					cT_Scaling.min = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Scaling.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Scaling;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (logBase != null)
			{
				logBase.Write(sw, "logBase");
			}
			if (orientation != null)
			{
				orientation.Write(sw, "orientation");
			}
			if (max != null)
			{
				max.Write(sw, "max");
			}
			if (min != null)
			{
				min.Write(sw, "min");
			}
			if (extLst != null)
			{
				foreach (CT_Extension item in extLst)
				{
					item.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public bool IsSetOrientation()
		{
			return orientationField != null;
		}

		public CT_Orientation AddNewOrientation()
		{
			orientationField = new CT_Orientation();
			return orientationField;
		}

		public bool IsSetLogBase()
		{
			return logBaseField != null;
		}

		public CT_LogBase AddNewLogBase()
		{
			logBaseField = new CT_LogBase();
			return logBaseField;
		}

		public bool IsSetMax()
		{
			return maxField != null;
		}

		public CT_Double AddNewMax()
		{
			maxField = new CT_Double();
			return maxField;
		}

		public bool IsSetMin()
		{
			return minField != null;
		}

		public CT_Double AddNewMin()
		{
			minField = new CT_Double();
			return minField;
		}
	}
}
