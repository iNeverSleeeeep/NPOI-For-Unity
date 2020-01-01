using System;
using System.Collections.Generic;
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
	public class CT_ErrBars
	{
		private CT_ErrDir errDirField;

		private CT_ErrBarType errBarTypeField;

		private CT_ErrValType errValTypeField;

		private CT_Boolean noEndCapField;

		private CT_NumDataSource plusField;

		private CT_NumDataSource minusField;

		private CT_Double valField;

		private CT_ShapeProperties spPrField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_ErrDir errDir
		{
			get
			{
				return errDirField;
			}
			set
			{
				errDirField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ErrBarType errBarType
		{
			get
			{
				return errBarTypeField;
			}
			set
			{
				errBarTypeField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_ErrValType errValType
		{
			get
			{
				return errValTypeField;
			}
			set
			{
				errValTypeField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Boolean noEndCap
		{
			get
			{
				return noEndCapField;
			}
			set
			{
				noEndCapField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_NumDataSource plus
		{
			get
			{
				return plusField;
			}
			set
			{
				plusField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_NumDataSource minus
		{
			get
			{
				return minusField;
			}
			set
			{
				minusField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Double val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		[XmlElement(Order = 8)]
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

		public static CT_ErrBars Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ErrBars cT_ErrBars = new CT_ErrBars();
			cT_ErrBars.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "errDir")
				{
					cT_ErrBars.errDir = CT_ErrDir.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "errBarType")
				{
					cT_ErrBars.errBarType = CT_ErrBarType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "errValType")
				{
					cT_ErrBars.errValType = CT_ErrValType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noEndCap")
				{
					cT_ErrBars.noEndCap = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "plus")
				{
					cT_ErrBars.plus = CT_NumDataSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minus")
				{
					cT_ErrBars.minus = CT_NumDataSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "val")
				{
					cT_ErrBars.val = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_ErrBars.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_ErrBars.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_ErrBars;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (errDir != null)
			{
				errDir.Write(sw, "errDir");
			}
			if (errBarType != null)
			{
				errBarType.Write(sw, "errBarType");
			}
			if (errValType != null)
			{
				errValType.Write(sw, "errValType");
			}
			if (noEndCap != null)
			{
				noEndCap.Write(sw, "noEndCap");
			}
			if (plus != null)
			{
				plus.Write(sw, "plus");
			}
			if (minus != null)
			{
				minus.Write(sw, "minus");
			}
			if (val != null)
			{
				val.Write(sw, "val");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
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
	}
}
